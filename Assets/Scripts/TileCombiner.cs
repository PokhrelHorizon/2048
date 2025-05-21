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


    //combine tiles to left
    public void CombineLeft()
    {
        //loop that selects tiles in order and tries to combine them
        for (int i = 0; i <= 3; i++)
        {
            for (int j = 0; j <=2; j++)
            {
                if (GCS.spawnedTiles[i, j] != null)
                {
                    for (int k = j + 1; k <= 3; k++)
                    {
                        if (GCS.spawnedTiles[i, k] != null)
                        {
                            if (GCS.spawnedTileValues[i, j] != GCS.spawnedTileValues[i, k])
                            {
                                break;
                            }
                            else if (GCS.spawnedTileValues[i, j] == GCS.spawnedTileValues[i, k])
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


    //combine tiles to top
    public void CombineTop()
    {
        //loop that selects tiles in order and tries to combine them
        for (int j = 0; j <= 3; j++)
        {
            for (int i = 0; i <= 2; i++)
            {
                if (GCS.spawnedTiles[i, j] != null)
                {
                    for (int k = i + 1; k <= 3; k++)
                    {
                        if (GCS.spawnedTiles[k, j] != null)
                        {
                            if (GCS.spawnedTileValues[i, j] != GCS.spawnedTileValues[k, j])
                            {
                                break;
                            }
                            else if (GCS.spawnedTileValues[i, j] == GCS.spawnedTileValues[k, j])
                            {
                                GCS.tileMoved = true;
                                StartCoroutine(CombineTiles(i, j, k, j));
                                break;
                            }

                        }
                    }
                }
            }
        }
    }


    //combine tiles to bottom
    public void CombineBottom()
    {
        //loop that selects tiles in order and tries to combine them
        for (int j = 0; j <= 3; j++)
        {
            for (int i = 3; i >= 1; i--)
            {
                if (GCS.spawnedTiles[i, j] != null)
                {
                    for (int k = i - 1; k >=0; k--)
                    {
                        if (GCS.spawnedTiles[k, j] != null)
                        {
                            if (GCS.spawnedTileValues[i, j] != GCS.spawnedTileValues[k, j])
                            {
                                break;
                            }
                            else if (GCS.spawnedTileValues[i, j] == GCS.spawnedTileValues[k, j])
                            {
                                GCS.tileMoved = true;
                                StartCoroutine(CombineTiles(i, j, k, j));
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
        //set value of tile X1 Y1 to 0 to prevent future reference errors
        GCS.spawnedTileValues[X1, Y1] = 0;

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

        //destroy both tiles  and set tile of X1, Y1 to null
        GameObject.Destroy(GCS.spawnedTiles[X0, Y0]);
        GameObject.Destroy(GCS.spawnedTiles[X1, Y1]);
        GCS.spawnedTiles[X1, Y1] = null;
        

        //instantiate double value tile in [X0,Y0] store it and its value in respective arrays
        GCS.spawnedTiles[X0, Y0] = Instantiate(GCS.TilePrefabs[newTilePrefabArrayPosition], finalPosition, Quaternion.identity);
        GCS.spawnedTileValues[X0, Y0] = newTileValue;
    }

}
