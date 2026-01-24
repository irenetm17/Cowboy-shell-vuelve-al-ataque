using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _chargingParticles;
    [Space]
    [SerializeField] private string[] _attackMessages;
    [SerializeField] private string[] _chargeMessages;
    [Space]
    [SerializeField] private int _health;

    public int _CurrentAction {  get; private set; }
    public int _AttackPower { get; private set; }

    private void Awake()
    {
        RestartAttackPower();
        _chargingParticles.SetActive(false);
    }

    public void SetAction()
    {
        _CurrentAction = Random.Range(0, 2);

        _animator.SetTrigger("Angry");

        if (_CurrentAction == 0) _text.SetText(_attackMessages[Random.Range(0, _attackMessages.Length)]);
        else if (_CurrentAction == 1)
        {
            _text.SetText(_chargeMessages[Random.Range(0, _chargeMessages.Length)]);
            _chargingParticles.SetActive(true);
        }
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger("Hit");
    }

    public void ChargePower()
    {
        _AttackPower++;
    }

    public void RestartAttackPower()
    {
        _AttackPower = 1;
    }

    public void DisableChargingParticles() { _chargingParticles.SetActive(false); }
}
