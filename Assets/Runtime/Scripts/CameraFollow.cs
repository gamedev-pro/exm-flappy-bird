using UnityEngine;

[ExecuteAlways]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, transform.position.z);
    }
}
