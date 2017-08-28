using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Enums;

namespace AddOn_Krosmaga___Blou_fire.Builders
{
    class GameFinished
    {
        private KrosmagaReader reader;

        private Enums.Result _result;
        private int _winnerPlayer;
        private int _loserPlayer;
        private Enums.Reason _reason;
        private List<Enums.ResultHandlerType> _resultHandlerType;
        private int _resultHandlerCount;

        public Result Result
        {
            get
            {
                return _result;
            }

            set
            {
                _result = value;
            }
        }

        public int WinnerPlayer
        {
            get
            {
                return _winnerPlayer;
            }

            set
            {
                _winnerPlayer = value;
            }
        }

        public int LoserPlayer
        {
            get
            {
                return _loserPlayer;
            }

            set
            {
                _loserPlayer = value;
            }
        }

        public Reason Reason
        {
            get
            {
                return _reason;
            }

            set
            {
                _reason = value;
            }
        }

        public List<ResultHandlerType> ResultHandlerType
        {
            get
            {
                return _resultHandlerType;
            }

            set
            {
                _resultHandlerType = value;
            }
        }

        public int ResultHandlerCount
        {
            get
            {
                return _resultHandlerCount;
            }

            set
            {
                _resultHandlerCount = value;
            }
        }

        public GameFinished()
        {
            ResultHandlerType = new List<Enums.ResultHandlerType>();
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
                    if(tag != 8)
                    {
                        if(tag == 16)
                        {
                            WinnerPlayer = (int)reader.ReadRawVarint32();
                            continue;
                        }
                    }
                    else
                    {
                        Result = (Enums.Result)Enum.Parse(typeof(Enums.Result), reader.ReadRawVarint32().ToString());
                        continue;
                    }       
                }
                else
                {
                    if(tag == 24)
                    {
                        LoserPlayer = (int)reader.ReadRawVarint32();
                        continue;
                    }
                    if(tag != 32)
                    {
                        switch(tag)
                        {
                            case 40:
                            case 42:
                                ResultHandlerType.Add((Enums.ResultHandlerType)Enum.Parse(typeof(Enums.ResultHandlerType), reader.ReadRawVarint32().ToString()));
                                continue;
                        }
                    }
                    else
                    {
                        Reason = (Enums.Reason)Enum.Parse(typeof(Enums.Reason), reader.ReadRawVarint32().ToString());
                        continue;
                    }
                }
            }
        }
    }
}
