using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackServer.ClientSection;

namespace TalkBackServer.Tools
{
    class PacketCreator
    {
        public static byte[] LoginRespose(byte res)
        {
            // 1 is good, 0 is wrong, 2 is already connected
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ServerHeader.LOGIN_RESPONSE);
            writer.WriteByte(res);
            return writer.ToArray();
        }
        public static byte[] SendUserUpdate(string nickName,byte action)
        {
            // action : 1 is add to list, 2 is remove from list
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ServerHeader.UPDATE_USERS);
            writer.WriteByte(action);
            writer.WriteCommonString(nickName);
            return writer.ToArray();
        }

        internal static byte[] SendAllAvilableUsers(string name)
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ServerHeader.SEND_ALL_USERS);
            var users = ClientFactory.Instance.GetAllClients().Where(x => x.NickName != name).ToList();
            int amount = users.Count;
            writer.WriteInt(amount); // The amount of clients online

            for (int i = 0; i < amount; i++)
            {
                writer.WriteCommonString(users[i].NickName);
            }
            return writer.ToArray();
        }

        internal static byte[] SendRegisterResponse(bool succ)
        {
            //true / false
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ServerHeader.REGISTER_RESPONSE);
            writer.WriteByte(succ ? 1 : 0);
            return writer.ToArray();
        }

        internal static byte[] SendChatRequest(int chatId, string senderName)
        {
            // Sends a request to the other client client to open a chat room from
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ServerHeader.CHAT_REQUEST);
            writer.WriteByte(1);  // case 1 == send request
            writer.WriteInt(chatId);
            writer.WriteCommonString(senderName);
            return writer.ToArray();
        }

        internal static byte[] DeclineChatRequest(string name)
        {
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ServerHeader.CHAT_REQUEST);
            writer.WriteByte(1);  // case 2 == request ans
            writer.WriteBool(false);
            writer.WriteCommonString(name);
            return writer.ToArray();

        }

        internal static byte[] NoClientChatRequest(string name)
        {
            // Error that occur if no client was found in chat request
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ServerHeader.CHAT_REQUEST);
            writer.WriteByte(0);  // case 0 == error
            writer.WriteCommonString(name);
            return writer.ToArray();
        }

        internal static byte[] AcceptedChatRequest(int chatId)
        {
            throw new NotImplementedException();
        }

    }
}
