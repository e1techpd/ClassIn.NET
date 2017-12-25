using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIn.Net
{
    public class EEORestClient:RestClient
    {
        public static string EEOApiUrl
        {
            get
            {
                return "http://www.eeo.cn";
            }
        }

        public EEORestClient()
        {
            this.BaseUrl = new Uri(EEORestClient.EEOApiUrl);
            //add header info for EEO client
            this.AddDefaultHeader("Host", "www.eeo.cn");
            this.AddDefaultHeader("Content-Type", "application/x-www-form-urlencoded");
            this.AddDefaultHeader("Cache-Control", "no-cache");
        }
    }
}
