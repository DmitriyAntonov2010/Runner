namespace Items
{
    public class CactusItems : BaseItemsEffect
    {
        public override void ApplyEffect(Player player)
        {
            player.GameOver();
        }
    }
}