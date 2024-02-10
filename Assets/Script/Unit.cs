using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using TMPro;
public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;
    public int damage;
    public int maxHP;
    public int currentHP;

    public GameObject numberprefab;

    public bool takeDamage(int dmg)//return if enemy is dead
    {
        int realdmg=dmg;
        if(currentHP-dmg<=0)
        {
            realdmg = currentHP-0;
        }
        //Instantiate a dmg number in the scene
        GameObject g = Instantiate(numberprefab,transform.position,Quaternion.identity );
        g.GetComponent<Draggable>().changenumber(realdmg);
        g.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,1));

        currentHP-=dmg;
        if(currentHP<=0)
        {
            return true;
        }
        return false;
    }
}
