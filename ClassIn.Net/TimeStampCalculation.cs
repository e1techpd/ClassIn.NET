using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassIn.Net
{
    public class TimeStampCalculation
    {
        /// <summary>
        /// calculation the timestamp
        /// </summary>
        /// <param name="utcTime"></param>
        /// <returns></returns>
        public static string ConvertUtcTimeToDecUnixStamp(DateTime utcTime)
        {
            DateTime utcStart = new DateTime(1970, 1, 1);
            long unixStamp = (long)(utcTime - utcStart).TotalSeconds;
            string hexUnixStamp = $"{unixStamp:D}".ToLower();

            return hexUnixStamp;
        }
    }
}
