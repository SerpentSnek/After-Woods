using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// just position lock for now, can change later
public class CameraController : MonoBehaviour
{
    private GameObject target;
    [SerializeField] private Vector2 offset;
    
    void Start()
    {
        target = GameManager.Instance.Player;
    }

    void LateUpdate()
    {
        var newPosition = target.transform.position;
        newPosition.x += offset.x;
        newPosition.y += offset.y;
        newPosition.z = gameObject.transform.position.z;
        gameObject.transform.position = newPosition;
    }
}