using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
    class SelectedDeckByGod
    {
        private KrosmagaReader reader;

        private bool _hasGod;
        private int _god;
        private bool _hasLastSelectedDeck;
        private Data.Uuid _lastSelectedDeck;

        public bool HasGod
        {
            get
            {
                return _hasGod;
            }

            set
            {
                _hasGod = value;
            }
        }

        public int God
        {
            get
            {
                return _god;
            }

            set
            {
                _god = value;
            }
        }

        public bool HasLastSelectedDeck
        {
            get
            {
                return _hasLastSelectedDeck;
            }

            set
            {
                _hasLastSelectedDeck = value;
            }
        }

        internal Uuid LastSelectedDeck
        {
            get
            {
                return _lastSelectedDeck;
            }

            set
            {
                _lastSelectedDeck = value;
            }
        }

        public void Decode(byte[] array)
        {
            reader = new KrosmagaReader();
            reader.SetData(array);
            int tag;

            while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
            {
                if(tag != 8)
                {
                    if(tag == 18)
                    {
                        Data.Uuid value = new Data.Uuid();
                        int size = (int)reader.ReadRawVarint32();
                        value.Decode(reader.ReadMessage(size));
                        LastSelectedDeck = value;
                        continue;
                    }
                }
                else
                {
                    God = (int)reader.ReadRawVarint32();
                }
            }
        }
    }
}
