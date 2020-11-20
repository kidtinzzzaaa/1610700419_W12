using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class N_Calculator : MonoBehaviour
{
    public enum InputType
    {
        Number,
        Add,
        Minus,
        Multiply,
        Devide,
        Clear
    }
    public class StepInput
    {
        public float InputData;
        public InputType inputType;
    }
    public string tempInput;

    public List<StepInput> stepInputList = new List<StepInput>();

    public delegate void DelegateHandle(string input);
    public delegate void DelegateHandleSound(int soundIndex);

    public DelegateHandle OnInput;

    public static N_Calculator instance;


    void Awake()
    {
        instance = this;
    }


    public void Input (string inputStr)
    {
        StepInput newStepInput = GetStepInputFromString(inputStr);

        if (stepInputList.Count > 0)
        {
            int lastIndex = stepInputList.Count - 1;

            if (stepInputList[lastIndex].inputType == InputType.Number)
            {
                if (newStepInput.inputType == InputType.Number)
                {        
                    if (newStepInput.InputData != 0)
                    {
                        stepInputList[lastIndex].InputData *= 10;
                        stepInputList[lastIndex].InputData += newStepInput.InputData;
                    } else {
                        stepInputList[lastIndex].InputData *= 10;
                    }
                }
                else
                {
                    stepInputList.Add(newStepInput);
                }
                
                if (OnInput != null)
                {
                    OnInput(stepInputList[lastIndex].InputData.ToString());
                }

            }
            else
            {
                if(newStepInput.inputType != InputType.Number)
                {
                    stepInputList[lastIndex].inputType = newStepInput.inputType;
                }
                else
                {
                    stepInputList.Add(newStepInput);
                }

                if (OnInput != null)
                {
                    OnInput(newStepInput.InputData.ToString());
                }
            }
        }
        else
        {
            stepInputList.Add(newStepInput);
            if (OnInput != null)
            {
                OnInput(newStepInput.InputData.ToString());
            }
        }
    }

    public void Enter()
    {
        float result = 0.0f;
        for(int i =0; i < stepInputList.Count; i++)
        {
            var stepInput = stepInputList[i];

            switch(stepInput.inputType)
            {
                case InputType.Number:
                    {
                        result = stepInput.InputData;
                        break;
                    }
                case InputType.Add:
                    {
                        i++;
                        if (i < stepInputList.Count)
                        {
                            result += stepInputList[i].InputData;
                        }
                        break;
                    }
                case InputType.Minus:
                    {
                        i++;
                        if (i < stepInputList.Count)
                        {
                            result -= stepInputList[i].InputData;
                        }
                        break;
                    }
                case InputType.Multiply:
                    {
                        i++;
                        if (i < stepInputList.Count)
                        {
                            result *= stepInputList[i].InputData;
                        }
                        break;
                    }
                case InputType.Devide:
                    {
                        i++;
                        if (i < stepInputList.Count)
                        {
                            result /= stepInputList[i].InputData;
                        }
                        break;
                    }
            }
        }
        stepInputList.Clear();
        stepInputList.Add(GetStepInputFromString(result.ToString()));

        OnInput(result.ToString());
    }

    public void Clear()
    {
        stepInputList.Clear();
        OnInput("0");
    }

    private StepInput GetStepInputFromString (string inputStr)
    {
        float converToNumber = 0;
        bool isNumber = float.TryParse(inputStr, out converToNumber);


        StepInput newStepInput = new StepInput();

        if(isNumber)
        {
            newStepInput.inputType = InputType.Number;
            newStepInput.InputData = converToNumber;
        }
        else
        {
            if(inputStr == "+")
            {
                newStepInput.inputType = InputType.Add;
            } 
            else if(inputStr == "-")
            {
                newStepInput.inputType = InputType.Minus;
            }
            else if(inputStr == "*")
            {
                newStepInput.inputType = InputType.Multiply;
            }
            else if(inputStr == "/")
            {
                newStepInput.inputType = InputType.Devide;
            }
        }

        return newStepInput;
    }
}
