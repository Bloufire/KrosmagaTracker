﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SniffingLibs;
using JsonCardsParser;
using NetFwTypeLib;
using System.Reflection;
using SQLiteConnector;
using AddOn_Krosmaga___Blou_fire.ProducerConsumer;
using System.Collections;

namespace AddOn_Krosmaga___Blou_fire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Socket mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] byteData = new byte[2048];
        private BinaryReader b;
        private CardCollection cards;
        private UIPlayerDatas UIDatas;

        private bool messageToBeCompleted;
        private int expectedSizeToComplete;
        private byte[] bytesToComplete;
        private int actualSizeToComplete;

        Queue<byte[]> queue = new Queue<byte[]>();
        SyncEvents syncEvents = new SyncEvents();

        public MainWindow()
        {
            InitializeComponent();
            UIDatas = (UIPlayerDatas)this.DataContext;
            cards = new CardCollection();
            cards.Collection = new JsonCard().ChargerCartes();

            messageToBeCompleted = false;

            FirewallValidation();

            Producer producer = new Producer(queue, syncEvents);
            Thread producerThread = new Thread(producer.ThreadRun);
            producerThread.Start();

            var consumer = Task.Factory.StartNew(() =>
            {
                while (WaitHandle.WaitAny(syncEvents.EventArray) != 1)
                {
                    byte[] data;
                    lock (((ICollection)queue).SyncRoot)
                    {
                        data = queue.Dequeue();
                    }
                    Krosmaga(0, data);
                }
            });
        }

        private void FirewallValidation()
        {
            Type tNetFwPolicy2 = Type.GetTypeFromProgID("HNetCfg.FwPolicy2");
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(tNetFwPolicy2);
            var currentProfiles = fwPolicy2.CurrentProfileTypes;

            bool _exist = false;

            foreach (INetFwRule rule in fwPolicy2.Rules)
            {
                if (rule.Name.IndexOf("KrosmagaAddOn: " + System.IO.Path.GetFileName(Assembly.GetEntryAssembly().Location)) != -1)
                {
                    fwPolicy2.Rules.Remove(rule.Name);
                    break;
                }
            }
            if(!_exist)
            {
                INetFwRule firewallRule = (INetFwRule)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));
                firewallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                firewallRule.Enabled = true;
                firewallRule.InterfaceTypes = "All";
                firewallRule.ApplicationName = Assembly.GetEntryAssembly().Location;
                firewallRule.Name = "KrosmagaAddOn: " + System.IO.Path.GetFileName(Assembly.GetEntryAssembly().Location);
                INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance
                (Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));
                firewallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                firewallPolicy.Rules.Add(firewallRule);
            }
        }

        private void LaunchSniffer()
        {
            string ipadd = "";
            IPHostEntry HosyEntry = Dns.GetHostEntry((Dns.GetHostName()));
            if (HosyEntry.AddressList.Length > 0)
            {
                foreach (IPAddress ip in HosyEntry.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                        ipadd = ip.ToString();
                }
            }

            mainSocket = new Socket(AddressFamily.InterNetwork,
                SocketType.Raw, ProtocolType.IP);

            mainSocket.Bind(new IPEndPoint(IPAddress.Parse(ipadd), 4988));

            mainSocket.SetSocketOption(SocketOptionLevel.IP,
                                       SocketOptionName.HeaderIncluded,
                                       true);

            byte[] byTrue = new byte[4] { 1, 0, 0, 0 };
            byte[] byOut = new byte[4] { 0, 0, 0, 0 };

            mainSocket.IOControl(IOControlCode.ReceiveAll, BitConverter.GetBytes(1), null);

            mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None,
                new AsyncCallback(OnReceive), null);
        }

        private void OnReceive(IAsyncResult ar)
        {
            try
            {
                int nReceived = mainSocket.EndReceive(ar);

                //Analyze the bytes received...

                ParseData(byteData, nReceived);

                byteData = new byte[4096];
                mainSocket.BeginReceive(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(OnReceive), null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ParseData(byte[] byteData, int nReceived)
        {
            IPHeader ipHeader = new IPHeader(byteData, nReceived);

            switch (ipHeader.ProtocolType)
            {
                case Protocol.TCP:
                    TCPHeader tcpHeader = new TCPHeader(ipHeader.Data, ipHeader.MessageLength); //Length of the data field
                    if (tcpHeader.SourcePort == "4988")
                    {
                        Thread thread = new Thread(() => Krosmaga(0, byteData));
                        thread.Start();
                    }
                    break;

                case Protocol.UDP:
                    break;

                case Protocol.Unknown:
                    break;
            }
        }

        private void Krosmaga(int ID, byte[] data)
        {
            //bool passTest = false;
            Stream f = new MemoryStream(data);
            b = new BinaryReader(f);

            /*b.ReadBytes(2);

            var d = b.ReadBytes(2);
            string txt = "";
            foreach (var item in d)
            {
                txt += item.ToString("X");
            }
            int sizeofmessage = int.Parse(txt, System.Globalization.NumberStyles.HexNumber);

            if (messageToBeCompleted && sizeofmessage > 41)
            {
                if(expectedSizeToComplete > (actualSizeToComplete + sizeofmessage - 40))
                {
                    messageToBeCompleted = true;
                    Buffer.BlockCopy(data, 40, bytesToComplete, actualSizeToComplete, sizeofmessage-40);
                    actualSizeToComplete += (sizeofmessage - 40);
                    return;
                }
                else
                {
                    Buffer.BlockCopy(data, 40, bytesToComplete, actualSizeToComplete, sizeofmessage - 40);

                    messageToBeCompleted = false;
                    expectedSizeToComplete = 0;
                    actualSizeToComplete = 0;

                    f = new MemoryStream(bytesToComplete);
                    b = new BinaryReader(f);
                    passTest = true;
                }
            }*/

            if (b.BaseStream.Length >= 41)
                b.BaseStream.Position = 40;

            b.ReadBytes(3);
            while (b.BaseStream.Position < b.BaseStream.Length && b.ReadByte() != 0)
            {
                b.ReadBytes(3);
                string messageId = ConcatHeader();
                b.ReadByte();
                uint size = ReadRawVarint32();
                byte[] send;
                /*if(!passTest && size > sizeofmessage)
                {
                    messageToBeCompleted = true;
                    bytesToComplete = new byte[4096];
                    expectedSizeToComplete = (int)size + (int)b.BaseStream.Position;
                    Buffer.BlockCopy(data, 0, bytesToComplete, actualSizeToComplete, sizeofmessage);
                    actualSizeToComplete += sizeofmessage;
                    return;
                }*/

                switch (messageId)
                {
                    //case "104BE831BFC1F6EB11C8B453B7F257C499": // LeaderboardPersonnalEntryEvent
                    case "A743D4E99C61572311D93D8A99B0D00AB9": // StartOfTurnEvent
                        Builders.StartOfTurn startofturn = new Builders.StartOfTurn();
                        send = b.ReadBytes((int)size);
                        startofturn.Decode(send);
                        UIActionStartOfTurn(startofturn);
                        break;
                    case "89438706FC2AE2CD11B3891BE848AD7887": // GameStartedEvent
                        Builders.GameStarted gamestarted = new Builders.GameStarted();
                        send = b.ReadBytes((int)size);
                        gamestarted.Decode(send);
                        UIActionGameEventStarted(gamestarted);
                        break;
                    case "1B4FF61A6FBC09E611F2CBA7E5FB5391BA": // GameFinishedEvent
                        Builders.GameFinished gamefinished = new Builders.GameFinished();
                        send = b.ReadBytes((int)size);
                        gamefinished.Decode(send);
                        UIActionGameFinishedEvent(gamefinished);
                        break;
                    case "98400741B1CB5A4A110FC4D2D51E2D4CA9": // GameEventsEvent
                        Builders.GameEvents gameevents = new Builders.GameEvents();
                        send = b.ReadBytes((int)size);
                        gameevents.Decode(send);
                        UIActionGameEventsEvent(gameevents);
                        break;
                    case "24454BD9B42E0A231174846DD1A86A7ABB": // PlayerAccountLoadedEvent
                        Builders.PlayerAccountLoaded playerAccountLoaded = new Builders.PlayerAccountLoaded();
                        send = b.ReadBytes((int)size);
                        playerAccountLoaded.Decode(send);
                        break;
                    /*case "F64A6D942BA9B2A01139131B120F0A6494":
                        test = true;
                        break;*/
                    default:
                        if (b.BaseStream.Position + size > b.BaseStream.Length)
                            size = (uint)b.BaseStream.Length - (uint)b.BaseStream.Position - 5;
                        if(size != 0)
                            b.ReadBytes((int)size); 
                        break;
                }
                b.ReadBytes(3);
            }
        }

        private void UIActionGameEventsEvent(Builders.GameEvents value)
        {
            if (UIDatas.HasIndex)
            {
                foreach (var item in value.EventsList)
                {
                    IterateEvents(item);
                }
            }
        }

        private void IterateEvents(Data.GameEvent value)
        {
            List<Builders.EventsManager.CardMovedEvent> list = new List<Builders.EventsManager.CardMovedEvent>();
            if (value.EventType == Enums.EventType.CARD_MOVED)
            {
                Builders.EventsManager.CardMovedEvent cardMoved = new Builders.EventsManager.CardMovedEvent(value);
                if ((cardMoved.CardLocationFrom == Enums.CardLocation.OwnHand && cardMoved.CardLocationTo == Enums.CardLocation.Playground ||
                    cardMoved.CardLocationFrom == Enums.CardLocation.OwnHand && cardMoved.CardLocationTo == Enums.CardLocation.OwnGraveyard  ||
                    cardMoved.CardLocationFrom == Enums.CardLocation.OpponentPile && cardMoved.CardLocationTo == Enums.CardLocation.OpponentGraveyard ||
                    cardMoved.CardLocationFrom == Enums.CardLocation.OwnPile && cardMoved.CardLocationTo == Enums.CardLocation.OwnGraveyard) && 
                    cardMoved.ConcernedFighter != UIDatas.MyIndex)
                {
                    UIDatas.OpponentPlayedCards.Add(cards.getCardById((int)cardMoved.TradingCard));
                    JsonCardsParser.Card card = cards.getCardById((int)cardMoved.TradingCard);
                    UIElements.DeckUI deckUI = UIDatas.Deck.FirstOrDefault(x => x.Card == card);
                    if (deckUI != null)
                        deckUI.CardCount++;
                    else
                    {
                        deckUI = new UIElements.DeckUI(card, 1);
                        UIDatas.AddCardToDeck(deckUI);
                    }
                    UIDatas.NotifyPropertyChanged("Deck");
                }
            }
            else if(value.EventType == Enums.EventType.CARD_TO_BE_PLAYED)
            {
                var eventCardMoved = value.TriggeredEvents.FirstOrDefault(x => x.EventType == Enums.EventType.CARD_MOVED);
                if(eventCardMoved != null)
                {
                    Builders.EventsManager.CardMovedEvent cardMoved = new Builders.EventsManager.CardMovedEvent(eventCardMoved);
                    if (cardMoved.CardLocationFrom == Enums.CardLocation.OwnHand && cardMoved.CardLocationTo == Enums.CardLocation.Nowhere && cardMoved.ConcernedFighter != UIDatas.MyIndex)
                    {
                        if (value.UInt1 == 1565)
                            UIDatas.OpponentFleaux -= 1;
                    }
                }
            }
            else if(value.EventType == Enums.EventType.A_O_E_ACTIVATED)
            {
                var eventCardMoved = value.TriggeredEvents.FirstOrDefault(x => x.EventType == Enums.EventType.CARD_MOVED);
                if(UIDatas.ActualFleauxIds.Contains(value.Int1) && eventCardMoved != null && eventCardMoved.Int1 != UIDatas.MyIndex)
                {
                    UIDatas.OpponentFleaux += 1;
                }
            }
            else if(value.EventType == Enums.EventType.TURN_STARTED)
            {
                var listOfEvents = value.TriggeredEvents.Where(x => x.UInt1 == 589);
                if (listOfEvents != null)
                {
                    foreach (var item in listOfEvents)
                    {
                        Builders.EventsManager.NewAOEEvent newAOE = new Builders.EventsManager.NewAOEEvent(item);
                        if (UIDatas.ActualFleauxIds.Count >= 4)
                            UIDatas.ActualFleauxIds.Dequeue();
                        if (newAOE.TradingCardId == 589)
                            UIDatas.ActualFleauxIds.Enqueue(newAOE.ConcernedFightObject);
                    }
                }
            }
            else if(value.EventType == Enums.EventType.NEW_A_O_E)
            {
                Builders.EventsManager.NewAOEEvent newAOE = new Builders.EventsManager.NewAOEEvent(value);
                if (UIDatas.ActualFleauxIds.Count >= 4)
                    UIDatas.ActualFleauxIds.Dequeue();
                if (newAOE.TradingCardId == 589)
                    UIDatas.ActualFleauxIds.Enqueue(newAOE.ConcernedFightObject);
            }

            foreach (var item in value.TriggeredEvents)
            {
                IterateEvents(item);
            }
        }

        private void UIActionGameFinishedEvent(Builders.GameFinished value)
        {
            if (UIDatas.HasIndex)
            {
                List<int> cardsToSave = new List<int>();
                foreach (var item in UIDatas.Deck)
                {
                    for (int i = 0; i < item.CardCount; i++)
                        cardsToSave.Add(item.Card.Id);
                }

                if (!Connector.SaveMatchResult(UIDatas.OpponentClasse, cardsToSave, UIDatas.OpponentPseudo, UIDatas.OwnClasse, value.WinnerPlayer == UIDatas.MyIndex ? 1 : 0, UIDatas.CurrentTurn, (int)UIDatas.GameType, DateTime.Now))
                    MessageBox.Show("Erreur lors de la sauvegarde des données.");
            }

            UIDatas.MyIndex = 0;
            UIDatas.HasIndex = false;

            toggleDeckButton.Dispatcher.Invoke(new HideGrid1(this.UpdateGrid1), new object[] { false });

            UIDatas.OpponentPlayedCards.Clear();
            UIDatas.ClearDeck();
            UIDatas.NotifyPropertyChanged("Deck");

            UIDatas.OpponentPseudo = UIDatas.OwnPseudo;
            UIDatas.OpponentVictories = UIDatas.OwnVictories;
            UIDatas.OpponentDefeats = UIDatas.OwnDefeats;
            UIDatas.OpponentLevel = UIDatas.OwnLevel;

            UIDatas.CurrentTurn = 0;
            UIDatas.OpponentFleaux = 0;
        }

        private void UIActionGameEventStarted(Builders.GameStarted value)
        {
            UIDatas.MyIndex = value.MyIndex;
            UIDatas.HasIndex = true;

            toggleStatsDeckButton.Dispatcher.Invoke(new HideGrid(this.UpdateGrid), new object[] { false });

            Data.Player player = value.PlayersList.FirstOrDefault(x => x.Index == UIDatas.MyIndex);

            if (player != null)
            {
                UIDatas.OwnPseudo = player.Profile.Nickname == null ? "" : player.Profile.Nickname;
                UIDatas.OwnVictories = player.Profile.VictoryCount;
                UIDatas.OwnDefeats = player.Profile.DefeatCount;
                UIDatas.OwnLevel = player.Profile.Level;
                UIDatas.OwnClasse = ((Enums.God)Enum.Parse(typeof(Enums.God), player.GodId.ToString())).ToString();
            }

            player = value.PlayersList.FirstOrDefault(x => x.Index != UIDatas.MyIndex);

            if (player != null)
            {
                UIDatas.OpponentPseudo = player.Profile.Nickname == null ? "" : player.Profile.Nickname;
                UIDatas.OpponentVictories = player.Profile.VictoryCount;
                UIDatas.OpponentDefeats = player.Profile.DefeatCount;
                UIDatas.OpponentLevel = player.Profile.Level;
                UIDatas.OpponentClasse = ((Enums.God)Enum.Parse(typeof(Enums.God), player.GodId.ToString())).ToString();
            }

            UIDatas.GameType = value.GameType;
        }

        private void UIActionStartOfTurn(Builders.StartOfTurn value)
        {
            if(UIDatas.HasIndex && value.PlayerIndex == UIDatas.MyIndex)
            {
                UIDatas.MaxAP = value.MaxActionPoints;
                UIDatas.CurrentAP = value.RealActionPoints;
                UIDatas.CurrentTurn = value.Turn;
            }
        }

        public delegate void HideGrid(bool value);

        private void UpdateGrid(bool value)
        {
            lock (this)
            {
                toggleStatsDeckButton.IsChecked = value;
            }
        }

        public delegate void HideGrid1(bool value);

        private void UpdateGrid1(bool value)
        {
            lock (this)
            {
                toggleDeckButton.IsChecked = value;
            }
        }

        private string ConcatHeader()
        {
            string toReturn = "";

            for(int i=0; i<17; i++)
            {
                toReturn += b.ReadByte().ToString("X2");
            }

            return toReturn.ToUpper();
        }

        private ulong ReadRawVarint64()
        {
            int i = 0;
            ulong num = 0uL;
            while (i < 64)
            {
                byte bb = b.ReadByte();
                num |= (ulong)((ulong)((long)(bb & 127)) << i);
                if ((bb & 128) == 0)
                {
                    return num;
                }
                i += 7;
            }
            return 0;
        }

        private uint ReadRawVarint32()
        {
            if (b.BaseStream.Position + 5 > b.BaseStream.Length)
            {
                return SlowReadRawVarint32();
            }
            int num = (int)b.ReadByte();
            if (num < 128)
            {
                return (uint)num;
            }
            int num2 = num & 127;
            if ((num = (int)b.ReadByte()) < 128)
            {
                num2 |= num << 7;
            }
            else
            {
                num2 |= (num & 127) << 7;
                if ((num = (int)b.ReadByte()) < 128)
                {
                    num2 |= num << 14;
                }
                else
                {
                    num2 |= (num & 127) << 14;
                    if ((num = (int)b.ReadByte()) < 128)
                    {
                        num2 |= num << 21;
                    }
                    else
                    {
                        num2 |= (num & 127) << 21;
                        num2 |= (num = (int)b.ReadByte()) << 28;
                        if (num >= 128)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (ReadRawByte() < 128)
                                {
                                    return (uint)num2;
                                }
                            }
                        }
                    }
                }
            }
            return (uint)num2;
        }

        private uint SlowReadRawVarint32()
        {
            int num = (int)ReadRawByte();
            if (num < 128)
            {
                return (uint)num;
            }
            int num2 = num & 127;
            if ((num = (int)ReadRawByte()) < 128)
            {
                num2 |= num << 7;
            }
            else
            {
                num2 |= (num & 127) << 7;
                if ((num = (int)ReadRawByte()) < 128)
                {
                    num2 |= num << 14;
                }
                else
                {
                    num2 |= (num & 127) << 14;
                    if ((num = (int)ReadRawByte()) < 128)
                    {
                        num2 |= num << 21;
                    }
                    else
                    {
                        num2 |= (num & 127) << 21;
                        num2 |= (num = (int)ReadRawByte()) << 28;
                        if (num >= 128)
                        {
                            for (int i = 0; i < 5; i++)
                            {
                                if (ReadRawByte() < 128)
                                {
                                    return (uint)num2;
                                }
                            }
                        }
                    }
                }
            }
            return (uint)num2;
        }

        private byte ReadRawByte()
        {
            if (b.BaseStream.Position == b.BaseStream.Length)
                return 0;
            return b.ReadByte();
        }

        private void closeApp_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void OpponentCardsPlayed_Click(object sender, RoutedEventArgs e)
        {
            string text = "Les cartes jouées par l'adversaire sont : ";
            foreach(var item in UIDatas.OpponentPlayedCards)
            {
                text += Environment.NewLine + item.Name;
            }
            MessageBox.Show(text);
        }

        private void toggleStatsDeckButton_Checked(object sender, RoutedEventArgs e)
        {
            UIDatas.MatchsList = Connector.GetMatches();
        }

        private void toggleStatsDeckButton_Unchecked(object sender, RoutedEventArgs e)
        {
            UIDatas.WinrateParClasse.Clear();
            UIDatas.ToursParClasse.Clear();

            var checkboxes_checked = checkboxes.Children.OfType<CheckBox>().Where(x => x.IsChecked.HasValue && x.IsChecked.Value);
            foreach(var item in checkboxes_checked)
            {
                item.IsChecked = false;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UIDatas.WinrateParClasse.Clear();
            UIDatas.ToursParClasse.Clear();
            UIDatas.WinsForThisGroup = 0;
            UIDatas.LosesForThisGroup = 0;
            var checkboxes_checked = checkboxes.Children.OfType<CheckBox>().Where(x => x.IsChecked.HasValue && x.IsChecked.Value);
            foreach(var item in checkboxes_checked)
            {
                var matchesConcerned = UIDatas.MatchsList.Where(x => x.PlayerClasse == item.Name);

                int vIop = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Iop" && x.ResultatMatch == 1);
                int vEca = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Ecaflip" && x.ResultatMatch == 1);
                int vCra = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Cra" && x.ResultatMatch == 1);
                int vEni = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Eniripsa" && x.ResultatMatch == 1);
                int vSacri = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sacrieur" && x.ResultatMatch == 1);
                int vSadi = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sadida" && x.ResultatMatch == 1);
                int vSram = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sram" && x.ResultatMatch == 1);
                int vXel = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Xelor" && x.ResultatMatch == 1);
                int vEnu = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Enutrof" && x.ResultatMatch == 1);

                int dIop = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Iop" && x.ResultatMatch == 0);
                int dEca = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Ecaflip" && x.ResultatMatch == 0);
                int dCra = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Cra" && x.ResultatMatch == 0);
                int dEni = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Eniripsa" && x.ResultatMatch == 0);
                int dSacri = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sacrieur" && x.ResultatMatch == 0);
                int dSadi = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sadida" && x.ResultatMatch == 0);
                int dSram = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Sram" && x.ResultatMatch == 0);
                int dXel = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Xelor" && x.ResultatMatch == 0);
                int dEnu = matchesConcerned.Count(x => x.Deck.OpponentClasse == "Enutrof" && x.ResultatMatch == 0);

                UIDatas.AddItemToWinrateParClasse(
                    item.Name,
                    new List<double>()
                    {
                        vIop * 100 / (vIop + (dIop == 0 ? 1 : dIop)),
                        vEca * 100 / (vEca + (dEca == 0 ? 1 : dEca)),
                        vCra * 100 / (vCra + (dCra == 0 ? 1 : dCra)),
                        vEni * 100 / (vEni + (dEni == 0 ? 1 : dEni)),
                        vSacri * 100 / (vSacri + (dSacri == 0 ? 1 : dSacri)),
                        vSadi * 100 / (vSadi + (dSadi == 0 ? 1 : dSadi)),
                        vSram * 100 / (vSram + (dSram == 0 ? 1 : dSram)),
                        vXel * 100 / (vXel + (dXel == 0 ? 1 : dXel)),
                        vEnu * 100 / (vEnu + (dEnu == 0 ? 1 : dEnu))
                    });
                UIDatas.AddItemToToursParClasse(
                    item.Name,
                    new List<double>()
                    {
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Iop") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Iop").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Ecaflip") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Ecaflip").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Cra") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Cra").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Eniripsa") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Eniripsa").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Sacrieur") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Sacrieur").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Sadida") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Sadida").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Sram") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Sram").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Xelor") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Xelor").Average(x => x.NbToursMatch) : 0,
                        matchesConcerned.Any(x => x.Deck.OpponentClasse == "Enutrof") ? matchesConcerned.Where(x => x.Deck.OpponentClasse == "Enutrof").Average(x => x.NbToursMatch) : 0
                    });

                UIDatas.WinsForThisGroup += matchesConcerned.Count(x => x.ResultatMatch == 1);
                UIDatas.LosesForThisGroup += matchesConcerned.Count(x => x.ResultatMatch == 0);
            }
        }
    }
}
