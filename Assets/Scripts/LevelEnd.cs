using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class LevelEnd : MonoBehaviour
{
    [Tooltip("Name of the scene to load when the player reaches this finish")]
    public string nextSceneName = "Level2";

    void Awake()
    {
        // Ujistíme se, že collider funguje jako trigger
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Když narazíme na hráče, zakončíme level
        if (other.CompareTag("Player"))
        {
            Debug.Log("Level Complete!");

            if (string.IsNullOrEmpty(nextSceneName))
            {
                Debug.LogError("LevelEnd: nextSceneName není nastaveno!");
                return;
            }

            // Načteme scénu Level2
            SceneManager.LoadScene(nextSceneName);
        }
    }
}