using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRotation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Vector3 mouseRef;
    private bool dragging = false;
    private bool autoRotating = false;
    private float sensitivity = 0.5f;
    private Vector3 rotation;
    private float speed = 300f;
    private Quaternion targetQuaternion;

    private ReadCube readCube;
    private CubeState cubeState;

    
    // Start is called before the first frame update
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            SpinSide(activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                RotateToRightAngle();
            }
        }
        if (autoRotating)
        {
            AutoRotate();
        }
    
    }

    private void SpinSide (List<GameObject> side)
    {
        //firstly we need to reset the rotation just before the side is spun
        rotation = Vector3.zero;
        // now we get the current mouse position and substract it from the previous mouse position
        // this is to deduce how much we need to rotate the side by depending on the users swipe motion from their mouse
        Vector3 mouseOffset = (Input.mousePosition - mouseRef);
        
        if (side == cubeState.up)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * (sensitivity * 1);
        }
        if (side == cubeState.down)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * (sensitivity * -1);
        }
        if (side == cubeState.right)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * (sensitivity * -1);
        }
        if (side == cubeState.left)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * (sensitivity * 1);
        }
        if (side == cubeState.front)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * (sensitivity * -1);
        }
        if (side == cubeState.back)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * (sensitivity * 1);
        }
        
        
        
        
        
        
        
        
        // rotate 
        transform.Rotate(rotation, Space.Self);
        // stores the mouse position so it can be used the next time this method is called
        mouseRef = Input.mousePosition;
    }

    public void Rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;
        // create a vector to rotate around, based on the local position of the part of the cube which we are rotating
        // as well as the centre of the cube which I made sure for the position of the centre of the cube to be (0,0,0) in the cartesian axis
        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }

    public void RotateToRightAngle()
    {
        Vector3 vec = transform.localEulerAngles;
        // round vec to nearest 90 degrees
        vec.x = Mathf.Round (vec.x / 90) * 90;
        vec.y = Mathf.Round (vec.y / 90) * 90;
        vec.z = Mathf.Round (vec.z / 90) * 90;

        targetQuaternion.eulerAngles = vec;
        autoRotating = true;
    }

    private void AutoRotate()
    {
        dragging = false;
        var step = speed * Time.deltaTime;
        transform. localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        // if the rotation is within 1 degree of the desired location of the face
        // we set the angle to the target angle and automatically will end the rotation
        if (Quaternion.Angle(transform. localRotation, targetQuaternion) <= 1)
        {
            transform. localRotation = targetQuaternion;
            // individual cubes which were previously parented, are unparented
            cubeState.PutDown(activeSide, transform.parent);
            readCube.ReadState();
            autoRotating = false;
            dragging = false;
        }
    }
}
