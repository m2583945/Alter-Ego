using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roomMove : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject wall1;
    GameObject wall2;
    GameObject wall3;
    //GameObject wall4;
    GameObject ceiling;
    GameObject transition;
    GameObject wardrobeZoom;

    public GameObject bedobj;
    public GameObject bedsheet;
    public GameObject wardrobeobj1;
    public GameObject wardrobeobj2;
    public GameObject ceilingobj;

    public GameObject wall1obj;
    public GameObject wall2obj;
    public GameObject wall3obj;

    public GameObject squarePuzzle;

    public bool wardrobeWon = false;
    public bool dressTaken = false;
    public bool sheetTaken = false;
    public Sprite openWardrobe;
    

    void Start()
    {
        wall1 = GameObject.Find("wall1");
        wall2 = GameObject.Find("wall2");
        wall3 = GameObject.Find("wall3");
        //wall4 = GameObject.Find("wall4");

        wardrobeZoom = GameObject.Find("wardrobepuzzle");
        
        ceiling = GameObject.Find("ceiling");
        transition = GameObject.Find("transition");
        StartCoroutine("fadeBlack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void loadBedsheet()
    {
        if(sheetTaken == true)
        {
            bedsheet.SetActive(false);
        }
        else
        {
            bedsheet.SetActive(true);
        }
    }
    public void moveUp()
    {
        StartCoroutine("fadeBlack");
        ceiling.GetComponent<SpriteRenderer>().enabled = true;
        ceilingobj.gameObject.SetActive(true);
        if(getCurrentView() == 1)
        {
            wall1obj.SetActive(false);
        }
        else if(getCurrentView() == 2)
        {
            wall2obj.SetActive(false);
        }
        else if(getCurrentView() == 3)
        {
            wall3obj.SetActive(false);
        }
    }
    public void moveDown()
    {
        StartCoroutine("fadeBlack");
        ceiling.GetComponent<SpriteRenderer>().enabled = false;
        ceilingobj.gameObject.SetActive(false);
        if (getCurrentView() == 1)
        {
            wall1obj.SetActive(true);
        }
        else if (getCurrentView() == 2)
        {
            wall2obj.SetActive(true);
        }
        else if (getCurrentView() == 3)
        {
            wall3obj.SetActive(true);
        }
    }

    public void moveLeft()
    {
        if(wall1.GetComponent<SpriteRenderer>().enabled == true)
        {
            StartCoroutine("fadeBlack");
            wall1.GetComponent<SpriteRenderer>().enabled = false;
            wall3.GetComponent<SpriteRenderer>().enabled = true;
            bedobj.SetActive(false);
            showWardrobeState(3);
        }
        else if (wall2.GetComponent<SpriteRenderer>().enabled == true)
        {
            StartCoroutine("fadeBlack");
            wall2.GetComponent<SpriteRenderer>().enabled = false;
            wall1.GetComponent<SpriteRenderer>().enabled = true;
            bedobj.SetActive(true);
            showWardrobeState(1);
            
        }
        else if (wall3.GetComponent<SpriteRenderer>().enabled == true)
        {
            StartCoroutine("fadeBlack");
            wall3.GetComponent<SpriteRenderer>().enabled = false;
            wall2.GetComponent<SpriteRenderer>().enabled = true;
            showWardrobeState(2);
        }
    }
    public void moveRight()
    {
        if (wall1.GetComponent<SpriteRenderer>().enabled == true)
        {
            StartCoroutine("fadeBlack");
            wall1.GetComponent<SpriteRenderer>().enabled = false;
            wall2.GetComponent<SpriteRenderer>().enabled = true;
            bedobj.SetActive(false);
            showWardrobeState(2);
            
        }
        else if (wall2.GetComponent<SpriteRenderer>().enabled == true)
        {
            StartCoroutine("fadeBlack");
            wall2.GetComponent<SpriteRenderer>().enabled = false;
            wall3.GetComponent<SpriteRenderer>().enabled = true;
            //bedobj.SetActive(false);
            //wardrobeobj.SetActive(true);
            showWardrobeState(3);
            

        }
        else if (wall3.GetComponent<SpriteRenderer>().enabled == true)
        {
            StartCoroutine("fadeBlack");
            wall3.GetComponent<SpriteRenderer>().enabled = false;
            wall1.GetComponent<SpriteRenderer>().enabled = true;
            bedobj.SetActive(true);
            showWardrobeState(1);
        }
    }
    public void activateWardrobe() //activates the puzzle
    {
        StartCoroutine("fadeBlack");
        wardrobeZoom.GetComponent<SpriteRenderer>().enabled = true;
        showWardrobeState(0);
        squarePuzzle.SetActive(true);
    }

    public void deactivateWardrobe()//deactivates the wardrobe puzzle
    {
        StartCoroutine("fadeBlack");
        wardrobeZoom.GetComponent<SpriteRenderer>().enabled = false;
        print("deactivated");
        showWardrobeState(getCurrentView());
        squarePuzzle.SetActive(false);
    }
    public void showWardrobeState(int viewNum)//view num should be the view number that the player is moving to
    {
        //print("wardrobewon: " + wardrobeWon);
        if (viewNum == 2)
        {
            wardrobeobj1.SetActive(true);
            wardrobeobj2.SetActive(false);
        }
        else if(viewNum == 3)
        {
            wardrobeobj2.SetActive(true);
            wardrobeobj1.SetActive(false);
        }
        else
        {
            wardrobeobj2.SetActive(false);
            wardrobeobj1.SetActive(false);
        }
        if(wardrobeWon == true)//if the player has won, deactivate the wardrobe
        {
            if(getCurrentView() == 2)
            {
                Transform wardrobe = wardrobeobj1.gameObject.GetComponent<Transform>().Find("wardrobe1");
                wardrobe.gameObject.GetComponent<SpriteRenderer>().sprite = openWardrobe;
                wardrobe.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                //wardrobe.gameObject.SetActive(false);
                Transform dress = wardrobeobj1.gameObject.GetComponent<Transform>().Find("dress1");
                dress.gameObject.SetActive(false);
            }
            if (getCurrentView() == 3)
            {
                Transform wardrobe = wardrobeobj2.gameObject.GetComponent<Transform>().Find("wardrobe2");
                //wardrobe.gameObject.SetActive(false);
                wardrobe.gameObject.GetComponent<SpriteRenderer>().sprite = openWardrobe;
                wardrobe.gameObject.GetComponent<SpriteRenderer>().flipX = true;
                wardrobe.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                Transform dress = wardrobeobj2.gameObject.GetComponent<Transform>().Find("dress2");
                dress.gameObject.SetActive(false);
            }
        }
        if(dressTaken == false && wardrobeWon == true)//if the player has won but not taken the dress, show it
        {
            if(wardrobeobj1.gameObject.GetComponent<Transform>().Find("dress1") != null)
            {
                Transform dress = wardrobeobj1.gameObject.GetComponent<Transform>().Find("dress1");
                dress.gameObject.SetActive(true);
            }
            if(wardrobeobj2.gameObject.GetComponent<Transform>().Find("dress2") != null)
            {
                Transform dress = wardrobeobj2.gameObject.GetComponent<Transform>().Find("dress2");
                dress.gameObject.SetActive(true);
            }        
        }
        else if(dressTaken == false && wardrobeWon == false) //if the player has not won, don't show the dress
        {
            if (wardrobeobj1.gameObject.GetComponent<Transform>().Find("dress1") != null)
            {
                Transform dress = wardrobeobj1.gameObject.GetComponent<Transform>().Find("dress1");
                dress.gameObject.SetActive(false);
                Transform wardrobe = wardrobeobj1.gameObject.GetComponent<Transform>().Find("wardrobe1");
                wardrobe.gameObject.SetActive(true);
            }
            if (wardrobeobj2.gameObject.GetComponent<Transform>().Find("dress2") != null)
            {
                Transform dress = wardrobeobj2.gameObject.GetComponent<Transform>().Find("dress2");
                dress.gameObject.SetActive(false);
                Transform wardrobe = wardrobeobj2.gameObject.GetComponent<Transform>().Find("wardrobe2");
                wardrobe.gameObject.SetActive(true);
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
}
