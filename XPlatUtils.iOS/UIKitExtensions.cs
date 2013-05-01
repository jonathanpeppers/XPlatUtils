//  UIKitExtensions.cs
//
//  Author:
//  Jonathan Peppers
//
//  Copyright 2012 Jonathan Peppers
//
//  Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance 
//  with the License. You may obtain a copy of the License at
//  
//  http://www.apache.org/licenses/LICENSE-2.0
//  
//  Unless required by applicable law or agreed to in writing, software distributed under the License is distributed 
//  on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for 
//  the specific language governing permissions and limitations under the License.

using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace XPlatUtils
{
	/// <summary>
	/// Set of "bad boy" extension methods for UIKit
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

        /// <summary>
        /// Helper method to instantiate a view controller the same storyboard id as its type
        /// </summary>
        public static TController InstantiateViewController<TController>(this UIStoryboard storyboard)
            where TController : UIViewController
        {
            return storyboard.InstantiateViewController(typeof(TController).Name) as TController;
        }
        
        /// <summary>
        /// Returns true if the controller's view is currently on screen
        /// </summary>
        public static bool IsOnScreen(this UIViewController controller)
        {
            return controller.IsViewLoaded && controller.View.Window != null;
        }

        /// <summary>
        /// Creates a 1x1 image that is a certain color
        /// </summary>
        public static UIImage ToImage(this UIColor color)
        {
            return color.ToImage(new SizeF(1, 1));
        }
        
        /// <summary>
        /// Creates an image of specified size that is a certain color
        /// </summary>
        public static UIImage ToImage(this UIColor color, SizeF size)
        {
            try
            {
                UIGraphics.BeginImageContext(size);
                
                using (var context = UIGraphics.GetCurrentContext())
                {
                    context.SetFillColor(color.CGColor);
                    context.FillRect(new RectangleF(PointF.Empty, size));
                    
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

