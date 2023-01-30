using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Reference to rigidbody component
    Rigidbody thisRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        // Assign the reference
        thisRigidBody = GetComponent<Rigidbody>();
        // Add a big force
        thisRigidBody.AddForce(transform.forward * 100 , ForceMode.Impulse );
        // Destroy with delay of 5 seconds
        Destroy(gameObject, 5f);

        // Don't want the balls to show up in the hierarchy, use this:
        gameObject.hideFlags = HideFlags.HideInHierarchy;
    }
}
