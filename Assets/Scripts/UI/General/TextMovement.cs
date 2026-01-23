using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextMovement : MonoBehaviour
{
    [SerializeField] private float _updateTime = 0.2f;
    [SerializeField] private float _force = 4f;

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _currentTime = Random.Range(0f, _updateTime);
    }

    private float _currentTime;
    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _updateTime)
        {
            UpdateText();
            _currentTime = 0;
        }
    }


    private void UpdateText()
    {
        _text.ForceMeshUpdate();
        var info = _text.textInfo;

        for (int i = 0; i < info.characterCount; i++)
        {
            var charInfo = info.characterInfo[i];

            if (!charInfo.isVisible) continue;

            var verticesInfo = info.meshInfo[charInfo.materialReferenceIndex].vertices;

            Vector3 offset = _force * new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            for (int j = 0; j < 4; j++)
            {
                var origin = verticesInfo[charInfo.vertexIndex + j];
                verticesInfo[charInfo.vertexIndex + j] = origin + offset;
            }
        }

        for (int i = 0; i < info.meshInfo.Length; ++i)
        {
            var mesh = info.meshInfo[i];
            mesh.mesh.vertices = mesh.vertices;
            _text.UpdateGeometry(mesh.mesh, i);
        }
    }
}
