using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonCardsParser
{
    public class JsonCard
    {
        public JsonCard() { }

        public List<Card> ChargerCartes()
        {
            List<Card> returnedList = new List<Card>();
            var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\cards\\");

            foreach (var file in files)
            {
                using (StreamReader r = new StreamReader(file))
                {
                    string json = r.ReadToEnd();
                    Card card = JsonConvert.DeserializeObject<Card>(json);
                    card.SetUIName("FR");
                    card.SetImagePath("FR");
                    returnedList.Add(card);
                }
            }

            Card unknown = new Card
            {
                Id = -1,
                CardType = 1,
                GodType = 0,
                Name = "unknown",
                UIName = "Unknown Card"
            };
            unknown.SetImagePath("FR");
            returnedList.Add(unknown);

            return returnedList;
        }
    }
}
