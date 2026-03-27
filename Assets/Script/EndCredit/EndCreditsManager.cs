using UnityEngine;
using TMPro;

public class EndCreditsManager : MonoBehaviour
{
    public TextMeshProUGUI finalTimeText;
    public GameObject buttonsPanel;

    void Start()
    {
        finalTimeText.gameObject.SetActive(false);
        buttonsPanel.SetActive(false);
    }

    public void ShowFinalTime()
    {
        float finalTime = PlayerPrefs.GetFloat("FinalTime", 0f);

        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);

        finalTimeText.text = "FINAL TIME\n" +
            minutes.ToString("00") + ":" + seconds.ToString("00");

        finalTimeText.gameObject.SetActive(true);
        buttonsPanel.SetActive(true);
    }
}