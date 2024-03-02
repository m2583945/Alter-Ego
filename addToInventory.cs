using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;
using UnityEngine.EventSystems;


public class addToInventory : MonoBehaviour
{
    inventory playerInventory;
    
    roomMove rm;
    combine cb;
    sceneLoader sl;
    room2Move r2m;
    public Button closeButton;

    public bool isDialogueActive = false;
    room1DialogueHandler dh;
    Room2DialogueHandler d2h;

    public Sprite switchSprite;
    public Sprite realSprite;
    public bool clickable = false;

    public bool instructionsOpen = false;
    instructionsAndSettings ias;

    
    //public Button backButton;

    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<inventory>();
        

        cb = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<combine>();
        sl = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<sceneLoader>();
        ias = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<instructionsAndSettings>();
        if (sl.getCurrentScene() == "Room1")
        {
            rm = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<roomMove>();
            dh = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<room1DialogueHandler>();
        }
        else if (sl.getCurrentScene() == "Room2")
        {
            r2m = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<room2Move>();
            d2h = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<Room2DialogueHandler>();
        }
        realSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseOver()
    {
        //this.gameObject.GetComponent<SpriteRenderer>().color = new Color(88,87,171);
        if(clickable == true)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = switchSprite;
        }

    }
    public void OnMouseExit()
    {
        // print("mouseexit");
        //this.gameObject.GetComponent<SpriteRenderer>().color = new Color(131, 138, 204);
        this.gameObject.GetComponent<SpriteRenderer>().sprite = realSprite;
    }
    public void OnMouseDown()
    {
        print(gameObject.name);
        if (EventSystem.current.IsPointerOverGameObject() && ias.instructionsOpen == true)
        {
            return;

        }
        else
        {
            //destroy object, add to inventory array
            if (this.gameObject.name == "wardrobe1" || this.gameObject.name == "wardrobe2"
                || this.gameObject.name == "chimes" || this.gameObject.name == "mirror" || this.gameObject.name == "window"
                || (this.gameObject.name.Length > 5 && this.gameObject.name.Substring(0, 6) == "chime-"))

            {
                if (isDialogueActive == false)
                {
                    this.gameObject.GetComponent<Button>().onClick.Invoke();
                }
                else
                {
                    print("dialogueactive");
                }
                //closeButton.onClick.Invoke();

                //backButton.gameObject.SetActive(true);
            }
            else if (this.gameObject.name == "ceilinghole")
            {

                if (cb.currentCursor == "longrope" || cb.currentCursor == "rope1")
                {
                    dh.runNode("noHook");
                }
                if (cb.currentCursor == "bedsheetHook")
                {
                    dh.runNode("tooShort");
                }
                if (cb.currentCursor == "finalRope")
                {
                    print("done");
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    cb.currentCursor = "null";
                    sl.loadScene(3);//CORRECT FOR FINAL
                                    //sl.loadScene(4); //temporary for playtest
                }

            }
            else if (this.gameObject.name == "bedsheets")
            {
                if (dh.isDialogueActive == false)
                {
                    this.gameObject.GetComponent<Button>().onClick.Invoke();
                    string name = this.gameObject.name;
                    playerInventory.addItem(name);
                    this.gameObject.SetActive(false);
                    rm.sheetTaken = true;
                }
                if (playerInventory.noneCrafted())
                {
                    dh.runNode("noneCrafted");
                }

            }
            else if (this.gameObject.name == "grill")
            {
                if (d2h.isDialogueActive == false)
                {
                    this.gameObject.GetComponent<Button>().onClick.Invoke();
                }

            }
            else if (this.gameObject.name == "lock")
            {
                if (cb.currentCursor == "lockpick")
                {
                    this.gameObject.GetComponent<Button>().onClick.Invoke();
                }
                else
                {
                    d2h.runNode("lockVisited");
                }
            }
            else if (this.gameObject.name == "grave")
            {

                //this.gameObject.GetComponent<Button>().onClick.Invoke();
                if (cb.currentCursor == "fixedTrowel")
                {
                    this.gameObject.GetComponent<Button>().onClick.Invoke();
                    d2h.runNode("keyFound");
                }
                if (cb.currentCursor == "brokenTrowel")
                {
                    d2h.runNode("brokenTrowel");
                }
                if (cb.currentCursor == "null")
                {
                    d2h.runNode("graveVisited");
                }

            }
            else if (this.gameObject.name == "door")
            {
                if (cb.currentCursor == "key")
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    cb.currentCursor = "null";
                    this.gameObject.GetComponent<Button>().onClick.Invoke();

                }
                if (cb.currentCursor == "lockpick")
                {
                    d2h.pickDoor();
                }
                else
                {
                    d2h.runNode("needKey");
                }
            }
            else if(this.gameObject.name == "deselect")
            {
                //print("name deselect");
                if(cb.currentCursor != null)
                {
                    //print("invoking");
                    this.gameObject.GetComponent<Button>().onClick.Invoke();
                }
            }
            else
            {
                if (this.gameObject.name == "dress1" || this.gameObject.name == "dress2")
                {
                    //check the player inventory & see if the player has collected every item without crafting it
                    //if so, play an additional line of dialogue suggesting that the player combines items
                    rm.dressTaken = true;
                    playerInventory.addItem("dress");
                    playerInventory.addItem("hanger");
                    dh.runNode("collectedDress");
                    dh.checkInventory();
                    this.gameObject.SetActive(false);
                    return;
                }
                if (this.gameObject.name == "tape")
                {
                    r2m.tapeTaken = true;
                    playerInventory.addItem("tape");
                    this.gameObject.SetActive(false);
                    return;
                }
                if (this.gameObject.name == "lockpick")
                {
                    //print("lockpickfound");
                    if (playerInventory.isInInventory("lockpick") == false)
                    {
                        //print("lockpickfound2");
                        d2h.runNode("getPick");
                        //print("lockpickfound3");
                        r2m.pickTaken = true;
                        //playerInventory.addItem("lockpick");
                        //this.gameObject.SetActive(false);
                    }

                }
                if (this.gameObject.name == "brokenTrowel")
                {
                    r2m.trowelTaken = true;
                    d2h.runNode("getTrowel");
                    playerInventory.addItem("brokenTrowel");
                    this.gameObject.SetActive(false);
                }
                else
                {
                    string name = this.gameObject.name;
                    playerInventory.addItem(name);
                    this.gameObject.SetActive(false);
                }

            }


        }
    }


}
