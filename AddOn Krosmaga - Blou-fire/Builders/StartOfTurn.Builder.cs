using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Builders
{
    class StartOfTurn
    {
        private KrosmagaReader reader;

        private int _turn;
        private int _playerIndex;
        private int _realActionPoints;
        private int _maxActionPoints;
        
        public int Turn
        {
            get
            {
                return _turn;
            }

            set
            {
                _turn = value;
            }
        }

        public int PlayerIndex
        {
            get
            {
                return _playerIndex;
            }

            set
            {
                _playerIndex = value;
            }
        }

        public int RealActionPoints
        {
            get
            {
                return _realActionPoints;
            }

            set
            {
                _realActionPoints = value;
            }
        }

        public int MaxActionPoints
        {
            get
            {
                return _maxActionPoints;
            }

            set
            {
                _maxActionPoints = value;
            }
        }

        public StartOfTurn() { }

        public void Decode(byte[] array)
        {
            reader = new KrosmagaReader();
            reader.SetData(array);
            int tag;

            while(reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
            {
                if(tag == 8)
                {
                    Turn = (int)reader.ReadRawVarint32();
                    continue;
                }
                else if(tag == 16)
                {
                    PlayerIndex = (int)reader.ReadRawVarint32();
                    continue;
                }
                else if(tag == 24)
                {
                    RealActionPoints = (int)reader.ReadRawVarint32();
                    continue;
                }
                else if(tag == 32)
                {
                    MaxActionPoints = (int)reader.ReadRawVarint32();
                }
            }
        }
    }
}
