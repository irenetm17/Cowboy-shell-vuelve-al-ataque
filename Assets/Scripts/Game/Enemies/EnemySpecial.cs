using UnityEngine;

public abstract class EnemySpecial : MonoBehaviour
{
    protected bool _isDoingSpecial;

    public virtual void Play(bool isUltiEnabled)
    {
        if (_isDoingSpecial) { return; }
    }
}
