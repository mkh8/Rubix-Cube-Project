using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationofCube : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;
    Vector3 previousMousePosition;
    Vector3 mouseDelta;

    // Public field to store the target GameObject
    public GameObject target;

    float speed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        Drag();
    } 
    void Drag()
    {
        if(Input.GetMouseButton(1))
        {
            // while the mouse is being held down, the cube will be able to move around its central axis in order to provide visual feedback
            mouseDelta = Input.mousePosition - previousMousePosition;
            mouseDelta *= 0.5f; // this is for the altering of rotation speed (can be modified at any time by the float multiplier)
            transform.rotation = Quaternion.Euler(mouseDelta.y - mouseDelta.x, 0, 0) * transform.rotation;

        }
        else
        {
            // aim to automatically move to target position
            if (transform.rotation != target.transform.rotation)
            {
                var step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }

        }
        previousMousePosition = Input.mousePosition;
    }


    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(1))
        {
            secondPressPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();
            if (LeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (UpLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (UpRightSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (DownLeftSwipe(currentSwipe))
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            } 
            else if (DownRightSwipe(currentSwipe))
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }
        }
    }

    bool LeftSwipe(Vector2 swipe)
    {
        return currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }

    bool RightSwipe(Vector2 swipe)
    {
        return currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f;
    }
    
    bool UpLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x < 0f;
    }
    
    bool UpRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y > 0 && currentSwipe.x > 0f;
    }
    
    bool DownLeftSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x < 0f;
    }
    
    bool DownRightSwipe(Vector2 swipe)
    {
        return currentSwipe.y < 0 && currentSwipe.x > 0f;
    }
}





