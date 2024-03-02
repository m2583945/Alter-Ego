using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class chimePuzzle : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> notes = new List<int>();
    int[] combo = new int[5];
    string finalCode;
    int index = 0;

    public List<GameObject> chimes = new List<GameObject>();
    public Button replay;
    public Button backButton;
    bool allowInput = false;

    AudioSource audio;

    public List<AudioClip> clips = new List<AudioClip>();

    room2Move r2m;
    Room2DialogueHandler r2dh;
    addToInventory add;

    public bool won;//debugging

    void Start()
    {
        r2m = GameObject.Find("SCRIPTHOLDER").GetComponent<room2Move>();
        r2dh = GameObject.Find("SCRIPTHOLDER").GetComponent<Room2DialogueHandler>();
        audio = this.gameObject.GetComponent<AudioSource>();
        for (int x = 0; x < combo.Length; x++)
        {
            combo[x] = Random.Range(0, 4);//returns a random int between 0 & 3

        }
        for (int y = 0; y < combo.Length; y++)
        {
            finalCode += combo[y].ToString();//makes a list of the code
        }
        print(finalCode);
        playCode();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void sendNote(int note)
    {
        if (allowInput == true)
        {
            notes.Add(note);
            StartCoroutine(playOneNote(note));
            if (notes[index] != combo[index])
            {
                wipe();
                return;
            }
            if (index == combo.Length - 1)
            {
                print("finished");
                if (notes[index] == combo[combo.Length - 1] || won == true)
                {
                    print("won");
                    StartCoroutine("winCon");
                    
                }
            }
            else
            {
                index++;
            }
        }


    }
    public IEnumerator winCon()
    {
        yield return new WaitForSeconds(0.9f);
        audio.clip = clips[5];
        audio.Play();
        yield return new WaitForSeconds(1f);
        r2m.chimesWon = true;
        backButton.onClick.Invoke();
        r2dh.runNode("chimesWon");
        r2m.checkChimes();
    }
    public IEnumerator playFullCode()
    {
        for (int x = 0; x < combo.Length; x++)
        {
            int chimeNum = combo[x];
            yield return new WaitForSeconds(0.5f);
            chimes[chimeNum].gameObject.GetComponent<Transform>().Rotate(0, 0, -10);
            audio.clip = clips[chimeNum];
            audio.Play();
            yield return new WaitForSeconds(0.5f);
            chimes[chimeNum].gameObject.GetComponent<Transform>().Rotate(0, 0, 10);
        }
        for (int y = 0; y < chimes.Count; y++)
        {
            add = chimes[y].GetComponent<addToInventory>();
            add.clickable = true;
        }
        replay.gameObject.SetActive(true);
        allowInput = true;
        yield return null;
        
    }
    public IEnumerator playOneNote(int chimeNum)
    {
        chimes[chimeNum].gameObject.GetComponent<Transform>().Rotate(0, 0, -10);
        audio.clip = clips[chimeNum];
        audio.Play();
        yield return new WaitForSeconds(0.5f);
        chimes[chimeNum].gameObject.GetComponent<Transform>().Rotate(0, 0, 10);
    }

    public void playCode()
    {
        replay.gameObject.SetActive(false);
        for (int y = 0; y < chimes.Count; y++)
        {
            add = chimes[y].GetComponent<addToInventory>();
            add.clickable = false;
        }
        StartCoroutine("playFullCode");
    }
    
    public void wipe()
    {
        print("wrong");
        notes.Clear();
        audio.clip = clips[4];
        index = 0;

        audio.Play();
        allowInput = false;
        playCode();
    }

    public void resetChimes()
    {
        for(int x = 0; x < chimes.Count; x++)
        {
            GameObject g = chimes[x];
            g.GetComponent<Transform>().rotation = Quaternion.identity;
        }
    }
}
