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
        private UIScrollView _scrollView;
        private UIImageView _imageViewSG;
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
                AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin,
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
            webviewButton.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;
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
            safariButton.AutoresizingMask = UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleBottomMargin;
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

            //View.AddSubviews(new UIView[] { imageView });
            //View.AddSubviews(new UIView[] { label, imageView, webviewButton, safariButton });

            // SG Image 864 x 1152
            _imageViewSG = new UIImageView(UIImage.FromBundle("SG_Standard_LG.jpg"));
            _imageViewSG.TranslatesAutoresizingMaskIntoConstraints = false;
            _imageViewSG.Frame = new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

            _scrollView = new UIScrollView(new RectangleF(0, 0, (float)View.Frame.Width, (float)View.Frame.Height));
            _scrollView.ContentSize = new CGSize(UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);  //_imageViewSG.Image.Size;
            _scrollView.MaximumZoomScale = 3f;
            _scrollView.MinimumZoomScale = .1f;
            _scrollView.TranslatesAutoresizingMaskIntoConstraints = false;
            _scrollView.BackgroundColor = UIColor.Orange;
            View.AddSubview(_scrollView);

            _scrollView.TopAnchor.ConstraintEqualTo(View.TopAnchor, 0).Active = true;
            _scrollView.BottomAnchor.ConstraintEqualTo(View.BottomAnchor, 0).Active = true;
            _scrollView.LeadingAnchor.ConstraintEqualTo(View.LeadingAnchor, 0).Active = true;
            _scrollView.TrailingAnchor.ConstraintEqualTo(View.TrailingAnchor, 0).Active = true;


            //_imageViewSG.Center = _scrollView.Center;
            _scrollView.AddSubview(_imageViewSG);

            _scrollView.ViewForZoomingInScrollView += (UIScrollView sv) => { return _imageViewSG; };

            _imageViewSG.TopAnchor.ConstraintEqualTo(_scrollView.TopAnchor, 0).Active = true;
            //_imageViewSG.WidthAnchor.ConstraintEqualTo(UIScreen.MainScreen.Bounds.Width).Active = true;
            //_imageViewSG.HeightAnchor.ConstraintEqualTo(UIScreen.MainScreen.Bounds.Height).Active = true;
            //_imageViewSG.TrailingAnchor.ConstraintEqualTo(_scrollView.TrailingAnchor, 0).Active = true;

            //Console.WriteLine($"ViewDidLoad() - _scrollView.ContentSize WIDTH: {_scrollView.ContentSize.Width} - _scrollView.ContentSize HEIGHT: {_scrollView.ContentSize.Height}");
        }
        #endregion

        //public override void ViewDidLayoutSubviews()
        //{
        //    base.ViewDidLayoutSubviews();

        //    //_scrollView.Frame = new RectangleF(0, 0, (float)View.Frame.Width, (float)View.Frame.Height);
        //    //_scrollView.Frame = new CGRect(0, 0, View.Frame.Width, View.Frame.Height);

        //    //_scrollView.ContentSize = new CGSize((float)_imageViewSG.Image.Size.Width, (float)_imageViewSG.Image.Size.Height);
        //    ////_imageViewSG.Frame = new CGRect(0, 0, _scrollView.Frame.Size.Width, _scrollView.Frame.Size.Height);
        //    //_imageViewSG.Center = _scrollView.Center;

        //    Console.WriteLine($"ViewDidLayoutSubviews() - _scrollView.ContentSize WIDTH: {_scrollView.ContentSize.Width} - _scrollView.ContentSize HEIGHT: {_scrollView.ContentSize.Height}");
        //}
    }
}
