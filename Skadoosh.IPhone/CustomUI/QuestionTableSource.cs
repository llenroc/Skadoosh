using System;
using MonoTouch.UIKit;
using Skadoosh.Common.DomainModels;
using MonoTouch.Foundation;

namespace Skadoosh.IPhone
{
	
	public class QuestionTableSource : UITableViewSource
	{
		public Question CurrentQuestion{ get; set;}


		//protected MedicalProvider[] tableItems;
		protected string cellIdentifier = "CustomProvider";


		public override int RowsInSection (UITableView tableview, int section)
		{
			return CurrentQuestion.Options.Count;
		}

		public override UITableViewCell  GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			// request a recycled cell to save memory

			var cell = tableView.DequeueReusableCell (cellIdentifier) as OptionsCellProvider;

			if (cell == null) {
				cell = new OptionsCellProvider (cellIdentifier);
			}
			Option obj = CurrentQuestion.Options [indexPath.Row];
			cell.BindFields (obj);
			//cell.lbl.Text = tableItems[indexPath.Row].FirstName;
			return cell;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var selectedOption= CurrentQuestion.Options [indexPath.Row];


		}

	}
}

