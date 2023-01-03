using UnityEngine;

[CreateAssetMenu(fileName="PlayerControllerConfig",menuName="MyConfigs/PlayerControllerConfig")]
public class PlayerControllerConfig : ScriptableObject{
    [Header("Movement Parameters")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
}