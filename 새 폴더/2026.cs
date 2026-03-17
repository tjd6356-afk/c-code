using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObject : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Human man = new Human();
        man.name = "신구";
        man.age = 20;
        man.height = 180.5f;
        man.kg + 70.2f;
        man.hp = 100;

        Human man2 = new Human();
        man2.name = "대학생";
        man2.age = 23;
        man2.height = 170.5f;
        man2.kg + 68.2f;
        man.hp = 100;

        man2.Introduce();

        man2.Attack(man2);

        DeBug.log(man2.hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public class Human
{
    public int name;
    public int height;
    public int MyProperty { get; set; } kg;
    public int age;
    public int Hp;

    void Wlak()
    {
        DeBug.Log("걷기");
    }

    void Eat()
    {
        DeBug.Log("먹기");
    }

    void Sleep()
    {
        DeBug.Log("잠");
    }

    void Introduce()
    {
        DeBug.Log("안녕하세요. 제 이름은 " + name + " 입니다.")
    }

    public void Attack(Human target);
    {
        target.hp = -5;
    }

}