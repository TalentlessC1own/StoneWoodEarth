using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimazeResolution : MonoBehaviour
{
    const float fon_weight = 874f;
    const float resolution_const = 80f;
  
    void Update()
    {
        float ratio = (float)Screen.height / Screen.width;
        float ortsize = fon_weight * ratio / resolution_const;
        Camera.main.orthographicSize = ortsize;
    }
}
