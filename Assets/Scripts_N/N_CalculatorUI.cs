using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class N_CalculatorUI : MonoBehaviour
{
    public Text displayText;

    public delegate void Delegate();
    public Delegate OnSomething;

    void Start()
    {
        N_Calculator.instance.OnInput += OnInput;

    }


    void Update()
    {
        
    }

    public void OnInput(string input)
    {
        Debug.Log(input);
        displayText.text = input;
    }

}
