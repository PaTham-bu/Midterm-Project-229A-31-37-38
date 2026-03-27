using UnityEngine;

public class EndCredits : MonoBehaviour
{
    public float scrollSpeed = 45f;
    public float endY = 2000f;     // adjust this
    public float delay = 15f;       // delay before showing time

    private RectTransform rectTransform;
    private bool hasFinished = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        // When credits finish
        if (!hasFinished && rectTransform.anchoredPosition.y > endY)
        {
            hasFinished = true;

            // 👉 Delay the final time display
            Invoke("ShowFinalTime", delay);
        }
    }

    void ShowFinalTime()
    {
        EndCreditsManager manager = FindObjectOfType<EndCreditsManager>();

        if (manager != null)
        {
            manager.ShowFinalTime();
        }
    }
}