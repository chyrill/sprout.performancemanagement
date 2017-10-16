using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace al.performancemanagement.App
{
    public class WebApiDelegatingHandler: DelagateHandlerBase
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
           
                string method = request.Method.Method;
                string requestBody = await request.Content.ReadAsStringAsync();

                requestBody = UpdateRequestBody(requestBody);

                //let other handlers process the request
                return await base.SendAsync(request, cancellationToken)
                    .ContinueWith(task =>
                    {
                        

                        return task.Result;
                    });
            
        }
    }
    public class DelagateHandlerBase : DelegatingHandler
    {
        protected string UpdateRequestBody(string requestBody)
        {
            return requestBody

            .UpdateJsonValue("Password", (val) =>
            {
                if (!string.IsNullOrEmpty(val) && val.Length > 1)
                {
                    val = val.Substring(val.Length - 1, 0).PadLeft(val.Length, '*');
                }
                return val;
            });
        }

        protected string UpdateResponseBody(string responseBody)
        {
            return responseBody;
        }
    }

    // Promote to Framework
    public static class Extensions
    {
        /// <summary>
        /// Update JSon string value. 
        /// Currently doesn't support complex type values for keys
        /// </summary>
        /// <param name="str"></param>
        /// <param name="key">Property to be updated</param>
        /// <param name="proc">Function to provide the update</param>
        /// <returns>updated json string</returns>
        public static string UpdateJsonValue(this string str, string key, Func<string, string> proc)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                    return str;

                string[] complex = str.Split('{');
                if (complex != null && complex.Length > 1)
                {
                    for (int i = 0; i < complex.Length; i++)
                    {
                        complex[i] = complex[i].UpdateJsonValue(key, proc);
                    }

                    return string.Join("{", complex);
                }

                string[] sp = str.Split(',');

                for (int i = 0; i < sp.Length; i++)
                {
                    string[] item = sp[i].Split(':');
                    if (item != null && item.Length > 1 && item[0].Contains(key))
                    {
                        string val = item[1];
                        if (val.Contains('\"') || val.Contains('\''))
                        {
                            char quote = val.Contains('\"') ? '\"' : '\'';

                            int firstIndex = val.IndexOf(quote);
                            int lastIndex = val.LastIndexOf(quote);

                            if (lastIndex > firstIndex)
                            {
                                string oldVal = val.Substring(firstIndex + 1, lastIndex - 1).Trim();
                                string newVal = proc.Invoke(oldVal);
                                string newItem = item[1].Replace(oldVal, newVal);

                                sp[i] = sp[i].Replace(item[1], newItem);
                            }
                        }
                        else
                        {
                            string newVal = string.Empty;
                            if (val.Contains('}'))
                            {
                                int braceIndex = val.IndexOf('}');
                                if (braceIndex > 0)
                                    val = val.Substring(0, braceIndex - 1).Trim();

                                newVal = proc.Invoke(val);
                                newVal = item[1].Replace(val, newVal);
                            }
                            else
                            {
                                newVal = proc.Invoke(val.Trim());
                            }
                            sp[i] = sp[i].Replace(item[1], newVal);
                        }
                    }
                }

                str = string.Join(",", sp);
            }
            catch (Exception)
            {
            }

            return str;
        }
    }
}