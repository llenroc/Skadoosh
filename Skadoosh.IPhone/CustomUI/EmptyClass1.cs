using System;
using MonoTouch.UIKit;
using Skadoosh.Common.DomainModels;

namespace Skadoosh.IPhone
{
	public partial class CustomProviderCell : UITableViewCell
	{
		public CustomProviderCell (IntPtr handle) : base (handle)
		{
		}

		public CustomProviderCell (string cellIdentifier):base(UITableViewCellStyle.Default, cellIdentifier)
		{

		}

		public void BindFields(Question q){

		}
	}
}

