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
	[Register ("HelpController")]
	partial class HelpController
	{
		[Outlet]
		MonoTouch.UIKit.UIImageView headImg { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (headImg != null) {
				headImg.Dispose ();
				headImg = null;
			}
		}
	}
}
