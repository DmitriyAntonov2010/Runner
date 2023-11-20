using UnityEngine;

namespace Interfaces
{
    public interface IPool
    {
        void ReturnToPool(GameObject coin);
    }
}