using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    CubeState cubeState;

    public Transform up;
    public Transform down;
    public Transform right;
    public Transform left;
    public Transform back;
    public Transform front;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Set()// Creation of a new method to set the colours of the cubemap
    {
         cubeState = FindObjectOfType<CubeState>();

         UpdateMap(cubeState.front, front);
         UpdateMap(cubeState.up, up);
         UpdateMap(cubeState.down, down);
         UpdateMap(cubeState.back, back);
         UpdateMap(cubeState.right, right);
         UpdateMap(cubeState.left, left);
    }
    
    
    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            if (face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = new Color (1 ,0.5f, 0, 1); //this allows for the colour orange to be displayed on the cube map (orange is the front of the cube)
            }
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Color.red;
            }
            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (face[i].name[0] == 'D')
            {
               map.GetComponent<Image>().color = Color.white;
            }
            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            i++;
        }   
    }
}
