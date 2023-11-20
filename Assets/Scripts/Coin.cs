using Enums;
using Interfaces;
using Items;
using UnityEngine;

public class Coin : GameItems
{
    [SerializeField]
    private CoinType _coinType;

    protected override void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null && player.CanCollect())
        {
            ApplyItemsEffect(player);
            ReturnToPool();
        }
    }

    private void ApplyItemsEffect(Player player)
    {
        IItemsEffect itemsEffect = _coinType switch
        {
            CoinType.SlowDown => new SlowDownItems(),
            CoinType.SpeedUp => new SpeedUpItems(),
            CoinType.Fly => new FlyItems(),
            _ => null
        };

        itemsEffect?.ApplyEffect(player);
    }
}