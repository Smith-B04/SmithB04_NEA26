//Created: Sprint 1
//Last Edited: Sprint 1
//Purpose: Track something to something else

using UnityEngine;

public class TrackingScript : MonoBehaviour
{
    public GameObject trackingObject;
    private bool isCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (this.GetComponent<Camera>() == null)
        {
            isCamera = false;
        } 
        else
        {
            isCamera = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCamera == true)
        {
            this.transform.position = new Vector3(trackingObject.transform.position.x, trackingObject.transform.position.y, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(trackingObject.transform.position.x + (float)(this.transform.localScale.x * 0.5), trackingObject.transform.position.y, this.transform.position.z);
        }
        
    }
}
