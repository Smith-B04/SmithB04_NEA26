//Created: Sprint 1
//Last Edited: Sprint 1
//Purpose: Move Camera to player character

using UnityEngine;

public class CameraMovementScript : MonoBehaviour
{
    public GameObject trackingObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(trackingObject.transform.position.x, trackingObject.transform.position.y, this.transform.position.z);
    }
}
