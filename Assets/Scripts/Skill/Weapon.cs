using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public virtual void Attak()
    {
        Console.WriteLine("기본 무기 데미지");
    }
}
