using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tangram : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject selectedObject;
    Vector3 offset;

    public Vector3 targetPosition;
    public Vector3 current;

    public Collider targetCollider;
    public Collider currentCollider;
    public float snapDistance = 0.01f;


    public List<GameObject> tangramPieces;
    public List<GameObject> tangramPositions;

    public bool gameWon = false;
    roomMove rm;
    int count = 0;

    public Button backButton1;
    public Button backButton2;

    AudioHandler ah;

    private void Start()
    {
        rm = GameObject.Find("SCRIPTHOLDER").gameObject.GetComponent<roomMove>();
        ah = GameObject.Find("SFX").gameObject.GetComponent<AudioHandler>();
    }

    void Update()
    {
        float snapDistanceSquared = snapDistance * snapDistance;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetMouseButtonDown(1))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.gameObject.name.StartsWith("tangram"))
            {
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (Input.GetMouseButtonUp(1) && selectedObject)
        {
            selectedObject.transform.Rotate(0, 0, -45);
            selectedObject = null;
            checkWon();
        }
        if (Input.GetMouseButtonDown(0))//check for collider. if there is a collider under the mouse, select that object
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.gameObject.name.StartsWith("tangram"))
            {
                selectedObject = targetObject.transform.gameObject;
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject)
        {
            print(selectedObject.name);
            selectedObject.transform.position = mousePosition + offset;
            current = selectedObject.transform.position;

        }


        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            int indext = tangramPieces.IndexOf(selectedObject);
            float dist = (selectedObject.transform.position - tangramPositions[indext].transform.position).magnitude;
            if (dist < snapDistance)
            {
                 selectedObject.transform.position = tangramPositions[indext].transform.position;
            }
            selectedObject = null;
            checkWon();

        }
        if (gameWon == true)
        {
            StartCoroutine("winGame");
            count++;
        }
    }


    void checkWon()
    {
        int numCorrect = 0;
        for (int x = 0; x < tangramPieces.Count; x++)
        {
            if (tangramPieces[x].transform.position == tangramPositions[x].transform.position)
            {
                float z = tangramPieces[x].transform.rotation.eulerAngles.z;
                if(z <= 2 && z >= -2)
                {
                    print("tangram " + x + "is right");
                    numCorrect++;
                    //print(numCorrect);
                }

            }
        }
        if((tangramPieces[3].transform.position - tangramPositions[3].transform.position).magnitude < snapDistance)
        {
            print("rotate" + Mathf.Abs(tangramPieces[3].transform.rotation.eulerAngles.z));
            float modNum = (Mathf.Abs(tangramPieces[3].transform.rotation.eulerAngles.z) % 90);
            if (modNum - 90 < 5 && !(tangramPieces[3].transform.rotation.eulerAngles.z <= 2 && tangramPieces[3].transform.rotation.eulerAngles.z >= -2))
                //checking for rotations 90, 180, 270, & 360)
            {
                //&& !(tangramPieces[3].transform.rotation.eulerAngles.z <= 2 && tangramPieces[3].transform.rotation.eulerAngles.z >= -2)
                numCorrect++;
            }
        }
        if((tangramPieces[0].transform.position - tangramPositions[1].transform.position).magnitude < snapDistance)
        {
            if (tangramPieces[0].transform.rotation.eulerAngles.z <= 182 && tangramPieces[0].transform.rotation.eulerAngles.z >= 178)
            {
                numCorrect++;
            }
        }
        if ((tangramPieces[1].transform.position - tangramPositions[0].transform.position).magnitude < snapDistance)
        {
            if (tangramPieces[1].transform.rotation.eulerAngles.z <= 182 && tangramPieces[1].transform.rotation.eulerAngles.z >= 178)
            {
                numCorrect++;
            }
        }
        if ((tangramPieces[2].transform.position - tangramPositions[6].transform.position).magnitude < 1)
        {
            print("t12" + tangramPieces[2].transform.rotation.eulerAngles.z);
            if (tangramPieces[2].transform.rotation.eulerAngles.z <= 317 && tangramPieces[2].transform.rotation.eulerAngles.z >= 313)
            {
                print("t12 correct");
                numCorrect++;
            }
        }
        if ((tangramPieces[6].transform.position - tangramPositions[2].transform.position).magnitude < 1)
        {
            print("t16" + tangramPieces[6].transform.rotation.eulerAngles.z);
            if (tangramPieces[6].transform.rotation.eulerAngles.z <= 47 && tangramPieces[6].transform.rotation.eulerAngles.z >= 43)
            {
                print("t16 correct");
                numCorrect++;
            }
        }
        print(numCorrect);
        if (numCorrect == 7)
        {
            gameWon = true;
        }
    }

    IEnumerator winGame()
    {
        if (count == 1)
        {
            count++;//check to make sure it happens only once or this coroutine plays over and over resulting in a flashing effect
            ah.playOnPuzzleWon();
            yield return new WaitForSeconds(1f);
            rm.wardrobeWon = true;
            print("won");
            if (rm.getCurrentView() == 2)
            {
                backButton1.onClick.Invoke();
            }
            else if (rm.getCurrentView() == 3)
            {
                backButton2.onClick.Invoke();
            }
        }
        else
        {
            yield return new WaitForSeconds(0f);
        }
    }
}
