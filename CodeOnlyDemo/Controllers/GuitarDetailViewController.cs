using System;
using System.Drawing;
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
        #region Fields
        private UIImageView _imageViewSG;

        private UILabel _guitarLabel;
        private UIButton _safariButton;
        private UIButton _webviewButton;
        private UIScrollView _scrollView;
        private UIImageView _guitarImage;
        private readonly GuitarDetailModel _guitar;
        #endregion

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

            _guitarLabel = new UILabel
            {
                Text = $"{_guitar?.Manufacturer} {_guitar.Name} (introduced: {_guitar.YearIntroduced})",
                TextColor = UIColor.White,
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
                Lines = 1,
                TextAlignment = UITextAlignment.Center,
                Frame = new CGRect(
                    0,
                    36,
                    View.Bounds.Width,
                    31.0f)
            };
            _guitarLabel.Font.WithSize(24);

            _guitarImage = new UIImageView
            {
                Image = UIImage.FromBundle(_guitar.LargeImageUrl),
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
                Frame = new CGRect(
                    (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                    _guitarLabel.Frame.Bottom + 10,
                    _guitar.LargeImageWidth,
                    _guitar.LargeImageHeight)
            };

            NSUrl url = new NSUrl(_guitar.DetailsURL);

            _webviewButton  = UIButton.FromType(UIButtonType.RoundedRect);
            _webviewButton.SetTitle("WV Details", UIControlState.Normal);
            _webviewButton.BackgroundColor = UIColor.SystemBlueColor;
            _webviewButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            _webviewButton.Layer.CornerRadius = 5f;
            _webviewButton.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;
            _webviewButton.Frame = new CGRect(
                (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                _guitarImage.Frame.Bottom + 20,
                _guitarImage.Frame.Width,
                31.0f);
            _webviewButton.TouchUpInside += (sender, e) =>
            {
                WKWebView webView = new WKWebView(View.Frame, new WKWebViewConfiguration());
                View.AddSubview(webView);
                var request = new NSUrlRequest(url);
                webView.LoadRequest(request);

                Console.WriteLine("WebView Details button clicked.");
            };

            _safariButton = UIButton.FromType(UIButtonType.RoundedRect);
            _safariButton.SetTitle("SF Details", UIControlState.Normal);
            _safariButton.BackgroundColor = UIColor.SystemBlueColor;
            _safariButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            _safariButton.Layer.CornerRadius = 5f;
            _safariButton.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;
            _safariButton.Frame = new CGRect(
                (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                _webviewButton.Frame.Bottom + 20,
                _guitarImage.Frame.Width,
                31.0f);
            _safariButton.TouchUpInside += (sender, e) =>
            {
                SFSafariViewController sfViewController = new SFSafariViewController(url);
                sfViewController.ShouldAutorotate();
                this.PresentViewController(sfViewController, true, null);

                Console.WriteLine("SafariViewControllerButton Details button clicked.");
            };

            // SG Image 864 x 1152
            //_imageViewSG = new UIImageView(UIImage.FromBundle("SG_Standard_LG.jpg"));
            //_imageViewSG.TranslatesAutoresizingMaskIntoConstraints = false;
            //_imageViewSG.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

            _scrollView = new UIScrollView(new RectangleF(0, 0, (float)View.Frame.Width, (float)View.Frame.Height));
            _scrollView.ContentSize = new CGSize(UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
            _scrollView.MaximumZoomScale = 3f;
            _scrollView.MinimumZoomScale = .1f;
            _scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            _scrollView.BackgroundColor = UIColor.SystemOrangeColor;
            _scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
            View.AddSubview(_scrollView);

            _scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor, 0).Active = true;
            _scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, 0).Active = true;
            _scrollView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 0).Active = true;
            _scrollView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, 0).Active = true;


            //_imageViewSG.Center = _scrollView.Center;
            _scrollView.AddSubviews(new UIView[] { _guitarLabel, _guitarImage, _webviewButton, _safariButton }); 

            //_scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => { return _imageViewSG; };

            //_imageViewSG.TopAnchor.ConstraintEqualTo(_scrollView.TopAnchor, 0).Active = true;
            ////_imageViewSG.WidthAnchor.ConstraintEqualTo(UIScreen.MainScreen.Bounds.Width).Active = true;
            ////_imageViewSG.HeightAnchor.ConstraintEqualTo(UIScreen.MainScreen.Bounds.Height).Active = true;
            ////_imageViewSG.TrailingAnchor.ConstraintEqualTo(_scrollView.TrailingAnchor, 0).Active = true;
        }
        #endregion

        #region ViewDidLayoutSubviews
        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            _guitarImage.Frame = new CGRect(
                    (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                    _guitarLabel.Frame.Bottom + 10,
                    _guitar.LargeImageWidth,
                    _guitar.LargeImageHeight);

            _webviewButton.Frame = new CGRect(
                (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                _guitarImage.Frame.Bottom + 20,
                _guitarImage.Frame.Width,
                31.0f);

            _safariButton.Frame = new CGRect(
                (int)(View.Bounds.GetMidX() - (_guitar.LargeImageWidth / 2)),
                _webviewButton.Frame.Bottom + 20,
                _guitarImage.Frame.Width,
                31.0f);
        }
        #endregion
    }
}
