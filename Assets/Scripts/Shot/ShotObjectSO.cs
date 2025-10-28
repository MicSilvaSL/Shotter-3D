using UnityEngine;

public abstract class ShotObjectSO : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }

    public abstract void Shot();
    
}
