using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBackServer.ClientSection;

namespace TalkBackServer.Tools
{
    interface PacketHandler
    {
        void handlePacket(PacketReader reader, Client client);

    }
}
