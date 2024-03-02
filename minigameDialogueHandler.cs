using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using UnityEngine.UI;
using TMPro;

namespace Yarn.Unity
{
    public class minigameDialogueHandler : MonoBehaviour
    {
        public DialogueRunner dr;
        InMemoryVariableStorage vs;
        public Animator spriteAnimator;
        public Animator fightAnimator;
        bool minigameStarted = false;
        GameObject transition;

        public Slider winBar;
        public GameObject gameOverPanel;
        public Image reeseHead;
        public Image winonaHead;
        AudioSource sfx;
        AudioSource music;
        bool hasLost = false;
        //float musicVolume;

        sceneLoader sl;
        dontdestroyonload dontdestroy;

        // Start is called before the first frame update
        void Start()
        {
            dontdestroy = GameObject.Find("DONTDESTROYONLOAD").gameObject.GetComponent<dontdestroyonload>();
            transition = GameObject.Find("transition");
            sfx = GameObject.Find("SFX").GetComponent<AudioSource>();
            music = GameObject.Find("music").GetComponent<AudioSource>();
            sfx.volume = dontdestroy.sfxVolume;
            music.volume = dontdestroy.musicVolume;
            //musicVolume = music.volume;

            vs = GameObject.FindObjectOfType<InMemoryVariableStorage>();
            dr.AddCommandHandler<string>("setReese", setReese);
            dr.AddCommandHandler("startMinigame", startMinigame);

            sl = GameObject.Find("SCRIPTHOLDER").GetComponent<sceneLoader>();
        }

        // Update is called once per frame
        void Update()
        {
            if(minigameStarted)
            {
                winBar.value -= 0.5f + Time.deltaTime;
                fightAnimator.SetInteger("fightValue", (int)(winBar.value));
                //reese and winona fight
                if (Input.GetMouseButtonDown(0))
                {
                    print("audio source is playing: " + sfx.isPlaying);
                    winBar.value += 6;
                    print("clicked");
                    
                    //if right clicked, do something
                    //minigame started might have to change in a coroutine to make sure winona has time to finish the animation

                }
                if (winBar.value >= 99)//if we win
                {
                    sl.loadScene(7);
                }
                if (winBar.value <= 2)//if we lose
                {
                    playOnFailure();
                    StartCoroutine("fadeBlack");
                }
            }
        }
        public void runNode(string nodeName)
        {
            dr.StartDialogue(nodeName);
        }

        void setReese(string spriteName)
        {
            if(spriteName == "reeseShocked")
            {
                print("switchshocked");
                spriteAnimator.SetBool("reeseShocked", true);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseSad", false);
                spriteAnimator.SetBool("reeseAnguish", false);
                spriteAnimator.SetBool("winonaMad", false);
            }
            if(spriteName == "reeseNeutral")
            {
                print("switchNeutral");
                spriteAnimator.SetBool("reeseNeutral", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseSad", false);
                spriteAnimator.SetBool("reeseAnguish", false);
            }
            if(spriteName == "reeseSad")
            {
                spriteAnimator.SetBool("reeseSad", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseAnguish", false);
            }
            if(spriteName == "reeseAnguish")
            {
                spriteAnimator.SetBool("reeseAnguish", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseSad", false);
            }
            if (spriteName == "winonaMad")
            {
                spriteAnimator.SetBool("winonaMad", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseSad", false);
            }
        }

        void startMinigame()
        {
            //winonaAnimator.SetTrigger("winonaMove");
            winBar.gameObject.SetActive(true);
            minigameStarted = true;
            reeseHead.gameObject.SetActive(true);
            winonaHead.gameObject.SetActive(true);
        }

        void playOnFailure()
        {
            if(hasLost == false)
            {
                print("playing1");
                sfx.Play();
                print("playing2");
                print(sfx.isPlaying);
                hasLost = true;
            }

        }
        public IEnumerator fadeBlack()
        {
            //sfx.Play();
            playOnFailure();
            winonaHead.gameObject.SetActive(false);
            reeseHead.gameObject.SetActive(false);
            Color black = transition.GetComponent<SpriteRenderer>().color;
            double fadeAmount;
            while (transition.GetComponent<SpriteRenderer>().color.a < 1)
            {
                fadeAmount = black.a + (2 * Time.deltaTime); ;
                black = new Color(black.r, black.g, black.b, (float)(fadeAmount));
                transition.GetComponent<SpriteRenderer>().color = black;


            }
            //yield return new WaitForSeconds(0.5f);
            gameOverPanel.SetActive(true);
            yield return null;
        }
    }
}

