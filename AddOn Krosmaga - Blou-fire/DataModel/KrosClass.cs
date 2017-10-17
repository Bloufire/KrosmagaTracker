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

    public static class KrosClassData
    {
        public static List<KrosClass> GetAllClassAndImage()
        {
            var ComboClasseValues = Enum.GetValues(typeof(ClassEnum));

            var listToReturn = new List<KrosClass>();
            listToReturn.Add(new KrosClass("Iop","../../Images/Gods/Iop.png"));
            listToReturn.Add(new KrosClass("Cra", "../../Images/Gods/Cra.png"));
            listToReturn.Add(new KrosClass("Eni", "../../Images/Gods/Eni.png"));
            listToReturn.Add(new KrosClass("Enu", "../../Images/Gods/Enu.png"));

            return listToReturn;
        }
    }
}
