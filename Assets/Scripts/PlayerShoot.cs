using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] public GameObject currentWall;
    [SerializeField] private Transform pointOfShot;
    [SerializeField] private int selection = 1; // 1 = shoot, 2 = build mode. Default to 1
    private float speed;
    [SerializeField] private Camera cam;

    int buildRotation = 1; //Goes from 1 to 8, to change rotation of the wall being built. Set default rotation to one. 45 Increment

    float currentScroll = 0f; //Used to see if they scroll up or down

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        if (selection == 2) //Build mode
        {
            if (currentWall == null) //If no current wall
            {
                currentWall = Instantiate(wallPrefab); //Instantiate wall
                    
            }
            if (currentWall != null) //If current wall is current on scrren
            {
                moveWallToMouse();

            }
        }


        if (Input.GetMouseButtonDown(0)) // If they click mouse click
        {
            if (selection == 1) // If shoot mode
            {
                Instantiate(BulletPrefab, pointOfShot.transform.position, transform.rotation);
            
            }
            else if(selection == 2) // If build mode
            {
                currentWall = null;
            }          

        }

      
        if (Input.GetKeyDown("1")) //  shoot mode
        {
            selection = 1; //  Change selection
            
            if (currentWall != null)
            {
                Destroy(currentWall);
            }
        }
        else if (Input.GetKeyDown("2")) // build mode
            {
            selection = 2;
        }

    
        if (Input.mouseScrollDelta.y > 0 && selection == 2  ) //Mouse wheel up

        {  
            changeRotation("up");    
        }
        else if (Input.mouseScrollDelta.y < 0 &&  selection == 2) //Mouse wheel down
        {
            changeRotation("down");     

        }
    }

    void moveWallToMouse()
    {
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        Vector3 pointToBuild;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            pointToBuild = cameraRay.GetPoint(rayLength);
            pointToBuild = new Vector3(pointToBuild.x, pointToBuild.y + 2, pointToBuild.z);
            currentWall.transform.position = pointToBuild;
            Quaternion currentRotation = getRotation(); //Get rotation of object
            currentWall.transform.rotation = currentRotation;
           

        }
    }
    void changeRotation(String upOrDown) //Change rotation, take in either up or down
    {
        if (upOrDown == "up") //If up
        {
            if (buildRotation >= 8) //If 8, go back to 0
            {
                buildRotation = 1;
            }
            else
            {
                buildRotation += 1;  //else, increment
            }
        }
        else if(upOrDown == "down") //If up)
        {
            if (buildRotation <= 1) //If 1, go to 8
            {
                buildRotation = 8;
            }
            else
            {
                buildRotation -= 1;  //else, decrement
            }
        }

        Debug.Log(buildRotation);
    }


     Quaternion getRotation() //Build wall
    {
        Vector3 rotationOfBuild = new Vector3(0, 0, 0);
        switch (buildRotation)
        {
            case 1:
                rotationOfBuild = new Vector3(0, 0, 0);
                break;
            case 2:
                rotationOfBuild = new Vector3(0, 45, 0);
                break;
            case 3:
                rotationOfBuild = new Vector3(0, 90, 0);
                break;
            case 4:
                rotationOfBuild = new Vector3(0, 135, 0);
                break;
            case 5:
                rotationOfBuild = new Vector3(0, 180, 0);
                break;
            case 6:
                rotationOfBuild = new Vector3(0, 225, 0);
                break;
            case 7:
                rotationOfBuild = new Vector3(0, 270, 0);
                break;
            case 8:
                rotationOfBuild = new Vector3(0, 315, 0);
                break;

              case 9:
                rotationOfBuild = new Vector3(0, 360, 0);
                break;


        }
        return Quaternion.Euler(rotationOfBuild);
    }
}
