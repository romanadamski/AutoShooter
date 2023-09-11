using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class ResizableController : MonoBehaviour
{
    private float _scalePerSizeX;
    private float _scalePerSizeY;
    private float _scalePerSizeZ;
    private Renderer[] _renderers;
    
    public Bounds Bounds { get; private set; }

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();

        SetBounds();

        _scalePerSizeX = transform.localScale.x / Bounds.size.x;
        _scalePerSizeY = transform.localScale.y / Bounds.size.y;
        _scalePerSizeZ = transform.localScale.z / Bounds.size.z;
    }

    private void SetBounds()
    {
        var maxX = float.NegativeInfinity;
        var maxY = float.NegativeInfinity;
        var maxZ = float.NegativeInfinity;

        var minX = float.PositiveInfinity;
        var minY = float.PositiveInfinity;
        var minZ = float.PositiveInfinity;

        foreach (var renderer in _renderers)
        {
            if (maxX < renderer.bounds.max.x)
            {
                maxX = renderer.bounds.max.x;
            }
            if (maxY < renderer.bounds.max.y)
            {
                maxY = renderer.bounds.max.y;
            }
            if (maxZ < renderer.bounds.max.z)
            {
                maxZ = renderer.bounds.max.z;
            }

            if (minX > renderer.bounds.min.x)
            {
                minX = renderer.bounds.min.x;
            }
            if (minY > renderer.bounds.min.y)
            {
                minY = renderer.bounds.min.y;
            }
            if (minZ > renderer.bounds.min.z)
            {
                minZ = renderer.bounds.min.z;
            }
        }

        Bounds = new Bounds
        {
            max = new Vector3(maxX, maxY, maxZ),
            min = new Vector3(minX, minY, minZ),
        };
    }

    public void SetSize(Vector3 size)
    {
        transform.localScale = new Vector3(
            _scalePerSizeX * size.x,
            _scalePerSizeY * size.y,
            _scalePerSizeZ * size.z);

        SetBounds();
    }
}
