using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconScript : MonoBehaviour
{
    [SerializeField]
    private Image on1;
    [SerializeField]
    private Image on2;
    [SerializeField]
    private Image on3;

    public void IconOn()
    {
        on1.enabled = true;
        on2.enabled = true;
        on3.enabled = true;
    }

    public void IconOff()
    {
        on1.enabled = false;
        on2.enabled = false;
        on3.enabled = false;
    }
}
