using System;
using System.Drawing;
using CoreGraphics;
using UIKit;

namespace CodeOnlyDemo
{
    public class CircleView : UIView
    {
        #region ctor
        public CircleView()
        {
            BackgroundColor = UIColor.LightGray;
        }
        #endregion

        #region Draw
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            using (var gctx = UIGraphics.GetCurrentContext())
            {
                // Set up drawing attrobutes
                gctx.SetLineWidth(10.0f);
                UIColor.Green.SetFill();
                UIColor.Blue.SetStroke();

                // Create Geometry
                var path = new CGPath();
                path.AddArc(Bounds.GetMidX(), Bounds.GetMidY(), 50.0f, 0, 2.0f * (float)Math.PI, true);

                // Add geometry to graphic context and draw
                gctx.AddPath(path);
                gctx.DrawPath(CGPathDrawingMode.FillStroke);
            }
        }
        #endregion
    }
}
