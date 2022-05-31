using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSend
{
    class Class1
    {
        static void Mainc(string[] args)
        {
            Console.Write("Enter Your Email: ");
            string mail = Console.ReadLine();

            var mailSplit = mail.Split("@");
            Console.WriteLine(mailSplit[0].ToUpper());
        }
    }
}
