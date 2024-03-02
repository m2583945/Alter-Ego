using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 targetPosition;
    private Vector3 correctPosition;

    float xDiff;
    float yDiff;

    public bool correct = false;
    public int tileNum;

    
    void Awake()
    {
        targetPosition = this.transform.position;
        correctPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.03f);
        xDiff = Mathf.Abs(transform.position.x - correctPosition.x);//to compensate for imperfect placement of tiles, allow < 1 unit of difference
        yDiff = Mathf.Abs(transform.position.y - correctPosition.y);
        if(xDiff < 1 && yDiff < 1)
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
            correct = true;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
            correct = false;
        }
    }
}
