using System;
using System.Collections.Generic;
using System.Text;

namespace InnerGameJamModel.Entities
{
    public class Collision
    {
        private int fromId;
        private int toId;
        private GameConstants.GameObjectType fromType;
        private GameConstants.GameObjectType toType;

        public int FromId { get => fromId; set => fromId = value; }
        public int ToId { get => toId; set => toId = value; }
        public GameConstants.GameObjectType FromType { get => fromType; set => fromType = value; }
        public GameConstants.GameObjectType ToType { get => toType; set => toType = value; }
    }
}
