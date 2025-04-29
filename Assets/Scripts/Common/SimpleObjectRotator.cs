using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Utility/Simple Object Rotator")]
public class SimpleObjectRotator : MonoBehaviour
{
    public enum RotationAxis { X, Y, Z }

    [SerializeField] private RotationAxis _axis = RotationAxis.Y;
    [SerializeField] private float _speed = 45f;
    [SerializeField] private bool _randomizeSpeed = false;

    private void Start()
    {
        if (_randomizeSpeed)
        {
            _speed = Random.Range(10f, 100f);
        }
    }

    private void Update()
    {
        Vector3 rotation = Vector3.zero;

        switch (_axis)
        {
            case RotationAxis.X: rotation.x = _speed * Time.deltaTime; break;
            case RotationAxis.Y: rotation.y = _speed * Time.deltaTime; break;
            case RotationAxis.Z: rotation.z = _speed * Time.deltaTime; break;
        }

        transform.Rotate(rotation);
    }
}