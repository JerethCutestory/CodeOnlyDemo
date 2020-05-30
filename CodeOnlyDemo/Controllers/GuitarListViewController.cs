using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;
using CodeOnlyDemo.Models;
using UIKit;

namespace CodeOnlyDemo.Controllers
{
    public class GuitarListViewController : UIViewController
    {
        #region ctor
        public GuitarListViewController(string title)
        {
            this.Title = title;
            View.BackgroundColor = UIColor.SystemBlueColor;
        }
        #endregion

        #region ViewDidLoad
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UITableView table = new UITableView(View.Bounds); // defaults to Plain style
            table.Source = new TableSource(GetGuitarData(), this);
            table.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
            Add(table);
        }
        #endregion

        //public override void ViewLayoutMarginsDidChange()
        //{
        //    base.ViewLayoutMarginsDidChange();

        //    Console.WriteLine("ViewLayoutMarginsDidChange was called.");
        //}

        #region GetGuitarData
        private List<GuitarDetailModel> GetGuitarData()
        {
            List<GuitarDetailModel> guitars = new List<GuitarDetailModel>();

            Assembly assembly = IntrospectionExtensions.GetTypeInfo(typeof(GuitarDetailModels)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("CodeOnlyDemo.Data.Guitars.xml");
            using (var reader = new System.IO.StreamReader(stream))
            {
                XmlRootAttribute xRoot = new XmlRootAttribute
                {
                    ElementName = "GuitarData",
                    IsNullable = true
                };
                XmlSerializer serializer = new XmlSerializer(typeof(GuitarDetailModels), xRoot);
                var xmlItems = (GuitarDetailModels)serializer.Deserialize(reader);

                guitars.AddRange(xmlItems.Guitars);
            }

            //var listView = new ListView();
            //listView.ItemsSource = monkeys;

            return guitars;
        }
        #endregion
    }
}
