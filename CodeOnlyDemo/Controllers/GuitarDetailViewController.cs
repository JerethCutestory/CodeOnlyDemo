using System;
using CodeOnlyDemo.Models;
using CoreGraphics;
using Foundation;
using SafariServices;
using UIKit;
using WebKit;

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

            UIImageView imageView = new UIImageView
            {
                Image = UIImage.FromBundle(_guitar.LargeImageUrl),
                Frame = new CGRect(
                    (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                    label.Frame.Bottom + 10,
                    _guitar.LargeImageWidth,
                    _guitar.LargeImageHeight)
            };

            NSUrl url = new NSUrl(_guitar.DetailsURL);

            var webviewButton = UIButton.FromType(UIButtonType.RoundedRect);
            webviewButton.SetTitle("WV Details", UIControlState.Normal);
            webviewButton.BackgroundColor = UIColor.SystemBlueColor;
            webviewButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            webviewButton.Layer.CornerRadius = 5f;
            webviewButton.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            webviewButton.Frame = new CGRect(
                (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                imageView.Frame.Bottom + 20,
                imageView.Frame.Width,
                31.0f);
            webviewButton.TouchUpInside += (sender, e) =>
            {
                WKWebView webView = new WKWebView(View.Frame, new WKWebViewConfiguration());
                View.AddSubview(webView);
                var request = new NSUrlRequest(url);
                webView.LoadRequest(request);

                Console.WriteLine("WebView Details button clicked.");
            };

            var safariButton = UIButton.FromType(UIButtonType.RoundedRect);
            safariButton.SetTitle("SF Details", UIControlState.Normal);
            safariButton.BackgroundColor = UIColor.SystemBlueColor;
            safariButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            safariButton.Layer.CornerRadius = 5f;
            safariButton.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
            safariButton.Frame = new CGRect(
                (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                webviewButton.Frame.Bottom + 20,
                imageView.Frame.Width,
                31.0f);
            safariButton.TouchUpInside += (sender, e) =>
            {
                SFSafariViewController sfViewController = new SFSafariViewController(url);
                this.PresentViewController(sfViewController, true, null);

                Console.WriteLine("SafariViewControllerButton Details button clicked.");
            };

            View.AddSubviews(new UIView[] { label, imageView, webviewButton, safariButton });
        }
        #endregion
    }
}
