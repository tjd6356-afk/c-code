using UnityEngine;

public class TextArray : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        int name = 3;

        int[] arrayName = new int[10];

        arrayName[0] = 10;
        arrayName[5] = 20;

        for (int i = 0; i < 10; i++)
        {
            arrayName[i] = i;
            Debug.Log(arrayName[i]);
        }


        List<int> testList = new List<int>();
        testList.Add(5);
        testList.Add(10);
        testList.Add(15);

        testList[1] = 30;

        for (int i = 0; i < testList.Count; i++)
        {
            Debug.Log(arrayName[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
