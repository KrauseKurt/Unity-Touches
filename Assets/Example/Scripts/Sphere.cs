using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    private MeshRenderer _rend;
    private Color _defaultColor;
    private Material _mat;
    public Color selectedColor;

    private void Start()
    {
        _rend = GetComponent<MeshRenderer>();
        _mat = _rend.material;
        _defaultColor = _mat.color;
    }

    private void OnTouchDown()
    {
        _mat.color = selectedColor;
    }

    private void OnTouchUp()
    {
        _mat.color = _defaultColor;
    }

    private void OnTouchStay()
    {
        _mat.color = selectedColor;
    }

    private void OnTouchMove()
    {
        _mat.color = selectedColor;
    }

    private void OnTouchExit()
    {
        _mat.color = _defaultColor;
    }
}
