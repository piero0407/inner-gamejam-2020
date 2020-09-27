using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnerGameJamModel
{
    public static class GameConstants
    {
        public static int initialLifes = 3;
        public static double minX = 0.0f;
        public static double maxX = 500.0f;
        public static double minY = 0.0f;
        public static double maxY = 500.0f;
        public enum PlayerStatus
        {
             ACTIVE = 1,
             INNACTIVE = 0,
             DISCONNECTED = -1
        }

        public enum StarStatus
        {
            ALIVE = 1,
            DEATH = 0
        }

        public enum GameStatus
        {
            ONGAME = 1,
            WAITING = 0
        }

        public enum GameObjectType
        {
            STAR,
            DARK_HOLE,
            BOUNDARY
        }
        public enum MessageType
        {
            // Client to server
            PLAYER_JOIN,
            PLAYER_MOVEMENT_UPDATE,
            COLLISION,

            //Server to client
            GAME_UPDATE,
            GAME_START,
            GAME_END
        }


    }
}
