using UnityEngine;

public class CameraFollowWithBounds : MonoBehaviour
{
    [Tooltip("Transform hráče")]
    public Transform target;

    [Tooltip("Offset kamery vůči hráči (např. z = –10)")]
    public Vector3 offset = new Vector3(0f, 0f, -10f);

    [Tooltip("Rychlost plynulého dojezdu kamery")]
    [Range(0f, 20f)]
    public float smoothSpeed = 5f;

    [Tooltip("Background pro zjištění hranic")]
    public Transform background;

    private float minX, maxX, minY, maxY;
    private float halfWidth, halfHeight;

    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player")?.transform;

        // spočítáme poloviční rozměry viewportu v jednotkách světa
        Camera cam = GetComponent<Camera>();
        halfHeight = cam.orthographicSize;
        halfWidth  = halfHeight * cam.aspect;

        // zjistíme světové bounds backgroundu
        SpriteRenderer sr = background.GetComponent<SpriteRenderer>();
        Bounds b = sr.bounds;
        minX = b.min.x + halfWidth;
        maxX = b.max.x - halfWidth;
        minY = b.min.y + halfHeight;
        maxY = b.max.y - halfHeight;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // cílová pozice kamery
        Vector3 desired = target.position + offset;

        // omezíme ji na rozsah backgroundu
        desired.x = Mathf.Clamp(desired.x, minX, maxX);
        desired.y = Mathf.Clamp(desired.y, minY, maxY);
        desired.z = offset.z;

        // plynulý pohyb
        Vector3 smoothed = Vector3.Lerp(transform.position, desired, smoothSpeed * Time.deltaTime);
        transform.position = smoothed;
    }
}