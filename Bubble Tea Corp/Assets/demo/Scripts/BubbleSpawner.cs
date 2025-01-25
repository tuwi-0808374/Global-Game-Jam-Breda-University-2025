using UnityEngine;

public class BubbleSpawners : MonoBehaviour
{
    public GameObject spawnObject;
    public Vector3 spawnPoint;
    public int timeTilNextSpawn = 5;
    float timer = 0;

    void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        Spawn();
    }

    void Spawn()
    {
        if (timer >= timeTilNextSpawn)
        {
            Instantiate(spawnObject, spawnPoint, Quaternion.identity);
            timer = 0;
        }
    }
}

