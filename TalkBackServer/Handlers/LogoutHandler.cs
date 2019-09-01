using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackServer.ClientSection;
using TalkBackServer.Tools;

namespace TalkBackServer.Handlers
{
    class LogoutHandler : PacketHandler
    {
        public void handlePacket(PacketReader reader, Client client)
        {
            ClientFactory.Instance.RemoveClient(client);
            client.Logout();
            if (!string.IsNullOrEmpty(client.NickName))
            {

                foreach (var c in ClientFactory.Instance.GetAllClients().Where(x => x != client))
                {
                    c.Announce(PacketCreator.SendUserUpdate(client.NickName, 2));
                }
            }
        }
    }
}
