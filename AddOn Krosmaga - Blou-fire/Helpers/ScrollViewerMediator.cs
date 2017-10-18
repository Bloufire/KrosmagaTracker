using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace AddOn_Krosmaga___Blou_fire.Helpers
{
	public class ScrollViewerMediator : FrameworkElement
	{

		/// <summary>
		/// ScrollViewer instance to forward Offset changes on to.
		/// </summary>
		public ScrollViewer ScrollViewer
		{
			get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
			set { SetValue(ScrollViewerProperty, value); }
		}
		public static readonly DependencyProperty ScrollViewerProperty =
			DependencyProperty.Register(
				"ScrollViewer",
				typeof(ScrollViewer),
				typeof(ScrollViewerMediator),
				new PropertyMetadata(OnScrollViewerChanged));
		private static void OnScrollViewerChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var mediator = (ScrollViewerMediator)o;
			var scrollViewer = (ScrollViewer)(e.NewValue);
			if (null != scrollViewer)
			{
				scrollViewer.ScrollToVerticalOffset(mediator.VerticalOffset);
			}
		}

		/// <summary>
		/// VerticalOffset property to forward to the ScrollViewer.
		/// </summary>
		public double VerticalOffset
		{
			get { return (double)GetValue(VerticalOffsetProperty); }
			set { SetValue(VerticalOffsetProperty, value); }
		}
		public static readonly DependencyProperty VerticalOffsetProperty =
			DependencyProperty.Register(
				"VerticalOffset",
				typeof(double),
				typeof(ScrollViewerMediator),
				new PropertyMetadata(OnVerticalOffsetChanged));
		public static void OnVerticalOffsetChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var mediator = (ScrollViewerMediator)o;
			if (null != mediator.ScrollViewer)
			{
				mediator.ScrollViewer.ScrollToVerticalOffset((double)(e.NewValue));
			}
		}

		/// <summary>
		/// Multiplier for ScrollableHeight property to forward to the ScrollViewer.
		/// </summary>
		/// <remarks>
		/// 0.0 means "scrolled to top"; 1.0 means "scrolled to bottom".
		/// </remarks>
		public double ScrollableHeightMultiplier
		{
			get { return (double)GetValue(ScrollableHeightMultiplierProperty); }
			set { SetValue(ScrollableHeightMultiplierProperty, value); }
		}
		public static readonly DependencyProperty ScrollableHeightMultiplierProperty =
			DependencyProperty.Register(
				"ScrollableHeightMultiplier",
				typeof(double),
				typeof(ScrollViewerMediator),
				new PropertyMetadata(OnScrollableHeightMultiplierChanged));
		public static void OnScrollableHeightMultiplierChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			var mediator = (ScrollViewerMediator)o;
			var scrollViewer = mediator.ScrollViewer;
			if (null != scrollViewer)
			{
				scrollViewer.ScrollToVerticalOffset((double)(e.NewValue) * scrollViewer.ScrollableHeight);
			}
		}

	}
}
