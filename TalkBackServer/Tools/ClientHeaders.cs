using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackServer.Tools
{
    class ClientHeaders
    {
        public static readonly short LOGIN = 1;
        public static readonly short REGISTER = 2;
        public static readonly short SEND_MESSAGE = 3;
        public static readonly short REQUEST_CHAT = 4;


    }
}
