using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animations : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    public Button openButton;
    public Button closeButton;

    combine combineInstance;
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        // openButton = GameObject.Find("OPEN").gameObject.GetComponent<Button>() ;
        //closeButton = GameObject.Find("CLOSE").gameObject.GetComponent<Button>();
        combineInstance = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<combine>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openInventory()
    {
        anim.SetBool("open", true);
        closeButton.gameObject.SetActive(true);
        openButton.gameObject.SetActive(false);
    }
    public void closeInventory()
    {
        anim.SetBool("open", false);
        openButton.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(false);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        combineInstance.currentCursor = "null";
    }
}
