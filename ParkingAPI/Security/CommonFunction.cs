using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace ParkingAPI.Security
{
    public class CommonFunction
    {
        public static void LogException(string Controller , string Action , string ExceptionMessage ,
            string FullException , string Description , string SessionId , string TerminalId)
        {
            try
            {
                using (var DbContext = new ParkingSystemEntities())
                {
                    ExceptionLog objException = new ExceptionLog();
                    objException.Message = ExceptionMessage;
                    objException.Source = FullException;
                    objException.StackTrace = Controller + "=>" + Action;
                    objException.CreatedDate = DateTime.Now.ToString();

                    DbContext.ExceptionLogs.Add(objException);
                    DbContext.SaveChanges();
                } 
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static bool SendEmail (string body , string receiver , string subject)
        {
            try
            {
                var smtpClient = new SmtpClient(CommonFunction.GetAppCanKeyValue("mailServer"))
                {
                    Port = Convert.ToInt32(CommonFunction.GetAppCanKeyValue("smtpPort")),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Timeout = 600000
                };
                smtpClient.Credentials = new NetworkCredential(CommonFunction.GetAppCanKeyValue("EmailMaskingUserName"), 
                    CommonFunction.GetAppCanKeyValue("EmailPassword"));
                var EmailMessage = new MailMessage
                {
                    Subject = subject,
                    From = new MailAddress(CommonFunction.GetAppCanKeyValue("EmailAddress"),
                    CommonFunction.GetAppCanKeyValue("EmailFromDisplayName")),
                    Body = body,
                    IsBodyHtml = true

                };
                EmailMessage.To.Add(receiver);
                smtpClient.Send(EmailMessage);
                smtpClient.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
        public static string GetAppCanKeyValue(string Key)
        {
            string Value = string.Empty;
            try
            {
                if (System.Configuration.ConfigurationManager.AppSettings[Key] != null &&
                    !string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings[Key]))
                {
                    Value = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings[Key]);
                }
                return Value;
            }
            catch (Exception)
            {

                return string.Empty;
            }
        }
    }
}