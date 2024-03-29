﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackServer.ClientSection;
using TalkBackServer.DataMembers;
using TalkBackServer.Tools;

namespace TalkBackServer.Handlers
{
    class RequestChatHandler : PacketHandler
    {
        public void handlePacket(PacketReader reader, Client client)
        {
            byte option = reader.ReadByte();
            if (option == 0)// Send Request From Client A to Client B
            {
                Random rnd = new Random();
                string senderName = client.NickName;
                string reciverName = reader.ReadCommonString();
                int chatId = rnd.Next(1000000);
                var reciverC = ClientFactory.Instance.GetAllClients().FirstOrDefault
                    (x => x.NickName == reciverName);
                
                if (reciverC == null)
                {
                    // if reciverName client is null, means there is an error and we need
                    //to send an error message about it ot the client
                    client.Announce(PacketCreator.NoClientChatRequest(reciverName));
                }
                else
                {
                    // if reciverName client is not null
                    reciverC.Announce(PacketCreator.SendChatRequest(chatId, senderName));

                }
            }
            if (option == 1) // Got the request answer from the requested client
            {
                bool ans = reader.ReadBool();
                string sender = reader.ReadCommonString(); // who asked!
                if (ans == false)
                {
                    // requested user decline     
                    var senderC = ClientFactory.Instance.GetAllClients().FirstOrDefault(x => x.NickName == sender);
                    senderC.Announce(PacketCreator.DeclineChatRequest(client.NickName));
                }
                else if (ans == true)
                {
                    // requested user accepted
                    int chatId = reader.ReadInt();
                    var senderC = ClientFactory.Instance.GetAllClients().FirstOrDefault(x => x.NickName == sender);
                    if (senderC == null)
                    {
                        //Client is not found, needs to send an error the client

                    }
                    else
                    {
                        // We can Open a chat!! 
                        senderC.Announce(PacketCreator.AcceptedChatRequest(chatId));
                        var chatRoom = new ChatRoom {ID = chatId};
                        chatRoom.Participents.Add(client);
                        chatRoom.Participents.Add(senderC);

                        client.ChatRooms.Add(chatRoom);
                        senderC.ChatRooms.Add(chatRoom);

                        chatRoom.Participents.ForEach(
                            x => x.ChatRooms.FirstOrDefault(
                            y => y == chatRoom)
                            .DisplyAllParticipends(x));
                        
                    }

                }
            }
        }
    }
}
