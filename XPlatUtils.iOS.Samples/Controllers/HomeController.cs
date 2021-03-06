using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace XPlatUtils.iOS.Samples
{
	public partial class HomeController : UIViewController
	{
		readonly SampleViewModel viewModel;

		public HomeController (IntPtr handle) : base (handle)
		{
			viewModel = ServiceContainer.Resolve<SampleViewModel>();

			Title = "Samples";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//One is for UITableViewDataSource, one for UITableViewDelegate
			tableView.WeakDataSource =
				tableView.WeakDelegate = this;
		}

		[Export("tableView:numberOfRowsInSection:")]
		public int RowsInSection (UITableView tableview, int section)
		{
			return viewModel.Samples.Count;
		}

		[Export("tableView:cellForRowAtIndexPath:")]
		public UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var sample = viewModel.Samples [indexPath.Row];
			var cell = tableView.DequeueReusableCell ("SampleCell");
			cell.TextLabel.Text = sample.Name;
			cell.TextLabel.Text = sample.Color;
			return cell;
		}

		[Export("tableView:didSelectRowAtIndexPath:")]
		public void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			Console.WriteLine ("Row clicked at: " + indexPath.Description);
		}
	}
}
