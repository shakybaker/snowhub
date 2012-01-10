using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Sporthub;
using Sporthub.Model;
using Sporthub.Repository;
using Sporthub.Utils;

namespace Sporthub.Services
{
    public class EmailService
    {
        public void Activation(User user, int id, string activationCode, bool useSmtpPickup)
        {
            //send activation link when user changes email address
            MailManager mm = new MailManager();

            try
            {
                mm.SendMail(
                    user.Email,
                    //string.Concat(user.Forename, " ", user.Surname),
                    user.GetName(),
                    "admin@thesnowhub.com",
                    "Snowhub Info",
                    "Activation email from theSnowhub.com",
                    string.Format("Please click on the link below to verify your email address and start using theSnowhub.com\r\n\r\nhttp://www.thesnowhub.com/account/activate?{0}/{1}", id, activationCode),
                    System.Net.Mail.MailPriority.Normal,
                    true,
                    useSmtpPickup);
            }
            catch (Exception ex)
            {
                var tmp = ex.Message;
            }
        }

        public void Welcome(User user, bool useSmtpPickup)
        {
            MailManager mm = new MailManager();

            try
            {
                mm.SendMail(
                    user.Email,
                    //string.Concat(user.Forename, " ", user.Surname),
                    user.GetName(),
                    "admin@thesnowhub.com",
                    "Snowhub Info",
                    "Welcome to theSnowhub.com",
                    string.Format("Thanks for registering with theSnowhub.com\r\n\r\nHere is your password:\r\n{0}", user.Password),
                    System.Net.Mail.MailPriority.Normal,
                    true,
                    useSmtpPickup);
            }
            catch (Exception ex)
            {
                var tmp = ex.Message;
            }
        }

        public void NonFacebookRegistrationEnquiry(string emailAddress, bool useSmtpPickup)
        {
            MailManager mm = new MailManager();

            try
            {
                mm.SendMail(
                    "shakybaker@gmail.com",
                    "Snowhub Admin",
                    "admin@thesnowhub.com",
                    "Snowhub Info",
                    "Snowhub: User interested in Non-Facebook signup",
                    string.Format("Email=\r\n{0}", emailAddress),
                    System.Net.Mail.MailPriority.Normal,
                    true,
                    useSmtpPickup);
            }
            catch (Exception ex)
            {
                var tmp = ex.Message;
            }
        }

        public void AdvertisingEnquiry(string emailAddress, string message, bool useSmtpPickup)
        {
            MailManager mm = new MailManager();

            try
            {
                mm.SendMail(
                    "shakybaker@gmail.com",
                    "Snowhub Admin",
                    "admin@thesnowhub.com",
                    "Snowhub Info",
                    "Snowhub: User interested in Advertising",
                    string.Format("Email=\r\n{0}\r\n\r\nMessage=\r\n{1}", emailAddress, message),
                    System.Net.Mail.MailPriority.Normal,
                    true,
                    useSmtpPickup);
            }
            catch (Exception ex)
            {
                var tmp = ex.Message;
            }
        }

        public void GeneralEnquiry(string emailAddress, string message, bool useSmtpPickup)
        {
            MailManager mm = new MailManager();

            try
            {
                mm.SendMail(
                    "shakybaker@gmail.com",
                    "Snowhub Admin",
                    "admin@thesnowhub.com",
                    "Snowhub Info",
                    "Snowhub: General Enquiry",
                    string.Format("Email=\r\n{0}\r\n\r\nMessage=\r\n{1}", emailAddress, message),
                    System.Net.Mail.MailPriority.Normal,
                    true,
                    useSmtpPickup);
            }
            catch (Exception ex)
            {
                var tmp = ex.Message;
            }
        }

        public void ReportAbuse(string message, bool useSmtpPickup)
        {
            MailManager mm = new MailManager();

            try
            {
                mm.SendMail(
                    "shakybaker@gmail.com",
                    "Snowhub Admin",
                    "admin@thesnowhub.com",
                    "Snowhub Info",
                    "Snowhub: Report Abuse",
                    string.Format("Message=\r\n{0}", message),
                    System.Net.Mail.MailPriority.High,
                    true,
                    useSmtpPickup);
            }
            catch (Exception ex)
            {
                var tmp = ex.Message;
            }
        }
    }
}
