using UnityEngine;

public class KeySequence : MonoBehaviour
{
    public class Key
    {
        public KeyCode key;
        public KeyObject data;

        public Key(KeyCode key, KeyObject data)
        {
            this.key = key;
            this.data = data;
        }
    }

    [SerializeField] private GameObject _keyPrefab;
    [SerializeField] private Transform _container;

    public Key[] _CurrentSequence { get; private set; }
    public bool _IsEnabled { get; private set; }


    private const float _KEY_OBJECTS_SEPARATION = 100;
    private const float _EXTRA_SPACE_SCALE_MULTIPLIER = 0.125f;
    public void CreateSequence(int length, bool movingRight, Key[] firstSequence = null, Key[] secondSequence = null)
    {
        if(_CurrentSequence != null)
            foreach (Key key in _CurrentSequence) Destroy(key.data.gameObject);

        _CurrentSequence = new Key[length];
        _SequenceProgress = 0;

        float keyScale = length > 4 ? 1 - _EXTRA_SPACE_SCALE_MULTIPLIER * (length - 4) : 1;

        int keyId = -1;
        for (int i = 0; i < length; i++)
        {
            int newKeyId = 0;
            do
            {
                newKeyId = Random.Range(0, PlayerKeyboard._KeyList.Length);
            } while (newKeyId == keyId);

            keyId = newKeyId;
            GameObject obj = Instantiate(_keyPrefab, _container);
            obj.transform.localPosition = _KEY_OBJECTS_SEPARATION * keyScale * (movingRight ? i : i - length) * Vector3.right;

            KeyObject key = obj.GetComponent<KeyObject>();
            key.SetLetter(PlayerKeyboard._KeyList[newKeyId], keyScale);

            _CurrentSequence[i] = new Key(PlayerKeyboard._KeyList[keyId], key);

            if (i == 0 && firstSequence != null)
            {
                if (_CurrentSequence[0].key == firstSequence[0].key) i = 0;
                else if (secondSequence != null) if (_CurrentSequence[0].key == secondSequence[0].key) i = 0;
            }
        }

        EnableSequence();
    }

    public void EnableSequence()
    {
        _IsEnabled = true;
        foreach (Key key in _CurrentSequence) key.data.SetNormal();
        _CurrentSequence[0].data.SetNext();
    }
    public void DisableSequence()
    {
        _IsEnabled = false;
        foreach (Key key in _CurrentSequence) key.data.SetOff();
    }

    public int _SequenceProgress {  get; private set; }
    public int CheckKey()
    {
        if (!_IsEnabled) return -1; // Disable

        if (_CurrentSequence[_SequenceProgress].key == PlayerKeyboard._CurrentKey)
        {
            _CurrentSequence[_SequenceProgress].data.SetGood();

            _SequenceProgress++;

            if (_SequenceProgress != _CurrentSequence.Length) _CurrentSequence[_SequenceProgress].data.SetNext(); else return 2;   //  Completed

            return 1;   //  Correct key
        }

        if (_SequenceProgress != 0)
        {
            _CurrentSequence[_SequenceProgress].data.SetBad();
            _SequenceProgress = 0;
        }

        return 0;   //  Wrong key
    }

    public void ResetKeys()
    {
        _SequenceProgress = 0;
        EnableSequence();
    }
}
