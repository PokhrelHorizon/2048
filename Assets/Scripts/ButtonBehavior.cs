using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
    //reference main game script
    [SerializeField] private GameController GCS;
    public void RestartClick()
    {
        //reset timescale if set to 0
        Time.timeScale = GCS.OverallGameSpeed;

        SceneManager.LoadScene("MainScene");
    }
}
