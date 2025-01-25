using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BubbleComponent : MonoBehaviour
{
    //public GameObject mainPanel;
    public RectTransform rt;
    private float sway = 0;
    private float swaydivide = 0.1f;
    private float startbounds = 890;
    private float upwardsspeed = 15f;
    public bool canPop = true;


    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upwardsspeed = upwardsspeed + Random.Range(1,20);
        // rt.anchoredPosition = new Vector3(Random.Range(left.position.x,right.position.x),-417,0);

        StartCoroutine(LifeSpan());
    }


    // Update is called once per frame
    void Update()
    {
        sway = sway + 7 *Time.deltaTime;
        //Debug.Log(Mathf.Sin(sway));
        rt.position = rt.position + new Vector3(Mathf.Sin(sway)*swaydivide,upwardsspeed * Time.deltaTime,0);//new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);
    }


    public void Popped(){

        if (!canPop)
        {
            return;
        }
        Debug.Log("Pop!");

        FindObjectOfType<BubbleSpawners>().spawnBubbles = false;
        FindObjectOfType<BubbleSpawners>().DestroyAllBubbes();
        

        EventManager eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        eventManager.TriggerChoice();

        Destroy(gameObject);
    }

    public void DestroyMe()
    {
        StartCoroutine(DestroyBubble());
    }

    IEnumerator DestroyBubble()
    {
        canPop = false;

        // DO SOMETHING HERE LIKE PARTICLE EFFECT
        // PLAY POP SOUND
        GetComponent<Image>().enabled = false;


        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    //lifespawn
    IEnumerator LifeSpan()
    {  
        yield return new WaitForSeconds(70);
        Destroy(gameObject);
    }
}
