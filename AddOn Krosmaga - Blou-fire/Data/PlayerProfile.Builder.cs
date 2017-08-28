using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddOn_Krosmaga___Blou_fire.Data
{
    class PlayerProfile
    {
        KrosmagaReader reader;

        private string _nickname;
        private int _level;
        private int _cardBack;
        private int _victoryCount;
        private int _defeatCount;
        private long _ranking;
        private List<CustomisationSlot> _customList;
        private int _customCount;

        public PlayerProfile()
        {
            CustomList = new List<CustomisationSlot>();
        }

        public string Nickname
        {
            get
            {
                return _nickname;
            }

            set
            {
                _nickname = value;
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

        public int CardBack
        {
            get
            {
                return _cardBack;
            }

            set
            {
                _cardBack = value;
            }
        }

        public int VictoryCount
        {
            get
            {
                return _victoryCount;
            }

            set
            {
                _victoryCount = value;
            }
        }

        public int DefeatCount
        {
            get
            {
                return _defeatCount;
            }

            set
            {
                _defeatCount = value;
            }
        }

        public long Ranking
        {
            get
            {
                return _ranking;
            }

            set
            {
                _ranking = value;
            }
        }

        public int CustomCount
        {
            get
            {
                return _customCount;
            }

            set
            {
                _customCount = value;
            }
        }

        internal List<CustomisationSlot> CustomList
        {
            get
            {
                return _customList;
            }

            set
            {
                _customList = value;
            }
        }

        public void Decode(byte[] array)
        {
            reader = new KrosmagaReader();
            reader.SetData(array);
            int tag;

            while (reader.B.BaseStream.Position < reader.B.BaseStream.Length && (tag = reader.ReadTag()) != 0)
            {
                if(tag <= 24)
                {
                    if(tag <= 10)
                    {
                        if(tag == 10)
                        {
                            Nickname = reader.ReadString();
                            continue;
                        }
                    }
                    else
                    {
                        if(tag == 16)
                        {
                            Level = (int)reader.ReadRawVarint32();
                            continue;
                        }
                        if(tag == 24)
                        {
                            CardBack = (int)reader.ReadRawVarint32();
                            continue;
                        }
                    }
                }
                else if(tag <= 40)
                {
                    if(tag == 32)
                    {
                        VictoryCount = (int)reader.ReadRawVarint32();
                        continue;
                    }
                    if(tag == 40)
                    {
                        DefeatCount = (int)reader.ReadRawVarint32();
                        continue;
                    }
                }
                else
                {
                    if(tag == 48)
                    {
                        Ranking = (long)reader.ReadRawVarint64();
                        continue;
                    }
                    if(tag == 58)
                    {
                        Data.CustomisationSlot customSlot = new Data.CustomisationSlot();
                        int size = (int)reader.ReadRawVarint32();
                        customSlot.Decode(reader.ReadMessage(size));
                        CustomList.Add(customSlot);
                        continue;
                    }
                }
            }
        }
    }
}
