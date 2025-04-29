using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Utility/Random Color Changer")]
public class RandomColorChanger : MonoBehaviour
{
    [SerializeField] private float _changeInterval = 2f;
    [SerializeField] private bool _useRandomAlpha = false;

    private Renderer _renderer;
    private float _timer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _timer = _changeInterval;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            ChangeColor();
            _timer = _changeInterval;
        }
    }

    private void ChangeColor()
    {
        Color randomColor = new Color(
            Random.value,
            Random.value,
            Random.value,
            _useRandomAlpha ? Random.value : 1f
        );

        _renderer.material.color = randomColor;
    }

    // Для тестування з редактора
    [ContextMenu("Test Color Change")]
    private void TestColorChange()
    {
        ChangeColor();
    }
}
