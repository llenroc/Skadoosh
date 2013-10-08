// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace tester
{
	[Register ("testerViewController")]
	partial class testerViewController
	{
		[Outlet]
		MonoTouch.UIKit.UILabel lblTest { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (lblTest != null) {
				lblTest.Dispose ();
				lblTest = null;
			}
		}
	}
}
