using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackServer.Handlers;

namespace TalkBackServer.Tools
{
    class PacketProccessor
    {
        private static Dictionary<short, PacketHandler> handlers = new Dictionary<short, PacketHandler>();

        internal static int InitPackets()
        {
            RegisterHandler(ClientHeaders.LOGIN, new LoginHandler());
            RegisterHandler(ClientHeaders.REGISTER, new RegisterHandler());
            RegisterHandler(ClientHeaders.LOGOUT, new LogoutHandler());
            RegisterHandler(ClientHeaders.CHAT_REQUEST, new RequestChatHandler());

            RegisterHandler(ClientHeaders.SEND_MESSAGE, new SendMessageHandler());
            
            

            return handlers.Count;

        }

        public static PacketHandler GetHandler(short header)
        {
            return handlers.FirstOrDefault(x => x.Key == header).Value;
        }
        public static short GetHandlerName(PacketHandler handler)
        {
            return handlers.FirstOrDefault(x => x.Value == handler).Key;
        }
        private static void RegisterHandler(short action, PacketHandler handler)
        {
            handlers.Add(action, handler);
        }
    }
}
