using Enums;
using Interfaces;
using Items;
using UnityEngine;

public class Obstacle : GameItems
{
    [SerializeField] 
    private EnemyType _enemyType;
    
    protected override void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            ApplyItemsEffect(player);
            ReturnToPool();
        }
    }

    private void ApplyItemsEffect(Player player)
    {
        IItemsEffect itemsEffect = _enemyType switch
        {
            EnemyType.Bird => new BirdItems(),
            EnemyType.Cactus => new CactusItems(),
            _ => null
        };

        itemsEffect?.ApplyEffect(player);
    }
}
