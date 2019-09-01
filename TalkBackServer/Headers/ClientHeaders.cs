using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackServer.Tools
{
    class ClientHeaders
    {
        //User Headers
        public static readonly short LOGIN =        1;
        public static readonly short REGISTER =     2;
        public static readonly short LOGOUT =       3;
        //Chat Headers
        public static readonly short CHAT_REQUEST = 4;
        public static readonly short SEND_MESSAGE = 5;


    }
}
