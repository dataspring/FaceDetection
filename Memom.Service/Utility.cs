using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Net.Security;
using System.Threading;
using System.ComponentModel;


using Memom.Entities.Models;
using Memom.Repo.Repositories;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System.Reflection;
using System.Runtime.Serialization;




namespace Memom.Service
{
    class EmailConfig
    {
        static Configuration rootWebConfig;

        public EmailConfig()
        {
            rootWebConfig =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
        }

        public  string Host
        {
            get
            {
                if (rootWebConfig.AppSettings.Settings.Count > 0)
                {
                    System.Configuration.KeyValueConfigurationElement customSetting =
                        rootWebConfig.AppSettings.Settings["EmailHost"];
                    if (customSetting != null)
                        return customSetting.Value;
                    else
                        return "smtp.gmail.com";
                }
                else
                    return "smtp.gmail.com";
            }
        }

        public  string Port
        {
            get
            {
                if (rootWebConfig.AppSettings.Settings.Count > 0)
                {
                    System.Configuration.KeyValueConfigurationElement customSetting =
                        rootWebConfig.AppSettings.Settings["EmailPort"];
                    if (customSetting != null)
                        return customSetting.Value;
                    else
                        return "587";
                }
                else
                    return "587";
            }
        }

        public  string Uid
        {
            get
            {
                if (rootWebConfig.AppSettings.Settings.Count > 0)
                {
                    System.Configuration.KeyValueConfigurationElement customSetting =
                        rootWebConfig.AppSettings.Settings["EmailUid"];
                    if (customSetting != null)
                        return customSetting.Value;
                    else
                        return "opencvlearn";
                }
                else
                    return "opencvlearn";
            }
        }

        public  string Password
        {
            get
            {
                if (rootWebConfig.AppSettings.Settings.Count > 0)
                {
                    System.Configuration.KeyValueConfigurationElement customSetting =
                        rootWebConfig.AppSettings.Settings["EmailPwd"];
                    if (customSetting != null)
                        return customSetting.Value;
                    else
                        return "Password123";
                }
                else
                    return "Password123";
            }
        }

        public  string DisplayName
        {
            get
            {
                if (rootWebConfig.AppSettings.Settings.Count > 0)
                {
                    System.Configuration.KeyValueConfigurationElement customSetting =
                        rootWebConfig.AppSettings.Settings["EmailDisplayName"];
                    if (customSetting != null)
                        return customSetting.Value;
                    else
                        return "opencv Learning";
                }
                else
                    return "opencv Learning";
            }
        }

        public  string FromEmailAddress
        {
            get
            {
                if (rootWebConfig.AppSettings.Settings.Count > 0)
                {
                    System.Configuration.KeyValueConfigurationElement customSetting =
                        rootWebConfig.AppSettings.Settings["FromEmailAddress"];
                    if (customSetting != null)
                        return customSetting.Value;
                    else
                        return "w@gmail.com";
                }
                else
                    return "w@gmail.com";
            }
        }

        public  bool EmailSSL
        {
            get
            {
                if (rootWebConfig.AppSettings.Settings.Count > 0)
                {
                    System.Configuration.KeyValueConfigurationElement customSetting =
                        rootWebConfig.AppSettings.Settings["EmailSSL"];
                    if (customSetting != null)
                        return bool.Parse(customSetting.Value);
                    else
                        return false;
                }
                else
                    return false;
            }
        }

    }

    public static class Utility
    {
        private static string passphrase = "MFrtNkv%?{~Uj6)";

        #region EncryptDecrypt

        /// <summary>
        /// Encrypts string
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string EncryptData(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }

        /// <summary>
        /// Decrypts string
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string DecryptString(string Message)
        {
            byte[] Results;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;

            try
            {
                byte[] DataToDecrypt = Convert.FromBase64String(Message);
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }

        public static string GenPassword(int length)
        {
            try
            {
                string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
                string res = "";
                Random rnd = new Random();
                while (0 < length--)
                    res += valid[rnd.Next(valid.Length)];
                return res;
            }
            catch (Exception ex)
            {
                //clsCommon.SaveErrorLog("GenPassword", ex.Message, "GenPassword", "", ex.StackTrace);
                return ex.Message;
            }

        }

        /// <summary>
        /// Send Email of the reset password using SendGrid
        /// </summary>
        /// <param name="ToEmail"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        private static void SendMail(string ToEmail, string Subject, string HtmlBody, string TextBody = "")
        {
            try
            {
                EmailConfig emailConfig = new EmailConfig();

                MailMessage mailMsg = new MailMessage(emailConfig.FromEmailAddress, ToEmail);

                // From
                mailMsg.From = new MailAddress(emailConfig.FromEmailAddress, emailConfig.DisplayName);

                // Subject and multipart/alternative Body
                mailMsg.Subject = Subject;
                string text = TextBody;
                string html = HtmlBody;


                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
                mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

                // Init SmtpClient and send
                SmtpClient smtpClient = new SmtpClient(emailConfig.Host, Convert.ToInt32(emailConfig.Port));
                smtpClient.EnableSsl = emailConfig.EmailSSL;

                //validate the certificate
                ServicePointManager.ServerCertificateValidationCallback =
                delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

                System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(emailConfig.Uid, emailConfig.Password);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credentials;

                //// Async OPeration Code ----------------------------------------
                ////http://stackoverflow.com/questions/8768863/two-ways-to-send-email-via-smtpclient-asynchronously-different-results
                //smtpClient.SendCompleted += (s, e) =>
                //{
                //    mailMsg.Dispose();
                //};

                //smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                //string userToken = DateTime.Now.Ticks.ToString();
                
                ////smtpClient.SendAsync(mailMsg, userToken);
                //// Async Op ends here ---------------------------------------

                smtpClient.Send(mailMsg);

            }
            catch(Exception ex)
            {

            }

        }

        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                //write your code here
            }
            if (e.Error != null)
            {
                //write your code here
            }
            else //mail sent
            {
                //write your code here
            }

            //mailSent = true;
        }


        public static void SendMailForPasswordReset(string ToEmail, string NewPassword)
        {
            // Subject and multipart/alternative Body
            string subject = "Memom IntelliFace - Password Reset";
            string text = "text body";
            string html = @"<p>Memom IntelliFace received a request to reset your password.<br/> The new password is <b>" + NewPassword + "</b> </p>";

            SendMail(ToEmail, subject, html, text);
        }

        public static void SendMailForRegisterConfirmation(string ToEmail)
        {
            // Subject and multipart/alternative Body
            string subject = "Memom IntelliFace - Registration Confirmation";
            string text = "text body";
            string html = @"<p>Memom IntelliFace received a request to register this user.<br/> The new user id is <b>" + ToEmail + "</b> and password is entered duirng registration </p>";

            SendMail(ToEmail, subject, html, text);
        }

        public static void SendMailForPasswordChange(string ToEmail)
        {
            // Subject and multipart/alternative Body
            string subject = "Memom IntelliFace - Password Change";
            string text = "text body";
            string html = @"<p>Memom IntelliFace received a request to change your password.<br/> The password has been successfully changed</p>";

            SendMail(ToEmail, subject, html, text);
        }

        #endregion

    }


    // This class has the method named GetKnownTypes that returns a generic IEnumerable. 
    static class Helper
    {
        public static IEnumerable<Type> GetKnownTypes(ICustomAttributeProvider provider)
        {
            System.Collections.Generic.List<System.Type> knownTypes =
                new System.Collections.Generic.List<System.Type>();
            // Add any types to include here.
            knownTypes.Add(typeof(Album));
            knownTypes.Add(typeof(Group));
            knownTypes.Add(typeof(UserAccount));
            knownTypes.Add(typeof(Member));
            knownTypes.Add(typeof(UserAlbumInstance));
            knownTypes.Add(typeof(UserAlbumInstanceDetail));

            return knownTypes;
        }
    }



}