﻿using System;
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
            string name = reader.ReadCommonString();
            string pass = reader.ReadCommonString();
            if (await Database.DatabaseManager.Instance.GetUser(name,pass))
            {
                
                // UPON Sucess - 1
                if (client.Login(name, pass))
                {
                    client.Announce(PacketCreator.LoginRespose(1));
                    foreach (var c in ClientFactory.Instance.GetAllClients().Where(x => x != client))
                    {
                        c.Announce(PacketCreator.SendUserUpdate(c.ID,c.Name, 1));
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