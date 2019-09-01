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
        public const short LOGIN =        1;
        public const short REGISTER =     2;
        public const short LOGOUT =       3;
        //Chat Headers
        public const short CHAT_REQUEST = 4;
        public const short SEND_MESSAGE = 5;


    }
}
