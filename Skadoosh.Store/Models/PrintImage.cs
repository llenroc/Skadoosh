using Skadoosh.Common.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace Skadoosh.Store.Models
{
    public class PrintImage: NotifyBase
    {
        private WriteableBitmap responses;

        public WriteableBitmap Responses
        {
            get { return responses; }
            set { responses = value; }
        }
        
    }
}
