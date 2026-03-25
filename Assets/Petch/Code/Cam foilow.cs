using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    Vector3 offset;
    Vector3 newpos;
    public GameObject Player;
    void Start()
    {
        offset = Player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position - offset;
    }
}
