using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollision : MonoBehaviour
{
    [Tooltip("Název scény, která se načte po Game Over")]
    public string gameOverSceneName = "GameOver";

    // pokud máš enemy collider nastavený jako trigger:
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            LoadGameOver();
    }

    // pokud máš enemy collider jako běžný collider:
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
            LoadGameOver();
    }

    private void LoadGameOver()
    {
        // ujisti se, že scénu máš v Build Settings
        if (string.IsNullOrEmpty(gameOverSceneName))
        {
            Debug.LogError("PlayerCollision: gameOverSceneName není nastavené!");
            return;
        }
        SceneManager.LoadScene(gameOverSceneName);
    }
}