using System;

namespace Items
{
    public class SpeedUpItems : BaseItemsEffect
    {
        public override void ApplyEffect(Player player)
        {
            player.ApplySpeedUpEffect(TimeSpan.FromSeconds(10));
        }
    }
}