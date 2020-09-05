using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 offset;

    private Vector3 position;

    private void Update()
    {
        transform.LookAt(target.transform.position);
        position = target.transform.position + offset - transform.position;
        transform.Translate(position * Time.deltaTime);
    }
}
