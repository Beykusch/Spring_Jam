using UnityEngine;
using UnityEngine.UI;

public class CinematicManager : MonoBehaviour
{
    private bool hasStartedCar = false;
    private bool hasDrivenCar = false;
    private bool hasStoppedCar = false;

    public AudioSource carStart;
    public AudioSource carDrive;
    public float inputBufferTime = 2f; //ChatGPT
    private float bufferTimer = 0f; //ChatGPT

    public GameObject dialoguePanel;
    private Dialogue dialogue;

    public GameObject imageObject1;   // The UI Image GameObject
    public Sprite[] imageList1;       // Array of images to cycle through
    public GameObject imageObject2;   // The UI Image GameObject
    public Sprite[] imageList2;

    private Image uiImage;
    private Image uiImage2;

    private int x = 0;
    private int y = 0;
    public bool inCinematic = false;
    public bool inCinematic2 = false;

    void Start()
    {
        // Get the Image component from the imageObject
        dialogue = dialoguePanel.GetComponent<Dialogue>();
        uiImage = imageObject1.GetComponent<Image>();
        uiImage2 = imageObject2.GetComponent<Image>();

        if (imageList1.Length > 0)
        {
            uiImage.sprite = imageList1[x];  // Set the initial image
        }

        Time.timeScale = 0;
        imageObject1.SetActive(true);
        inCinematic = true;

        dialogue.textSpeak.text = string.Empty;
        dialoguePanel.SetActive(true);
        dialogue.StartDialogue();
        dialogue.inDialogue = true;
    }

    void Update()
    {
        if (y == 1 && !hasStartedCar)
        {
            carStart.Play();
            hasStartedCar = true;
        }
        else if (y == 2 && !hasDrivenCar)
        {
            carStart.Stop();
            carDrive.Play();
            hasDrivenCar = true;
        }
        else if (y == 3 && !hasStoppedCar)
        {
            carDrive.Stop();
            hasStoppedCar = true;
        }

        /*if (Input.GetKeyDown(KeyCode.E) && inCinematic==false) // Enter cinematic
        {
            Time.timeScale = 0;
            imageObject.SetActive(true);
            inCinematic = true;
            
            dialogue.textSpeak.text = string.Empty;
            dialoguePanel.SetActive(true);
            dialogue.StartDialogue();
            dialogue.inDialogue = true;
        }*/

        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && inCinematic && dialogue.inDialogue == false)
        {
            if (x > imageList1.Length - 2) // Exit cinematic
            {
                bufferTimer = inputBufferTime; //ChatGPT
                x = 0;
                uiImage.sprite = imageList1[0];
                imageObject1.SetActive(false);
                Time.timeScale = 1;
                inCinematic = false;
            }
            else // Next image
            {
                x++;
                uiImage.sprite = imageList1[x];
            }
        }
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && inCinematic2 && dialogue.inDialogue == false)
        {
            if (y > imageList2.Length - 2) // Exit cinematic 2
            {
                bufferTimer = inputBufferTime; //ChatGPT
                y = 0;
                uiImage2.sprite = imageList2[0];
                imageObject2.SetActive(false);
                Time.timeScale = 1;
                inCinematic2 = false;
            }
            else // Next image 2
            {
                y++;
                uiImage2.sprite = imageList2[y];
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
    public void Cutscene2()
    {
        if (imageList2.Length > 0)
        {
            uiImage2.sprite = imageList2[y];  // Set the initial image
        }

        Time.timeScale = 0;
        imageObject2.SetActive(true);
        inCinematic2 = true;

        hasStartedCar = false;
        hasDrivenCar = false;
        hasStoppedCar = false;
    }
}