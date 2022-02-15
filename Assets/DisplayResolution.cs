using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResolution : MonoBehaviour
{
    public Text text_Res;
    // Start is called before the first frame update
    void Start()
    {
        ChangeTextRes();
    }

    public void ChangeTextRes()
    {
        text_Res.text = Screen.width + "x" + Screen.height;
    }
}
