using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InnerGameJamModel
{
    public class Star
    {
        public Star()
        {
            status = GameConstants.StarStatus.ALIVE;
            Lifes = GameConstants.initialLifes;
        }
        private Tuple<double, double> position;
        private Tuple<double, double> velocity;
        private Tuple<double, double> aceleration;
        private GameConstants.StarStatus status;
        private int lifes;

        public GameConstants.StarStatus Status { get => status; set => status = value; }
        public int Lifes { get => lifes; set => lifes = value; }
        public Tuple<double, double> Position { get => position; set => position = value; }
        public Tuple<double, double> Velocity { get => velocity; set => velocity = value; }
        public Tuple<double, double> Aceleration { get => aceleration; set => aceleration = value; }
    }
}
