using TMPro;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    //reference main game script
    [SerializeField] private GameController GCS;

    //spawns two tiles at start
    public void SpawnTilesAtStart()
    {
        TileSpawn();
        TileSpawn();
    }


    //spawns one tile after every successful move
    public void SpawnTileEveryMove()
    {
        Invoke("TileSpawn", GCS.AnimationTime);
    }


    //used by different tile spawn functions to spawn one tile in random unoccupied position to game
    private void TileSpawn()
    {
        bool didTileSpawn = false;

        while(!didTileSpawn)
        {
            //random position in board, random in int is maxexclusive
            int row = Random.Range(0, 4); 
            int column = Random.Range(0, 4);

            //90% 2, 10% 4 tile chance when spawning
            GameObject tileToSpawn = (Random.Range(0f,1f) <0.9) ? GCS.TilePrefabs[0] : GCS.TilePrefabs[1];

            if (GCS.spawnedTiles[row,column] == null)
            {
                didTileSpawn = true;
                GCS.spawnedTiles[row, column] = Instantiate(tileToSpawn, GCS.TilePositions[row, column], Quaternion.identity);
                GCS.spawnedTileValues[row, column] = int.Parse(GCS.spawnedTiles[row, column].GetComponentInChildren<TMP_Text>().text);
            }
        
        }

    }
}
