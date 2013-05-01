using System;
using System.Collections.Generic;

namespace XPlatUtils.iOS.Samples
{
	public class SampleViewModel
	{
		public SampleViewModel ()
		{
			Samples = new List<Sample>
			{
				new Sample { Name = "Gif Sample", Color = "Red" },
				new Sample { Name = "Banana", Color = "Yellow" },
				new Sample { Name = "Carrot", Color = "Orange" },
				new Sample { Name = "Cucumber", Color = "Green" },
			};
		}

		public IList<Sample> Samples 
		{
			get;
			private set; 
		}
	}
}

