namespace Items
{
    public class BirdItems : BaseItemsEffect
    {
        public override void ApplyEffect(Player player)
        {
            player.GameOver();
        }
    }
}
