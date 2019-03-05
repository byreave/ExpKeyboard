using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardControl : MonoBehaviour
{
    public Text inputText;
    public Text nextText;
    public Text TimerText;
    public Splatter splatter;
    [SerializeField]
    Vector3 StartPos;
    [SerializeField]
    Vector3 xInterval;
    [SerializeField]
    Vector3 yInterval;
    private string text;
    private float timer = 60;
    private int txtIndex = 0;
    private string[] inputStrings =
    {
        "1", "q", "a", "z", "2", "w", "s", "x", "3", "e", "d", "c", "4", "r", "f", "v", "5", "t", "g", "b", "6", "y", "h", "n", "7", "u", "j", "m", "8", "i", "k", ",", "9", "o", "l", ".", "0", "p", ";", "/", "-", "[", "'"
    };

    public struct KeyInstance
    {
        public int blockLevel;
        public Splatter splat;
    }
    private KeyInstance[] keyInstances;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(inputStrings.Length);
        timer = 60.0f;
        text = GameManager.instance.text;
        nextText.text = "Next: " + text[txtIndex];
        keyInstances = new KeyInstance[43]; 
        for(int i = 0; i < keyInstances.Length; ++ i)
        {
            keyInstances[i].blockLevel = 0;
            keyInstances[i].splat = Instantiate(splatter, GetPositionInKeyboard(i), Quaternion.identity);
            keyInstances[i].splat.randomColor = false;
            keyInstances[i].splat.splatColor = new Color32(keyInstances[i].splat.splatColor.r, keyInstances[i].splat.splatColor.g, keyInstances[i].splat.splatColor.b, 0);
            keyInstances[i].splat.ApplyStyle();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown && Input.inputString != "")
        {
            CheckInput();
        }
        timer -= Time.deltaTime;
        TimerText.text = ((int)timer).ToString();
        if (timer <= 0)
            GameManager.instance.GameEnd();
    }

    void GenerateStainAt(Vector3 pos)
    {
        var s = Instantiate(splatter, pos, Quaternion.identity);
    }
    
    Vector3 GetPositionInKeyboard(int index)
    {
        return StartPos + index / 4 * xInterval + index % 4 * yInterval;
    }

    int GetIndexInString(string s)
    {
        for(int i = 0; i < inputStrings.Length; ++ i)
        {
            if (s == inputStrings[i])
                return i;
        }
        return -1;
    }

    void CheckInput()
    {
        int index = GetIndexInString(Input.inputString.ToLower());

        if (Input.inputString[0] == text[txtIndex])
        {
            if(index != -1)
            {
                if(keyInstances[index].blockLevel != 0)
                {
                    ChangeStain(index);
                    return;
                }
            }
            inputText.text += text[txtIndex++];
            GameManager.instance.scores++;
            while (text[txtIndex] == ' ' || text[txtIndex] == '\n' || text[txtIndex] == '\t')
                inputText.text += text[txtIndex++];
            nextText.text = "Next: " + text[txtIndex];
            nextText.GetComponentInChildren<ParticleSystem>().Play();
        }
        else
        {
            if (index != -1)
            {
                ChangeStain(index);
            }
        }
        
    }

    void ChangeStain(int index)
    {
        if(keyInstances[index].blockLevel == 0)
        {
            keyInstances[index].blockLevel = 3;
            keyInstances[index].splat.splatColor = new Color32(keyInstances[index].splat.splatColor.r, keyInstances[index].splat.splatColor.g, keyInstances[index].splat.splatColor.b, 255);
            keyInstances[index].splat.ApplyStyle();
        }
        else
        {
            keyInstances[index].blockLevel--;
            keyInstances[index].splat.splatColor = new Color32(keyInstances[index].splat.splatColor.r, keyInstances[index].splat.splatColor.g, keyInstances[index].splat.splatColor.b, System.Convert.ToByte(keyInstances[index].blockLevel / 3.0f * 255));
            keyInstances[index].splat.ApplyStyle();
        }
    }
}
