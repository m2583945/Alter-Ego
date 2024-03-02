using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn;

namespace Yarn.Unity
{
    public class cutscenescript : MonoBehaviour
    {

        public List<Sprite> panels;
        sceneLoader sl;
        GameObject transition;
        GameObject panel;
        public int panelCount = 0;
        int dialogueCount = 0;
        public bool allowClick = true;
        public DialogueRunner dr;
        string currentScene;

        public List<AudioClip> sounds;
        public AudioSource aud;
        public AudioSource aud2;

        dontdestroyonload dontdestroy;

        // Start is called before the first frame update
        void Start()
        {
            sl = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<sceneLoader>();
            dontdestroy = GameObject.Find("DONTDESTROYONLOAD").gameObject.GetComponent<dontdestroyonload>();
            print("found");
            transition = GameObject.Find("transition");
            panel = GameObject.Find("panel");
            StartCoroutine("fadeOut");
            currentScene = sl.getCurrentScene();
            aud.volume = dontdestroy.musicVolume;
            aud2.volume = aud.volume * 0.6f;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && allowClick == true)
            {
                panelCount++;

                Debug.Log("advance");
                StartCoroutine("fadeBlack");
                if (panelCount == panels.Count)
                {
                    advanceNextScene();
                }
                else if(currentScene == "OpeningCutscene")
                {
                    if(panelCount == 1 || panelCount == 2 || panelCount == 3)
                    {
                        aud.clip = sounds[panelCount];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if (panelCount == 4)
                    {
                        //dr.StartDialogue("Panel4");
                        aud2.gameObject.SetActive(true);
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];

                    }
                    else if (panelCount == 6)
                    {
                        aud.Pause();
                        aud2.volume *= 1.2f;
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    else if (panelCount == 7)
                    {
                        aud2.volume *= 0.5f;
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    else if(panelCount == 8)
                    {
                        aud.clip = sounds[5];
                        aud.volume *= 0.6f;
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                        aud2.Pause();
                    }
                    else if (panelCount == 9)
                    {
                        aud.volume = dontdestroy.musicVolume;
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    else if (panelCount == 13)
                    {
                        aud.volume *= 0.6f;
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    else if (panelCount == 14)
                    {
                        aud.volume = dontdestroy.musicVolume;
                        aud.clip = sounds[6];
                        aud.Play();
                        aud.loop = false;
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    else
                    {
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                }

		        else if(currentScene == "Room1WonCutscene")
                {
                    if(panelCount == 3)
                    {
                        aud.clip = sounds[1];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if(panelCount == 6)
                    {
                        aud.clip = sounds[2];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if(panelCount == 8)
                    {
                        aud.clip = sounds[3];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
		            else 
			        {
				        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
			        }
		        }
                else if(currentScene == "Room2WonCutscene")
                {
                    if(panelCount == 1)
                    {
                        print("panel1");
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if(panelCount == 2)
                    {
                        aud.clip = sounds[1];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if(panelCount == 7)
                    {
                        aud.clip = sounds[3];
                        aud2.clip = sounds[2];
                        aud.Play();
                        aud2.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if (panelCount == 10)
                    {
                        aud.clip = sounds[4];
                        aud2.Stop();
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if (panelCount == 12)
                    {
                        aud.clip = sounds[5];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if (panelCount == 14)
                    {
                        aud.clip = sounds[6];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    else
                    {
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                }
                else if(currentScene == "FinalCutscene")
                {
                    if (panelCount == 3)
                    {
                        aud.clip = sounds[1];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if (panelCount == 6)
                    {
                        aud.clip = sounds[2];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if (panelCount == 9)
                    {
                        aud.volume *= 0.6f;
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    if (panelCount == 10)
                    {
                        aud.volume = dontdestroy.musicVolume;
                        aud.clip = sounds[3];
                        aud.Play();
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                    else
                    {
                        panel.gameObject.GetComponent<SpriteRenderer>().sprite = panels[panelCount];
                    }
                }
            }
        }

        public void changeBool()
        {
            allowClick = false;

        }
        
        void advanceNextScene()
        {
            if(currentScene == "OpeningCutscene")
            {
                sl.loadScene(2);
            }
            if (currentScene == "Room1WonCutscene")
            {
                sl.loadScene(4);
            }
            if(currentScene == "Room2WonCutscene")
            {
                sl.loadScene(6);
            }
            if(currentScene == "FinalCutscene")
            {
                sl.loadScene(8);
            }


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

        public void skipPanel(int panelNum)
        {
            if(panelNum == 3)
            {
                //dr.gameObject.SetActive(false);
            }
        }

        [YarnCommand("allow")]
        public void allow()
        {
            this.gameObject.GetComponent<cutscenescript>().allowClick = true;
            print("allowed");
        }
        [YarnCommand("disallow")]
        public void disallow()
        {
            allowClick = false;
        }

    }
}
