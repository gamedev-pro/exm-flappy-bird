using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool TapUp()
    {
        if (!enabled)
        {
            return false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            return true;
        }

        for (int i = 0; i < Input.touchCount; i++)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                return true;
            }
        }
        return false;
    }
}
