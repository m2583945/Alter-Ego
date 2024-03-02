using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class grillPuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    room2Move r2m;
    public List<GameObject> sliders = new List<GameObject>();
    int currentSlider = 0;
    Slider current;
    AudioHandler aud;

    public bool won = false;
    void Start()
    {
        r2m = GameObject.Find("SCRIPTHOLDER").GetComponent<room2Move>();
        aud = GameObject.Find("SFX").GetComponent<AudioHandler>();
        //resetGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void resetGame()
    {
        foreach (GameObject x in sliders)
        {
            slideScript slide2 = x.gameObject.GetComponent<slideScript>();
            slide2.inc = slide2.incHold;
            slide2.changeOrange();
        }

    }

    void playGame()
    {
        StartCoroutine("barBlink");
        currentSlider = 0;
    }

    public void OnMouseDown()
    {
        current = sliders[currentSlider].gameObject.GetComponent<Slider>();
        attempt(current.value);
    }

    public void attempt(float num)
    {
        Debug.Log(num);
        slideScript slide = sliders[currentSlider].gameObject.GetComponent<slideScript>();
        if (Mathf.Abs(num - slide.ideal) <= 0.05)
        {
            slide.changeGreen();
            slide.inc = 0;
            slide.stop = true;
            //if the hit is close enough, then we stop this one and move onto the next
            if(currentSlider < sliders.Count-1)
            {
                currentSlider++;
            }
            else//if the hit succeeds on the last slider, the player has won the puzzle
            {
                print("gamewon");
                StartCoroutine("winCon");
            }

        }
        else//if the hit is not close enough, the player must restart the puzzle
        {
            aud.playOnNegativeFeedback();
            slide.stop = true;
            playGame();
        }
    }
    public IEnumerator winCon()
    {
        aud.playOnPuzzleWon();
        r2m.grillOpen = true;
        yield return new WaitForSeconds(1.5f);
        this.gameObject.GetComponent<Button>().onClick.Invoke();
        
    }

    public IEnumerator barBlink()
    {
        print("current slider" + currentSlider);
        slideScript slide = sliders[currentSlider].gameObject.GetComponent<slideScript>();
        foreach (GameObject x in sliders)
        {
            slideScript slide2 = x.gameObject.GetComponent<slideScript>();
            slide2.inc = 0;
        }
        yield return new WaitForSeconds(0.5f);
        slide.changeWhite();
        yield return new WaitForSeconds(0.2f);
        slide.changeOrange();
        yield return new WaitForSeconds(0.2f);
        slide.changeWhite();
        yield return new WaitForSeconds(0.2f);
        slide.changeOrange();
        slide.inc = slide.incHold;
        foreach (GameObject x in sliders)
        {
            slideScript slide2 = x.gameObject.GetComponent<slideScript>();
            slide2.inc = slide2.incHold;
            slide2.stop = false;
            slide2.changeOrange();
        }

    }
}
