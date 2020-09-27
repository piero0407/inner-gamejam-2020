using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnerGameJamModel
{
    public class Game
    {
        public Game()
        {
            players = new List<Player>();
        }
        private List<Player> players;

        public List<Player> Players { get => players; set => players = value; }
    }
}
