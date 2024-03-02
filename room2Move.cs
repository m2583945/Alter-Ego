using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room2Move : MonoBehaviour
{
    GameObject wall1;
    GameObject wall2;
    GameObject wall3;

    GameObject transition;

    public GameObject wall1obj;
    public GameObject wall2obj;
    public GameObject wall3obj;

    public bool tapeTaken;
    public bool grillOpen;
    public bool trowelTaken;
    public bool chimesWon = false;
    public bool pickTaken = false;

    public Sprite openGrill;
    // Start is called before the first frame update
    void Start()
    {
        wall1 = GameObject.Find("wall1");
        wall2 = GameObject.Find("wall2");
        wall3 = GameObject.Find("wall3");
        transition = GameObject.Find("transition");
        StartCoroutine("fadeBlack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void moveLeft()
    {
        if (wall1.GetComponent<SpriteRenderer>().enabled == true)//move from 2 to 3
        {
            StartCoroutine("fadeBlack");
            wall1.GetComponent<SpriteRenderer>().enabled = false;
            wall3.GetComponent<SpriteRenderer>().enabled = true;
            wall1obj.SetActive(false);
            wall3obj.SetActive(true);
            checkChimes();

        }
        else if (wall2.GetComponent<SpriteRenderer>().enabled == true)//move from 2 to 1
        {
            StartCoroutine("fadeBlack");
            wall2.GetComponent<SpriteRenderer>().enabled = false;
            wall1.GetComponent<SpriteRenderer>().enabled = true;
            wall2obj.SetActive(false);
            wall1obj.SetActive(true);
            checkTape();
            checkGrill();

        }
        else if (wall3.GetComponent<SpriteRenderer>().enabled == true)//move from 3 to 2
        {
            StartCoroutine("fadeBlack");
            wall3.GetComponent<SpriteRenderer>().enabled = false;
            wall2.GetComponent<SpriteRenderer>().enabled = true;
            wall3obj.SetActive(false);
            wall2obj.SetActive(true);
            checkTape();
        }
    }
    public void moveRight()
    {
        if (wall1.GetComponent<SpriteRenderer>().enabled == true)//move from 1 to 2
        {
            StartCoroutine("fadeBlack");
            wall1.GetComponent<SpriteRenderer>().enabled = false;
            wall2.GetComponent<SpriteRenderer>().enabled = true;
            wall1obj.SetActive(false);
            wall2obj.SetActive(true);
            checkTape();


        }
        else if (wall2.GetComponent<SpriteRenderer>().enabled == true)//move from 2 to 3
        {
            StartCoroutine("fadeBlack");
            wall2.GetComponent<SpriteRenderer>().enabled = false;
            wall3.GetComponent<SpriteRenderer>().enabled = true;
            wall2obj.SetActive(false);
            wall3obj.SetActive(true);
            checkChimes();


        }
        else if (wall3.GetComponent<SpriteRenderer>().enabled == true)//move from 3 to 1
        {
            StartCoroutine("fadeBlack");
            wall3.GetComponent<SpriteRenderer>().enabled = false;
            wall1.GetComponent<SpriteRenderer>().enabled = true;
            wall3obj.SetActive(false);
            wall1obj.SetActive(true);
            checkTape();
            checkGrill();

        }
    }
    public void checkTape()
    {
        if (getCurrentView() == 1)
        {
            Transform tape = wall1obj.gameObject.GetComponent<Transform>().Find("tape");
            if (tapeTaken == true)
            {
                tape.gameObject.SetActive(false);
            }
        }
        if(getCurrentView() == 2)
        {
            Transform tape = wall2obj.gameObject.GetComponent<Transform>().Find("tape");
            if (tapeTaken == true)
            {
                tape.gameObject.SetActive(false);
            }
        }

    }
    public void checkChimes()
    {
        Transform chimes = wall3obj.gameObject.GetComponent<Transform>().Find("chimes");
        Transform lockPick = wall3obj.gameObject.GetComponent<Transform>().Find("lockpick");
        if (chimesWon == true)
        {
            chimes.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            lockPick.gameObject.SetActive(true);
            if(pickTaken == true)
            {
                lockPick.gameObject.SetActive(false);
            }
        }
        else
        {
            chimes.gameObject.SetActive(true);
            lockPick.gameObject.SetActive(false);
            
        }
    }
    public void checkGrill()
    {
        Transform grill = wall1obj.gameObject.GetComponent<Transform>().Find("grill");
        Transform trowel = wall1obj.gameObject.GetComponent<Transform>().Find("brokenTrowel");
        if(grillOpen == false)
        {
            trowel.gameObject.SetActive(false);
        }
        else//if grill open is true
        {
            grill.gameObject.GetComponent<SpriteRenderer>().sprite = openGrill;
            grill.gameObject.GetComponent<addToInventory>().clickable = false;
            grill.gameObject.GetComponent<addToInventory>().realSprite = openGrill;
            //grill.gameObject.SetActive(false);
            if(trowelTaken == true)
            {
                trowel.gameObject.SetActive(false);
            }
            else
            {
                trowel.gameObject.SetActive(true);
            }

        }
    }
    public int getCurrentView()
    {
        if (wall1.GetComponent<SpriteRenderer>().enabled == true)
        {
            return 1;

        }
        else if (wall2.GetComponent<SpriteRenderer>().enabled == true)
        {
            return 2;

        }
        else if (wall3.GetComponent<SpriteRenderer>().enabled == true)
        {
            return 3;
        }
        return 0;
    }

    public void runTransition()
    {
        StartCoroutine("fadeBlack");
    }
    public IEnumerator fadeBlack()
    {
        //Debug.Log("fadeblack");
        Color black = transition.GetComponent<SpriteRenderer>().color;
        double fadeAmount;
        while (transition.GetComponent<SpriteRenderer>().color.a < 1)
        {
            fadeAmount = black.a + (2 * Time.deltaTime); ;
            black = new Color(black.r, black.g, black.b, (float)(fadeAmount));
            transition.GetComponent<SpriteRenderer>().color = black;


        }
        StartCoroutine("fadeOut");
        yield return null;
    }
    public IEnumerator fadeOut()
    {
        Color black = transition.GetComponent<SpriteRenderer>().color;
        double fadeAmount;
        while (transition.GetComponent<SpriteRenderer>().color.a > 0)
        {
            fadeAmount = black.a - (2 * Time.deltaTime); ;
            black = new Color(black.r, black.g, black.b, (float)(fadeAmount));
            transition.GetComponent<SpriteRenderer>().color = black;
            yield return null;
        }
    }
    public void test()
    {
        Debug.Log("test");
    }


}
