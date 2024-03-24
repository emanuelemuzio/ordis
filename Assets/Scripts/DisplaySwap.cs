using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySwap : MonoBehaviour
{
    public Camera cam1;
    public Camera cam2;
    private bool focusFirstPerson = true;

    void Start()
    {
        cam1.enabled = focusFirstPerson;
        cam2.enabled = !focusFirstPerson;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            focusFirstPerson = !focusFirstPerson;
            cam1.enabled = focusFirstPerson;
            cam2.enabled = !focusFirstPerson;
        }
    }
}
