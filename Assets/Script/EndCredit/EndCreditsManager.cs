using UnityEngine;
using TMPro;

public class EndCreditsManager : MonoBehaviour
{
    public TextMeshProUGUI finalTimeText;

    void Start()
    {
        // Hide at start
        finalTimeText.gameObject.SetActive(false);
    }

    public void ShowFinalTime()
    {
        float finalTime = PlayerPrefs.GetFloat("FinalTime", 0f);

        int minutes = Mathf.FloorToInt(finalTime / 60f);
        int seconds = Mathf.FloorToInt(finalTime % 60f);

        finalTimeText.text = "FINAL TIME\n" +
            minutes.ToString("00") + ":" + seconds.ToString("00");

        finalTimeText.gameObject.SetActive(true);
    }
}