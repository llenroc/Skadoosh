// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Skadoosh.IPhone
{
	[Register ("HomeController")]
	partial class HomeController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnLive { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnStatic { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnLive != null) {
				btnLive.Dispose ();
				btnLive = null;
			}

			if (btnStatic != null) {
				btnStatic.Dispose ();
				btnStatic = null;
			}
		}
	}
}
