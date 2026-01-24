using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Director : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [Space]
    [SerializeField] private KeySequence _attackSequence;
    [SerializeField] private KeySequence _defenseSequence;
    [SerializeField] private KeySequence _ultiSequence;
    [Space]
    [SerializeField] private GameObject _timerParent;
    [SerializeField] private Image _countdownSlider;
    [SerializeField] private Transform _clock;
    [SerializeField] private TMP_Text _clockTimeText;
    [SerializeField] private float _clockStartHeight;
    [SerializeField] private float _clockEndHeight;
    [Space]
    [SerializeField] private int _keySequencesLength;
    [SerializeField] private int _timerMaxTime;

    private bool _isActionSequenceOn;
    private bool _isReadingKeys;
    private bool _isUltiEnabled;

    private void Start()
    {
        _isUltiEnabled = true;

        StartActionSequence();
    }

    private void Update()
    {
        if (!_isActionSequenceOn) return;

        if(_isReadingKeys) CheckKeys();
    }

    private void StartActionSequence()
    {
        StopAllCoroutines();

        _enemy.SetAction();
        _isUltiEnabled = _player._Combo >= 4;

        CreateNewSequences();
        StartCountdown();

        _isActionSequenceOn = true;
        _isReadingKeys = true;
    }

    private void CreateNewSequences()
    {
        _attackSequence.CreateSequence(_keySequencesLength, true);
        _defenseSequence.CreateSequence(_keySequencesLength, false, _attackSequence._CurrentSequence);
        _ultiSequence.CreateSequence(_keySequencesLength, false, _attackSequence._CurrentSequence, _defenseSequence._CurrentSequence);
        _ultiSequence.gameObject.SetActive(_isUltiEnabled);
    }

    private void StartCountdown()
    {
        StartCoroutine(Countdown_EVENT());
    }
    private float _countdownTimer;
    private IEnumerator Countdown_EVENT()
    {
        _timerParent.SetActive(true);
        _countdownTimer = _timerMaxTime;

        while (_countdownTimer > 0)
        {
            _countdownTimer -= Time.deltaTime;

            float fixedTimeLeft = _countdownTimer / _timerMaxTime;

            _clockTimeText.SetText(Mathf.CeilToInt(_countdownTimer).ToString("F0"));
            _countdownSlider.fillAmount = fixedTimeLeft;
            _clock.localPosition = new Vector2(_clock.localPosition.x, Mathf.LerpUnclamped(_clockStartHeight, _clockEndHeight, fixedTimeLeft));

            yield return null;
        }

        EndActionSequence(-1);
    }

    private void CheckKeys()
    {
        if (PlayerKeyboard._CurrentKey != KeyCode.None)
        {
            int stateAttack = _attackSequence.CheckKey();
            int stateDefense = _defenseSequence.CheckKey();
            int stateUlti = _ultiSequence.CheckKey();

            if (stateAttack > 0 && stateDefense != -1) _defenseSequence.DisableSequence();
            else if (stateAttack == 0 && stateDefense == -1) StartCoroutine(StartWrongKeyCooldown_EVENT());

            if (stateDefense > 0 && stateAttack != -1) _attackSequence.DisableSequence();
            else if (stateDefense == 0 && stateAttack == -1) StartCoroutine(StartWrongKeyCooldown_EVENT());

            if (_isUltiEnabled)
            {
                if (stateAttack > 0 && stateUlti != -1) _ultiSequence.DisableSequence();
                else if (stateAttack == 0 && stateUlti == -1) StartCoroutine(StartWrongKeyCooldown_EVENT());

                if (stateDefense > 0 && stateUlti != -1) _ultiSequence.DisableSequence();
                else if (stateDefense == 0 && stateUlti == -1) StartCoroutine(StartWrongKeyCooldown_EVENT());

                if (stateUlti > 0 && stateAttack != -1) _attackSequence.DisableSequence();
                else if (stateUlti == 0 && stateAttack == -1) StartCoroutine(StartWrongKeyCooldown_EVENT());

                if (stateUlti > 0 && stateDefense != -1) _defenseSequence.DisableSequence();
                else if (stateUlti == 0 && stateDefense == -1) StartCoroutine(StartWrongKeyCooldown_EVENT());
            }

            if (stateAttack == 2) EndActionSequence(0);
            else if (stateDefense == 2) EndActionSequence(1);
            else if (stateUlti == 2) EndActionSequence(2);
        }
    }

    private IEnumerator StartWrongKeyCooldown_EVENT()
    {
        _isReadingKeys = false;

        yield return new WaitForSeconds(1f);

        _attackSequence.ResetKeys();
        _defenseSequence.ResetKeys();
        _ultiSequence.ResetKeys();

        _isReadingKeys = true;
    }

    private void EndActionSequence(int playerState)
    {
        _timerParent.SetActive(false);

        _isActionSequenceOn = false;
        _isReadingKeys = false;

        PlayActionResult(playerState);
    }

    private void PlayActionResult(int playerState)
    {
        _enemy.DisableChargingParticles();

        if (_enemy._CurrentAction == 0)
        {
            _enemy.Attack();

            switch (playerState)
            {
                case 1:
                    _player.Defend();
                    _player.AddCombo();
                    break;

                default:
                    _player.TakeDamage(_enemy._AttackPower);
                    break;
            }

            _enemy.RestartAttackPower();
        }
        else
        {
            _enemy.ChargePower();

            switch (playerState)
            {
                case 0:
                    _enemy.TakeDamage(1);
                    break;

                case 1:
                    _player.Defend();
                    break;


                case 2:
                    _enemy.TakeDamage(3);
                    _player.RestartCombo();
                    break;

                default:
                    break;
            }
        }

        StartCoroutine(ActionEventCooldown_EVENT());
    }

    private IEnumerator ActionEventCooldown_EVENT()
    {
        yield return new WaitForSeconds(2);

        StartActionSequence();
    }
}
