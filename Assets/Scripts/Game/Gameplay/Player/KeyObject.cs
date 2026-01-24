using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyObject : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _text;
    [Space]
    [SerializeField] private Color _normalTextColor;
    [SerializeField] private Color _normalBackgroundColor;
    [SerializeField] private float _normalSize;
    [SerializeField] private Color _nextTextColor;
    [SerializeField] private Color _nextBackgroundColor;
    [SerializeField] private float _nextSize;
    [SerializeField] private Color _goodTextColor;
    [SerializeField] private Color _goodBackgroundColor;
    [SerializeField] private float _goodSize;
    [SerializeField] private Color _badTextColor;
    [SerializeField] private Color _badBackgroundColor;
    [SerializeField] private float _badSize;
    [SerializeField] private Color _offTextColor;
    [SerializeField] private Color _offBackgroundColor;
    [SerializeField] private float _offSize;

    private float _keyScale;

    public void SetLetter(KeyCode key, float keyScale) { _text.SetText(key.ToString()); _keyScale = keyScale; }
    public void SetNormal() { _text.color = _normalTextColor; _background.color = _normalBackgroundColor; transform.localScale = _keyScale * _normalSize * Vector3.one; }
    public void SetNext() { _text.color = _nextTextColor; _background.color = _nextBackgroundColor; transform.localScale = _keyScale * _nextSize * Vector3.one; }
    public void SetGood() { _text.color = _goodTextColor; _background.color = _goodBackgroundColor; transform.localScale = _keyScale * _goodSize * Vector3.one; }
    public void SetBad() { _text.color = _badTextColor; _background.color = _badBackgroundColor; transform.localScale = _keyScale * _badSize * Vector3.one; }
    public void SetOff() { _text.color = _offTextColor; _background.color = _offBackgroundColor; transform.localScale = _keyScale * _offSize * Vector3.one; }
}
