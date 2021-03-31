using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    [SerializeField] LayerMask richochetMask;
    [SerializeField] float bulletSpeed = 170f;

    [SerializeField] int bounceCount;

    Vector3 lastVel;

   


    // Start is called before the first frame update
    void Start()
    {
       


        //  transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * bulletSpeed);  // Move bullet forward
        transform.position = new Vector3(transform.position.x, 2f, transform.position.z); // Set height to 2f

        Ray rayCheck = new Ray(transform.position, transform.forward); // Shoot a ray out
        RaycastHit hitInfo; // Shoot a ray out

        if (Physics.Raycast(rayCheck, out hitInfo, Time.deltaTime * bulletSpeed + 0.1f, richochetMask))
        {


            Vector3 reflectDirection = Vector3.Reflect(rayCheck.direction, hitInfo.normal); // Get vector 3 direction of the reflection
            float rotation = 90  - Mathf.Atan2(reflectDirection.z, reflectDirection.x) * Mathf.Rad2Deg; //use trig to find  new y angle
            transform.eulerAngles = new Vector3(0, rotation, 0); //Change rotation on y axis
            bounceCount += 1; //Increment bounce count

        }
    }
}
