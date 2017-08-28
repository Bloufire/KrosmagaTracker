using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteConnector
{
    public class Card
    {
        private int _idCard;
        private int _realCardId;

        public int IdCard
        {
            get
            {
                return _idCard;
            }

            set
            {
                _idCard = value;
            }
        }

        public int RealCardId
        {
            get
            {
                return _realCardId;
            }

            set
            {
                _realCardId = value;
            }
        }
    }
}
