using InnerGameJamModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace InnerGameJamWebSocketServer.Services
{
    public static class GameServer
    {
        public static Game game = new Game();
        public static Dictionary<string, WebSocketBehavior> sessions = new Dictionary<string, WebSocketBehavior>();
        public static int globalId = 0;
    }
}
