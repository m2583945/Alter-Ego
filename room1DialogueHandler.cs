using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using UnityEngine.UI;
using TMPro;

namespace Yarn.Unity
{
    public class room1DialogueHandler : MonoBehaviour
    {
        // Start is called before the first frame update
        bool holeVisited;
        bool wardrobeVisited;
        int mirrorRoll = 2;
        public DialogueRunner dr;
        InMemoryVariableStorage vs;
        public List<GameObject> disabledDuringDialogue;
        public bool isDialogueActive;
        public bool noneCrafted = false;
        public Button instructionsButton;
        public Animator spriteAnimator;

        inventory playerInventory;
        public bool allowClicks = true;
        void Start()
        {
            vs = GameObject.FindObjectOfType<InMemoryVariableStorage>();
            playerInventory = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<inventory>();
            dr.AddCommandHandler<string>("setReese", setReese);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void runNode(string nodeName)
        {
            dr.StartDialogue(nodeName);
        }

        public void visitHole()//plays upon seeing the ceiling hole for the first time
        {
            if (holeVisited == false)
            {
                dr.StartDialogue("visitHole");
                vs.TryGetValue("$holeVisited", out holeVisited);
                //holeVisited = true;
            }
        }

        public void visitMirror()
        {
            mirrorRoll = Random.Range(0, 5);
            vs.SetValue("$mirrorRoll", mirrorRoll);
            dr.StartDialogue("visitMirror");
        }

        public void visitWardrobe()
        {
            if (wardrobeVisited == false)
            {
                dr.StartDialogue("visitWardrobe");
                vs.TryGetValue("$wardrobeVisited", out wardrobeVisited);
            }
        }

        public void checkInventory()
        {
            print("checking inventory" + playerInventory.noneCrafted());
            if (playerInventory.noneCrafted())
            {
                print("none crafted = true");
                noneCrafted = true;
                vs.SetValue("$noneCrafted", noneCrafted);
            }
        }

        public void collectSheets()
        {
            dr.StartDialogue("collectedSheets");

        }

        public void itemFail(int num)
        {
            if (num == 0)//if player tries to use the short rope on the hole
            {
                dr.StartDialogue("tooShort");
            }
        }

        public void disableObjects()
        {
            foreach (GameObject g in disabledDuringDialogue)
            {
                g.gameObject.GetComponent<Button>().CancelInvoke();
                //g.gameObject.GetComponent<addToInventory>().enabled = false;
                //g.gameObject.SetActive(false);
                g.gameObject.GetComponent<addToInventory>().isDialogueActive = true;
                /*
                if (g.gameObject.GetComponent<BoxCollider2D>() != null)
                {
                    g.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
                else if(g.gameObject.GetComponent<PolygonCollider2D>() != null)
                {
                    g.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                }
                //g.gameObject.SetActive(false);*/
            }
        }
        public void enableObjects()
        {
            foreach (GameObject g in disabledDuringDialogue)
            {
                //g.gameObject.SetActive(true);
                //g.gameObject.GetComponent<addToInventory>().enabled = true;
                g.gameObject.GetComponent<addToInventory>().isDialogueActive = false;
                /*
                if (g.gameObject.GetComponent<BoxCollider2D>() != null)
                {
                    g.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                }
                else if (g.gameObject.GetComponent<PolygonCollider2D>() != null)
                {
                    g.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
                }*/
            }
        }
        void setReese(string spriteName)
        {
            if (spriteName == "reeseShocked")
            {
                print("switchshocked");
                spriteAnimator.SetBool("reeseShocked", true);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseSad", false);
                spriteAnimator.SetBool("reeseAnguish", false);
                spriteAnimator.SetBool("reeseHappy", false);
                spriteAnimator.SetBool("reeseConfused", false);
                spriteAnimator.SetBool("reeseTired", false);
            }
            if (spriteName == "reeseNeutral")
            {
                print("switchNeutral");
                spriteAnimator.SetBool("reeseNeutral", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseSad", false);
                spriteAnimator.SetBool("reeseAnguish", false);
                spriteAnimator.SetBool("reeseHappy", false);
                spriteAnimator.SetBool("reeseConfused", false);
                spriteAnimator.SetBool("reeseTired", false);
            }
            if (spriteName == "reeseSad")
            {
                spriteAnimator.SetBool("reeseSad", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseAnguish", false);
                spriteAnimator.SetBool("reeseHappy", false);
                spriteAnimator.SetBool("reeseConfused", false);
                spriteAnimator.SetBool("reeseTired", false);
            }
            if (spriteName == "reeseAnguish")
            {
                spriteAnimator.SetBool("reeseAnguish", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseSad", false);
                spriteAnimator.SetBool("reeseHappy", false);
                spriteAnimator.SetBool("reeseConfused", false);
                spriteAnimator.SetBool("reeseTired", false);
            }
            if (spriteName == "reeseHappy")
            {
                print("reesehappy");
                spriteAnimator.SetBool("reeseHappy", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseSad", false);
                spriteAnimator.SetBool("reeseAnguish", false);
                spriteAnimator.SetBool("reeseConfused", false);
                spriteAnimator.SetBool("reeseTired", false);
            }
            if (spriteName == "reeseConfused")
            {
                print("reeseHappy");
                spriteAnimator.SetBool("reeseConfused", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseSad", false);
                spriteAnimator.SetBool("reeseAnguish", false);
                spriteAnimator.SetBool("reeseHappy", false);
                spriteAnimator.SetBool("reeseTired", false);
            }
            if (spriteName == "reeseTired")
            {
                print("reeseHappy");
                spriteAnimator.SetBool("reeseTired", true);
                spriteAnimator.SetBool("reeseConfused", false);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseSad", false);
                spriteAnimator.SetBool("reeseAnguish", false);
                spriteAnimator.SetBool("reeseHappy", false);
            }
        }
        public void isActive()
        {
            isDialogueActive = true;
        }
        public void notActive()
        {
            isDialogueActive = false;
        }

        public void allowObjectClicks()
        {
            allowClicks = true;
        }
        public void disableClicks()
        {
            allowClicks = false;
        }

        [YarnCommand("tutorial")]
        public void instruct()
        {
            instructionsButton.onClick.Invoke();
        }
    }
}