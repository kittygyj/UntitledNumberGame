using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int defense;

    public GameObject numberprefab;

    public bool takeDamage(int dmg)//return if enemy is dead
    {
        dmg = dmg-defense;
        if(dmg<=0)
        {
            dmg=1;
        }
        int realdmg=dmg;
        if(currentHP-dmg<=0)
        {
            realdmg = currentHP-0;
        }
        int[] digitdmg = System.Array.ConvertAll(realdmg.ToString().ToCharArray(), c => (int)Char.GetNumericValue(c));
        //Instantiate a dmg number in the scene
        for(int i=0;i<digitdmg.Length;i++)
        {
            GameObject g = Instantiate(numberprefab,transform.position,Quaternion.identity );
            g.GetComponent<Draggable>().changenumber(digitdmg[i]); 
            g.GetComponent<Rigidbody2D>().velocity=generateRandomVelocity(4f,90f-i*0.1f);
        }

        currentHP-=dmg;
        if(currentHP<=0)
        {
            currentHP=0;
            return true;
        }
        return false;
    }

    Vector2 generateRandomVelocity(float speed,float angle)
    {
        //float angle = UnityEngine.Random.Range(0,45);
        float x = speed*Mathf.Cos(angle);
        float y = speed*Mathf.Sin(angle);
        return new Vector2(x,y);
    
    }
}
