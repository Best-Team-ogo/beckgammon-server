using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackServer.ClientSection
{
    class Client
    {
        public Socket Session { get; set; }
        public int ID { get; set; }     
        public string NickName { get; set; } = "";
        public bool Connected = false;
        private static int _counter = 0;
        public Client(Socket socket)
        {
            Session = socket;
            ID = _counter;
            _counter++;

        }
        public bool CheckForLogin(string nickName)
        {
            var tmpC = ClientFactory.Instance.GetAllClients().FirstOrDefault(x => x.NickName == nickName);
            if (tmpC != null)
                return false;
            NickName = nickName;
            Connected = true;
            return true;
        }
        public void Logout()
        {
            NickName = "";
            Connected = false;

        }
        public void Announce(byte[] packet)
        {
            Session.Send(packet);
        }
    }
}
