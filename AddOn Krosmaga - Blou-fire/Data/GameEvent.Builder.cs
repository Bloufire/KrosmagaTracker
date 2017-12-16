using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Enums;

namespace AddOn_Krosmaga___Blou_fire.Data
{
	class GameEvent
	{
		KrosmagaReader reader;

		private bool _hasEventType;
		private Enums.EventType _eventType;
        private bool _hasId;
        private uint _id;
        private bool _hasParentId;
        private uint _parentId;
        private List<GameEvent> _triggeredEvents;
		private int _triggeredEventsCount;
		private bool _hasRelatedCardInstance;
		private int _relatedCardInstance;
		private bool _hasRelatedTradingCardId;
		private int _relatedTradingCardId;
		private bool _hasTriggerer;
		private int _triggerer;
		private bool _hasInt1;
		private int _int1;
		private bool _hasInt2;
		private int _int2;
		private bool _hasInt3;
		private int _int3;
		private bool _hasInt4;
		private int _int4;
		private bool _hasInt5;
		private int _int5;
		private bool _hasBool1;
		private bool _bool1;
		private bool _hasBool2;
		private bool _bool2;
		private bool _hasUInt1;
		private uint _uInt1;
		private bool _hasLocation1;
		private Data.Location _location1;
		private bool _hasLocation2;
		private Data.Location _location2;
		private bool _hasValueModification1;
		private Data.ValueModification _valueModification1;
		private bool _hasValueModification2;
		private Data.ValueModification _valueModification2;
		private bool _hasCellCoord1;
		private Data.CellCoord _cellCoord1;
		private bool _hasCellCoord2;
		private Data.CellCoord _cellCoord2;
		private bool _hasMovementType1;
		private Enums.MovementType _movementType;
		private bool _hasSInt1;
		private int _sInt1;
		private bool _hasString1;
		private string _string1;
		private bool _hasTriggerableCapacityType1;
		private Enums.TriggerableCapacityType _TriggerableCapacityType1;

		public bool HasEventType
		{
			get { return _hasEventType; }

			set { _hasEventType = value; }
		}

		internal List<GameEvent> TriggeredEvents
		{
			get { return _triggeredEvents; }

			set { _triggeredEvents = value; }
		}

		public EventType EventType
		{
			get { return _eventType; }

			set { _eventType = value; }
		}

		public int TriggeredEventsCount
		{
			get { return _triggeredEventsCount; }

			set { _triggeredEventsCount = value; }
		}

		public bool HasRelatedCardInstance
		{
			get { return _hasRelatedCardInstance; }

			set { _hasRelatedCardInstance = value; }
		}

		public int RelatedCardInstance
		{
			get { return _relatedCardInstance; }

			set { _relatedCardInstance = value; }
		}

		public bool HasRelatedTradingCardId
		{
			get { return _hasRelatedTradingCardId; }

			set { _hasRelatedTradingCardId = value; }
		}

		public int RelatedTradingCardId
		{
			get { return _relatedTradingCardId; }

			set { _relatedTradingCardId = value; }
		}

		public bool HasTriggerer
		{
			get { return _hasTriggerer; }

			set { _hasTriggerer = value; }
		}

		public int Triggerer
		{
			get { return _triggerer; }

			set { _triggerer = value; }
		}

		public bool HasInt1
		{
			get { return _hasInt1; }

			set { _hasInt1 = value; }
		}

		public int Int1
		{
			get { return _int1; }

			set { _int1 = value; }
		}

		public bool HasInt2
		{
			get { return _hasInt2; }

			set { _hasInt2 = value; }
		}

		public int Int2
		{
			get { return _int2; }

			set { _int2 = value; }
		}

		public bool HasInt3
		{
			get { return _hasInt3; }

			set { _hasInt3 = value; }
		}

		public int Int3
		{
			get { return _int3; }

			set { _int3 = value; }
		}

		public bool HasInt4
		{
			get { return _hasInt4; }

			set { _hasInt4 = value; }
		}

		public int Int4
		{
			get { return _int4; }

			set { _int4 = value; }
		}

		public bool HasInt5
		{
			get { return _hasInt5; }

			set { _hasInt5 = value; }
		}

		public int Int5
		{
			get { return _int5; }

			set { _int5 = value; }
		}

		public bool HasBool1
		{
			get { return _hasBool1; }

			set { _hasBool1 = value; }
		}

		public bool Bool1
		{
			get { return _bool1; }

			set { _bool1 = value; }
		}

		public bool HasBool2
		{
			get { return _hasBool2; }

			set { _hasBool2 = value; }
		}

		public bool Bool2
		{
			get { return _bool2; }

			set { _bool2 = value; }
		}

		public bool HasUInt1
		{
			get { return _hasUInt1; }

			set { _hasUInt1 = value; }
		}

		public uint UInt1
		{
			get { return _uInt1; }

			set { _uInt1 = value; }
		}

		public bool HasLocation1
		{
			get { return _hasLocation1; }

			set { _hasLocation1 = value; }
		}

		internal Location Location1
		{
			get { return _location1; }

			set { _location1 = value; }
		}

		public bool HasLocation2
		{
			get { return _hasLocation2; }

			set { _hasLocation2 = value; }
		}

		internal Location Location2
		{
			get { return _location2; }

			set { _location2 = value; }
		}

		public bool HasValueModification1
		{
			get { return _hasValueModification1; }

			set { _hasValueModification1 = value; }
		}

		internal ValueModification ValueModification1
		{
			get { return _valueModification1; }

			set { _valueModification1 = value; }
		}

		public bool HasValueModification2
		{
			get { return _hasValueModification2; }

			set { _hasValueModification2 = value; }
		}

		internal ValueModification ValueModification2
		{
			get { return _valueModification2; }

			set { _valueModification2 = value; }
		}

		public bool HasCellCoord1
		{
			get { return _hasCellCoord1; }

			set { _hasCellCoord1 = value; }
		}

		internal CellCoord CellCoord1
		{
			get { return _cellCoord1; }

			set { _cellCoord1 = value; }
		}

		public bool HasCellCoord2
		{
			get { return _hasCellCoord2; }

			set { _hasCellCoord2 = value; }
		}

		internal CellCoord CellCoord2
		{
			get { return _cellCoord2; }

			set { _cellCoord2 = value; }
		}

		public bool HasMovementType1
		{
			get { return _hasMovementType1; }

			set { _hasMovementType1 = value; }
		}

		public MovementType MovementType
		{
			get { return _movementType; }

			set { _movementType = value; }
		}

		public bool HasSInt1
		{
			get { return _hasSInt1; }

			set { _hasSInt1 = value; }
		}

		public int SInt1
		{
			get { return _sInt1; }

			set { _sInt1 = value; }
		}

		public bool HasString1
		{
			get { return _hasString1; }

			set { _hasString1 = value; }
		}

		public string String1
		{
			get { return _string1; }

			set { _string1 = value; }
		}

		public bool HasTriggerableCapacityType1
		{
			get { return _hasTriggerableCapacityType1; }

			set { _hasTriggerableCapacityType1 = value; }
		}

		public TriggerableCapacityType TriggerableCapacityType1
		{
			get { return _TriggerableCapacityType1; }

			set { _TriggerableCapacityType1 = value; }
		}

        public bool HasId { get => _hasId; set => _hasId = value; }
        public bool HasParentId { get => _hasParentId; set => _hasParentId = value; }
        public uint Id { get => _id; set => _id = value; }
        public uint ParentId { get => _parentId; set => _parentId = value; }

        public GameEvent()
		{
			TriggeredEvents = new List<GameEvent>();
		}

		public void Decode(byte[] array)
		{
			reader = new KrosmagaReader();
			reader.SetData(array);
			int tag;

			while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
			{
				if (tag <= 88)
				{
					if (tag <= 40)
					{
						if (tag <= 16)
						{
							if (tag != 8)
							{
								/*if (tag == 18)
								{
									Data.GameEvent gameEvent = new Data.GameEvent();
									int size = (int) reader.ReadRawVarint32();
									gameEvent.Decode(reader.ReadMessage(size));
									TriggeredEvents.Add(gameEvent);
									continue;
								}*/
                                if(tag == 16)
                                {
                                    Id = reader.ReadRawVarint32();
                                    continue;
                                }
							}
							else
							{
								EventType = (Enums.EventType) Enum.Parse(typeof(Enums.EventType), reader.ReadRawVarint32().ToString());
								continue;
							}
						}
						else
						{
							if (tag == 24)
							{
                                ParentId = reader.ReadRawVarint32();
                                continue;
							}
							if (tag == 32)
							{
                                RelatedCardInstance = (int)reader.ReadRawVarint32();
                                continue;
                            }
							if (tag == 40)
							{
                                RelatedTradingCardId = (int)reader.ReadRawVarint32();
                                continue;
							}
						}
					}
					else if (tag <= 64)
					{
						if (tag == 48)
						{
                            Triggerer = (int)reader.ReadRawVarint32();
                            continue;
						}
						if (tag == 56)
						{
                            Int1 = (int)reader.ReadRawVarint32();
                            continue;
						}
						if (tag == 64)
						{
                            Int2 = (int)reader.ReadRawVarint32();
                            continue;
						}
					}
					else
					{
						if (tag == 72)
						{
                            Int3 = (int)reader.ReadRawVarint32();
                            continue;
                        }
						if (tag == 80)
						{
                            Int4 = (int)reader.ReadRawVarint32();
                            continue;
						}
						if (tag == 88)
						{
                            Int5 = (int)reader.ReadRawVarint32();
                            continue;
						}
					}
				}
				else if (tag <= 138)
				{
					if (tag <= 112)
					{
						if (tag == 96)
						{
                            Bool1 = reader.ReadBool();
                            continue;
						}
						if (tag == 104)
						{
                            Bool2 = reader.ReadBool();
                            continue;
						}
						if (tag == 112)
						{
                            UInt1 = reader.ReadRawVarint32();
                            continue;
						}
					}
					else
					{
						if (tag == 122)
						{
                            Data.Location location = new Data.Location();
                            int size = (int)reader.ReadRawVarint32();
                            location.Decode(reader.ReadMessage(size));
                            Location1 = location;
                            continue;
						}
						if (tag == 130)
						{
                            Data.Location location = new Data.Location();
                            reader.B.ReadByte();
                            int size = (int)reader.ReadRawVarint32();
                            location.Decode(reader.ReadMessage(size));
                            Location2 = location;
                            continue;
						}
						if (tag == 138)
						{
                            Data.ValueModification valueModification = new Data.ValueModification();
                            reader.B.ReadByte();
                            int size = (int)reader.ReadRawVarint32();
                            valueModification.Decode(reader.ReadMessage(size));
                            ValueModification1 = valueModification;
                            continue;
						}
					}
				}
				else if (tag <= 162)
				{
					if (tag == 146)
					{
                        Data.ValueModification valueModification = new Data.ValueModification();
                        reader.B.ReadByte();
                        int size = (int)reader.ReadRawVarint32();
                        valueModification.Decode(reader.ReadMessage(size));
                        ValueModification2 = valueModification;
                        continue;
					}
					if (tag == 154)
					{
                        Data.CellCoord cellCoord = new Data.CellCoord();
                        int size = (int)reader.ReadRawVarint32();
                        cellCoord.Decode(reader.ReadMessage(size));
                        CellCoord1 = cellCoord;
                        continue;
					}
					if (tag == 162)
					{
                        Data.CellCoord cellCoord = new Data.CellCoord();
                        int size = (int)reader.ReadRawVarint32();
                        cellCoord.Decode(reader.ReadMessage(size));
                        CellCoord2 = cellCoord;
                        continue;
					}
				}
                else if (tag <= 176)
                {
                    if (tag != 168)
                    {
                        if (tag == 176)
                        {
                            SInt1 = reader.DecodeZigZag32(reader.ReadRawVarint32());
                            continue;
                        }
                    }
                    else
                    {
                        MovementType = (Enums.MovementType)Enum.Parse(typeof(Enums.MovementType), reader.ReadRawVarint32().ToString());
                        continue;
                    }
                }
				else
				{
					if (tag == 186)
					{
                        String1 = reader.ReadString();
                        continue;
                    }
					if (tag == 192)
					{
                        TriggerableCapacityType1 = (Enums.TriggerableCapacityType)Enum.Parse(typeof(Enums.TriggerableCapacityType),
                            reader.ReadRawVarint32().ToString());
                        continue;
                    }
				}
			}
		}
	}
}