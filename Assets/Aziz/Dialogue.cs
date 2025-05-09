using UnityEngine;
using TMPro;
using System.Collections;

public class Dialogue : MonoBehaviour
{
    public AudioSource dialogueSFX;

    public GameObject cinObject;
    private CinematicManager cinMan;

    public TextMeshProUGUI textSpeak;
    public TextMeshProUGUI textPerson;

    public string[] lines;
    public float textSpeed;
    private int index;
    public bool inDialogue=false;

    private void Start()
    {
        cinMan = cinObject.GetComponent<CinematicManager>();
    }
    private void Update()
    {
        if (textSpeak.text == lines[index])
        {
            dialogueSFX.Stop();
        }
        if (index % 2 == 0)
        {
            textPerson.text = "Doctor: ";
        }
        else
        {
            textPerson.text = "Assistant: ";
        }
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Space)) && cinMan.inCinematic)
        {
            if (textSpeak.text == lines[index])
            {
                NextLine();
            }
            else
            {
                dialogueSFX.Stop();
                StopAllCoroutines();
                textSpeak.text = lines[index];
            }
        }
    }
    public void StartDialogue()
    {
        index = 0;
        dialogueSFX.Play();
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
        {
            textSpeak.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
        }
    }

    void NextLine()
    {
        
        if (index < lines.Length - 1)
        {
            index++;
            textSpeak.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            inDialogue = false;
        }
        if (inDialogue)
        {
            dialogueSFX.Play();
        }
    }
 }
