using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndgameControl : MonoBehaviour
{
    public Text resultText;
    string[] ProgrammingLevel =
    {
        "Terrible",
        "Mediocre",
        "Good",
        "Excellent",
        "Godlike",
        "Undefined"
    };
    // Start is called before the first frame update
    void Start()
    {
        resultText.text = "You coded " + GameManager.instance.scores.ToString() + " characters in 60 seconds!\n" + "You are now a/an " +
            getLevel(GameManager.instance.scores) + " Progammer!";
    }

    string getLevel(int score)
    {
        if (score <= 20)
            return ProgrammingLevel[0];
        else if (score <= 40)
            return ProgrammingLevel[1];
        else if (score <= 60)
            return ProgrammingLevel[2];
        else if (score <= 80)
            return ProgrammingLevel[3];
        else if (score <= 100)
            return ProgrammingLevel[4];
        else 
            return ProgrammingLevel[5];
    }
}
