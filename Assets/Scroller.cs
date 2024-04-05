using UnityEngine;

public class Scroller : MonoBehaviour
{
    [SerializeField] private Renderer backgroundRenderer;
    [SerializeField] private float scrollSpeed = 1f;

    private void Update()
    {
        // Calculate the offset based on time and speed
        float offset = Time.time * scrollSpeed;

        // Apply the offset to the material's main texture
        Vector2 offsetVector = new Vector2(offset, 0);
        backgroundRenderer.material.mainTextureOffset = offsetVector;
    }
}
