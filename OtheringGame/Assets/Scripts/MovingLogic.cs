using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLogic : MonoBehaviour
{

    Vector3 startPoint;
    Vector3 startPosition;

    public GameObject DayObject;

    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.position;
    }


    private void OnMouseDrag()
    {
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0;

        //check nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(newPosition, .2f);

        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject !=gameObject)
            {
                UpdatePillPosition(collider.transform.position);

                if(transform.parent.name.Equals(DayObject))
                {
                    collider.GetComponent<MovingLogic>()?.Done();
                    Done();
                }
                return;
            }
        }

        UpdatePillPosition(newPosition);
    }

    void Done()
    {
        Destroy(this);
    }

    //might fix later, meant to reset position of pills
    /*
    private void OnMouseUp()
    {
         UpdatePillPosition(startPosition);
    }
    */

    void UpdatePillPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }
}
