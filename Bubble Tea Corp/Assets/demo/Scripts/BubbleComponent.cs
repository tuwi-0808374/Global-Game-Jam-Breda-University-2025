using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BubbleComponent : MonoBehaviour
{
    public RectTransform rt;
    private float sway = 0;
    private float swaydivide = 0.1f;
    private float startbounds = 890;
    private float upwardsspeed = 15f;

    public Button button;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        upwardsspeed = upwardsspeed + Random.Range(-3,7);
        rt.anchoredPosition = new Vector3(Random.Range(-startbounds,startbounds),-417,0);

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
        Debug.Log("Pop!");
        

    }


    //lifespawn
    IEnumerator LifeSpan()
    {  
        yield return new WaitForSeconds(70);
        Destroy(gameObject);
    }
}
