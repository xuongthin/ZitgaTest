using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    public bool isOpen = false;

    public void Set(bool isOpen)
    {
        this.isOpen = isOpen;
        gameObject.SetActive(!isOpen);
    }

    public bool GetStatus()
    {
        return isOpen;
    }
}
