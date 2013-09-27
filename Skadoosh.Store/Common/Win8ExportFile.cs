using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using System.Reflection;


namespace Skadoosh.Store.Common
{
    public class Win8ExportFile
    {

        public static async void SaveCSVFile<T>(List<T> collection, string path)
        {
            var storageFolder = KnownFolders.DocumentsLibrary;
            var file = await storageFolder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
          
            //var converter = new CSVExport<T>();
            //await FileIO.WriteTextAsync(file, Export());
        }
    }

}
