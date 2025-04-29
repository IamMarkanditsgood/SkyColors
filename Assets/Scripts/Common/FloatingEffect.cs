using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Effects/Floating Effect")]
public class FloatingEffect : MonoBehaviour
{
    [SerializeField] private float _floatHeight = 0.5f;
    [SerializeField] private float _floatSpeed = 1f;
    [SerializeField] private bool _randomizeParameters = true;

    private Vector3 _startPosition;
    private float _randomOffset;

    private void Start()
    {
        _startPosition = transform.position;

        if (_randomizeParameters)
        {
            _floatHeight *= Random.Range(0.8f, 1.2f);
            _floatSpeed *= Random.Range(0.7f, 1.3f);
            _randomOffset = Random.Range(0f, 2f * Mathf.PI);
        }
    }

    private void Update()
    {
        float newY = _startPosition.y + Mathf.Sin((Time.time + _randomOffset) * _floatSpeed) * _floatHeight;
        transform.position = new Vector3(_startPosition.x, newY, _startPosition.z);
    }
}