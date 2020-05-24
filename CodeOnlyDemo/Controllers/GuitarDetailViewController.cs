using CodeOnlyDemo.Models;
using CoreGraphics;
using UIKit;

namespace CodeOnlyDemo.Controllers
{
    public class GuitarDetailViewController : UIViewController
    {
        private readonly GuitarDetailModel _guitar;

        #region ctor
        public GuitarDetailViewController(GuitarDetailModel guitar)
        {
            this._guitar = guitar;

            View.BackgroundColor = UIColor.Black;
        }
        #endregion

        #region ViewDidLoad
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UIImageView imageView = new UIImageView
            {
                Image = UIImage.FromBundle(_guitar.LargeImageUrl),
                Frame = new CGRect(
                    (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                    (int)(View.Bounds.GetMidY() - (_guitar.LargeImageHeight / 2)),
                    _guitar.LargeImageWidth,
                    _guitar.LargeImageHeight)
            };

            UILabel label = new UILabel
            {
                Text = $"{_guitar?.Manufacturer} {_guitar.Name} (introduced: {_guitar.YearIntroduced})",
                TextColor = UIColor.White,
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
                Lines = 1,
                TextAlignment = UITextAlignment.Center,
                Frame = new CGRect(
                    0,
                    110,
                    View.Bounds.Width,
                    31.0f)
            };
            label.Font.WithSize(24);

            View.AddSubviews(new UIView[] { label, imageView });
        }
        #endregion
    }
}
