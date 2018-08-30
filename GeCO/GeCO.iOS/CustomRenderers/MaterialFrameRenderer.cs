using System;
using CoreGraphics;
using GeCO.iOS.CustomRenderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Frame), typeof(MaterialFrameRenderer))]
namespace GeCO.iOS.CustomRenderers
{
    /// <summary>
    /// Renderer que custumiza todas as Frames (no iOS) alterando as sombras, para melhor cumprir os standard de material design.
    /// </summary>
    public class MaterialFrameRenderer : FrameRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            // Modificações nas sombras para tentar cumprir os standards
            Layer.ShadowRadius = 2.0f;
            Layer.ShadowColor = UIColor.Gray.CGColor;
            Layer.ShadowOffset = new CGSize(2, 2);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;
        }
    }
}