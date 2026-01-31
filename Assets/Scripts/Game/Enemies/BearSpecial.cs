using System.Collections;
using UnityEngine;

public class BearSpecial : EnemySpecial
{
    [SerializeField] private KeySequence[] _sequences;

    public override void Play(bool isUltiEnabled)
    {
        base.Play(isUltiEnabled);

        StartCoroutine(ShakePapers_EVENT());
    }

    private IEnumerator ShakePapers_EVENT()
    {
        _isDoingSpecial = true;

        Vector3[] attackPos = new Vector3[_sequences[0]._CurrentSequence.Length];
        for(int i = 0; i < attackPos.Length; i++) attackPos[i] = _sequences[0]._CurrentSequence[i].data.gameObject.transform.position;
        Vector3[] defendPos = new Vector3[_sequences[1]._CurrentSequence.Length];
        for(int i = 0; i < defendPos.Length; i++) defendPos[i] = _sequences[1]._CurrentSequence[i].data.gameObject.transform.position;
        Vector3[] ultiPos = new Vector3[_sequences[2]._CurrentSequence.Length];
        for(int i = 0; i < ultiPos.Length; i++) ultiPos[i] = _sequences[2]._CurrentSequence[i].data.gameObject.transform.position;

        float time = Random.Range(1f, 3f);
        while (time > 0f)
        {
            for (int i = 0; i < _sequences.Length; i++)
            {
                Vector3[] ogPos = i switch
                {
                    0 => attackPos,
                    1 => defendPos,
                    2 => ultiPos,
                    _ => new Vector3[0]
                };

                for (int j = 0; j < _sequences[i]._CurrentSequence.Length; j++)
                {
                    const float offset = 48f;
                    Vector3 pos = ogPos[j] + Random.Range(-offset, offset) * Vector3.up;
                    _sequences[i]._CurrentSequence[j].data.gameObject.transform.position = pos;
                }
            }

            time -= Time.deltaTime;
            yield return null;
        }

        _isDoingSpecial = false;
    }
}
