using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace XPlatUtils
{
	/// <summary>
	/// Set of extension methods for UIKit
	/// </summary>
	public static class UIKitExtensions
	{
		/// <summary>
		/// Takes a screenshot from a UIView
		/// </summary>
		/// <returns>The screenshot.</returns>
		/// <param name="view">View to take a screenshot of.</param>
		public static UIImage TakeScreenshot(this UIView view)
		{
			UIGraphics.BeginImageContext(view.Frame.Size);
			try
			{
				using (var context = UIGraphics.GetCurrentContext())
				{
					view.Layer.RenderInContext(context);
					
					return UIGraphics.GetImageFromCurrentImageContext();
				}
			}
			finally
			{
				UIGraphics.EndImageContext();
			}
		}
	}
}

