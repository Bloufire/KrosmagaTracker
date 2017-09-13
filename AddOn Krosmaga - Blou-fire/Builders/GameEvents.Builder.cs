using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Data;

namespace AddOn_Krosmaga___Blou_fire.Builders
{
	class GameEvents
	{
		private KrosmagaReader reader;

		private List<Data.GameEvent> _eventsList;
		private int _eventsCount;

		internal List<GameEvent> EventsList
		{
			get { return _eventsList; }

			set { _eventsList = value; }
		}

		public int EventsCount
		{
			get { return _eventsCount; }

			set { _eventsCount = value; }
		}

		public GameEvents()
		{
			EventsList = new List<GameEvent>();
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag == 10)
				{
					Data.GameEvent gameEvent = new Data.GameEvent();
					int size = (int) reader.ReadRawVarint32();
					gameEvent.Decode(reader.ReadMessage(size));
					EventsList.Add(gameEvent);
					continue;
				}
			}
		}
	}
}