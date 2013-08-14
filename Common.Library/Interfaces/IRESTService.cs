using System;
namespace Common.Library.Interfaces
{
    interface IRESTService
    {
        void Get<T>(string url, Action<T> successAction, Action<System.Net.WebException> errorAction);
        System.Threading.Tasks.Task<T> GetAsync<T>(string url);
        void Post<T>(string url, object obj, Action<T> successAction, Action<System.Net.WebException> errorAction);
    }
}
