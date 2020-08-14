using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Options
{
    public class EmailSenderOptions
    {
        public string SenderEmailAddress { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; } = true;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
