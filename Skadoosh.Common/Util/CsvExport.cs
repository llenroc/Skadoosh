using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime;
using System.IO;
#if NETFX_CORE
using System.Reflection;
#endif
namespace Skadoosh.Common.Util
{
    public class CsvExport<T> where T : class
    {
        private const string ListSeparator = ";";

        public IList<T> Objects;

        public CsvExport(IList<T> objects)
        {
            Objects = objects;
        }

        public string Export()
        {
            return Export(true);
        }

#if NETFX_CORE
        public string Export(bool includeHeaderLine)
        {

            var sb = new StringBuilder();

            //Get properties using reflection. 

            var propertyInfos = typeof(T).GetTypeInfo();

            if (includeHeaderLine)
            {
                //add header line. 
                foreach (var propertyInfo in propertyInfos.DeclaredProperties)
                {
                    sb.Append(propertyInfo.Name).Append(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
                }
                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            //add value for each property. 
            foreach (T obj in Objects)
            {
                foreach (var propertyInfo in propertyInfos.DeclaredProperties)
                {
                    sb.Append(MakeValueCsvFriendly(propertyInfo.GetValue(obj, null))).Append(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
                }

                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            return sb.ToString();
        }
#else
        public string Export(bool includeHeaderLine)
        {

            var sb = new StringBuilder();

            //Get properties using reflection.

            var properties = typeof(T).GetProperties();

            if (includeHeaderLine)
            {
                //add header line. 
                foreach (var prop in properties)
                {
                    sb.Append(prop.Name).Append(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
                }
                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            //add value for each property. 
            foreach (T obj in Objects)
            {
                foreach (var prop in properties)
                {
                    sb.Append(MakeValueCsvFriendly(prop.GetValue(obj, null))).Append(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
                }

                sb.Remove(sb.Length - 1, 1).AppendLine();
            }

            return sb.ToString();
        }
#endif

        

        public string ExportToString()
        {
            return Export();
        }

        public MemoryStream ExportToMemoryStream()
        {
            return new MemoryStream(ExportToBytes());
        }

        //export as binary data. 
        public byte[] ExportToBytes()
        {
            return Encoding.UTF8.GetBytes(Export());
        }

        //get the csv value for field. 
        private string MakeValueCsvFriendly(object value)
        {
            if (value == null) return "";

            if (value is DateTime)
            {
                if (((DateTime)value).TimeOfDay.TotalSeconds == 0)
                    return ((DateTime)value).ToString("yyyy-MM-dd");
                return ((DateTime)value).ToString("yyyy-MM-dd HH:mm:ss");
            }
            string output = value.ToString();

            if (output.Contains(",") || output.Contains("\""))
                output = '"' + output.Replace("\"", "\"\"") + '"';

            return output;

        }
    } 
}
