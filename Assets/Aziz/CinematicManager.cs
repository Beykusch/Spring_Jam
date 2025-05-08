using UnityEngine;
using UnityEngine.UI;

public class CinematicManager : MonoBehaviour
{
    public float inputBufferTime = 2f; //ChatGPT
    private float bufferTimer = 0f; //ChatGPT

    public GameObject dialoguePanel;
    private Dialogue dialogue;

    public GameObject imageObject;   // The UI Image GameObject
    public Sprite[] imageList;       // Array of images to cycle through
    private Image uiImage;

    private int x = 0;
    public bool inCinematic = false;

    void Start()
    {
        // Get the Image component from the imageObject
        dialogue = dialoguePanel.GetComponent<Dialogue>();
        uiImage = imageObject.GetComponent<Image>();
        if (imageList.Length > 0)
        {
            uiImage.sprite = imageList[x];  // Set the initial image
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inCinematic==false) // Enter cinematic
        {
            Time.timeScale = 0;
            imageObject.SetActive(true);
            inCinematic = true;
            
            dialogue.textSpeak.text = string.Empty;
            dialoguePanel.SetActive(true);
            dialogue.StartDialogue();
            dialogue.inDialogue = true;
        }

        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && inCinematic && dialogue.inDialogue == false)
        {
            if (x > imageList.Length-2) // Exit cinematic
            {
                bufferTimer = inputBufferTime; //ChatGPT
                x = 0;
                uiImage.sprite = imageList[0];
                imageObject.SetActive(false);
                Time.timeScale = 1;
                inCinematic = false;
            }
            else // Next image
            {
                x++;
                uiImage.sprite = imageList[x];
            }
        }
        if (bufferTimer > 0) //ChatGPT
        {
            bufferTimer -= Time.unscaledDeltaTime;
        }
    }
    public bool IsInputBuffered() //ChatGPT
    {
        return bufferTimer > 0;
    }
}