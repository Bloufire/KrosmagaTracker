using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddOn_Krosmaga___Blou_fire.Enums;
using AddOn_Krosmaga___Blou_fire.Properties;

namespace AddOn_Krosmaga___Blou_fire.DataModel
{
    public class KrosClass
    {
        public KrosClass()
        {
            
        }

        public KrosClass(string nameClass, string imageURI)
        {
            this.NameClass = nameClass;
            this.ImageUri = imageURI;
        }

        public string NameClass { get; set; }
        public string ImageUri { get; set; }
    }

   
}
