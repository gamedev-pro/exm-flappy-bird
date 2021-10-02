using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerMovementParameters")]
public class PlayerMovementParameters : ScriptableObject
{
    [field: SerializeField]
    public float ForwardSpeed { get; private set; } = 2.5f;

    [field: SerializeField]
    public float FlapVelocity { get; private set; } = 9.5f;

    [field: SerializeField]
    public float Gravity { get; private set; } = 40;

    [field: SerializeField]
    [Range(0, 180)]
    public float FlapAngleDegress = 30;

    [field: SerializeField]
    public float RotateDownSpeed = 150;
}
