using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UltimateGameManager : MonoBehaviour
{
    [SerializeField] private Director _director;

    GameObject activeSlider;
    public GameObject prefabSlider;
    public GameObject background;
    public GameObject background_time;

    private void Awake()
    {
        background.SetActive(false);
        background_time.SetActive(false);
    }

    public float speedAppear = 10f;
    public float speedDisappear = 10f;

    public int N_SLIDERS = 3;
    public float N_TIME = 10f;
    private float time;
    int sliderDone;
    bool gameStarted;
    public void StartGame()
    {
        activeSlider = Instantiate(prefabSlider, this.transform);
        StartCoroutine(Appear(activeSlider));
        sliderDone = 0;
        time = N_TIME;
        gameStarted = true;
        background.SetActive(true);
        background_time.SetActive(true);
        background.GetComponent<Animator>().SetBool("EndAnim", false);
        background_time.GetComponent<Animator>().SetBool("EndAnim", false);

    }

    public void Randomize(GameObject obj)
    {
        RectTransform start = obj.transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform parent = start.parent as RectTransform;

        float margin = 600f;

        Vector2 startPos;
        float angle;
        float length;

        startPos = new Vector2(
                Random.Range(-Screen.width/2 + margin, Screen.width/2 - margin),
                Random.Range(-Screen.height/2 + margin, Screen.height/2 - margin)
        );
        angle = Random.Range(0f, 360f);
        length = Random.Range(300f, 500f);

        start.anchoredPosition = startPos;
        start.localRotation = Quaternion.Euler(0f, 0f, angle);
        start.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length);
    }

    public void Next()
    {

        StartCoroutine(Disappear(activeSlider));
        activeSlider = null;
        sliderDone++;
        if (sliderDone != N_SLIDERS)
        {
            activeSlider = Instantiate(prefabSlider, this.transform);
            StartCoroutine(Appear(activeSlider));
        }
        else
        {
            background.gameObject.GetComponent<Animator>().SetBool("EndAnim", true);
            background_time.SetActive(false);
            StartCoroutine(WaitTillDisapear(background, true));
        }
    }

    IEnumerator WaitTillDisapear(GameObject obj, bool win)
    {
        yield return null;
        obj.SetActive(false);
        gameStarted = false;
        if(win) _director.CompleteUlti();
    }

    IEnumerator Appear(GameObject obj)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();

        if (cg == null)
            cg = obj.AddComponent<CanvasGroup>();

        float t = 0f;

        rt.localScale = Vector3.one * 0.8f;
        cg.alpha = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speedAppear;

            rt.localScale = Vector3.Lerp(Vector3.one * 0.8f, Vector3.one, t);
            cg.alpha = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        rt.localScale = Vector3.one;
        cg.alpha = 1f;
        Randomize(obj);
        obj.SetActive(true);
        CircleDrag cd = obj.GetComponentInChildren<CircleDrag>();
        cd.ultimate = this;
        cd.RecalculateLine();
    }

    IEnumerator Disappear(GameObject obj)
    {
        RectTransform rt = obj.GetComponent<RectTransform>();
        CanvasGroup cg = obj.GetComponent<CanvasGroup>();

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * speedDisappear;

            rt.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 0.8f, t);
            cg.alpha = Mathf.Lerp(1f, 0f, t);

            yield return null;
        }

        obj.SetActive(false);
        Destroy(obj.gameObject);
    }

    public void Update()
    {
        if (!gameStarted) return;

        time -= Time.deltaTime;

        Image img = background_time.GetComponent<Image>();
        img.fillAmount = Mathf.Clamp01(time / N_TIME);

        if (time <= 0f)
            Lose();
    }

    public void Lose()
    {
        if (activeSlider)
            Destroy(activeSlider);
        background.gameObject.GetComponent<Animator>().SetBool("EndAnim", true);
        background_time.SetActive(false);
        StartCoroutine(WaitTillDisapear(background, false));
        gameStarted = false;
        _director.EndUlti();
    }
}
