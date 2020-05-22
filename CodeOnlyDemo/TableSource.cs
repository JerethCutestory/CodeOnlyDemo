using System;
using System.Collections.Generic;
using CodeOnlyDemo.Controllers;
using CodeOnlyDemo.Models;
using Foundation;
using UIKit;

namespace CodeOnlyDemo
{
    public class TableSource : UITableViewSource
    {
        UIViewController _owner;
        List<GuitarDetailModel> _guitars;
        readonly string _cellIdentifier = "GuitarTableCell";

        public TableSource(List<GuitarDetailModel> items)
        {
            _guitars = items;
        }
        public TableSource(List<GuitarDetailModel> items, UIViewController owner)
        {
            _guitars = items;
            this._owner = owner;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _guitars.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            GuitarDetailModel guitar = _guitars[indexPath.Row];

            UITableViewCell cell = tableView.DequeueReusableCell(_cellIdentifier);
            //if there are no cells to reuse, create a new one
            if (cell == null)
            {
                cell = new UITableViewCell(UITableViewCellStyle.Default, _cellIdentifier);
            }

            cell.TextLabel.Text = guitar.Name;

            return cell;
        }

        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            string guitarName = $"{_guitars[indexPath.Row]?.Manufacturer} {_guitars[indexPath.Row]?.Name}";
            string yearIntroduced = $"Introduced in {_guitars[indexPath.Row]?.YearIntroduced.ToString()}";

            GuitarDetailViewController guitarDetailViewController = new GuitarDetailViewController(_guitars[indexPath.Row]);
            _owner.NavigationController.PushViewController(guitarDetailViewController, true);

            // Generate an alert
            //UIAlertController okAlertController = UIAlertController.Create (guitarName, yearIntroduced, UIAlertControllerStyle.Alert);
            //okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));
            //_owner.PresentViewController(okAlertController, true, null);

            tableView.DeselectRow (indexPath, true);
        }
    }
}
