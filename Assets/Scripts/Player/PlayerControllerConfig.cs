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