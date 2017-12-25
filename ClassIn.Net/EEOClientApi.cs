using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIn.Net
{
    public class EEOClientApi
    {

        /// <summary>
        /// Generate safeKey by secret,
        /// current in EEO, timestamp range is from now to now+20 minutes
        /// </summary>
        /// <param name="secret">only secret is enough,we will use current time as timeStamp</param>
        /// <returns></returns>
        public static string GenerateSafeKey(string secret)
        {
            var timeStamp = TimeStampCalculation.ConvertUtcTimeToDecUnixStamp(DateTime.UtcNow);
            return EEOMD5.MD5Encode(secret + timeStamp);
        }

        /// <summary>
        /// call the EEO Api
        /// </summary>
        /// <param name="SID">your Sid</param>
        /// <param name="secret">your scret</param>
        /// <param name="action">method you will call</param>
        /// <param name="methodType">method type,more detail see Method enum</param>
        /// <param name="criteria">query parameters</param>
        /// <returns></returns>
        public static string CallEEOApi(string SID,string secret,string action,string methodType, Dictionary<string,string> criteria)
        {
            //transfer string methodType to Method Enum
            var defaultMethod = Method.GET;
            Enum.TryParse<Method>(methodType,out defaultMethod);

            //build the parameters
            var timeStamp = TimeStampCalculation.ConvertUtcTimeToDecUnixStamp(DateTime.UtcNow);
            var safeKey = EEOMD5.MD5Encode(secret + timeStamp);
            var resource = string.Format("/partner/api/course.api.php?action={0}", action);

            //setup the reqest parameter
            var request = new RestRequest(resource, defaultMethod)
            {
                RootElement = "data"
            };

            var bodyString = string.Format("SID={0}&safeKey={1}&timeStamp={2}", SID, safeKey, timeStamp);
            foreach(KeyValuePair<string,string> pair in criteria)
            {
                bodyString += string.Format("&{0}={1}", pair.Key, string.IsNullOrEmpty(pair.Value) ? string.Empty : pair.Value);
            }

            request.AddParameter("application/x-www-form-urlencoded", bodyString, ParameterType.RequestBody);

            var EEOClient = new EEORestClient();
            var response = EEOClient.Execute<dynamic>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //log the api which called
                Debug.WriteLine(resource);
                //for SDK,we still need to return json string rather than ob
                //return response.Data;
                return request.JsonSerializer.Serialize(response.Data);
            }
            else
            {
                Debug.Write("Error: " + response.ResponseStatus + response.Content);
                throw response.ErrorException;
            }

        }
    }
}
