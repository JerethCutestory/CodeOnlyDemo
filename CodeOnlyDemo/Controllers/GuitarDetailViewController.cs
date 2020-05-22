using System;
using CodeOnlyDemo.Models;
using CoreGraphics;
using UIKit;

namespace CodeOnlyDemo.Controllers
{
    public class GuitarDetailViewController : UIViewController
    {
        private readonly GuitarDetailModel _guitar;

        public GuitarDetailViewController(GuitarDetailModel guitar)
        {
            this._guitar = guitar;

            View.BackgroundColor = UIColor.Black;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            nfloat height = 31.0f;
            nfloat width = View.Bounds.Width;

            UILabel label = new UILabel
            {
                Text = $"{_guitar?.Manufacturer} {_guitar.Name} (introduced: {_guitar.YearIntroduced})",
                TextColor = UIColor.White,
                Frame = new CGRect(10, 110, width - 20, height),
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth
            };
            label.Font.WithSize(24);

            CGRect screen = UIScreen.MainScreen.Bounds;

            UIImageView imageView = new UIImageView();
            imageView.Image = UIImage.FromBundle(_guitar.LargeImageUrl);
            imageView.Frame = new CGRect(10, 160, _guitar.LargeImageWidth, _guitar.LargeImageHeight);

            View.AddSubviews(new UIView[] { label, imageView });
        }
    }
}
