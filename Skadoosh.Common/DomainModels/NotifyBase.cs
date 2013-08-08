using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Skadoosh.Common.DomainModels
{
    public class Global
    {
        private static readonly string azureKey = "cWjMTkNykoYJWqzkUeYFWjOcgLdwUs85";
        private static readonly string azureUrl = "https://skadoosh.azure-mobile.net/";
        public static MobileServiceClient MobileService = new MobileServiceClient(azureUrl, azureKey);
    }


    public abstract class NotifyBase : INotifyPropertyChanged
    {

        public MobileServiceClient AzureClient
        {
            get
            {
                return Global.MobileService;
            }
            set
            {
                Global.MobileService = value;
            }
        }


        public async Task<bool> ProfileExists()
        {
            var list = AzureClient.GetTable<AccountUser>().Where(x => x.UserId == AzureClient.CurrentUser.UserId).ToCollectionAsync();
            var collection = await list;
            if (collection != null && collection.FirstOrDefault()!=null)
            {
                return true;
            }
            else{
                return false;
            }
        }



        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
