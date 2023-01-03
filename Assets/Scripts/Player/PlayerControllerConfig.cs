using UnityEngine;

[CreateAssetMenu(fileName="PlayerControllerConfig",menuName="MyConfigs/PlayerControllerConfig")]
public class PlayerControllerConfig : ScriptableObject{
    [Header("Movement Parameters")]
    public float walkSpeed = 5f;
    public float runSpeed = 10f;

    [Header("Look Parameters")]
    [Range(1f, 200f)] public float lookSensitivity = 100f;
    [SerializeField] private bool lookInvertedX = false;
    [SerializeField] private bool lookInvertedY = false;

    [Header("Fall Parameters")]
    public float gravityMultiplier = 3f;

    [Header("Jump Parameters")]
    public float jumpHeight = 1f;

    [Header("HeadBob Parameters")]
    public float walkBobAmplitude = 0.2f;
    public float walkBobFrequency = 30f;
    public float runBobAmplitude = 0.4f;
    public float runBobFrequency = 30f;

    public float isLookInvertedX
    {
        get
        {
            if (lookInvertedX == false)
            {
                return -1f;
            }
            else
            {
                return 1f;
            }
        }
    }
    public float isLookInvertedY
    {
        get
        {
            if (lookInvertedY == false){
                return 1f;
            }
            else{
                return -1f;
            }
        }
    }
}