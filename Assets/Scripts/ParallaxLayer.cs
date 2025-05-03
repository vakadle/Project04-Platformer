using UnityEngine;

public class ParallaxSimple : MonoBehaviour
{
    [Range(0f, 1f), Tooltip("0 = stojí, 1 = kopíruje kameru")]
    public float parallaxFactor = 0.3f;

    private Transform cam;
    private Vector3 startCamPos;
    private Vector3 startPos;

    void Start()
    {
        cam = Camera.main.transform;
        startCamPos = cam.position;
        startPos     = transform.position;
    }

    void LateUpdate()
    {
        // Aplikuje parallax jen v Play Mode
        if (!Application.isPlaying) return;

        // Horizontální delta kamery od startu
        float deltaX = cam.position.x - startCamPos.x;
        float newX   = startPos.x + deltaX * parallaxFactor;

        // Y a Z zůstávají beze změny
        transform.position = new Vector3(newX, startPos.y, startPos.z);
    }
}