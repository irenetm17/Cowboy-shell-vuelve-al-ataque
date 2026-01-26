using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _healthBoxPrefab;
    [SerializeField] private Transform _healthBoxesParent;
    [Space]
    [SerializeField] private Animator _animator;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private GameObject _chargingParticles;
    [Space]
    [SerializeField] private string[] _attackMessages;
    [SerializeField] private string[] _chargeMessages;

    public int _Health { get; private set; }
    [SerializeField] private float _healthBarHeight;
    [SerializeField] private Transform _healthBarTarget;

    private GameObject[] _healthBoxes;

    public int _CurrentAction {  get; private set; }
    public int _AttackPower { get; private set; }

    private void Awake()
    {
        RestartAttackPower();
        _chargingParticles.SetActive(false);

        float offset = _Health * 0.5f;
        _healthBoxes = new GameObject[_Health];
        const int _BOX_SEPARATION = 32;

        for (int i = 0; i < _Health; i++)
        {
            _healthBoxes[i] = Instantiate(_healthBoxPrefab, _healthBoxesParent);
            _healthBoxes[i].transform.localPosition = _BOX_SEPARATION * (-i + offset) * Vector3.left;
        }
    }

    private void Update()
    {
        _healthBoxesParent.position = _healthBarTarget.position + _healthBarHeight * Vector3.up;
    }

    public void SetAction()
    {
        _CurrentAction = Random.Range(0, 2);

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
        _Health -= damage;
        UpdateHealth();
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

    private void UpdateHealth()
    {
        for (int i = 0; i < _healthBoxes.Length; i++) _healthBoxes[i].SetActive(i < _Health);
    }
}
