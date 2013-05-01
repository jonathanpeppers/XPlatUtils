//  Messenger.cs
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
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ImageIO;

namespace XPlatUtils
{
	/// <summary>
	/// A class for loading *.gif files for use with UIImageView
	/// </summary>
	public class Gif : IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="XPlatUtils.iOS.Gif"/> class.
		/// </summary>
		/// <param name="path">Path to a gif file</param>
		public Gif(string path)
		{
			var images = new List<UIImage>();
			using (var url = NSUrl.FromFilename (path))
			{
				using (CGImageSource source = CGImageSource.FromUrl (url))
				{
					var options = new CGImageOptions { ShouldCache = false };
					
					using (var parameters = source.GetProperties (0, options).Dictionary)
					{
						using (var dictionary = parameters[new NSString("{GIF}")] as NSDictionary)
						{
							Framerate = ((NSNumber)dictionary[new NSString("DelayTime")]).FloatValue;
							
							for (int i = 0; i < source.ImageCount; i++) 
							{
								using (var cgImage = source.CreateImage (i, options))
								{
									var image = new UIImage(cgImage);
									
									images.Add (image);
								}
							}
						}
					}
				}
			}
			Images = images.ToArray();
		}

		/// <summary>
		/// An array of images for passing to UIImageView
		/// </summary>
		public UIImage[] Images
		{
			get;
			private set;
		}

		/// <summary>
		/// The framerate of the gif
		/// </summary>
		public float Framerate
		{
			get;
			private set;
		}

		/// <summary>
		/// The animation duration for UIImageView, does some math with Framerate
		/// </summary>
		public float AnimationDuration
		{
			get { return Images == null ? 0 : Images.Length * Framerate; }
		}

		/// <summary>
		/// Applies this gif to a UIImageView, just a helper method
		/// </summary>
		public void ApplyToView(UIImageView imageView)
		{
			imageView.AnimationDuration = AnimationDuration;
			imageView.AnimationImages = Images;
			imageView.StartAnimating();
		}

		/// <summary>
		/// Releases all resource used by the <see cref="XPlatUtils.iOS.Gif"/> object.
		/// </summary>
		public void Dispose ()
		{
			if (Images != null)
			{
				foreach (var image in Images) 
				{
					image.Dispose ();
				}
				Images = null;
			}
		}
	}
}

