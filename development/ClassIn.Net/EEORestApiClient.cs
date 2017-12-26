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
    public class EEORestApiClient
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

        #region Public methods

        /// <summary>
        /// Gets the user course list.
        /// </summary>
        /// <param name="userAccount">The user account.</param>
        /// <param name="beginTime">The begin time.</param>
        /// <param name="endTime">The end time.</param>
        /// <returns></returns>
        public List<CourseInfo> GetUserCourseList(string userAccount, DateTime? beginTime = null, DateTime? endTime = null)
        {
            try
            {
                userAccount.CheckEmptyString(nameof(userAccount));

                var data = new Dictionary<string, string>();

                data.Add("userAccount", userAccount);
                data.AddIfNotNullOrEmpty("beginTime", (beginTime.ToUnixMillisecondsDateTime() / 1000).SafeToString());
                data.AddIfNotNullOrEmpty("endTime", (endTime.ToUnixMillisecondsDateTime() / 1000).SafeToString());

                return Invoke<List<CourseInfo>>(ModuleNames.Course, "getUserCourseList", HttpMethod.Post, data);
            }
            catch (Exception ex)
            {
                throw ex.Handle(new { userAccount, beginTime, endTime });
            }
        }

        /// <summary>
        /// get student list
        /// </summary>
        /// <param name="page">current page, default equal to 1</param>
        /// <param name="perpage">current perpage,default equal to 20</param>
        /// <returns></returns>
        public List<StudentInfo> GetStudentList(int? page = 1,int? perpage = 20)
        {
            try
            {
                var data = new Dictionary<string, string>();
                data.AddIfNotNullOrEmpty("page", page.SafeToString());
                data.AddIfNotNullOrEmpty("perpage", perpage.SafeToString());

                return Invoke<List<StudentInfo>>(ModuleNames.Course, "getStudentList", HttpMethod.Post, data);
            }
            catch(Exception ex)
            {
                throw ex.Handle(new { page, perpage });
            }
        }

        #endregion

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
                throw exception.Handle(new { moduleName, actionName, httpMethod, data, utcDateTime, error = responseObject.ErrorInfo });
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
