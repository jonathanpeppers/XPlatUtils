using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace XPlatUtils.iOS
{
	/// <summary>
	/// An extension on UIViewController including most strongly typed overrides for use with WeakDelegate on iOS
	/// * Including: UITableViewSource, UIScrollViewDelegate, UITextFieldDelegate, UITextViewDelegate
	/// </summary>
	public class UIViewControllerEx : UIViewController
	{
		public UIViewControllerEx ()
		{
		}

		public UIViewControllerEx (string nibName, NSBundle bundle)
        	: base(nibName, bundle)
		{
            
		}
        
        #region UITableViewSource
        
		[Export("tableView:accessoryButtonTappedForRowWithIndexPath:")]
		public virtual void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:accessoryTypeForRowWithIndexPath:")]
		public virtual UITableViewCellAccessory AccessoryForRow (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:canEditRowAtIndexPath:")]
		public virtual bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:canMoveRowAtIndexPath:")]
		public virtual bool CanMoveRow (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:canPerformAction:forRowAtIndexPath:withSender:")]
		public virtual bool CanPerformAction (UITableView tableView, Selector action, NSIndexPath indexPath, NSObject sender)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:didEndDisplayingCell:forRowAtIndexPath:")]
		public virtual void CellDisplayingEnded (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:commitEditingStyle:forRowAtIndexPath:")]
		public virtual void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:targetIndexPathForMoveFromRowAtIndexPath:toProposedIndexPath:")]
		public virtual NSIndexPath CustomizeMoveTarget (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath proposedIndexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:didEndEditingRowAtIndexPath:")]
		public virtual void DidEndEditing (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:editingStyleForRowAtIndexPath:")]
		public virtual UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:didEndDisplayingFooterView:forSection:")]
		public virtual void FooterViewDisplayingEnded (UITableView tableView, UIView footerView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:cellForRowAtIndexPath:")]
        public virtual UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            throw new You_Should_Not_Call_base_In_This_Method ();   
		}
        
		[Export("tableView:heightForFooterInSection:")]
		public virtual float GetHeightForFooter (UITableView tableView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:heightForHeaderInSection:")]
		public virtual float GetHeightForHeader (UITableView tableView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:heightForRowAtIndexPath:")]
		public virtual float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:viewForFooterInSection:")]
		public virtual UIView GetViewForFooter (UITableView tableView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:viewForHeaderInSection:")]
		public virtual UIView GetViewForHeader (UITableView tableView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:didEndDisplayingHeaderView:forSection:")]
		public virtual void HeaderViewDisplayingEnded (UITableView tableView, UIView headerView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:indentationLevelForRowAtIndexPath:")]
		public virtual int IndentationLevel (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:moveRowAtIndexPath:toIndexPath:")]
		public virtual void MoveRow (UITableView tableView, NSIndexPath sourceIndexPath, NSIndexPath destinationIndexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("numberOfSectionsInTableView:")]
		public virtual int NumberOfSections (UITableView tableView)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:performAction:forRowAtIndexPath:withSender:")]
		public virtual void PerformAction (UITableView tableView, Selector action, NSIndexPath indexPath, NSObject sender)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:didDeselectRowAtIndexPath:")]
		public virtual void RowDeselected (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:didHighlightRowAtIndexPath:")]
		public virtual void RowHighlighted (UITableView tableView, NSIndexPath rowIndexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:didSelectRowAtIndexPath:")]
		public virtual void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:numberOfRowsInSection:")]
		public virtual int RowsInSection (UITableView tableview, int section)
        {
            throw new You_Should_Not_Call_base_In_This_Method ();   
		}
        
		[Export("tableView:didUnhighlightRowAtIndexPath:")]
		public virtual void RowUnhighlighted (UITableView tableView, NSIndexPath rowIndexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:sectionForSectionIndexTitle:atIndex:")]
		public virtual int SectionFor (UITableView tableView, string title, int atIndex)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("sectionIndexTitlesForTableView:")]
		public virtual string[] SectionIndexTitles (UITableView tableView)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:shouldHighlightRowAtIndexPath:")]
		public virtual bool ShouldHighlightRow (UITableView tableView, NSIndexPath rowIndexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:shouldIndentWhileEditingRowAtIndexPath:")]
		public virtual bool ShouldIndentWhileEditing (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:shouldShowMenuForRowAtIndexPath:")]
		public virtual bool ShouldShowMenu (UITableView tableView, NSIndexPath rowAtindexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:titleForDeleteConfirmationButtonForRowAtIndexPath:")]
		public virtual string TitleForDeleteConfirmation (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:titleForFooterInSection:")]
		public virtual string TitleForFooter (UITableView tableView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:titleForHeaderInSection:")]
		public virtual string TitleForHeader (UITableView tableView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:willBeginEditingRowAtIndexPath:")]
		public virtual void WillBeginEditing (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:willDeselectRowAtIndexPath:")]
		public virtual void WillDeselectRow (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:willDisplayCell:forRowAtIndexPath:")]
		public virtual void WillDisplay (UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:willDisplayFooterView:forSection:")]
		public virtual void WillDisplayFooterView (UITableView tableView, UIView footerView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:willDisplayHeaderView:forSection:")]
		public virtual void WillDisplayHeaderView (UITableView tableView, UIView headerView, int section)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
		[Export("tableView:willSelectRowAtIndexPath:")]
		public virtual NSIndexPath WillSelectRow (UITableView tableView, NSIndexPath indexPath)
		{
			throw new You_Should_Not_Call_base_In_This_Method ();
		}
        
        #endregion
        
        #region UIScrollViewDelegate
        
        [Export("scrollViewDidEndDecelerating:")]
        public virtual void DecelerationEnded(UIScrollView scrollView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewWillBeginDecelerating:")]
        public virtual void DecelerationStarted(UIScrollView scrollView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewDidZoom:")]
        public virtual void DidZoom(UIScrollView scrollView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewDidEndDragging:willDecelerate:")]
        public virtual void DraggingEnded(UIScrollView scrollView, bool willDecelerate)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewWillBeginDragging:")]
        public virtual void DraggingStarted(UIScrollView scrollView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewDidEndScrollingAnimation:")]
        public virtual void ScrollAnimationEnded(UIScrollView scrollView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewDidScroll:")]
        public virtual void Scrolled(UIScrollView scrollView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewDidScrollToTop:")]
        public virtual void ScrolledToTop(UIScrollView scrollView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewShouldScrollToTop:")]
        public virtual bool ShouldScrollToTop(UIScrollView scrollView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("viewForZoomingInScrollView:")]
        public virtual UIView ViewForZoomingInScrollView(UIScrollView scrollView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewWillEndDragging:withVelocity:targetContentOffset:")]
        public virtual void WillEndDragging(UIScrollView scrollView, PointF velocity, ref PointF targetContentOffset)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewDidEndZooming:withView:atScale:")]
        public virtual void ZoomingEnded(UIScrollView scrollView, UIView withView, float atScale)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("scrollViewWillBeginZooming:withView:")]
        public virtual void ZoomingStarted(UIScrollView scrollView, UIView view)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        #endregion
        
        #region UITextFieldDelegate
        
        [Export("textFieldDidEndEditing:")]
        public virtual void EditingEnded(UITextField textField)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textFieldDidBeginEditing:")]
        public virtual void EditingStarted(UITextField textField)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textFieldShouldBeginEditing:")]
        public virtual bool ShouldBeginEditing(UITextField textField)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textField:shouldChangeCharactersInRange:replacementString:")]
        public virtual bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textFieldShouldClear:")]
        public virtual bool ShouldClear(UITextField textField)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textFieldShouldEndEditing:")]
        public virtual bool ShouldEndEditing(UITextField textField)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textFieldShouldReturn:")]
        public virtual bool ShouldReturn(UITextField textField)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        #endregion
        
        #region UITextViewDelegate
        
        [Export("textViewDidChange:")]
        public virtual void Changed(UITextView textView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textViewDidEndEditing:")]
        public virtual void EditingEnded(UITextView textView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textViewDidBeginEditing:")]
        public virtual void EditingStarted(UITextView textView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textViewDidChangeSelection:")]
        public virtual void SelectionChanged(UITextView textView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textViewShouldBeginEditing:")]
        public virtual bool ShouldBeginEditing(UITextView textView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textView:shouldChangeTextInRange:replacementText:")]
        public virtual bool ShouldChangeText(UITextView textView, NSRange range, string text)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("textViewShouldEndEditing:")]
        public virtual bool ShouldEndEditing(UITextView textView)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        #endregion
        
        #region UISearchBarDelegate
        
        [Export("searchBarBookmarkButtonClicked:")]
        public virtual void BookmarkButtonClicked(UISearchBar searchBar)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBarCancelButtonClicked:")]
        public virtual void CancelButtonClicked(UISearchBar searchBar)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBarResultsListButtonClicked:")]
        public virtual void ListButtonClicked(UISearchBar searchBar)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBarTextDidBeginEditing:")]
        public virtual void OnEditingStarted(UISearchBar searchBar)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBarTextDidEndEditing:")]
        public virtual void OnEditingStopped(UISearchBar searchBar)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBarSearchButtonClicked:")]
        public virtual void SearchButtonClicked(UISearchBar searchBar)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBar:selectedScopeButtonIndexDidChange:")]
        public virtual void SelectedScopeButtonIndexChanged(UISearchBar searchBar, int selectedScope)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBarShouldBeginEditing:")]
        public virtual bool ShouldBeginEditing(UISearchBar searchBar)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBar:shouldChangeTextInRange:replacementText:")]
        public virtual bool ShouldChangeTextInRange(UISearchBar searchBar, NSRange range, string text)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBarShouldEndEditing:")]
        public virtual bool ShouldEndEditing(UISearchBar searchBar)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        [Export("searchBar:textDidChange:")]
        public virtual void TextChanged(UISearchBar searchBar, string searchText)
        {
            throw new You_Should_Not_Call_base_In_This_Method();
        }
        
        #endregion
	}
}
