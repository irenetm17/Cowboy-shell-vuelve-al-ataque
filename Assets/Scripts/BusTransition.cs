using System;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BusTransition : MonoBehaviour
{
    public RectTransform[] points;
    public TMP_Text text;
    public string[] destinos;

    RectTransform rect;

    public const float timeTravel = 2f;
    float elapsed;
    int travel;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        //Leer un valor de un script que Angel dice que existirá
        travel = 2;
        // Position at the first point
        rect.localPosition = points[travel].localPosition;

        // Compute direction in UI local space
        Vector2 fromPos = points[travel].localPosition;
        Vector2 toPos = points[travel + 1].localPosition;

        Vector2 direction = toPos - fromPos;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rect.localRotation = Quaternion.Euler(0f, 0f, angle);

        //Init values:
        elapsed = 0;
        text.text += destinos[travel];
        this.transform.localScale = new Vector3(this.transform.localScale.x, -transform.localScale.y, 0);
    }

    public void Update()
    {
        if (elapsed >= timeTravel)
            SceneManager.LoadScene("Game");

        elapsed += Time.deltaTime;
        float t = elapsed / timeTravel;

        rect.localPosition = Vector2.Lerp(points[travel].localPosition, points[travel + 1].localPosition, t);
    }
}
