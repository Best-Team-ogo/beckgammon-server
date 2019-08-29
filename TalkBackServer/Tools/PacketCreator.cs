using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static byte[] SendUserUpdate(int clientID,string name,byte action)
        {
            // action : 1 is add to list, 2 is remove from list
            PacketWriter writer = new PacketWriter();
            writer.WriteShort(ServerHeader.UPDATE_USERS);
            writer.WriteByte(action);
            writer.WriteInt(clientID);
            writer.WriteCommonString(name);
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
    }
}
