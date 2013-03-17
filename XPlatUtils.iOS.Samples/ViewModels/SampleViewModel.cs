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
				new Sample { Name = "UITableView Sample", Segue = "Sample1", ClassName = "UITableViewSource" },
			};
		}

		public IList<Sample> Samples {
			get;
			private set; 
		}
	}
}

