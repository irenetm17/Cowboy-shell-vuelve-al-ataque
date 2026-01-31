using System.Collections;
using UnityEngine;

public class DrawingsSpecial : EnemySpecial
{
    [SerializeField] private Transform _standbyPosition;
    [SerializeField] private Transform[] _papers;
    [Space]
    [SerializeField] private KeySequence[] _sequences;
    
    private int[] _papersTarget;

    private void Awake()
    {
        _papersTarget = new int[_papers.Length];
        for (int i = 0; i < _papersTarget.Length; i++) _papersTarget[i] = -1;
    }

    public override void Play(bool isUltiEnabled)
    {
        int random = isUltiEnabled ? Random.Range(0, _papers.Length) : Random.Range(0, _papers.Length - 1);

        if (_papersTarget[random] == -1) StartCoroutine(PlayPaper_EVENT(random));
    }

    private IEnumerator PlayPaper_EVENT(int paperId)
    {
        int sequence = -1;
        do
        {
            sequence = Random.Range(0, _sequences.Length);
            if (_sequences[sequence]._CurrentSequence.Length == 0 || !_sequences[sequence]._IsEnabled) sequence = -1;

        } while (sequence == -1);

        int target = -1;
        do
        {
            target = Random.Range(0, _sequences[sequence]._CurrentSequence.Length);
            for (int i = 0; i < _papersTarget.Length; i++)
            {
                if (i == paperId) continue;
                if (target == _papersTarget[i])
                {
                    target = -1;
                    break;
                }
            }

        } while (target == -1);

        _papers[paperId].position = _sequences[sequence]._CurrentSequence[target].data.gameObject.transform.position;

        yield return new WaitForSeconds(Random.Range(1f, 3f));

        _papers[paperId].position = _standbyPosition.position;
        _papersTarget[paperId] = -1;
    }
}
