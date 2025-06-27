using UnityEngine;

public class PowerupUI : MonoBehaviour
{
    public static PowerupUI Instance { get; private set; }

    [SerializeField] private Transform timerBarBlue;

    private float timerMax;
    private float timer;

    private void Awake()
    {
        Instance = this;

        Hide();
    }

    private void Update()
    {
        if (timer < 0f)
        {
            timer = 0f;
            Hide();
        }

        if (timer == 0f) return;

        timer -= Time.deltaTime;

        timerBarBlue.localScale = new Vector3(timer / timerMax, 1f, 1f);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        timerBarBlue.localScale = new Vector3(1f, 1f, 1f);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public void StartTimer(float time)
    {
        Show();
        timerMax = time;
        timer = time;
    }
}
