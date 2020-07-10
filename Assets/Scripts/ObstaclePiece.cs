using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePiece : MonoBehaviour
{
    Rigidbody _rigidbody;
    public Vector3 _position;
    public Quaternion _rotation;
    public Material _material;

    private void Awake()
    {
        _position = transform.localPosition;
        _rotation = transform.localRotation;
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    void OnEnable()
    {
        _rigidbody.isKinematic = true;
    }

    private void OnDisable()
    {
        tag = "Obstacle";
        transform.localPosition = _position;
        transform.localRotation = _rotation;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        GetComponent<Renderer>().material = _material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _rigidbody.useGravity = true;
            _rigidbody.detectCollisions = true;
            _rigidbody.AddForce(Vector3.forward * 100f);
        }
    }
}
