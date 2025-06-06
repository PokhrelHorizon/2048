using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TileMover : MonoBehaviour
{
    //reference main game script
    [SerializeField] private GameController GCS;

    public UnityEvent TileSpawnOnInput;


    //move tiles to right after combination finishes in AnimationTime seconds
    public void MoveRight(){Invoke(nameof(MoveRightAfterDelay), GCS.AnimationTime + 0.05f);}
    private void MoveRightAfterDelay()
    {
        //loop that selects tiles in order and tries to move them
        for(int i=0; i<=3; i++)
        {
            for(int j=2; j>=0; j--)
            {
                if (GCS.spawnedTiles[i,j] !=null)
                {
                    for(int k=3; k>=j+1; k--)
                    {
                        if(GCS.spawnedTiles[i, k] == null)
                        {
                            GCS.tileMoved = true;
                            StartCoroutine(MoveTiles(i,j,i,k));
                            break;
                        }
                    }
                }
            }
        }
        CheckIfTileMoves();
    }


    //move tiles to left after combination finishes in AnimationTime seconds, +0.05f as a buffer
    public void MoveLeft() { Invoke(nameof(MoveLeftAfterDelay), GCS.AnimationTime + 0.05f); }
    private void MoveLeftAfterDelay()
    {
        //loop that selects tiles in order and tries to move them
        for (int i = 0; i <= 3; i++)
        {
            for (int j = 1; j <= 3; j++)
            {
                if (GCS.spawnedTiles[i, j] != null)
                {
                    for (int k = 0; k <= j - 1; k++)
                    {
                        if (GCS.spawnedTiles[i, k] == null)
                        {
                            GCS.tileMoved = true;
                            StartCoroutine(MoveTiles(i, j, i, k));
                            break;
                        }
                    }
                }
            }
        }
        CheckIfTileMoves();
    }


    //move tiles to top after combination finishes in AnimationTime seconds, +0.05f as a buffer
    public void MoveTop() { Invoke(nameof(MoveTopAfterDelay), GCS.AnimationTime + 0.05f); }
    private void MoveTopAfterDelay()
    {
        //loop that selects tiles in order and tries to move them
        for (int j = 0; j <= 3; j++)
        {
            for (int i = 1; i <= 3; i++)
            {
                if (GCS.spawnedTiles[i, j] != null)
                {
                    for (int k = 0; k <= i - 1; k++)
                    {
                        if (GCS.spawnedTiles[k, j] == null)
                        {
                            GCS.tileMoved = true;
                            StartCoroutine(MoveTiles(i, j, k, j));
                            break;
                        }
                    }
                }
            }
        }
        CheckIfTileMoves();
    }


    //move tiles to bottom after combination finishes in AnimationTime seconds, +0.05f as a buffer
    public void MoveBottom() { Invoke(nameof(MoveBottomAfterDelay), GCS.AnimationTime + 0.05f); }
    private void MoveBottomAfterDelay()
    {
        //loop that selects tiles in order and tries to move them
        for (int j = 0; j <= 3; j++)
        {
            for (int i = 2; i >=0; i--)
            {
                if (GCS.spawnedTiles[i, j] != null)
                {
                    for (int k = 3; k >= i + 1; k--)
                    {
                        if (GCS.spawnedTiles[k, j] == null)
                        {
                            GCS.tileMoved = true;
                            StartCoroutine(MoveTiles(i, j, k, j));
                            break;
                        }
                    }
                }
            }
        }
        CheckIfTileMoves();
    }


    //used by all movedirection functions to move tile from (x0,y0) to (x1,y1)
    private IEnumerator MoveTiles(int x0, int y0, int x1, int y1)
    {
        //change tile and its value locations in array
        GCS.spawnedTiles[x1, y1] = GCS.spawnedTiles[x0, y0];
        GCS.spawnedTiles[x0, y0] = null;

        GCS.spawnedTileValues[x1, y1] = GCS.spawnedTileValues[x0, y0];
        GCS.spawnedTileValues[x0, y0] = 0;

        //move tile
        float timeElapsed = 0;
        Vector2 initialPosition = GCS.TilePositions[x0, y0];
        Vector2 finalPosition = GCS.TilePositions[x1, y1];

        while (timeElapsed < GCS.AnimationTime)
        {
            GCS.spawnedTiles[x1, y1].transform.position = Vector2.Lerp(initialPosition, finalPosition, (timeElapsed / GCS.AnimationTime));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        GCS.spawnedTiles[x1, y1].transform.position = finalPosition;
    }


    //spawns tile if tile combined or moved
    private void CheckIfTileMoves()
    {
        if(GCS.tileMoved)
        {
            TileSpawnOnInput.Invoke();
        }
    }
}
