using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackServer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Server server = new Server();
            Process.GetCurrentProcess().WaitForExit();
        }
    }
}
