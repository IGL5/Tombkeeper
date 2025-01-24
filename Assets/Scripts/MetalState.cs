using UnityEngine;

public class MetalState : MonoBehaviour
{
    public bool IsHot { get; private set; } = false;

    public void HeatMetal()
    {
        IsHot = true;
    }
}