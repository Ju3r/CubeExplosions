using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeExploder _exploder;

    public float _chanceToCreate { get; private set; } = 100f;

    private void OnMouseUpAsButton()
    {
        _spawner.Spawn(this);

        //if (IsCreateSuccessful())
        //{
        //    GameObject gameObjectToCreate = Modify();
        //    int count = GetRandomObjectsCount();

        //    SpawnObjects(gameObjectToCreate, count);
        //}

        _exploder.Explode(this);
        DestroyObject();
    }

    public void SetChanceToCreate(float chance)
    {
        _chanceToCreate = chance;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }

}