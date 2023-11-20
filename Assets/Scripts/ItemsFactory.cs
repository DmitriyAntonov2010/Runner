using Enums;
using Interfaces;
using Items;

public class ItemsFactory
{
    public IItemsEffect CreateCoinEffect(CoinType coinType)
    {
        return coinType switch
        {
            CoinType.SlowDown => new SlowDownItems(),
            CoinType.SpeedUp => new SpeedUpItems(),
            CoinType.Fly => new FlyItems(),
            _ => throw new System.ArgumentOutOfRangeException(nameof(coinType), coinType, null)
        };
    }
    
    public IItemsEffect CreateEnemyEffect(EnemyType enemyType)
    {
        return enemyType switch
        {
            EnemyType.Bird => new BirdItems(),
            EnemyType.Cactus => new CactusItems(),
            _ => throw new System.ArgumentOutOfRangeException(nameof(enemyType), enemyType, null)
        };
    }
}