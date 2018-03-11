using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamingGod {
    public class DeletionProcess {
        public ulong userid;
        public string name;
        public int confirmation;
        public bool success = false;
        public ISocketMessageChannel channel;
    }
}
