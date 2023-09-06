using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ResizableController : MonoBehaviour
{
    private float _scalePerSizeX;
    private float _scalePerSizeY;
    private float _scalePerSizeZ;

    public Renderer Renderer { get; private set; }
    public Bounds Bounds => Renderer.bounds;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();

        _scalePerSizeX = transform.localScale.x / Renderer.bounds.size.x;
        _scalePerSizeY = transform.localScale.y / Renderer.bounds.size.y;
        _scalePerSizeZ = transform.localScale.z / Renderer.bounds.size.z;
    }

    public void SetSize(Vector3 size)
    {
        transform.localScale = new Vector3(
            _scalePerSizeX * size.x,
            _scalePerSizeY * size.y,
            _scalePerSizeZ * size.z);
    }
}
