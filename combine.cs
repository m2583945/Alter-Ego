using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class combine : MonoBehaviour
{
    //room1
    public Texture2D knifeTexture;
    public Texture2D bedsheetTexture;
    public Texture2D sheetsRopeTexture;
    public Texture2D dressTexture;
    public Texture2D hangerTexture;
    public Texture2D longRopeTexture;
    public Texture2D bedsheetHookTexture;
    public Texture2D finalRope;

    //room2
    public Texture2D tapeTexture;
    public Texture2D lockpickTexture;
    public Texture2D brokenTrowel;
    public Texture2D fixedTrowel;
    public Texture2D keyTexture;

    inventory playerInventory;
    public string currentCursor = "null";

    public int currentSlot = 0;
    room1DialogueHandler dh;
    Room2DialogueHandler d2h;

    sceneLoader sl;
    AudioHandler ah;

    public GameObject itemOutline;
    public List<GameObject> itemPositions;


    // Start is called before the first frame update
    void Start()
    {
        playerInventory = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<inventory>();
        dh = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<room1DialogueHandler>();
        sl = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<sceneLoader>();
        ah = GameObject.Find("SFX").gameObject.GetComponent<AudioHandler>();
        if (sl.getCurrentScene() == "Room1")
        {
            dh = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<room1DialogueHandler>();
        }
        else if (sl.getCurrentScene() == "Room2")
        {
            d2h = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<Room2DialogueHandler>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectItem(int slotNum)
    {
        List<inventory.inventorySlot> inventoryList = playerInventory.inventoryList;
        string inventoryContent = "";
        for(int x = 0; x < inventoryList.Count; x++)
        {
            inventoryContent += inventoryList[x].itemName + ",";
        }
        print("zzz" + inventoryList[slotNum].itemName + currentCursor);
        showSelectedItem(slotNum);
        print(inventoryContent);
        //Debug.Log("current cursor: " + currentCursor + " current item: " + inventoryList[currentSlot].itemName + " just clicked: " + inventoryList[slotNum]);
        if (currentCursor == "null")//if no item is selected already
        {
            string itemName = inventoryList[slotNum].itemName;
            
            currentCursor = itemName;
            if (itemName == "scissors")
            {
                Cursor.SetCursor(knifeTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "bedsheets")
            {
                Cursor.SetCursor(bedsheetTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "rope1")
            {
                Cursor.SetCursor(sheetsRopeTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "dress")
            {
                Cursor.SetCursor(dressTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "hanger")
            {
                Cursor.SetCursor(hangerTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "longrope")
            {
                Cursor.SetCursor(longRopeTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "bedsheetHook")
            {
                Cursor.SetCursor(bedsheetHookTexture, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "finalRope")
            {
                Cursor.SetCursor(finalRope, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "tape")
            {
                Cursor.SetCursor(tapeTexture, Vector2.zero, CursorMode.Auto);
            }
            else if(itemName == "lockpick")
            {
                Cursor.SetCursor(lockpickTexture, Vector2.zero, CursorMode.Auto);
            }
            else if(itemName == "brokenTrowel")
            {
                Cursor.SetCursor(brokenTrowel, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "fixedTrowel")
            {
                Cursor.SetCursor(fixedTrowel, Vector2.zero, CursorMode.Auto);
            }
            else if (itemName == "key")
            {
                Cursor.SetCursor(keyTexture, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                currentCursor = "null";
            }
        }
        else //if an item is selected
        {
            print("zzz" + inventoryList[slotNum].itemName + currentCursor);
            if (inventoryList[slotNum].itemName == "none")//if player clicks on a blank space with an item selected
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);//reset the cursor
                currentCursor = "null";
            }
            if (currentCursor == "scissors")
            {
                if (inventoryList[slotNum].itemName == "bedsheets")//scissors clicking on sheets
                {
                    print("scizz");
                    ah.playOnCombine();
                    inventoryList[slotNum].itemName = "rope1";
                    playerInventory.spriteSwap("rope1", slotNum);//we are just swapping the sprite, not the cursor
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    currentCursor = "null";
                    print(inventoryContent);
                }
                else
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    currentCursor = "null";
                }
            }
            else if (currentCursor == "bedsheets")
            {
                if(inventoryList[slotNum].itemName == "scissors")//sheets clicking on scissors
                {
                    ah.playOnCombine();
                    int newNum = getIndexFromName("bedsheets");
                    inventoryList[newNum].itemName = "rope1";
                    playerInventory.spriteSwap("rope1", newNum);
                    print(inventoryContent);
                }
            }
            else if (currentCursor == "rope1")//rope1 clicking on dress or hanger
            {
                int currentSlot = getIndexFromName("rope1");
                if (inventoryList[slotNum].itemName == "dress")
                {
                    combineItems(currentSlot, slotNum, "longrope");
                    dh.runNode("combiningDress");
                }
                else if (inventoryList[slotNum].itemName == "hanger")
                {
                    combineItems(currentSlot, slotNum, "bedsheetHook");
                }
                else
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    currentCursor = "null";
                }
                currentSlot = slotNum;
            }
            else if (currentCursor == "dress")//dress clicking on rope1 or bedsheethook
            {
                int currentSlot = getIndexFromName("dress");
                if (inventoryList[slotNum].itemName == "rope1")
                {
                    dh.runNode("combiningDress");
                    combineItems(currentSlot, slotNum, "longrope");
                }
                else if (inventoryList[slotNum].itemName == "bedsheetHook")
                {
                    dh.runNode("combiningDress");
                    combineItems(currentSlot, slotNum, "finalRope");
                }
                else
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    currentCursor = "null";
                }
                currentSlot = slotNum;
            }
            else if (currentCursor == "hanger")//hanger clicking on rope1 or long rope
            {
                int currentSlot = getIndexFromName("hanger");
                if (inventoryList[slotNum].itemName == "rope1")
                {
                    combineItems(currentSlot, slotNum, "bedsheetHook");
                }
                else if (inventoryList[slotNum].itemName == "longrope")
                {
                    dh.runNode("hookDone");
                    combineItems(currentSlot, slotNum, "finalRope");
                }
                else
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    currentCursor = "null";
                }
                currentSlot = slotNum;
            }
            else if (currentCursor == "bedsheetHook")//bedsheethook clicking on dress
            {
                int currentSlot = getIndexFromName("bedsheetHook");
                if (inventoryList[slotNum].itemName == "dress")//bedsheet hook + dress
                {
                    dh.runNode("combiningDress");
                    combineItems(currentSlot, slotNum, "finalRope");
                }
                else
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    currentCursor = "null";
                }
            }
            else if (currentCursor == "longrope")//longrope clicking on hanger
            {
                int currentSlot = getIndexFromName("longrope");
                if (inventoryList[slotNum].itemName == "hanger")//bedsheet/dress + hook
                {
                    dh.runNode("hookDone");
                    combineItems(currentSlot, slotNum, "finalRope");
                }
                else
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    currentCursor = "null";
                }
            }
            else if(currentCursor == "tape")
            {
                int currentSlot = getIndexFromName("tape");
                if (inventoryList[slotNum].itemName == "brokenTrowel")//tape + broken trowel
                {
                    d2h.runNode("fixedTrowel");
                    combineItems(currentSlot, slotNum, "fixedTrowel");
                }
                else
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    currentCursor = "null";
                }
            }
            else if (currentCursor == "brokenTrowel")
            {
                int currentSlot = getIndexFromName("brokenTrowel");
                if (inventoryList[slotNum].itemName == "tape")//broken trowel + tape
                {
                    d2h.runNode("fixedTrowel");
                    combineItems(currentSlot, slotNum, "fixedTrowel");
                }
                else
                {
                    Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                    currentCursor = "null";
                }
            }
            else
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
                currentCursor = "null";
            }
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            currentCursor = "null";
            currentSlot = slotNum;
        }
        }
    public void combineItems(int first, int second, string newItem)
    {
        List<inventory.inventorySlot> inventoryList = playerInventory.inventoryList;
        ah.playOnCombine();
        if (first < second)
        {
            inventoryList[second] = new inventory.inventorySlot(false, false, "none");
            inventoryList[first].itemName = newItem;

            playerInventory.spriteSwap("none", second);
            playerInventory.spriteSwap(newItem, first);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            currentCursor = "null";
        }
        else
        {
            inventoryList[first] = new inventory.inventorySlot(false, false, "none");
            inventoryList[second].itemName = newItem;

            playerInventory.spriteSwap("none", first);
            playerInventory.spriteSwap(newItem, second);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            currentCursor = "null";
        }
    }
    public int getIndexFromName(string name)
    {
        List<inventory.inventorySlot> inventoryList = playerInventory.inventoryList;
        for (int x = 0; x < 4; x++)
        {
            if (inventoryList[x].itemName == name)
            {
                return x;
            }
        }
        return 0;
    }

    public void showSelectedItem(int num)
    {
        itemOutline.gameObject.SetActive(true);
        itemOutline.transform.position = itemPositions[num].gameObject.transform.position;
    }

    public void selectNone()
    {
        print("deselected");
        itemOutline.gameObject.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        currentCursor = "null";
    }
}
