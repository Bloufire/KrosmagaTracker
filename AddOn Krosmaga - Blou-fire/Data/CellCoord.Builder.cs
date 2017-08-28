using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
    class CellCoord
    {
        KrosmagaReader reader;

        private bool _hasX;
        private int _x;
        private bool _hasY;
        private int _y;

        public bool HasX
        {
            get
            {
                return _hasX;
            }

            set
            {
                _hasX = value;
            }
        }

        public int X
        {
            get
            {
                return _x;
            }

            set
            {
                _x = value;
            }
        }

        public bool HasY
        {
            get
            {
                return _hasY;
            }

            set
            {
                _hasY = value;
            }
        }

        public int Y
        {
            get
            {
                return _y;
            }

            set
            {
                _y = value;
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
                        Y = (int)reader.ReadRawVarint32();
                        continue;
                    }
                }
                else
                {
                    X = (int)reader.ReadRawVarint32();
                }
            }
        }
    }
}
