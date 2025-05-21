using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameInputs : MonoBehaviour
{
    //reference main game script
    [SerializeField] private GameController GCS;

    public UnityEvent CombineRight, MoveRight; //events invoked on right input

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


}
