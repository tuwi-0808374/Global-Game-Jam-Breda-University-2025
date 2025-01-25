using UnityEngine;

public class BubbleSpawners : MonoBehaviour
{
    public GameObject spawnObject;
    public Vector3 spawnPoint;
    public int timeTilNextSpawn = 5;
    float timer = 0;
    public Transform left;
    public Transform right;

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
            GameObject newBubble = Instantiate(spawnObject, spawnPoint, Quaternion.identity);
            // newBubble.upwardsspeed = newBubble.upwardsspeed + Random.Range(-3,7);
            newBubble.GetComponent<BubbleComponent>().rt.anchoredPosition = new Vector3(Random.Range(left.position.x,right.position.x),-417,0);
            timer = 0;
        }
    }
}

