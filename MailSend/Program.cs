using System;
using System.Linq;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;

namespace MailSend
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Welcome to using my Console Mail Application" +
                " Enter your Credentials to Start the application");

            var detailClass = new myDetailsClass();
            string emailAddress = detailClass.myEmail();
            string password = detailClass.myPassword();
            string subject = detailClass.mySubject();
            string addressMail = detailClass.myAddress(emailAddress);

            //Create Message Data
            var message = new MimeMessage();

            //Sender of the Mail
            message.From.Add(new MailboxAddress(addressMail, emailAddress));

            string toRecceiver = detailClass.toReceiverMail();
            //Receiver Mail
            message.To.Add(MailboxAddress.Parse(toRecceiver));
            //message.ReplyTo.Add(MailboxAddress.Parse("Another Mail Address"));

            if (detailClass.ccAndBcc("carbon Copy") == true)
            {
                Console.Write("Enter Carbon Copy CC Address:");
                message.Cc.Add(MailboxAddress.Parse(Console.ReadLine()));
            }

            if (detailClass.ccAndBcc("Blind Copy") == true)
            {
                Console.Write("Enter Carbon Copy BCC Address:");
                message.Bcc.Add(MailboxAddress.Parse(Console.ReadLine()));
            }

            //add the message subject
            message.Subject = subject;
            Console.Write("What is the Content of the Mail: ");
            string textBody = Console.ReadLine();

            message.Body = new TextPart("plain") //can be plain or html
            {
                Text = textBody
            };

            //Create a new SMTP Client

            var smtpClient = new SmtpClient();

            smtpClient.CheckCertificateRevocation = false;

            try
            {
                smtpClient.Connect("smtp.gmail.com", 465, true);
                smtpClient.Authenticate(emailAddress, password);
                smtpClient.Send(message);


                Console.WriteLine("Email Sent!. ");
            }
            catch (Exception ex)
            {
                //in case of an error display the message
                Console.WriteLine("Exception Error Message that Occurecd: {0} ", ex.Message);
            }
            finally
            {
                //at any case always disconnet from the client

                smtpClient.Disconnect(true);
                //and dispose of the client object
                smtpClient.Dispose();
            }
            Console.ReadLine();

        }
    }


   class myDetailsClass
    {
        public string mySubject()
        {
            Console.Write("Enter Email Subject: ");
            return Console.ReadLine();
        }
        public string myEmail()
        {
            Console.Write("Enter Email : ");
            return Console.ReadLine();
        }
        public string myPassword()
        {
            //To Encode the Password we have to change the Background color
            Console.Write("Enter Password: ");
            
            /*
            ConsoleColor originalBGColor = Console.BackgroundColor;
            ConsoleColor originalFGColor = Console.ForegroundColor;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Green;

            //Reset our console back to it normal background and Foreground color
            Console.BackgroundColor = originalBGColor;
            Console.ForegroundColor = originalFGColor;
            */
            
            return Console.ReadLine();
        }

        public string myAddress(string mail)
        {
            var mailSplit = mail.Split("@");
            return mailSplit[0].ToUpper();
        }

        public string toReceiverMail()
        {
            Console.Write("Enter Receiver Mail : ");
            return Console.ReadLine();
        }

        public bool ccAndBcc(string emailType)
        {
            Console.Write("Would You want to attach a {0} Copy Mail Enter Yes or No:",emailType);
            string answer = Console.ReadLine();

            if (answer.ToLower() == "yes")
                return true;
            else
                return false;
        }
    }
}
