using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TalkBackServer.ClientSection;
using TalkBackServer.Constants;
using TalkBackServer.Tools;

namespace TalkBackServer
{

    class Server
    {
        private Socket _server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private byte[] _buffer = new byte[10240];
        public Server()
        {
            Database.DatabaseManager.Instance.InitDatabase();
            Console.WriteLine("Starting Server!");
            //Register Handlers
            int amount = PacketProccessor.InitPackets();
            Console.WriteLine($"{amount} Handler(s) has been registered!");
            Thread.Sleep(100);
#pragma warning disable CS0618 // Type or member is obsolete
            string myIp = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
#pragma warning restore CS0618 // Type or member is obsolete
            Console.WriteLine($"Host : {myIp}");
            _server.Bind(new IPEndPoint(IPAddress.Any, ServerConstants.PORT));
            _server.Listen(5);

            Console.WriteLine($"Server is listening on port {ServerConstants.PORT}");
            _server.BeginAccept(new AsyncCallback(OnClientAccpet), null);
        }


        private void OnClientAccpet(IAsyncResult ar)
        {
            Socket clientSocket = (Socket)_server.EndAccept(ar);
            ClientFactory.Instance.AddClient(new Client(clientSocket));
            Console.WriteLine($"Session with {clientSocket.RemoteEndPoint} has Started");
            BeginRecive(clientSocket);
            _server.BeginAccept(new AsyncCallback(OnClientAccpet), null);

        }

        private void BeginRecive(Socket clientSocket)
        {
            try
            {
                clientSocket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(RecivePacketFromClient), clientSocket);
            }
            catch (Exception e)
            {

            }
        }


        private volatile Mutex mutex = new Mutex();
        private object locker = new object();
        private void RecivePacketFromClient(IAsyncResult ar)
        {
            lock (locker)
            {
                Socket session = (Socket)ar.AsyncState;
                Client client = ClientFactory.Instance.GetClientBySession(session);
                if (client == null)
                    return;
                int recived = 0;
                try
                {
                    recived = session.EndReceive(ar);
                }
                catch (Exception e)
                {
                    // Client has disconnected
                    if (client == null) Console.WriteLine($"Error occur {e.Message}");
                    else
                    {
                        ClientFactory.Instance.RemoveClient(client);
                        session.Shutdown(SocketShutdown.Both);

                        Console.WriteLine(session.RemoteEndPoint + " Has been disconnected");
                        client.Logout();
                        if (!string.IsNullOrEmpty(client.NickName))
                        {

                            foreach (var c in ClientFactory.Instance.GetAllClients().Where(x => x != client))
                            {
                                c.Announce(PacketCreator.SendUserUpdate(client.NickName, 2));
                            }
                        }
                        session = null;
                    }
                    return;
                }
                byte[] packet = GetBytes(recived);
                PacketReader reader = new PacketReader(packet);
                short header = reader.ReadShort();
                PacketHandler handler = PacketProccessor.GetHandler(header);
                Console.WriteLine("Packet ID is : " + header);
                if (handler != null)
                    handler.handlePacket(reader, client);
                else
                {
                    Console.WriteLine($"Error, No Such Header was found, Header : {header}");
                }
                BeginRecive(session);
            }

        }

        private byte[] GetBytes(int recived)
        {
            byte[] dataBuf = new byte[recived];
            Array.Copy(_buffer, dataBuf, recived);
            return dataBuf;
        }
    }
}
