using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _healthBar;

    private int _health;
    public int _Combo {  get; private set; }

    private const int _MAX_HEALTH = 10;

    private void Awake()
    {
        _health = _MAX_HEALTH;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        _animator.SetTrigger("TakeDamage");
        _healthBar.fillAmount = _health / ((float)_MAX_HEALTH);
        RestartCombo();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    public void Defend()
    {
        _animator.SetTrigger("Defend");
    }

    public void AddCombo() { _Combo++; }
    public void RestartCombo() { _Combo = 0; }
}
