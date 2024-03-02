using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn;
using UnityEngine.UI;
using TMPro;

namespace Yarn.Unity
{
    public class Room2DialogueHandler : MonoBehaviour
    {
        bool lockVisited;
        bool graveVisited = false;
        int mirrorRoll = 2;
        public DialogueRunner dr;
        InMemoryVariableStorage vs;
        public List<GameObject> disabledDuringDialogue;
        public bool isDialogueActive;
        public bool allowClicks = true;
        public Animator spriteAnimator;
        // Start is called before the first frame update
        void Start()
        {
            vs = GameObject.FindObjectOfType<InMemoryVariableStorage>();
            dr.AddCommandHandler<string>("setReese", setReese);
            //playerInventory = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<inventory>();
        }

    // Update is called once per frame
        void Update()
        {
        
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
            if(spriteName == "reeseHappy")
            {
                spriteAnimator.SetBool("reeseHappy", true);
                spriteAnimator.SetBool("reeseShocked", false);
                spriteAnimator.SetBool("reeseNeutral", false);
                spriteAnimator.SetBool("reeseSad", false);
                spriteAnimator.SetBool("reeseAnguish", false);
                spriteAnimator.SetBool("reeseConfused", false);
                spriteAnimator.SetBool("reeseTired", false);
            }
            if(spriteName == "reeseConfused")
            {
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

        public void runNode(string nodeName)
        {
            dr.StartDialogue(nodeName);
        }
        public void visitMirror()
        {
            mirrorRoll = Random.Range(0, 7);
            vs.SetValue("$mirrorRoll", mirrorRoll);
            dr.StartDialogue("visitMirror");
        }

        public void visitGrill()
        {
            if (lockVisited == false)
            {
                dr.StartDialogue("visitGrill");
                vs.TryGetValue("$lockVisited", out lockVisited);
            }
        }

        public void visitGrave()
        {
            if(graveVisited == false)
            {
                dr.StartDialogue("graveVisited");
                vs.TryGetValue("$graveVisited", out graveVisited);
            }
        }

        public void pickDoor()
        {
            dr.StartDialogue("noKey");
        }
        public void disableObjects()
        {
            //THIS ISN'T WORKING MIGHT HAVE TO CHANGE TO BOX COLLIDER??
            foreach (GameObject g in disabledDuringDialogue)
            {
                //g.gameObject.GetComponent<Button>().CancelInvoke();
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
            print("allow");
            allowClicks = true;
        }
        public void disableClicks()
        {
            allowClicks = false;
        }

    }
}
