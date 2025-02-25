using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData
{
    [Serializable]
    public class Skill
    {
        public int id;
        public string name;
        public string type;
        public float value;
    }

    [Serializable]
    public class SkillList
    {
        public List<Skill> skills;
    }
   
}
