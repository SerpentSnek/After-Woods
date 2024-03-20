using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour
{
    public delegate void ParallaxCameraDelegate(Vector2 deltaMovement);
    public ParallaxCameraDelegate onCameraTranslate;

    private Vector2 oldPosition;

    void Start()
    {
        oldPosition = transform.position;
    }

    void LateUpdate()
    {
        Vector2 temp = transform.position;
        if (temp != oldPosition)
        {
            if (onCameraTranslate != null)
            {
                Vector2 delta = oldPosition - temp;
                onCameraTranslate(delta);
            }

            oldPosition = temp;
        }
    }
}