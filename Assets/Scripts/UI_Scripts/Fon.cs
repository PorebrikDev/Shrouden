using UnityEngine;

public class Parallax : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxStrength = 0.5f;

    private Vector3 lastCameraPos;
    private Vector3 startPos;

    void Start()
    {
        lastCameraPos = cameraTransform.position;
        startPos = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 delta = cameraTransform.position - lastCameraPos;

        transform.position = new Vector3(
            startPos.x + delta.x * parallaxStrength,
            startPos.y + delta.y * parallaxStrength,
            startPos.z
        );

    }
}