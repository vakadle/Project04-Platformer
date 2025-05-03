using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Collectible : MonoBehaviour
{
    [Tooltip("Body, které přidáme při sbírání")]
    public int points = 1;

    void Awake()
    {
        // zajistí, že collider je triggerem
        var c = GetComponent<Collider2D>();
        c.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // přidáme body
            ScoreManager.Instance.AddPoints(points);
            // zničíme minci
            Destroy(gameObject);
        }
    }
}