using UnityEngine;

public class EndCutscene : MonoBehaviour
{
    public GameObject cinObject;
    private CinematicManager cinMan;
    private void Start()
    {
        cinMan = cinObject.GetComponent<CinematicManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            cinMan.Cutscene2();
        }
    }
}
