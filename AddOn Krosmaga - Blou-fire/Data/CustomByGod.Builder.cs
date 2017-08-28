using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
    class CustomByGod
    {
        private KrosmagaReader reader;

        private bool _hasGod;
        private int _god;
        private List<Data.Custom> _customsList;
        private int _customsCount;

        public CustomByGod()
        {
            CustomsList = new List<Custom>();
        }

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

        internal List<Custom> CustomsList
        {
            get
            {
                return _customsList;
            }

            set
            {
                _customsList = value;
            }
        }

        public int CustomsCount
        {
            get
            {
                return _customsCount;
            }

            set
            {
                _customsCount = value;
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
                        Data.Custom value = new Data.Custom();
                        int size = (int)reader.ReadRawVarint32();
                        value.Decode(reader.ReadMessage(size));
                        CustomsList.Add(value);
                        continue;
                    }
                }
                else
                {
                    God = (int)reader.ReadRawVarint32();
                    continue;
                }
            }
        }
    }
}
