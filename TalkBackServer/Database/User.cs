using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBackServer.Database
{
    class User
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public string pass { get; set; }
    }
}
