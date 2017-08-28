using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
    class PlayerRankInfo
    {
        private KrosmagaReader reader;

        private bool _hasXp;
        private int _xp;
        private bool _hasLevel;
        private int _level;
        private bool _hasFloorXpForLevel;
        private int _floorXpForLevel;
        private bool _hasCeilXpForLevel;
        private int _ceilXpForLevel;
        private bool _hasRank;
        private long _rank;
        private bool _hasBestLevel;
        private int _bestLevel;

        public bool HasXp
        {
            get
            {
                return _hasXp;
            }

            set
            {
                _hasXp = value;
            }
        }

        public int Xp
        {
            get
            {
                return _xp;
            }

            set
            {
                _xp = value;
            }
        }

        public bool HasLevel
        {
            get
            {
                return _hasLevel;
            }

            set
            {
                _hasLevel = value;
            }
        }

        public int Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
            }
        }

        public bool HasFloorXpForLevel
        {
            get
            {
                return _hasFloorXpForLevel;
            }

            set
            {
                _hasFloorXpForLevel = value;
            }
        }

        public int FloorXpForLevel
        {
            get
            {
                return _floorXpForLevel;
            }

            set
            {
                _floorXpForLevel = value;
            }
        }

        public bool HasCeilXpForLevel
        {
            get
            {
                return _hasCeilXpForLevel;
            }

            set
            {
                _hasCeilXpForLevel = value;
            }
        }

        public int CeilXpForLevel
        {
            get
            {
                return _ceilXpForLevel;
            }

            set
            {
                _ceilXpForLevel = value;
            }
        }

        public bool HasRank
        {
            get
            {
                return _hasRank;
            }

            set
            {
                _hasRank = value;
            }
        }

        public long Rank
        {
            get
            {
                return _rank;
            }

            set
            {
                _rank = value;
            }
        }

        public bool HasBestLevel
        {
            get
            {
                return _hasBestLevel;
            }

            set
            {
                _hasBestLevel = value;
            }
        }

        public int BestLevel
        {
            get
            {
                return _bestLevel;
            }

            set
            {
                _bestLevel = value;
            }
        }

        public void Decode(byte[] array)
        {
            reader = new KrosmagaReader();
            reader.SetData(array);
            int tag;

            while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
            {
                if(tag <= 16)
                {
                    if(tag == 8)
                    {
                        Xp = (int)reader.ReadRawVarint32();
                        continue;
                    }
                    if(tag == 16)
                    {
                        Level = (int)reader.ReadRawVarint32();
                        continue;
                    }
                }
                else if(tag <= 32)
                {
                    if(tag == 24)
                    {
                        FloorXpForLevel = (int)reader.ReadRawVarint32();
                        continue;
                    }
                    if(tag == 32)
                    {
                        CeilXpForLevel = (int)reader.ReadRawVarint32();
                        continue;
                    }
                }
                else
                {
                    if(tag == 40)
                    {
                        Rank = (long)reader.ReadRawVarint64();
                        continue;
                    }
                    if(tag == 48)
                    {
                        BestLevel = (int)reader.ReadRawVarint32();
                        continue;
                    }
                }
            }
        }
    }
}
