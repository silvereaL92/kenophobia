using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Component park;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Door1"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Animator anim1 = other.GetComponentInChildren<Animator>();
                anim1.SetTrigger("openclose");
            }
        }

        if (other.CompareTag("DoubleDoor"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Animator anim = other.GetComponentInChildren<Animator>();
                anim.SetTrigger("openclose");
                park.gameObject.SetActive(false);
            }
        }
    }
}
