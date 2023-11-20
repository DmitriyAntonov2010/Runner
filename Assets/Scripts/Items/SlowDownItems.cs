using System;

namespace Items
{
    public class SlowDownItems : BaseItemsEffect
    {
        public override void ApplyEffect(Player player)
        {
            player.ApplySlowDownEffect(TimeSpan.FromSeconds(10));
        }
    }
}