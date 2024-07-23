using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public float basicSkillColdown;
        
    public abstract void Attack();
    public abstract void BasicSkill();
}
