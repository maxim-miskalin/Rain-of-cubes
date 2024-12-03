using System;
using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    [SerializeField] protected float MinLifeTime = 2f;
    [SerializeField] protected float MaxLifeTime = 5f;

    public event Action<PoolableObject> Deactivated;

    private void OnValidate()
    {
        MaxLifeTime = Mathf.Max(MinLifeTime, MaxLifeTime);
    }

    protected void Deactivate()
    {
        Deactivated?.Invoke(this);
    }

    public virtual void ResetState() { }
}
