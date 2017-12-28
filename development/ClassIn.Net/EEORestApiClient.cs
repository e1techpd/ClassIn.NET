using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Beyova;
using static Beyova.HttpConstants;

namespace EF.E1Technology.EEO
{
    /// <summary>
    /// Class EEORestApiClient
    /// </summary>
    public partial class EEORestApiClient
    {
        #region Constants

        /// <summary>
        /// The eeo base URL
        /// </summary>
        private const string eeoBaseUrl = "https://www.eeo.cn";

        /// <summary>
        /// The API URL format
        /// </summary>
        private const string apiUrlFormat = "{0}/partner/api/{1}.api.php?action={2}";

        #endregion

        #region Properties

        /// <summary>
        /// Gets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public string OrganizationId { get; private set; }

        /// <summary>
        /// Gets or sets the secret key.
        /// </summary>
        /// <value>
        /// The secret key.
        /// </value>
        public string SecretKey { get; private set; }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EEORestApiClient"/> class.
        /// </summary>
        /// <param name="secretKey">The secret key.</param>
        public EEORestApiClient(string organizationId, string secretKey)
        {
            organizationId.CheckEmptyString(nameof(organizationId));
            secretKey.CheckEmptyString(nameof(secretKey));

            this.SecretKey = secretKey;
            this.OrganizationId = organizationId;
        }

        /// <summary>
        /// Invokes the specified module name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="httpMethod">The HTTP method.</param>
        /// <param name="data">The data.</param>
        /// <param name="utcDateTime">The UTC date time.</param>
        /// <returns></returns>
        private T Invoke<T>(string moduleName, string actionName, string httpMethod, Dictionary<string, string> data, DateTime? utcDateTime = null)
        {
            // Initialize http request.
            var httpRequest = GetApiUrl(moduleName, actionName).CreateHttpWebRequest(httpMethod);

            // Force to append basic info
            var timeStamp = ((utcDateTime ?? DateTime.UtcNow).ToUnixMillisecondsDateTime() / 1000).ToString();

            data.Merge("SID", this.OrganizationId, true);
            data.Merge("safeKey", string.Concat(SecretKey, timeStamp).ToMD5String(Encoding.UTF8).ToLowerInvariant(), true);
            data.Merge("timeStamp", timeStamp, true);

            // Fill body using key-value dictionary.
            httpRequest.FillData(httpMethod, data, Encoding.UTF8);
            httpRequest.ContentType = HttpConstants.ContentType.FormSubmit;

            HttpStatusCode httpStatusCode;
            Exception exception;
            var responseObject = httpRequest.ReadResponseAsObject<EEOResponse<T>>(out httpStatusCode, out exception);

            if (exception != null)
            {
                throw exception.Handle(new { moduleName, actionName, httpMethod, data, utcDateTime });
            }
            else if (responseObject.ErrorInfo.ErrorNumber != 1)
            {
                throw new Exception(responseObject.ErrorInfo.ErrorMessage);
                //exception = new Exception(responseObject.ErrorInfo.ErrorMessage);
                //throw exception.Handle(new { moduleName, actionName, httpMethod, data, utcDateTime, error = responseObject.ErrorInfo });
                //throw exception;
            }

            return responseObject.Data;
        }

        #region Private util

        /// <summary>
        /// Gets the relative URL.
        /// </summary>
        /// <param name="moduleName">Name of the module.</param>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        private string GetApiUrl(string moduleName, string action)
        {
            moduleName.CheckEmptyString(nameof(moduleName));
            action.CheckEmptyString(nameof(action));

            return string.Format(apiUrlFormat, eeoBaseUrl, moduleName, action);
        }

        /// <summary>
        /// Generates the safe key.
        /// </summary>
        /// <param name="utcDateTime">The UTC date time.</param>
        /// <returns></returns>
        public string GenerateSafeKey(DateTime? utcDateTime = null)
        {
            var timeStamp = (utcDateTime ?? DateTime.UtcNow).ToUnixMillisecondsDateTime() / 1000;
            return string.Concat(SecretKey, timeStamp).ToMD5String(Encoding.UTF8);
        }

        #endregion
    }
}
