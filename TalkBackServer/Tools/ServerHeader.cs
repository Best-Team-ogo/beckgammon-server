using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackServer.Tools
{
    class ServerHeader
    {
        public static short LOGIN_RESPONSE = 1;
        public static short REGISTER_RESPONSE = 2;
        public static short UPDATE_USERS = 3;
        internal static int SEND_ALL_USERS = 4;
    }
}
