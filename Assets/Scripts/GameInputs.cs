using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameInputs : MonoBehaviour
{
    //reference main game script
    [SerializeField] private GameController GCS;

    //reference gameOverUI
    [SerializeField] private GameObject gameOverUI;

    public UnityEvent CombineRight, MoveRight; //events invoked on right input
    public UnityEvent CombineLeft, MoveLeft; //events invoked on left input
    public UnityEvent CombineTop, MoveTop; //events invoked on top input
    public UnityEvent CombineBottom, MoveBottom; //events invoked on bottom input

    //used to set min time between registering inputs
    [SerializeField] private float timeBetweenInputs;
    private float inputTimeRemaining;
    private void Update()
    {
        inputTimeRemaining -= Time.deltaTime;
    }


    //called on right input
    public void OnRightInput(InputAction.CallbackContext context)
    {
        if(context.started && inputTimeRemaining<0)
        {
            //reset tilemoved state
            GCS.tileMoved = false;

            inputTimeRemaining = timeBetweenInputs; //reset input timer

            //do tile combination
            CombineRight.Invoke();

            //do tile movement, check if tile moved, then spawn new tile
            MoveRight.Invoke();

            Invoke(nameof(GameOver), 3*GCS.AnimationTime); //check if game over after everything else completes executing
        }
    }

    //called on left input
    public void OnLeftInput(InputAction.CallbackContext context)
    {
        if (context.started && inputTimeRemaining < 0)
        {
            //reset tilemoved state
            GCS.tileMoved = false;

            inputTimeRemaining = timeBetweenInputs; //reset input timer

            //do tile combination
            CombineLeft.Invoke();

            //do tile movement, check if tile moved, then spawn new tile
            MoveLeft.Invoke();

            Invoke(nameof(GameOver), 3 * GCS.AnimationTime); //check if game over after everything else completes executing
        }
    }

    //called on top input
    public void OnTopInput(InputAction.CallbackContext context)
    {
        if (context.started && inputTimeRemaining < 0)
        {
            //reset tilemoved state
            GCS.tileMoved = false;

            inputTimeRemaining = timeBetweenInputs; //reset input timer
 
            //do tile combination
            CombineTop.Invoke();

            //do tile movement, check if tile moved, then spawn new tile
            MoveTop.Invoke();

            Invoke(nameof(GameOver), 3 * GCS.AnimationTime); //check if game over after everything else completes executing
        }
    }


    //called on bottom input
    public void OnBottomInput(InputAction.CallbackContext context)
    {
        if (context.started && inputTimeRemaining < 0)
        {
            //reset tilemoved state
            GCS.tileMoved = false;

            inputTimeRemaining = timeBetweenInputs; //reset input timer

            //do tile combination
            CombineBottom.Invoke();

            //do tile movement, check if tile moved, then spawn new tile
            MoveBottom.Invoke();

            Invoke(nameof(GameOver), 3 * GCS.AnimationTime); //check if game over after everything else completes executing
        }
    }



    //check game over
    private void GameOver()
    {
        //determine if game over, true at first because false is being checked
        bool gameisPotentiallyOver = true;

        //function to check if there is adjacent equivalent tile value(empty=0),if none exist, then game over
        gameisPotentiallyOver = GameOverCheck();

        if(gameisPotentiallyOver)
        {
            gameOverUI.SetActive(true);
            //freeze game
            Time.timeScale = 0f;
        }

    }
    //checks conditions for game over
    private bool GameOverCheck()
    {

        //horizontal check
        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j <= 2; j++)
            {
                if (GCS.spawnedTileValues[i, j]==0 || GCS.spawnedTileValues[i, j + 1] ==0 || GCS.spawnedTileValues[i, j] == GCS.spawnedTileValues[i, j + 1])
                {
                    return false;
                }
            }
        }

        //vertical check
        for(int k=0; k<=3; k++)
        {
            for(int l=0;l<=2;l++)
            {
                if (GCS.spawnedTileValues[l, k] == 0 || GCS.spawnedTileValues[l + 1, k] ==0 || GCS.spawnedTileValues[l, k] == GCS.spawnedTileValues[l+1, k])
                {
                    return false;
                }
            }
        }

        //if both checks fails
        return true;
    }

}
