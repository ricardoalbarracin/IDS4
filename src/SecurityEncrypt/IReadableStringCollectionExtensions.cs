using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace SecurityEncrypt
{
    public static class IReadableStringCollectionExtensions
    {
        public static NameValueCollection AsNameValueCollection(this IDictionary<string, StringValues> collection)
        {
            NameValueCollection values = new NameValueCollection();
            foreach (KeyValuePair<string, StringValues> pair in collection)
            {
                string introduced3 = pair.Key;
                values.Add(introduced3, Enumerable.First<string>(pair.Value));
            }
            return values;
        }

        public static NameValueCollection AsNameValueCollection(this IEnumerable<KeyValuePair<string, StringValues>> collection)
        {
            NameValueCollection values = new NameValueCollection();
            foreach (KeyValuePair<string, StringValues> pair in collection)
            {
                string introduced3 = pair.Key;
                values.Add(introduced3, Enumerable.First<string>(pair.Value));
            }
            return values;
        }
    }
   
}

