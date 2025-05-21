using UnityEngine;
using TMPro;
public class ScoreController : MonoBehaviour
{
    //reference main game script
    [SerializeField] private GameController GCS;

    //reference score,best TMPs
    [SerializeField] private TMP_Text scoreText, bestText;

    //store text, best is stored in storage
    private int score;
    public void UpdateScoreandBest()
    {
        score += GCS.updateScore;
        if(score > PlayerPrefs.GetInt("best"))
        {
            PlayerPrefs.SetInt("best", score);
            PlayerPrefs.Save();
        }
        scoreText.text = score.ToString();
        bestText.text = PlayerPrefs.GetInt("best").ToString();
    }
}
