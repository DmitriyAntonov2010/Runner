using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public float GameSpeed;
    public float PlayerForceJump;
    public float Gravity;
    public float MaxFlyHeight;
    public float SpeedUp;
    public float SlowDown;
}