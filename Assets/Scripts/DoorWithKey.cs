using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWithKey : MonoBehaviour
{
    public GameObject door;
    public Animator doorAnimator;
    private bool hasKey = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && hasKey)
        {
            doorAnimator.SetTrigger("DoorOpen");
            Destroy(gameObject);
        }    
    }

    public void PlayerHasKey()
    {
        hasKey = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
