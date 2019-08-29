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
        public string Name { get; set; } = "";
        public string Password { get; set; } = "";
        public bool Connected = false;
        private static int _counter = 0;
        public Client(Socket socket)
        {
            Session = socket;
            ID = _counter;
            _counter++;

        }
        public bool Login(string name, string pass)
        {
            if (Connected)
                return false;
            Name = name;
            Password = pass;
            Connected = true;
            return true;
        }
        public void Logout()
        {
            Name = "";
            Password = "";
            Connected = false;

        }
        public void Announce(byte[] packet)
        {
            Session.Send(packet);
        }
    }
}
