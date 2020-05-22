using System.Collections.Generic;
using System.Xml.Serialization;

namespace CodeOnlyDemo.Models
{
    public class GuitarDetailModel
    {
        #region ctor
        public GuitarDetailModel()
        {
        }
        #endregion

        #region Properties
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int YearIntroduced { get; set; }
        public string SmallImageUrl { get; set; }
        public string LargeImageUrl { get; set; }
        public int LargeImageWidth { get; set; }
        public int LargeImageHeight { get; set; }
        #endregion
    }

    public class GuitarDetailModels
    {
        [XmlElement("Guitar")]
        public List<GuitarDetailModel> Guitars { get; set; }

        #region ctor
        public GuitarDetailModels()
        {
            Guitars = new List<GuitarDetailModel>();
        }
        #endregion
    }
}
