

using System;
using System.IO;
using Newtonsoft.Json;
#if NETFX_CORE
using Windows.Storage;
using System.Threading.Tasks;
#else
using System.IO.IsolatedStorage;
using System.Threading.Tasks;
#endif

namespace Statera.Xamarin.Common
{
    public class LocalStorageService 
    {
        #if NETFX_CORE
        public async Task<T> GetIsolatedStorage<T>(string contentName) where T : new()
        {

            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(contentName + ".dat", CreationCollisionOption.OpenIfExists);
            var content = await FileIO.ReadTextAsync(file);
            return JsonConvert.DeserializeObject<T>(content);
        }

        public async void SaveIsolatedStorage<T>(string contentName, object obj)
        {
            var data = JsonConvert.SerializeObject(obj);
            var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(contentName + ".dat", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, data);
        }
        #else
        public async Task<T> GetIsolatedStorage<T>(string contentName) where T : new()
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
        public async void SaveIsolatedStorage<T>(string contentName, object obj)
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
        #endif

    }
}
