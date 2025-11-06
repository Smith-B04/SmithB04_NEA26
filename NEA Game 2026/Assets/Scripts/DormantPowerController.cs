using UnityEngine;
using UnityEngine.InputSystem;

public class DormantPowerController : MonoBehaviour
{
    public Canvas canvas;
    public TMPro.TextMeshProUGUI prompt;
    private bool interactable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas.scaleFactor = 0f;
        interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            canvas.scaleFactor = 1.0f;
            prompt.text = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            prompt.text = "Interact [E]";
            Debug.Log("Interacting with you <3");
            interactable = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            prompt.text = "";
            canvas.scaleFactor = 0f;
            interactable = false;
        }
    }

    /*private void OnInteract()
    {
        Debug.Log("Interacting with you <3");
        canvas.scaleFactor = 1.0f;
    }*/
}
