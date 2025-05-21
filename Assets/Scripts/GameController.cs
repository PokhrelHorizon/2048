using System;
using UnityEngine;
using UnityEngine.Events;
public class GameController : MonoBehaviour
{
    public UnityEvent StartofGame; //invoked at start of game

    //arrays to store tiles and properties in
    [NonSerialized] public GameObject[,] spawnedTiles = new GameObject[4, 4];
    [NonSerialized] public int[,] spawnedTileValues = new int[4, 4];
    public Vector2[,] TilePositions { get; private set; } = new Vector2[4, 4];

    //array to store tile prefabs
    [field: SerializeField] public GameObject[] TilePrefabs { get; private set; } = new GameObject[17];

    //used in combination/movement to check if tile moved/combined then spawn one tile
    [NonSerialized] public bool tileMoved; 

    //Animation time taken by combination/movement to occur
    [field: SerializeField] public float AnimationTime { get; private set; }

    //set tile positions
    void Awake()
    {
        for(int row=0; row<4; row++)
        {
            for(int column=0;  column<4; column++)
            {
                TilePositions[row,column] = new Vector2(2 * column, -2 * row);
            }
        }
    }

    
    void Start()
    {
        StartofGame.Invoke();
    }
}
