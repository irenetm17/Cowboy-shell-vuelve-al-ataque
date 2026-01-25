using System.Collections;
using UnityEngine;

public class PeacockSpecial : EnemySpecial
{
    [SerializeField] private Transform _wing;
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
        _wing.SetLocalPositionAndRotation(pos.localPosition, pos.localRotation);

        StartCoroutine(DoSpecial_EVENT());
    }

    private IEnumerator DoSpecial_EVENT()
    {
        _isDoingSpecial = true;

        _wing.gameObject.SetActive(true);
        float currentRot = 0;
        Quaternion rot = _wing.localRotation;

        while (currentRot < 360)
        {
            _wing.localRotation = rot * Quaternion.Euler(-currentRot * Vector3.forward);
            currentRot += Time.deltaTime * 360;

            yield return null;
        }

        _wing.gameObject.SetActive(false);
        _isDoingSpecial = false;
    }
}
