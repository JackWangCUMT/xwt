// 
// IWidgetBackend.cs
//  
// Author:
//       Lluis Sanchez <lluis@xamarin.com>
// 
// Copyright (c) 2011 Xamarin Inc
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;

namespace Xwt.Backends
{
	public interface IWidgetBackend: IBackend, IDisposable
	{
		void Initialize (IWidgetEventSink eventSink);
		
		bool Visible { get; set; }
		bool Sensitive { get; set; }
		bool CanGetFocus { get; set; }
		bool HasFocus { get; }
		Size Size { get; }
		Point ConvertToScreenCoordinates (Point widgetCoordinates);
		
		void SetFocus ();
		
		void UpdateLayout ();
		WidgetSize GetPreferredWidth ();
		WidgetSize GetPreferredHeightForWidth (double width);
		WidgetSize GetPreferredHeight ();
		WidgetSize GetPreferredWidthForHeight (double height);
		
		object NativeWidget { get; }
		
		void DragStart (TransferDataSource data, DragDropAction allowedDragActions, object imageBackend, double hotX, double hotY);
		void SetDragSource (string[] types, DragDropAction dragAction);
		void SetDragTarget (string[] types, DragDropAction dragAction);
	}
	
	public interface IWidgetEventSink
	{
		void OnDragOverCheck (DragOverCheckEventArgs args);
		void OnDragOver (DragOverEventArgs args);
		void OnDragDropCheck (DragCheckEventArgs args);
		void OnDragDrop (DragEventArgs args);
		void OnDragLeave (EventArgs args);
		void OnDragFinished (DragFinishedEventArgs args);
		
		/// <summary>
		/// Notifies the frontend that the preferred size of this widget has changed
		/// </summary>
		/// <remarks>
		/// This method must be called when the widget changes its preferred size.
		/// This method doesn't need to be called if the resize is the result of changing
		/// a widget property. For example, it is not necessary to call it when the text
		/// of a label is changed (the fronted will automatically rise this event when
		/// the property changes). However, it has to be called when the the shape of the
		/// widget changes on its own, for example if the size of a button changes as
		/// a result of clicking on it.
		/// </remarks>
		void OnPreferredSizeChanged ();
	}
	
	[Flags]
	public enum WidgetEvent
	{
		DragOverCheck = 1,
		DragOver = 2,
		DragDropCheck = 4,
		DragDrop = 8,
		DragLeave = 16
	}
	
	public interface DragOperationEventSink
	{
		
	}
	
	public interface ITransferDataSource
	{
		string[] Types { get; }
	}
}
