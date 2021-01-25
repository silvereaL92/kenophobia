using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public Component park;

    private void OnTriggerStay(Collider other)
    {
        Animator anim = other.GetComponentInChildren<Animator>();
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("FrontDoorOpen");
            park.gameObject.SetActive(false);
        }
    }
}
