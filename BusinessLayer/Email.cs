using System;
using System.Net.Mail;

namespace BusinessLayer
{
    public class Email : BLBase
    {
        public Email() : base() { }
        public Email(CommonLayer.DBModelEntities Entities) : base(Entities) { }

        public void SendEmailToAdmin(string Subject, string Message)
        {
            foreach(CommonLayer.User User in new Users(this.Entities).GetUserRoles("ADM"))
            {
                SmtpClient smtpClient = new SmtpClient("smpt.gmail.com", 465);

                smtpClient.Credentials = new System.Net.NetworkCredential("telecellstore@gmail.com", "mobileshop");
                smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = true;

                MailMessage mail = new MailMessage();

                //Settings
                mail.From = new MailAddress("info@telecell", "Telecell");
                mail.To.Add(new MailAddress(User.Email));
                mail.CC.Add(new MailAddress("gabriel@gaucimaistre.com"));
                mail.Subject = Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = Message;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                try
                {
                    smtpClient.Send(mail);
                }
                catch (Exception ex)
                {
                    Exception ex2 = ex;
                    string errorMessage = string.Empty;
                    while (ex2 != null)
                    {
                        errorMessage += ex2.ToString();
                        ex2 = ex2.InnerException;
                    }
                }
            }
        }

        public void SendEmailToCustomer(string Email, string Subject, string Message)
        {
            SmtpClient smtpClient = new SmtpClient("mail.telecell.com", 25);

            smtpClient.Credentials = new System.Net.NetworkCredential("info@telecell.com", "myIDPassword");
            smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mail = new MailMessage();

            //Settings
            mail.From = new MailAddress("info@telecell", "Telecell");
            mail.To.Add(new MailAddress(Email));
            mail.CC.Add(new MailAddress("gabriel@gaucimaistre.com"));
            mail.Subject = Subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = Message;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Exception ex2 = ex;
                string errorMessage = string.Empty;
                while (ex2 != null)
                {
                    errorMessage += ex2.ToString();
                    ex2 = ex2.InnerException;
                }
            }
        }
    }
}