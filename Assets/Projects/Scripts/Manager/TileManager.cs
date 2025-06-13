using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour, IObservable<TileTouchingData>
{
    public GameObject prefab;
    public int tileCountPool = 10;
    private Queue<GameObject> tilePool = new Queue<GameObject>();
    private List<GameObject> spawnedNote = new List<GameObject>();
    public Vector3 despawnPos;

    [Header("Transforms")]
    public Transform[] listSpawnTransforms;
    public Transform despawnTransform;
    [Header("Settings")]
    public float fallSpeed = 1.0f;

    private Vector3 currentSpawnPosition;
    private Vector3[] listSpawnPositions;
    private Vector3 despawnPosition;
    private GameRunner gameRunner;
    private List<Vector3> listAvailablePositions = new List<Vector3>();
    private ObserverManager observerManager;
    public void OnUpdate(float smoothDeltaTime)
    {
        for (int i = spawnedNote.Count - 1; i >= 0; i--)
        {
            var note = spawnedNote[i];
            if (note.transform.position.y < despawnPosition.y)
            {
                gameRunner.GameOver(false);
                ResetPool();
            }
            else
            {
                note.transform.position += new Vector3(0, fallSpeed * smoothDeltaTime, 0);
            }
        }
    }

    public void OnAwake(TileTouchingObserver tileTouchingObserver, ObserverManager observerManager, GameRunner gameRunner)
    {
        this.gameRunner = gameRunner;
        observerManager.tileTouchingObserver.AddObservable(this);
        this.observerManager = observerManager;
        listSpawnPositions = new Vector3[listSpawnTransforms.Length];
        for (int i = 0; i < listSpawnTransforms.Length; i++)
        {
            listSpawnPositions[i] = listSpawnTransforms[i].transform.position;
        }
        despawnPosition = despawnTransform.transform.position;

        for (int i = 0; i < tileCountPool; i++)
        {
            GameObject obj = Instantiate(prefab, transform);
            obj.name = "Tile_" + i;
            obj.SetActive(false);
            tilePool.Enqueue(obj);
            obj.GetComponent<Tile>().OnAwake(tileTouchingObserver);
        }
    }

    public void SpawnTile()
    {
        // avoid the next spawn position to be the same
        if (listAvailablePositions.Count == 0)
        {
            listAvailablePositions = new List<Vector3>(listSpawnPositions);
        }

        currentSpawnPosition = listAvailablePositions[Random.Range(0, listAvailablePositions.Count)];
        var ind = listAvailablePositions.IndexOf(currentSpawnPosition);
        if (ind > -1)
        {
            listAvailablePositions.RemoveAt(ind);
        }

        GameObject obj;
        if (tilePool.Count == 0)
        {
            obj = Instantiate(prefab, transform);
            obj.name = "Tile_" + spawnedNote.Count;
            obj.GetComponent<Tile>().OnAwake(observerManager.tileTouchingObserver);
        }
        else
        {
            obj = tilePool.Dequeue();
            obj.SetActive(true);
        }
        obj.GetComponent<Tile>().ResetTile();
        spawnedNote.Add(obj);
        obj.transform.position = currentSpawnPosition;
    }

    public void ReturnTileToPool(GameObject tile)
    {
        tile.SetActive(false);
        tilePool.Enqueue(tile);
        spawnedNote.Remove(tile);
    }

    public void ResetPool()
    {
        foreach (GameObject obj in spawnedNote)
        {
            obj.SetActive(false);
            obj.GetComponent<Tile>().ResetTile();
            tilePool.Enqueue(obj);
        }
        spawnedNote.Clear();
    }

    public void OnNotify(TileTouchingData value)
    {
        ReturnTileToPool(value.tile.gameObject);
    }
}
