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
    [SerializeField] private Color _nextTextColor;
    [SerializeField] private Color _nextBackgroundColor;
    [SerializeField] private Color _goodTextColor;
    [SerializeField] private Color _goodBackgroundColor;
    [SerializeField] private Color _badTextColor;
    [SerializeField] private Color _badBackgroundColor;
    [SerializeField] private Color _offTextColor;
    [SerializeField] private Color _offBackgroundColor;
    public void SetLetter(KeyCode key) { _text.SetText(key.ToString()); }
    public void SetNormal() { _text.color = _normalTextColor; _background.color = _normalBackgroundColor; }
    public void SetNext() { _text.color = _nextTextColor; _background.color = _nextBackgroundColor; }
    public void SetGood() { _text.color = _goodTextColor; _background.color = _goodBackgroundColor; }
    public void SetBad() { _text.color = _badTextColor; _background.color = _badBackgroundColor; }
    public void SetOff() { _text.color = _offTextColor; _background.color = _offBackgroundColor; }
}
