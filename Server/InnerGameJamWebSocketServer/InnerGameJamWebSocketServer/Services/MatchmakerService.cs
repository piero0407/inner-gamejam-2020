using InnerGameJamModel;
using InnerGameJamModel.Entities;
using InnerGameJamModel.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace InnerGameJamWebSocketServer.Services
{
    class MatchmakerService : WebSocketBehavior
    {
        // Overrides
        protected override void OnOpen()
        {
            string name = Context.QueryString["name"];
            Player newPlayer = AddPlayer(name);

            Message messageJoin = new Message()
            {
                type = GameConstants.MessageType.PLAYER_JOIN,
                player = newPlayer
            };
            GameServer.sessions.Add(this.ID, this);
            string joinUpdate = JsonConvert.SerializeObject(messageJoin);
            Sessions.Broadcast(joinUpdate);
            //Send(joinUpdate);
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            Message message = JsonConvert.DeserializeObject<Message>(e.Data);
            if (message.type == GameConstants.MessageType.PLAYER_MOVEMENT_UPDATE)
            {
                UpdatePlayer(message.player);
            }
            else if (message.type == GameConstants.MessageType.GAME_START)
            {
                InitializeGame();
            }
            else if (message.type == GameConstants.MessageType.COLLISION)
            {
                HandleCollision(message.collision);
            }

            IntervalGameUpdate();
            Message messageGame = new Message()
            {
                type = GameConstants.MessageType.GAME_UPDATE,
                game = GameServer.game
            };
            string gameUpdate = JsonConvert.SerializeObject(messageGame);
            // Console.WriteLine(ID);
            Sessions.Broadcast(gameUpdate);
            //Send(msg);
        }
        protected override void OnClose(CloseEventArgs e)
        {
            GameServer.sessions.Remove(this.ID);
        }
        // Game functions
        private static void InitializeGame()
        {
            foreach (KeyValuePair<string, WebSocketBehavior> behavior in GameServer.sessions)
            {
                if (behavior.Value.State == WebSocketState.Closed)
                {
                    GameServer.sessions.Remove(behavior.Value.ID);
                }
            }
            foreach (Player player in GameServer.game.Players)
            {
                player.Status = GameConstants.PlayerStatus.ACTIVE;
                player.Star = new Star();
                player.Star.Position = GetRandomPosition();
                player.Star.Velocity = player.Star.Aceleration = new Tuple<double, double>(0.0f, 0.0f);
            }
        }
        private void IntervalGameUpdate()
        {
            IntervalPlayersUpdate();
        }
        // Player functions
        private static int GeneratePlayerId()
        {
            return Interlocked.Increment(ref GameServer.globalId);
        }

        public static Tuple<double, double> GetRandomPosition()
        {
            Random random = new Random();
            Tuple<double, double> position = new Tuple<double, double>(random.NextDouble() * (GameConstants.maxX - GameConstants.minX) + GameConstants.minX,
                random.NextDouble() * (GameConstants.maxY - GameConstants.minY) + GameConstants.minY);
            return position;
        }

        private Player AddPlayer(string name)
        {
            Player newPlayer = new Player()
            {
                Id = GeneratePlayerId(),
                Status = GameConstants.PlayerStatus.ACTIVE,
                Name = name
            };
            GameServer.game.Players.Add(newPlayer);
            return newPlayer;
        }
        private void UpdatePlayer(Player update)
        {
            if (ValidatePlayerUpdate(update))
            {
                Star originStar = GameServer.game.Players.Where(x => x.Id == update.Id).First().Star;
                if (update.Star != null)
                {
                    originStar.Position = update.Star.Position;
                    originStar.Velocity = update.Star.Velocity;
                    originStar.Aceleration = update.Star.Aceleration;
                }
            }
        }
        private void IntervalPlayersUpdate()
        {
            foreach (Player player in GameServer.game.Players)
            {
                if (player.Star.Lifes == 0)
                {
                    player.Star.Status = GameConstants.StarStatus.DEATH;
                }
            }
        }
        private bool ValidatePlayerUpdate(Player update)
        {
            if (update.Star.Status == GameConstants.StarStatus.DEATH)
            {
                return false;
            }
            return true;
        }
        // Collision functions
        private void HandleCollision(Collision collision)
        {
            if (collision.FromType == GameConstants.GameObjectType.STAR)
            {
                if (collision.ToType == GameConstants.GameObjectType.STAR)
                {
                    GameServer.game.Players.Where(x => x.Id == collision.ToId).FirstOrDefault().Star.Lifes--;
                }
                else if (collision.ToType == GameConstants.GameObjectType.DARK_HOLE)
                {
                    GameServer.game.Players.Where(x => x.Id == collision.FromId).FirstOrDefault().Star.Lifes = 0;
                }
                else if (collision.ToType == GameConstants.GameObjectType.BOUNDARY)
                {
                    GameServer.game.Players.Where(x => x.Id == collision.FromId).FirstOrDefault().Star.Lifes--;
                }
            }
        }
    }
}
