using UIKit;

namespace CodeOnlyDemo.Controllers
{
    public class CircleController : UIViewController
    {
        CircleView _view;

        #region ctor
        public CircleController()
        {
        }
        #endregion

        #region LoadView
        public override void LoadView()
        {
            base.LoadView();

            _view = new CircleView();
        }
        #endregion
    }
}
