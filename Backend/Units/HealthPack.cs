using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RogueFeature.Backend.Units
{
    public class HealthPack : Item
    {
        private int _healAmount;
        public HealthPack(int x, int y, String imgPath, Direction face, String name, int healAmount)
            : base(x, y, imgPath, face, name)
        {
            _healAmount = healAmount;
        }

        public override void Interact(PlayerChar pc)
        {
            if (!_consumed)
            {
                pc.hits = pc.hits + _healAmount;
                _consumed = true;
            }
            Core.delta.DeltaRemove(this);
            _point.RemoveUnit(this);
        }
    }
}
