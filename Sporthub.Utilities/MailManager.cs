using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace Sporthub.Utils 
{
    public partial class MailManager
    {
        private string _smtpServer;

        public MailManager()
        {
            try
            {
                //_smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
                _smtpServer = "smtp.thesnowhub.com";
            }
            catch
            {
                throw new ArgumentNullException("SMTP Server is missing from Configuration");
            }
        }

        public MailManager(string smtpServer)
        {
            _smtpServer = smtpServer;
        }

        public XmlDocument rejectAgentXmlDoc(string text1, string text2, string text3)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/email/agent_rejected.xml"));

            XmlNodeList nodeList = xmlDoc.SelectNodes("//email");

            // update text1 
            nodeList[0].ChildNodes[0].InnerText = text1;
            // update text2
            nodeList[0].ChildNodes[1].InnerText = text2;
            // update text3
            nodeList[0].ChildNodes[2].InnerText = text3;

            // Don't forget to save the file
            xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/email/agent_rejected.xml"));

            return xmlDoc;
        }

        public XmlDocument deactivateAgentXmlDoc(string text1, string text2, string text3)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/email/agent_deactivated.xml"));

            XmlNodeList nodeList = xmlDoc.SelectNodes("//email");

            // update text1 
            nodeList[0].ChildNodes[0].InnerText = text1;
            // update text2
            nodeList[0].ChildNodes[1].InnerText = text2;
            // update text3
            nodeList[0].ChildNodes[2].InnerText = text3;          

            // Don't forget to save the file
            xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/email/agent_deactivated.xml"));

            return xmlDoc;
        }

        public XmlDocument activateAgentXmlDoc(string text1, string text2, string text3)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/email/agent_activated.xml"));

            XmlNodeList nodeList = xmlDoc.SelectNodes("//email");

            // update text1 
            nodeList[0].ChildNodes[0].InnerText = text1;
            // update text2
            nodeList[0].ChildNodes[1].InnerText = text2;
            // update text3
            nodeList[0].ChildNodes[2].InnerText = text3;

            // Don't forget to save the file
            xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/email/agent_activated.xml"));

            return xmlDoc;
        }

        public XmlDocument newPressAgentXmlDoc(string text, string name, string email, string password)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/email/newAgentRegistration.xml"));

            XmlNodeList nodeList = xmlDoc.SelectNodes("//email");

            // update text 
            nodeList[0].ChildNodes[0].InnerText = text;
            // update name
            nodeList[0].ChildNodes[1].InnerText = name;
            // update email
            nodeList[0].ChildNodes[2].InnerText = email;
            // update password
            nodeList[0].ChildNodes[3].InnerText = password;

            // Don't forget to save the file
            xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/email/newAgentRegistration.xml"));

            return xmlDoc;
        }

        public XmlDocument pressPassXmlDoc(string firstName, string lastName, string subject, string message, string eventName, string email)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/email/pressPassRequest.xml"));

            XmlNodeList nodeList = xmlDoc.SelectNodes("//email");

            // update text 
            nodeList[0].ChildNodes[0].InnerText = firstName;
            nodeList[0].ChildNodes[1].InnerText = lastName;
            nodeList[0].ChildNodes[2].InnerText = subject;
            nodeList[0].ChildNodes[3].InnerText = message;
            nodeList[0].ChildNodes[4].InnerText = eventName;
            nodeList[0].ChildNodes[5].InnerText = email;

            // Don't forget to save the file
            xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/email/pressPassRequest.xml"));

            return xmlDoc;
        }

        public XmlDocument newEmailXmlDoc(string url)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/email/activateNewEmailAddress.xml"));

            XmlNodeList nodeList = xmlDoc.SelectNodes("//email");

            // update text 
            nodeList[0].ChildNodes[0].InnerText = url;

            // Don't forget to save the file
            xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/email/activateNewEmailAddress.xml"));

            return xmlDoc;
        }

        public XmlDocument userChangedXmlDoc(string text, string name, string profile)
        {        
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/email/user_modified.xml"));

            XmlNodeList nodeList = xmlDoc.SelectNodes("//email");

            // update text 
            nodeList[0].ChildNodes[0].InnerText = text;
            // update domain
            nodeList[0].ChildNodes[1].InnerText = name;
            // update domain
            nodeList[0].ChildNodes[2].InnerText = profile;
            
            // Don't forget to save the file
            xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/email/user_modified.xml"));

            return xmlDoc;            
        }

        public XmlDocument createDomainRequestXmlDoc(string text, string domain)
        {        
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/email/domain_request.xml"));

            XmlNodeList nodeList = xmlDoc.SelectNodes("//email");

            // update text 
            nodeList[0].ChildNodes[0].InnerText = text;
            // update domain
            nodeList[0].ChildNodes[1].InnerText = domain;
            
            // Don't forget to save the file
            xmlDoc.Save(HttpContext.Current.Server.MapPath("~/App_Data/email/domain_request.xml"));

            return xmlDoc;            
        }

        public string GetEmailBody(XmlDocument xmlDoc, string xslFileName)
        {
            XslCompiledTransform transform = null;

            try
            {                                
                transform = new XslCompiledTransform();
                transform.Load(HttpContext.Current.Server.MapPath("~/App_Data/email/" + xslFileName));                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            StringReader sr = new StringReader(xmlDoc.InnerXml);
            XPathDocument xpDoc = new XPathDocument(sr);

            StringWriter w = new StringWriter();
            XPathNavigator nav = xmlDoc.CreateNavigator();

            transform.Transform(nav, null, w);

            return w.ToString();
        }

        /// <summary>
        /// Sends mail messages via SMTP server
        /// </summary>
        /// <param name="To">The Address of the recipient of the e-mail message</param>
        /// <param name="NameTo">The Name of the recipient of the e-mail message</param>
        /// <param name="From">The Address of the sender of the e-mail message</param>
        /// <param name="NameFrom">The Name of the recipient of the e-mail message</param>
        /// <param name="Subject">The Subject of the e-mail message</param>
        /// <param name="Body">The Body of the e-mail message</param>
        /// <param name="Priority">The Priority of the e-mail message</param>
        public void SendMail(String AddressTo, String NameTo, String AddressFrom, String NameFrom, String Subject, String Body, MailPriority Priority, Boolean isHtml, Boolean useSmtpPickup)
        {
            try
            {
                //MailAddress objFrom = new MailAddress(AddressFrom, NameFrom);
                //MailAddress objTo = new MailAddress(AddressTo, NameTo);
                //MailMessage objMessage = new MailMessage(objFrom, objTo);
                //objMessage.Subject = Subject;
                //objMessage.Body = Body;
                //objMessage.Priority = Priority;
                //objMessage.IsBodyHtml = isHtml;

                ////SmtpClient objSmtpClient = new SmtpClient(_smtpServer);
                //SmtpClient objSmtpClient = new SmtpClient("smtp.thesnowhub.com");

                //try
                //{
                //    objSmtpClient.Send(objMessage);
                //}
                //catch (Exception ex)
                //{
                //    throw new Exception("Error Occured Sending SMTP Mail, review inner Exception for more detail", ex);
                //}

                //create the mail message
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                //set the addresses
                mail.From = new MailAddress(AddressFrom, NameFrom);
                var mailTo = new MailAddress(AddressTo, NameTo);
                mail.To.Add(mailTo);
                mail.Bcc.Add(new MailAddress("shakybaker@gmail.com", "Mark Baker"));
                //set the content
                mail.Subject = Subject;
                mail.Body = Body;
                //send the message
                var smtp = new SmtpClient();

                if (useSmtpPickup)
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtp.PickupDirectoryLocation = "C:\\SmtpPickup";
                }
                else
                {
                    smtp.Host = "auth.smtp.1and1.co.uk";
                    smtp.EnableSsl = true;

                    smtp.Credentials = new System.Net.NetworkCredential("admin@thesnowhub.com", "first2010");
                }

                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured Sending SMTP Mail, review inner Exception for more detail", ex);
            }
        }

        /// <summary>
        /// Sends mail messages via SMTP server
        /// </summary>
        /// <param name="To">The Address of the recipient of the e-mail message</param>
        /// <param name="NameTo">The Name of the recipient of the e-mail message</param>
        /// <param name="From">The Address of the sender of the e-mail message</param>
        /// <param name="NameFrom">The Name of the recipient of the e-mail message</param>
        /// <param name="Subject">The Subject of the e-mail message</param>
        /// <param name="Body">The Body of the e-mail message</param>
        /// <param name="Priority">The Priority of the e-mail message</param>
        public void SendMail(String AddressTo, String NameTo, String AddressFrom, String NameFrom, String Subject, String Body, MailPriority Priority, Boolean isHtml, string[] attachments)
        {
            MailAddress objFrom = new MailAddress(AddressFrom, NameFrom);
            MailAddress objTo = new MailAddress(AddressTo, NameTo);
            MailMessage objMessage = new MailMessage(objFrom, objTo);
            objMessage.Subject = Subject;
            objMessage.Body = Body;
            objMessage.Priority = Priority;
            objMessage.IsBodyHtml = isHtml;

            foreach (string attachment in attachments)
            {
                objMessage.Attachments.Add(new Attachment(attachment));
            }

            //SmtpClient objSmtpClient = new SmtpClient(_smtpServer);
            SmtpClient objSmtpClient = new SmtpClient("smtp.thesnowhub.com");

            try
            {
                objSmtpClient.Send(objMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured Sending SMTP Mail, review inner Exception for more detail", ex);
            }
        }

        /// <summary>
        /// Sends mail messages via SMTP server
        /// </summary>
        /// <param name="To">The Address of the recipient of the e-mail message</param>
        /// <param name="NameTo">The Name of the recipient of the e-mail message</param>
        /// <param name="From">The Address of the sender of the e-mail message</param>
        /// <param name="NameFrom">The Name of the recipient of the e-mail message</param>
        /// <param name="Subject">The Subject of the e-mail message</param>
        /// <param name="Body">The Body of the e-mail message</param>
        /// <param name="Priority">The Priority of the e-mail message</param>
        public void SendMail(String AddressTo, String NameTo, String AddressFrom, String NameFrom, String Subject, String Body, MailPriority Priority, Boolean isHtml, MailAddress[] ccaddresses)
        {
            MailAddress objFrom = new MailAddress(AddressFrom, NameFrom);
            MailAddress objTo = new MailAddress(AddressTo, NameTo);
            MailMessage objMessage = new MailMessage(objFrom, objTo);

            foreach (MailAddress ccToAdd in ccaddresses)
            {
                objMessage.CC.Add(ccToAdd);
            }

            objMessage.Subject = Subject;
            objMessage.Body = Body;
            objMessage.Priority = Priority;
            objMessage.IsBodyHtml = isHtml;

            //SmtpClient objSmtpClient = new SmtpClient(_smtpServer);
            SmtpClient objSmtpClient = new SmtpClient("smtp.thesnowhub.com");

            try
            {
                objSmtpClient.Send(objMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error Occured Sending SMTP Mail, review inner Exception for more detail", ex);
            }
        }
    }
}
