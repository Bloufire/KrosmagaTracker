using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.FlyoutControls.Tracker.PageModel;
using AddOn_Krosmaga___Blou_fire.Helpers;
using AddOn_Krosmaga___Blou_fire.Models;
using AddOn_Krosmaga___Blou_fire.ProducerConsumer;
using AddOn_Krosmaga___Blou_fire.Services.Interfaces;
using JsonCardsParser;

namespace AddOn_Krosmaga___Blou_fire.Services
{
    public class TrackerCoreSrv : ObservableObject,ITrackerCore
	{ 
		#region Properties

	    BlockingCollection<byte[]> _workQueue = new BlockingCollection<byte[]>(new ConcurrentQueue<byte[]>());
	    private BinaryReader _binaryReader;
	    private byte[] _lastMessage = new byte[1];
		public CardCollection CardsCollection { get; set; }
	    public TrackerModel TrackerModel { get; set; }//Model qui sera à découper par page. Peut contenir toutes les données actuellement.
	   
		
	    #endregion

		protected override void OnPropertyChanged(string propertyName)
		{
			base.OnPropertyChanged(propertyName);
		}

		#region Core
		public TrackerCoreSrv()
	    {
		    CardsCollection = new CardCollection { Collection = new JsonCard().ChargerCartes() };
		    Helpers.Helpers.FirewallValidation();
		    Producer producer = new Producer(_workQueue);

		    StartService();

	    }

	    public void StartService()
	    {

		    TrackerModel = new TrackerModel();
			var consumer = Task.Run(() =>
			{
				while (true)
				{
					byte[] data = _workQueue.Take();
					Krosmaga(0, data);
				}
			});
		}
	    private void Krosmaga(int ID, byte[] data)
	    {
		    try
		    {
			    if (data.Length > 40 && _lastMessage.SequenceEqual(data.Skip(40)))
				    return;
			    _lastMessage = data.Skip(40).ToArray();

			    //bool passTest = false;
			    Stream f = new MemoryStream(data);
			    _binaryReader = new BinaryReader(f);

			    if (_binaryReader.BaseStream.Length >= 41)
				    _binaryReader.BaseStream.Position = 40;
			    else
				    return;

			    _binaryReader.ReadBytes(3);
			    while (_binaryReader.BaseStream.Position < _binaryReader.BaseStream.Length && _binaryReader.ReadByte() != 0)
			    {
				    //fileLog.WriteLine("Reading...");

				    _binaryReader.ReadBytes(3);
				    string messageId = Helpers.Helpers.ConcatHeader(_binaryReader);
				    _binaryReader.ReadByte();
				    uint size = Helpers.Helpers.ReadRawVarint32(_binaryReader);

				    byte[] send;

				    switch (messageId)
				    {
					    //case "104BE831BFC1F6EB11C8B453B7F257C499": // LeaderboardPersonnalEntryEvent
					    case "A743D4E99C61572311D93D8A99B0D00AB9": // StartOfTurnEvent
						    Builders.StartOfTurn startofturn = new Builders.StartOfTurn();
						    send = _binaryReader.ReadBytes((int)size);
						    startofturn.Decode(send);
						    //UIActionStartOfTurn(startofturn);
						    break;
					    case "89438706FC2AE2CD11B3891BE848AD7887": // GameStartedEvent
						    Builders.GameStarted gamestarted = new Builders.GameStarted();
						    send = _binaryReader.ReadBytes((int)size);
						    gamestarted.Decode(send);
						    UIActionGameEventStarted(gamestarted);
						    break;
					    case "1B4FF61A6FBC09E611F2CBA7E5FB5391BA": // GameFinishedEvent
						    Builders.GameFinished gamefinished = new Builders.GameFinished();
						    send = _binaryReader.ReadBytes((int)size);
						    gamefinished.Decode(send);
						    //UIActionGameFinishedEvent(gamefinished);
						    break;
					    case "98400741B1CB5A4A110FC4D2D51E2D4CA9": // GameEventsEvent
						    Builders.GameEvents gameevents = new Builders.GameEvents();
						    send = _binaryReader.ReadBytes((int)size);
						    gameevents.Decode(send);
						    //UIActionGameEventsEvent(gameevents);
						    break;
					    case "24454BD9B42E0A231174846DD1A86A7ABB": // PlayerAccountLoadedEvent
						    Builders.PlayerAccountLoaded playerAccountLoaded = new Builders.PlayerAccountLoaded();
						    send = _binaryReader.ReadBytes((int)size);
						    playerAccountLoaded.Decode(send);
						    break;
					    /*case "F64A6D942BA9B2A01139131B120F0A6494":
                            test = true;
                            break;*/
					    default:
						    if (_binaryReader.BaseStream.Position + size > _binaryReader.BaseStream.Length)
							    size = (uint)_binaryReader.BaseStream.Length - (uint)_binaryReader.BaseStream.Position - 5;
						    if (size != 0)
							    _binaryReader.BaseStream.Position = _binaryReader.BaseStream.Position + size;
						    break;
				    }
				    _binaryReader.ReadBytes(3);

				    //fileLog.WriteLine("Header : " + messageId + " - " + size);
			    }
		    }
		    catch (Exception e)
		    {
			    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"log.txt", true))
			    {
				    file.WriteLine("ERROR - " + e);
				    foreach (var item in data)
					    file.Write(item);
			    }
		    }
	    }
	    private void UIActionGameEventStarted(Builders.GameStarted value)
	    {
		    var MyIndex = value.MyIndex;

		    Data.Player player = value.PlayersList.FirstOrDefault(x => x.Index == MyIndex);

			if (player != null)
			{
				TrackerModel.OwnPseudo = player.Profile.Nickname == null ? "" : player.Profile.Nickname;
				TrackerModel.OwnWinsNb = player.Profile.VictoryCount;
				TrackerModel.OwnLosesNb = player.Profile.DefeatCount;
				//UIDatas.OwnLevel = player.Profile.Level;
				//TrackerModel.OwnClasse = ((Enums.God)Enum.Parse(typeof(Enums.God), player.GodId.ToString())).ToString();
			}

			player = value.PlayersList.FirstOrDefault(x => x.Index != MyIndex);

			if (player != null)
			{
				TrackerModel.VsPseudo = player.Profile.Nickname == null ? "" : player.Profile.Nickname;
				TrackerModel.VsWinsNb = player.Profile.VictoryCount;
				TrackerModel.VsLosesNb = player.Profile.DefeatCount;
				//TrackerModel.OpponentLevel = player.Profile.Level;
				//TrackerModel.OpponentClasse = ((Enums.God)Enum.Parse(typeof(Enums.God), player.GodId.ToString())).ToString();
			}

			//UIDatas.GameType = value.GameType;
		}

		#endregion


	}
}
