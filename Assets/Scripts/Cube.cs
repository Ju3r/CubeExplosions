using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public event Action<Cube> OnCubeClicked;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;

    public float ChanceToCreate { get; private set; } = 100f;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        OnCubeClicked?.Invoke(this);
        DestroyObject();
    }

    public void SetColor(Color color)
    {
        if (_meshRenderer != null)
            _meshRenderer.material.color = color;
    }

    public void SetChanceToCreate(float chance)
    {
        ChanceToCreate = chance;
    }

    public Rigidbody GetRigidbody()
    {
        return _rigidbody;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}