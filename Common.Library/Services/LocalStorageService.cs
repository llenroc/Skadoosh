using Common.Library.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace Statera.Xamarin.Common
{
    public class LocalStorageService : ILocalStorageService
    {
        public T GetIsolatedStorage<T>(string contentName) where T:new()
        {
            using (var isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (isoStorage.FileExists(contentName))
                {
                    var s = isoStorage.OpenFile(contentName, FileMode.OpenOrCreate);
                    var sr = new StreamReader(s);
                    var content = sr.ReadToEnd();
                    sr.Close();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                else
                {
                    return new T();
                }
            }
        }
        public void SaveIsolatedStorage<T>(string contentName, object obj)
        {
            using (var isoStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var data = JsonConvert.SerializeObject(obj);
                var s = isoStorage.OpenFile(contentName, FileMode.Create);
                var sw = new StreamWriter(s);
                sw.Write(data);
                sw.Flush();
                sw.Close();
            }
        }
    }
}
