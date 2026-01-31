using System.Collections;
using UnityEngine;

public class PeacockSpecial : EnemySpecial
{
    [SerializeField] private Transform _wingParent;
    [SerializeField] private Transform _wing;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform[] _positions;

    private void Awake()
    {
        _wing.gameObject.SetActive(false);
    }

    public override void Play(bool isUltiEnabled)
    {
        base.Play(isUltiEnabled);

        int random = isUltiEnabled ? Random.Range(0, _positions.Length) : Random.Range(0, _positions.Length - 1);

        Transform pos = _positions[random];
        _wingParent.SetLocalPositionAndRotation(pos.localPosition, pos.localRotation);

        StartCoroutine(DoSpecial_EVENT());
    }

    private IEnumerator DoSpecial_EVENT()
    {
        _isDoingSpecial = true;

        _wing.gameObject.SetActive(true);
        _animator.SetTrigger("Play");

        yield return new WaitForSeconds(3f);

        _wing.gameObject.SetActive(false);
        _isDoingSpecial = false;
    }
}
