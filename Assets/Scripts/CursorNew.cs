using UnityEngine;

public class CursorChanger : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;

    private void Start()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}
