using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackServer.ClientSection;
using TalkBackServer.Tools;

namespace TalkBackServer.Handlers
{
    class LoginHandler : PacketHandler
    {
        public async void handlePacket(PacketReader reader, Client client)
        {
            string username = reader.ReadCommonString();
            string pass = reader.ReadCommonString();
            if (await Database.DatabaseManager.Instance.GetUser(username, pass))
            {
                
                // UPON Sucess - 1
                if (client.Login(username, pass))
                {
                    client.Announce(PacketCreator.LoginRespose(1)); //Announce to the client to login
                    client.Announce(PacketCreator.SendAllAvilableUsers(client.Name));
                    foreach (var c in ClientFactory.Instance.GetAllClients().Where(x => x != client))
                    {
                        c.Announce(PacketCreator.SendUserUpdate(client.Name, 1));
                    }
                }
                else
                {

                    client.Announce(PacketCreator.LoginRespose(2));
                   
                }
                
            }
            else
            {
                // UPON Fail - 0
                client.Announce(PacketCreator.LoginRespose(0));
            }
        }
    }
}
