using System;

namespace Items
{
    public class FlyItems : BaseItemsEffect
    {
        public override void ApplyEffect(Player player)
        {
            player.ApplyFlyEffect(TimeSpan.FromSeconds(10));
        }
    }
}