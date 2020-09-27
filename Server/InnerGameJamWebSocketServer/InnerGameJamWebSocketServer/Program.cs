using InnerGameJamModel;
using InnerGameJamModel.Messages;
using InnerGameJamWebSocketServer.Services;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Example
{
    

    public class Program
    {
        public static void Main(string[] args)
        {
            var wssv = new WebSocketServer(System.Net.IPAddress.Any, 8181);
            wssv.AddWebSocketService<MatchmakerService>("/Matchmaker");
            wssv.Start();
            Console.ReadKey(true);

            wssv.Stop();
        }
    }
}