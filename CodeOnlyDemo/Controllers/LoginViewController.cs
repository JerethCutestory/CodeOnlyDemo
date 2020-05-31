using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace CodeOnlyDemo.Controllers
{
    public class LoginViewController : UIViewController
    {
        #region Fields
        public NSUrlSession session;
        public NSUrlSessionDownloadTask downloadTask;
        const string Identifier = "com.chronicpleasure.CodeOnlyDemo.BackgroundSession";
        const string DownloadUrlString = "https://upload.wikimedia.org/wikipedia/commons/3/31/Daintree_Rainforest_4.jpg";

        private GuitarListViewController GuitarListViewController;
        private UITextField usernameField;
        private UITextField passwordField;
        private UIBarButtonItem testButton;
        #endregion

        #region ctor
        public LoginViewController()
        {
            GuitarListViewController = new GuitarListViewController("Guitars");
        }
        #endregion

        #region ViewDidLoad
        // Called when the View controller is first loaded into memory
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // https://www.grapecity.com/blogs/adding-a-share-button-to-a-xamarin-ios-app
            testButton = new UIBarButtonItem()
            {
                Title = "Show"
            };
            testButton.Clicked += Start;
            GuitarListViewController.NavigationItem.RightBarButtonItem = testButton;

            if (session == null)
            {
                session = InitBackgroundSession();
            }

            View.BackgroundColor = UIColor.SystemOrangeColor;
            Title = "Authenticate Yourself";

            nfloat height = 31.0f;
            nfloat width = View.Bounds.Width;

            usernameField = new UITextField
            {
                Placeholder = "Enter Your Username",
                BorderStyle = UITextBorderStyle.RoundedRect,
                Frame = new CGRect(10, 110, width - 20, height),
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth
            };

            passwordField = new UITextField
            {
                Placeholder = "Enter your password",
                BorderStyle = UITextBorderStyle.RoundedRect,
                Frame = new CGRect(10, usernameField.Frame.Bottom + 6, width - 20, height),
                SecureTextEntry = true,
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth
            };

            var submitButton = UIButton.FromType(UIButtonType.RoundedRect);
            submitButton.Frame = new CGRect(10, passwordField.Frame.Bottom + 20, width - 20, 44);
            submitButton.SetTitle("Submit", UIControlState.Normal);
            submitButton.BackgroundColor = UIColor.SystemBlueColor;
            submitButton.SetTitleColor(UIColor.White, UIControlState.Normal);
            submitButton.Layer.CornerRadius = 5f;
            submitButton.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

            View.AddSubviews(new UIView[] { usernameField, passwordField, submitButton });

            submitButton.TouchUpInside += (sender, e) =>
            {
                Console.WriteLine("Login button clicked.");
                this.NavigationController.PushViewController(GuitarListViewController, true);
            };
        }
        #endregion

        #region Start
        void Start(object sender, EventArgs e)
        {
            Console.WriteLine("Show button clicked.");

            if (downloadTask != null)
            {
                return;
            }

            using (var url = NSUrl.FromString(DownloadUrlString))
            using (var request = NSUrlRequest.FromUrl(url))
            {
                downloadTask = session.CreateDownloadTask(request);
                downloadTask.Resume();
            }

            //imageView.Hidden = true;
        }
        #endregion

        #region InitBackgroundSession
        public NSUrlSession InitBackgroundSession()
        {
            Console.WriteLine("InitBackgroundSession");

            using (var configuration = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration(Identifier))
            {
                return NSUrlSession.FromConfiguration(configuration, new UrlSessionDelegate(GuitarListViewController), null);
            }
        }
        #endregion
    }
}
