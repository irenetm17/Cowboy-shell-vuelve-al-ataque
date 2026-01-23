using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    [SerializeField] private KeySequence _attackSequence;
    [SerializeField] private KeySequence _defenseSequence;
    [Space]
    [SerializeField] private Slider _countdownSlider;

    private void Start()
    {
        StartActionSequence();
    }

    private void StartActionSequence()
    {
        //  Play enemy events

        CreateNewSequences();
        StartCountdown();
    }

    private void CreateNewSequences()
    {
        _attackSequence.CreateSequence(4, false);
        _defenseSequence.CreateSequence(4, true, _attackSequence._CurrentSequence);
    }

    private void StartCountdown()
    {
        StartCoroutine(Countdown_EVENT());
    }
    private float _countdownTimer;
    private const float _COUNTDOWN_TIME = 5f;
    private IEnumerator Countdown_EVENT()
    {
        _countdownTimer = _COUNTDOWN_TIME;

        while (_countdownTimer > 0)
        {
            _countdownTimer -= Time.deltaTime;
            yield return null;
        }

        EndActionSequence();
    }

    private void EndActionSequence()
    {

    }
}
