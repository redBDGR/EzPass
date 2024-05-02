using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using System.Windows;
using System.Reflection;
using System.Threading;

namespace EzPass
{
    static class Request
    {
        /// <summary>
        /// Asynchronous method to post data to the webserver
        /// </summary>
        /// <param name="url"></param>
        /// <param name="input"></param>
        /// <param name="expiry"></param>
        /// <param name="ip"></param>
        /// <returns></returns>

        public static async Task<string> PostAsync(string url, string input, int expiry, string ip = "0.0.0.0/0")
        {
            // Create dictionary to be converted to header parameters to feed to the website
            var headers = new Dictionary<string, string>
            {
                 { "note", input },                      // Message
                 { "expires", expiry.ToString() },       // Time until expiry (in days) (default = 7)
                 { "cidr", ip }                          // IP restriction
            };

            using (HttpClient client = new HttpClient())    // Disposable
            {
                // Encode the previously created dictionary into values usable for the header paramters for the post request (disposable)
                using (var content = new FormUrlEncodedContent(headers))
                {
                    // Set UserAgent type, so the website interface knows that this entry was submitted from the desktop app (includes app version)
                    client.DefaultRequestHeaders.Add("User-Agent", $"EzPass/{Assembly.GetExecutingAssembly().GetName().Version}");

                    try
                    {
                        using (var result = await client.PostAsync(url, content))     // await the async post request (wait for a response)
                        {
                            return await result.Content.ReadAsStringAsync();             // Response has been recieved... start and await reading the data (wait for end of data)
                        }
                    }

                    // Catch exception if something went wrong
                    catch (Exception e)
                    {
                        // Generic error handling. This needs to be improved
                        MessageBox.Show($"{e.Message}\n\n{e.InnerException}", e.Source);
                        return null;
                    }
                }
            }
        }
    }
}
