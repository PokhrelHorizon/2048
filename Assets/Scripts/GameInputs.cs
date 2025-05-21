using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameInputs : MonoBehaviour
{
    //reference main game script
    [SerializeField] private GameController GCS;

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

            //check if game over

            //do tile combination
            CombineRight.Invoke();

            //do tile movement, check if tile moved, then spawn new tile
            MoveRight.Invoke();
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

            //check if game over

            //do tile combination
            CombineLeft.Invoke();

            //do tile movement, check if tile moved, then spawn new tile
            MoveLeft.Invoke();
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

            //check if game over

            //do tile combination
            CombineTop.Invoke();

            //do tile movement, check if tile moved, then spawn new tile
            MoveTop.Invoke();
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

            //check if game over

            //do tile combination
            CombineBottom.Invoke();

            //do tile movement, check if tile moved, then spawn new tile
            MoveBottom.Invoke();
        }
    }
}
