using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    //room 1
    public Sprite scissorsSprite;
    public Sprite bedsheetSprite;
    public Sprite dressSprite;
    public Sprite hangerSprite;

    public Sprite ropeSprite;

    public Sprite bedsheetHook;
    public Sprite longRope;
    public Sprite grapplingHook;

    //room2
    public Sprite tapeSprite;
    public Sprite lockPickSprite;
    public Sprite brokenTrowel;
    public Sprite fixedTrowel;
    public Sprite key;

    animations anim;
    public RuntimeAnimatorController bedsheetAnimator;
    public RuntimeAnimatorController rope1Animator;
    public RuntimeAnimatorController hangerAnimator;
    public RuntimeAnimatorController longRopeAnimator;
    public RuntimeAnimatorController grapplingHookAnimator;
    public RuntimeAnimatorController sheetHookAnimator;
    public RuntimeAnimatorController dressAnimator;
    //public Sprite blank;

    public List<inventorySlot> inventoryList = new List<inventorySlot>();
    public List<Image> slotList;
    // Start is called before the first frame update
    void Start()
    {
        inventoryList.Add(new inventorySlot(true, false, "scissors") { });
        for(int x = 0; x < 5; x++)
        {
            inventoryList.Add(new inventorySlot(false, false, "none"));
        }
        anim = GameObject.Find("openinventory").gameObject.GetComponent<animations>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public class inventorySlot
    {
        public bool isFilled;
        //public Image item;
        public bool isSelected;
        public string itemName;


        public inventorySlot(bool filled,  bool selected, string name)
        {
            isFilled = filled;
            //item = itemSprite;
            isSelected = selected;
            itemName = name;
        }

    }
    public void addItem(string name)
    {
        for(int i = 0; i < inventoryList.Count; i++)
        {
            if(inventoryList[i].isFilled == false)
            {
                //if list slot is not filled, fill it and assign the name!
                inventoryList[i].isFilled = true;
                inventoryList[i].itemName = name;
                spriteSwap(name, i);
                //anim.openInventory();
                return;
            }
        }
    }
    public void spriteSwap(string name, int index)
    {
        print(name + index);
        if(name == "bedsheets")
        {
            //slotList[index].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
            slotList[index].sprite = bedsheetSprite;
            slotList[index].GetComponent<Image>().color = new Color(255,255,255);
            slotList[index].GetComponent<Animator>().runtimeAnimatorController = bedsheetAnimator;
        }
        if(name == "rope1")
        {
            print("switch1");
            //print(slotList[index].GetComponent<Image>().sprite.name);
            slotList[index].GetComponent<Image>().sprite = ropeSprite;
            slotList[index].GetComponent<Animator>().runtimeAnimatorController = rope1Animator;
            slotList[index].sprite = ropeSprite;

        }
        if(name == "dress")
        {
            slotList[index].sprite = dressSprite;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
            slotList[index].GetComponent<Animator>().runtimeAnimatorController = dressAnimator;

        }
        if(name == "hanger")
        {
            slotList[index].sprite = hangerSprite;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
            slotList[index].GetComponent<Animator>().runtimeAnimatorController = hangerAnimator;
        }
        if (name == "bedsheetHook")
        {
            slotList[index].sprite = bedsheetHook;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
            slotList[index].GetComponent<Animator>().runtimeAnimatorController = sheetHookAnimator;
        }
        if (name == "longrope")
        {
            slotList[index].sprite = longRope;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
            slotList[index].GetComponent<Animator>().runtimeAnimatorController = longRopeAnimator;
        }
        if(name == "finalRope")
        {
            slotList[index].sprite = grapplingHook;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
            slotList[index].GetComponent<Animator>().runtimeAnimatorController = grapplingHookAnimator;
        }
        if (name == "tape")
        {
            slotList[index].sprite = tapeSprite;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        if (name == "lockpick")
        {
            slotList[index].sprite = lockPickSprite;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        if(name == "brokenTrowel")
        {
            slotList[index].sprite = brokenTrowel;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        if(name == "fixedTrowel")
        {
            slotList[index].sprite = fixedTrowel;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        if(name == "key")
        {
            slotList[index].sprite = key;
            slotList[index].GetComponent<Image>().color = new Color(255, 255, 255);
        }
        if (name == "none")
        {
            //slotList[index].sprite = longRope;
            print(inventoryList[index].itemName);
            slotList[index].GetComponent<Image>().color = new Color(1f, 1f, 1f, 0) ;
        }

    }

    public bool isInInventory(string name)
    {
        for (int i = 0; i < inventoryList.Count; i++)
        {
            if(inventoryList[i].itemName == name)
            {
                return true;
            }
        }
        return false;
    }

    public bool noneCrafted()
    {
        print("bedsheets" + isInInventory("bedsheets"));
        print("dress" + isInInventory("dress"));
        print("hanger" + isInInventory("hanger"));
        if(isInInventory("bedsheets") && isInInventory("dress") && isInInventory("hanger"))
        {
            print("none crafted");
            return true;

        }
        return false;
    }

}
