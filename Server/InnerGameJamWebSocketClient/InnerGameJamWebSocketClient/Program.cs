using InnerGameJamModel;
using InnerGameJamModel.Messages;
using Newtonsoft.Json;
using System;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace InnerGameJamWebSocketClient
{
    class Program
    {

        public static void HandleMatchmakingMessage(string messageString)
        {
            Message message = JsonConvert.DeserializeObject<Message>(messageString);
            if(message.type == GameConstants.MessageType.GAME_UPDATE)
            {
                Console.WriteLine("Matchmaking Message GAME UPDATE: " + message.game.Players.Count);
            }
            else if(message.type == GameConstants.MessageType.PLAYER_JOIN)
            {
                Console.WriteLine("Matchmaking Message PLAYER JOINED: " + message.player.Name + " with ID = " + message.player.Id);
            }
        }
        public static void Main(string[] args)
        {
            string username = Console.ReadLine();
            
            using (var ws = new WebSocket("ws://127.0.0.1:8181/Matchmaker/?name=" + username))
            {
                

                ws.OnOpen += (sender, e) => {
                    Console.WriteLine("Matchmaking Open success: " + e.ToString());
                };
                ws.OnMessage += (sender, e) =>
                    HandleMatchmakingMessage(e.Data);

                ws.Connect();
                //ws.Send("BALUS");

                Console.ReadKey(true);
            }
        }

    }

}
