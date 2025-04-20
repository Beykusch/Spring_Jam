using UnityEngine;
using UnityEngine.AI;

public class NewEmptyCSharpScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        agent.destination = player.position;
    }

}
