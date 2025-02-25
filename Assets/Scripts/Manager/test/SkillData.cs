using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillData
{
    public int id;
    public string name;
    public string type;
    public float value;
    public string description;
    public string sprite;
}

[Serializable]
public class SkillList
{
    public SkillData[] skills;
}
