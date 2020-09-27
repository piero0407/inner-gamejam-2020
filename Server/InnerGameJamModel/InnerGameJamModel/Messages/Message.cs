using InnerGameJamModel.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace InnerGameJamModel.Messages
{
    public class Message
    {
        public GameConstants.MessageType type;
        public Game game;
        public Player player;
        public Collision collision;
    }
}
