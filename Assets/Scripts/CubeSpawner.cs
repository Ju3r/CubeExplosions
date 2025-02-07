using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private float _divisionNumber = 2.0f;

    private int _startChanceNumber = 1;
    private int _endChanceNumber = 100;

    private int _startRandomNumber = 2;
    private int _endRandomNumber = 6;

    public void Spawn(Cube cube)
    {
        if (IsCreateSuccessful(cube))
        {
            GameObject gameObjectToCreate = Modify(cube);

            for (int i = 0; i < GetRandomObjectsCount(); i++)
            {
                GameObject newCube = Instantiate(gameObjectToCreate, cube.transform.position, Quaternion.identity);
                Cube newCubeScript = newCube.GetComponent<Cube>();
                newCubeScript.SetChanceToCreate(cube._chanceToCreate / _divisionNumber);
            }
        }
    }

    private bool IsCreateSuccessful(Cube cube)
    {
        if (Random.Range(_startChanceNumber, _endChanceNumber + 1) <= cube._chanceToCreate)
            return true;

        return false;
    }

    private int GetRandomObjectsCount()
    {
        return Random.Range(_startRandomNumber, _endRandomNumber + 1);
    }

    private GameObject Modify(Cube cube)
    {
        Vector3 scale = cube.transform.localScale;

        float minRandomColorNumber = 0f;
        float maxRandomColorNumber = 1f;

        Color randomColor = new Color(
                                Random.Range(minRandomColorNumber, maxRandomColorNumber),
                                Random.Range(minRandomColorNumber, maxRandomColorNumber),
                                Random.Range(minRandomColorNumber, maxRandomColorNumber)
                                );

        GameObject gameObjectToCreate = cube.gameObject;
        gameObjectToCreate.transform.localScale = scale / _divisionNumber;
        gameObjectToCreate.GetComponent<MeshRenderer>().material.color = randomColor;

        return gameObjectToCreate;
    }
}