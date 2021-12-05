using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Difficulty : MonoBehaviour
{
    public byte difficulty = 0;
    public Button self;
    public TMP_Text text;
    public CreateSudoku create;

    public void OnClick()
    {
        difficulty++;
        if (difficulty > 2)
        {
            difficulty = 0;
        }

        switch (difficulty)
        {
            case 0:
                text.text = "Easy";
                break;
            case 1:
                text.text = "Medium";
                break;
            case 2:
                text.text = "Hard";
                break;
        }

        create.Create(difficulty);
    }
}
