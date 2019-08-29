using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackServer.ClientSection;
using TalkBackServer.Tools;

namespace TalkBackServer.Handlers
{
    class RegisterHandler : PacketHandler
    {
        public async void handlePacket(PacketReader reader, Client client)
        {
            string name = reader.ReadCommonString();
            string pass = reader.ReadCommonString();
            bool succ = await Database.DatabaseManager.Instance.RegisterUser(name,pass);
            client.Announce(PacketCreator.SendRegisterResponse(succ));
        }
    }
}
