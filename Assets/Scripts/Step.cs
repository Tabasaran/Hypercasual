using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step : MonoBehaviour
{
    [SerializeField] private GameObject playerPlacingPoint;
    [SerializeField] private float maxBallDistance;

    public GameObject PlayerPlacingPoint => playerPlacingPoint;
    public float MaxBallDistance => maxBallDistance;

    public void Activate(Vector3 position)
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
