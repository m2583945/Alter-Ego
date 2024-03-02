using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class squares : MonoBehaviour
{
    [SerializeField] private GameObject emptySpace;
    private Camera _camera;
    // Start is called before the first frame update
    [SerializeField] private tileScript[] tiles;
    int emptySpaceIndex=8;
    public bool shuffled = false;
    bool gamePlayed = false;
    int count = 0;

    roomMove rm;
    public Button backButton1;
    public Button backButton2;
    public bool test = false;

    float speed ;

    public bool squaresActive = false;
    void Start()
    {
        _camera = Camera.main;
        rm = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<roomMove>();
        //speed = 10f * Time.deltaTime;

    }

    // Update is called once per frame
    void Update()
    {
        float speed = 2500f + Time.fixedDeltaTime;
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("clickmouse");
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit)
            {
                
                //Debug.Log(hit.transform.name);
                if(Vector2.Distance(emptySpace.transform.position, hit.transform.position) < 2.5 && hit.transform.name != "empty")
                {
                    //Debug.Log("swap");
                    Vector2 lastEmptySpacePos = emptySpace.transform.position;
                    tileScript thisTile = hit.transform.GetComponent<tileScript>();
                    //emptySpace.transform.position = thisTile.targetPosition;
                    emptySpace.transform.position = Vector2.MoveTowards(emptySpace.transform.position, thisTile.targetPosition, speed);
                    thisTile.targetPosition = Vector2.MoveTowards(thisTile.targetPosition, lastEmptySpacePos, speed);
                    //thisTile.targetPosition = lastEmptySpacePos;
                    hit.transform.position = lastEmptySpacePos;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                    StartCoroutine("playGame");
                }
                else
                {
                    //Debug.Log("??");
                }
            }
        }
        int correctTiles = 0;
        foreach(var a in tiles)
        {
            if(a != null)
            {
                if (a.correct)
                {
                    correctTiles++;
                }
            }
        }
        if((correctTiles == tiles.Length-1 && shuffled == true && gamePlayed == true) || test == true)
        {
            count++;
            //Debug.Log("gamewon");
            StartCoroutine("winGame");
        }


    }
    
    public void shuffle()
    {
        gamePlayed = false;
        int inversions;
        if(shuffled == false)
        {
            //make tile 9 the blank tile
            //tiles[8] = null;
            //int replace = tiles[8].tileNum;

            do
            {
                for (int i = 0; i < 8; i++)
                {
                    if (tiles[i] != null)
                    {
                        var lastPos = tiles[i].targetPosition;
                        int randomIndex = Random.Range(0, 7);
                        if(tiles[randomIndex] != null)
                        {
                            //emptySpace.transform.position = Vector2.MoveTowards(emptySpace.transform.position, thisTile.targetPosition, speed);
                            tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                            //tiles[i].targetPosition = Vector2.MoveTowards(tiles[i].targetPosition, tiles[randomIndex].targetPosition, speed);
                            tiles[randomIndex].targetPosition = lastPos;
                            //tiles[randomIndex].targetPosition = Vector2.MoveTowards(tiles[randomIndex].targetPosition, lastPos, speed);
                            var tile = tiles[i];
                            tiles[i] = tiles[randomIndex];
                            tiles[randomIndex] = tile;
                        }

                      
                    }
                    else
                    {
                        //Debug.Log("null" + i.ToString());
                    }
                }
                inversions = getInversions();
            } while (inversions % 2 == 0);
            shuffled = true;
        }
        shuffled = true;


    }
    public void changeShuffledBool()
    {
        shuffled = false;
    }
    public int findIndex(tileScript ts)
    {
        for(int i = 0; i < tiles.Length; i++)
        {
            if(tiles[i] != null)
            {
                if(tiles[i] == ts)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    int getInversions()
    {
        int inversionsSum = 0;
        for(int i = 0; i < tiles.Length; i++)
        {
            int thisTileInversion = 0;
            for(int j = 1; j < tiles.Length; j++)
            {
                if(tiles[j] != null && tiles[i] != null)
                {
                    if(tiles[i].tileNum > tiles[j].tileNum)
                    {
                        thisTileInversion++;
                    }
                }
            }
            inversionsSum += thisTileInversion;
        }
        return inversionsSum;
    }
    IEnumerator playGame()
    {
        yield return new WaitForSeconds(1f);
        gamePlayed = true;

    }

    IEnumerator winGame()
    {
        if(count == 1)
        {
            count++;
            test = false;
            gamePlayed = false;
            yield return new WaitForSeconds(1f);
            rm.wardrobeWon = true;
            print("won");
            if(rm.getCurrentView() == 2)
            {
                backButton1.onClick.Invoke();
            }
            else if(rm.getCurrentView() == 3)
            {
                backButton2.onClick.Invoke();
            }
        }
        else
        {
            yield return new WaitForSeconds(0f) ;
        }
    }

    public void autoWin()
    {
        test = true;
    }

}
