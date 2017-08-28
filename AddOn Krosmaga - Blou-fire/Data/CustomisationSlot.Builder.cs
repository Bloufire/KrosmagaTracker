using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
    class CustomisationSlot
    {
        KrosmagaReader reader;

        private int _slot;
        private int _customId;

        public CustomisationSlot() { }

        public int Slot
        {
            get
            {
                return _slot;
            }

            set
            {
                _slot = value;
            }
        }

        public int CustomId
        {
            get
            {
                return _customId;
            }

            set
            {
                _customId = value;
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
                    if(tag == 16)
                    {
                        CustomId = (int)reader.ReadRawVarint32();
                        continue;
                    }
                }
                else
                {
                    Slot = (int)reader.ReadRawVarint32();
                    continue;
                }
            }
        }
    }
}
