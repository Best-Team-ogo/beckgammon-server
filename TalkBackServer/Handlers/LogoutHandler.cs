using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackServer.ClientSection;
using TalkBackServer.Database;
using TalkBackServer.Tools;

namespace TalkBackServer.Handlers
{
    class LogoutHandler : PacketHandler
    {
        public async void handlePacket(PacketReader reader, Client client)
        {
            ClientFactory.Instance.RemoveClient(client);
            client.Logout();
            await SendDisconnecToAllClients(client);
        }

        private static Task<User> SendDisconnecToAllClients(Client client)
        {
            if (!string.IsNullOrEmpty(client.NickName))
            {

                foreach (var c in ClientFactory.Instance.GetAllClients().Where(x => x != client))
                {
                    c.Announce(PacketCreator.SendUserUpdate(client.NickName, 2));
                }
            }
            return null;
        }
    }
}
