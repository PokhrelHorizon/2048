using System.Collections;
using UnityEngine;

public class TileCombiner : MonoBehaviour
{
    //reference main game script
    [SerializeField] private GameController GCS;

    //combine tiles to right
    public void CombineRight()
    {
        //loop that selects tiles in order and tries to combine them
        for(int i = 0; i<=3; i++)
        {
            for (int j = 3; j>=1; j--)
            {
                if(GCS.spawnedTiles[i,j] != null)
                {
                    for(int k=j-1; k>=0;  k--)
                    {
                        if (GCS.spawnedTiles[i, k] != null)
                        {
                            if(GCS.spawnedTileValues[i, j] != GCS.spawnedTileValues[i, k])
                            {
                                break;
                            }
                            else if(GCS.spawnedTileValues[i, j] == GCS.spawnedTileValues[i, k])
                            {
                                GCS.tileMoved = true;
                                StartCoroutine(CombineTiles(i, j, i, k));
                                break;
                            }
                           
                        }
                    }
                }
            }
        }
    }


    //used by all combine functions to combine tiles in [X0,Y0] and [X1,Y1] and put it in [X0,Y0]
    private IEnumerator CombineTiles(int X0, int Y0, int X1, int Y1)
    {
        //move tile [i,k] to [i,j]
        float timeElapsed = 0;

        Vector2 initialPosition = GCS.TilePositions[X1, Y1];
        Vector2 finalPosition = GCS.TilePositions[X0, Y0];

        while(timeElapsed < GCS.AnimationTime)
        {
            GCS.spawnedTiles[X1, Y1].transform.position = Vector2.Lerp(initialPosition, finalPosition, (timeElapsed/GCS.AnimationTime)); 
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        GCS.spawnedTiles[X1, Y1].transform.position = finalPosition;


        //set new tile value and determine array position of new tile game object
        int newTileValue = 2 * GCS.spawnedTileValues[X0, Y0];
        int newTilePrefabArrayPosition = (int)Mathf.Log((float)newTileValue, 2) -1;

        //destroy both tiles
        GameObject.Destroy(GCS.spawnedTiles[X0, Y0]);
        GameObject.Destroy(GCS.spawnedTiles[X1, Y1]);

        //instantiate double value tile in [X0,Y0] store it and its value in respective arrays
        GCS.spawnedTiles[X0, Y0] = Instantiate(GCS.TilePrefabs[newTilePrefabArrayPosition], finalPosition, Quaternion.identity);
        GCS.spawnedTileValues[X0, Y0] = newTileValue;
    }

}
