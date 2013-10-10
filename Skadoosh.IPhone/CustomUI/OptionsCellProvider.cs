using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Skadoosh.Common.DomainModels;

namespace Skadoosh.IPhone
{
	public partial class OptionsCellProvider : UITableViewCell
	{
		public static readonly UINib Nib = UINib.FromName ("OptionsCellProvider", NSBundle.MainBundle);
		public static readonly NSString Key = new NSString ("OptionsCellProvider");

		public OptionsCellProvider (IntPtr handle) : base (handle)
		{
		}
		public OptionsCellProvider (string cellIdentifier):base(UITableViewCellStyle.Default, cellIdentifier)
		{

		}


		public static OptionsCellProvider Create ()
		{
			return (OptionsCellProvider)Nib.Instantiate (null, null) [0];
		}

		public void BindFields(Option opt){
			//this.ProviderName.Text=mp.FirstName + " " + mp.LastName;
			//this.ProviderAddress.Text = mp.Address;
			//this.ProviderCityState.Text = mp.City + ", " + mp.State + "     "+mp.ZipCode;
		}
	}
}

