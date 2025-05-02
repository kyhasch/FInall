using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        Debug.Log("Start button clicked!");
        SceneManager.LoadScene("towerdefense alpha"); // Ensure "MainScene" is the exact name of your game scene
    }
}
