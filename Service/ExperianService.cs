using CreditOne.Batch.ExperianPWUpdater.Password;
using CreditOne.Batch.ExperianPWUpdater.Utils;
using CreditOne.CoreLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;


namespace CreditOne.Batch.ExperianPWUpdater.Service
{
    class ExperianService
    {
        private PasswordHelper _passwordHelper;
      
        private string ExperianPasswordResetUrl
        {
            get
            {
                return ParameterValuesUtil.ExperianPasswordResetUrl;
            }
        }
      
        private string Host
        {
            get
            {
                return ParameterValuesUtil.Host;
            }
        }

        private string Application
        {
            get
            {
                return ParameterValuesUtil.Application;
            }
        }

        public ExperianService(PasswordHelper passwordHelper)
        {
            _passwordHelper = passwordHelper;         
        }

        public string GetNewPassword()
        {
            string newPassword = "";

            try
            {
                var url = this.ExperianPasswordResetUrl;

                var dict = new Dictionary<string, string>
                {
                    { "Host", this.Host},
                    { "Connection", "keep-alive" },
                    { "command", "requestnewpassword" },
                    { "application", this.Application},
                    { "version", "1" }
                };

                var client = new HttpClient();

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _passwordHelper.AutherizationString); 

                var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) };
                var response = client.SendAsync(request).Result;

                newPassword = response.Headers.TryGetValues("Response", out var values) ? values.FirstOrDefault() : null;

                if (newPassword == null)
                {
                    throw new Exception("Received empty password from experian.");
                }

                Logger.WriteEntry($"Get password method is complete. Received password: {newPassword}", LoggingLevel.Informational);

            } catch(Exception ex)
            {
                Logger.WriteEntry($"Error getting password from experian: " + ex.Message, LoggingLevel.Error);
            }
            return newPassword;
        }

        public void ResetNewPassword(PasswordHelper passwordHelper, string newPassword)
        {
            try
            {
                var url = this.ExperianPasswordResetUrl;
                var dict = new Dictionary<string, string>
                {
                    { "Host", this.Host },
                    { "Connection", "keep-alive" },
                    { "command", "resetpassword" },
                    { "application", this.Application },
                    { "version", "1" },
                    { "newpassword", newPassword }
                };

                var client = new HttpClient();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", _passwordHelper.AutherizationString); 
               
                var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = new FormUrlEncodedContent(dict) };
                var response = client.SendAsync(request).Result;
                var resultString = response.Headers.TryGetValues("Response", out var values) ? values.FirstOrDefault() : null;

                if (resultString == null|| resultString != "SUCCESS")
                    throw new Exception("Reset of New Password Failed. Error: " + resultString);

                Logger.WriteEntry($"Reset Password method is complete.", LoggingLevel.Informational);

            }
            catch (Exception ex)
            {
                Logger.WriteEntry($"Error when resetting password: " + ex.Message, LoggingLevel.Error);
            }

        }               
    }
}
