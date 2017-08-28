using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonCardsParser
{
    public class CardCollection
    {
        public List<Card> Collection { get; set; }

        public CardCollection()
        {
            Collection = new List<Card>();
        }

        public Card getCardById(int id)
        {
            return Collection.Where(x => x.Id == id).FirstOrDefault();
        }

        public Card getCardByName(string name)
        {
            return Collection.Where(x => x.Name == name).FirstOrDefault();
        }
    }
}
