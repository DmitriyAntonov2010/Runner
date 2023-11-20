using Interfaces;

namespace Items
{
    public abstract class BaseItemsEffect : IItemsEffect
    {
        public override bool Equals(object obj)
        {
            return obj != null && GetType() == obj.GetType();
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public virtual void ApplyEffect(Player player)
        {
        }
    }
}
