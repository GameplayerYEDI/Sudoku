using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RememberButton : MonoBehaviour
{
    public bool start;
    public Button self;
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        self.image.enabled = start;
        self.enabled = start;
        
    }
}
