using UnityEngine;
using UnityEngine.SceneManagement;

public class CityEnter : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CheckCollision(player))
        {
            Debug.Log("Collider");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Space");
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

   
}