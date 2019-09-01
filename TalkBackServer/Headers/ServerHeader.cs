using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackServer.Tools
{
    class ServerHeader
    {
        //User Headers
        public static short LOGIN_RESPONSE = 1;
        public static short REGISTER_RESPONSE = 2;
        public static short UPDATE_USERS = 3;
        public static short SEND_ALL_USERS = 4;
        //Chat Headers
        public static short CHAT_REQUEST = 5;
    }
}
