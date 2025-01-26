using UnityEngine;

public class BubbleSpawners : MonoBehaviour
{
    public GameObject spawnObject; // Prefab to spawn
    public GameObject canvas;
    public GameObject mainPanel;
    public Vector3 spawnPoint; // Spawn position
    public int timeTilNextSpawn = 5; // Time between spawns
    private float timer = 0;
    public Transform left;
    public Transform right;
    public bool spawnBubbles = true;


    void Start()
    {
        timer = 0;
    }

    private void Update()
    {
        if(!spawnBubbles)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= timeTilNextSpawn)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        // Instantiate the bubble prefab
        GameObject newBubble = Instantiate(spawnObject);


        newBubble.transform.SetParent(canvas.transform, false);

        BubbleComponent bubbleComponent = newBubble.GetComponent<BubbleComponent>();
        //if (bubbleComponent != null)
        //{
        //    bubbleComponent.mainPanel = mainPanel;
        //}


        RectTransform bubbleRect = newBubble.GetComponent<RectTransform>();
        if (bubbleRect != null)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();
            Vector3 leftLocal = canvasRect.InverseTransformPoint(left.position);
            Vector3 rightLocal = canvasRect.InverseTransformPoint(right.position);

            float randomX = Random.Range(leftLocal.x, rightLocal.x);
            bubbleRect.anchoredPosition = new Vector3(randomX, -417, 0);
        }

        // Reset the spawn timer
        timer = 0;
        timeTilNextSpawn = UnityEngine.Random.Range(7,10);
    }

    public void DestroyAllBubbes()
    {
        BubbleComponent[] bubbles = FindObjectsOfType<BubbleComponent>();
        foreach (BubbleComponent bubble in bubbles)
        {
            bubble.DestroyMe();
        }
    }


}
