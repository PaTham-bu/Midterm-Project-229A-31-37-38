using UnityEngine;

public class QuitExit : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Exit");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}