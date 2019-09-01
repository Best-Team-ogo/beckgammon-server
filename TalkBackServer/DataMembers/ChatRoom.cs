using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackServer.ClientSection;
using TalkBackServer.Tools;

namespace TalkBackServer.DataMembers
{
    class ChatRoom
    {
        public int ID { get; set; }
        public List<Client> Participents { get; set; } = new List<Client>();

        public void DisplyAllParticipends(Client Exclude)
        {
            foreach (var paric in Participents.Where(x => x != Exclude))
            {
                // 1 is to add a member, 2 is to remove a member
                Exclude.Announce(PacketCreator.SendChatRoomMember(1, paric.NickName));
            }
        }
        public void SendMessage(Client Exclude,string msg)
        {
            msg = Exclude.NickName + " : " + msg;
            foreach (var paric in Participents.Where(x => x != Exclude))
            {
                paric.Announce(PacketCreator.SendMessageToChatRoom(msg));
            }
        }
    }
}
