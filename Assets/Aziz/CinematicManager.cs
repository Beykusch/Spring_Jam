using UnityEngine;
using UnityEngine.UI;

public class CinematicManager : MonoBehaviour
{
    public GameObject imageObject;   // The UI Image GameObject
    public Sprite[] imageList;       // Array of images to cycle through

    private Image uiImage;
    private int x = 0;
    bool inCinematic = false;

    void Start()
    {
        // Get the Image component from the imageObject
        uiImage = imageObject.GetComponent<Image>();
        if (imageList.Length > 0)
        {
            uiImage.sprite = imageList[x];  // Set the initial image
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = 0;
            imageObject.SetActive(true); // Show image when E is pressed
            inCinematic = true;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && inCinematic)
        {
            if (x > imageList.Length-2)
            {
                x = 0;
                uiImage.sprite = imageList[0];
                imageObject.SetActive(false);
                Time.timeScale = 1;
                inCinematic = false;
            }
            else
            {
                x++;  // Cycle to the next image
                uiImage.sprite = imageList[x];
            }
        }
    }
}