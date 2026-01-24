using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerKeyboard : MonoBehaviour
{
    public static readonly KeyCode[] _KeyList = new KeyCode[15]
    {
        KeyCode.Q,
        KeyCode.W,
        KeyCode.E,
        KeyCode.R,
        KeyCode.T,
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.F,
        KeyCode.G,
        KeyCode.Z,
        KeyCode.X,
        KeyCode.C,
        KeyCode.V,
        KeyCode.B
    };

    [SerializeField] private InputActionReference[] _keyActions;

    public static KeyCode _CurrentKey { get; private set; }

    private void Awake()
    {
        _CurrentKey = KeyCode.None;
    }

    private void Update()
    {
        _CurrentKey = KeyCode.None;

        for (int i = 0; i < _keyActions.Length; i++)
            if (_keyActions[i].action.WasPressedThisFrame()) _CurrentKey = _KeyList[i];
    }
}