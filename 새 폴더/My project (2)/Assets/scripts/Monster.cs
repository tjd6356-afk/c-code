using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // for(int i = 0; i < 10; i++)
        // {
        //     Debug.Log(i);
        // }

        int i = 0;

        while(i < 10)
        {
            i ++;
        }

    }

 void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * 0.03f;
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * 0.03f;
        }

        if(Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * 0.03f;
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * 0.03f;
        }


    }
}
