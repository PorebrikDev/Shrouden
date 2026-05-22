using UnityEngine;
using UnityEngine.InputSystem;

public class ParallaxMouse : MonoBehaviour
{
    public float parallaxStrength = 0.5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        UpdateMouse();
    }

    private void UpdateMouse()
    {
        if (GameInput.Instance == null)  return;
        if (Mouse.current == null) return;

        Vector2 mouse = GameInput.Instance.GetMousePosition();
        float x = (mouse.x / Screen.width) * 2f - 1f;
        float y = (mouse.y / Screen.height) * 2f - 1f;
        Vector3 targetOffset = new Vector3(x, y, 0f) * parallaxStrength;
        transform.position = startPos + targetOffset;
    }
}