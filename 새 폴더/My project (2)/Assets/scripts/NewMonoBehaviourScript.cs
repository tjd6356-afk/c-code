using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    //

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int a = 5;
        float b;
        string c;
        bool d;
        
        // PlusMinus(10, 10, false);
    }

    // Update is called once per frame
    void Update()
    {
        {
            
        }
        
        int Plus(int left, int right)
        {
            return (left + right);
        }
        
        int Minus(int left, int right)
        {
            return (left - right);
        }

        int Multiply(int left, int right)
        {
            return (left * right);
        }

        int Divide(int left, int right)
        {
            return (left / right);
        }

        int PlusMinus(int left, int right,bool isPlus)
        {
            if(isPlus)
            {
                return left + right;
            }
            else
            {
                return left - right;
            }
        }
    }
}
