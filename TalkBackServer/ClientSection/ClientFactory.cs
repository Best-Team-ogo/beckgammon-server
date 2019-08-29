using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackServer.ClientSection
{
    class ClientFactory
    {
        // ClientFactory Singleton Declaration
        public static ClientFactory Instance = new ClientFactory();
        private ClientFactory()
        {
            if (Instance != null)
            {
                Console.WriteLine("Error! More than 1 Client Factory was Init!");
            }
        }

        //Variables
        private List<Client> Clients = new List<Client>();

        // ClientFactory Client Managment
        public void AddClient(Client client)
        {
            Clients.Add(client);
        }
        public List<Client> GetAllClients()
        {
            return Clients;
        }
        public Client GetClientBySession(Socket session)
        {
            return Clients.FirstOrDefault(x => x.Session == session);

        }
        public void RemoveClient(Client client)
        {
            Clients.Remove(client);
        }


    }
}
