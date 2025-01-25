using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadScene(){
        Debug.Log("Goto next scene!");
        SceneManager.LoadScene("StockMarketScene",LoadSceneMode.Single);
    }
}
