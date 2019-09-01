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
        public const short LOGIN_RESPONSE = 1;
        public const short REGISTER_RESPONSE = 2;
        public const short UPDATE_USERS = 3;
        public const short SEND_ALL_USERS = 4;
        //Chat Headers
        public const short CHAT_REQUEST = 5;
        public const short CHAT_MEMBERS_UPDATE = 6;
        public const short SEND_MESSAGE_TO_CHATROOM = 7;
    }
}
