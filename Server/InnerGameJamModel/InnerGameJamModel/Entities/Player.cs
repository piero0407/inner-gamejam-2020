using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnerGameJamModel
{
    public class Player
    {
        public Player()
        {
            id = 0;
            status = GameConstants.PlayerStatus.ACTIVE;
        }
        private long id;
        private string name;
        private GameConstants.PlayerStatus status;
        private Star star;

        public long Id { get => id; set => id = value; }
        public GameConstants.PlayerStatus Status { get => status; set => status = value; }
        public Star Star { get => star; set => star = value; }
        public string Name { get => name; set => name = value; }
    }
}
