using UnityEngine;

public class KeySequence : MonoBehaviour
{
    public struct Key
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


    private const float _KEY_OBJECTS_SEPARATION = 120;
    public void CreateSequence(int length, bool movingLeft, Key[] otherSequence = null)
    {
        _CurrentSequence = new Key[length];

        int keyId = 0;
        for (int i = 0; i < length; i++)
        {
            int newKeyId = 0;
            do
            {
                newKeyId = Random.Range(0, PlayerKeyboard._KeyList.Length);
            } while (newKeyId == keyId);

            keyId = newKeyId;
            GameObject obj = Instantiate(_keyPrefab, _container);
            obj.transform.localPosition = _KEY_OBJECTS_SEPARATION * (movingLeft ? i : i - length) * Vector3.left;

            KeyObject key = obj.GetComponent<KeyObject>();
            key.SetLetter(PlayerKeyboard._KeyList[newKeyId]);

            _CurrentSequence[i] = new Key(PlayerKeyboard._KeyList[keyId], key);

            if (otherSequence != null && i == 0)
                if (_CurrentSequence[0].key == otherSequence[0].key) i = 0;
        }
    }
}
