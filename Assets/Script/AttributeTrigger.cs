using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public enum Attributes
{
    ATK,
    DEF,
    HP
}

public enum Role
{
    Player,
    Enemy
}

public class AttributeTrigger : MonoBehaviour
{
    [SerializeField]TMP_Text Value;
    [SerializeField]TMP_Text Attribute;
    [SerializeField]int value;
    public Attributes attributes;
    public Role role;
    public Unit unit;
    // Start is called before the first frame update
    void Start()
    {
    }

    void LateUpdate()
    {
        Value.text = value.ToString();
        if(attributes==Attributes.DEF)
        {
            if(unit !=null)
            {
                value = unit.defense ;
            }
            else
            {
                value = 0;
                unit.defense = value;
            }
        }
        else if(attributes==Attributes.ATK)
        {
            if(unit !=null)
            {
                value = unit.damage ;
            }
            else
            {
                value = 0;
                unit.damage = value;
            }
        }
        else if(attributes==Attributes.HP)
        {
            if(unit !=null)
            {
                value = unit.currentHP;
            }
            else
            {
                value = 0;
                unit.currentHP = value;
            }
        }
    }
    public void Updatenumber(int number)
    {
        Debug.Log(number);
        value+=number;
        if(attributes==Attributes.DEF)
        {
            if(unit !=null)
            {
                unit.defense = value;
            }
            else
            {
                value = 0;
                unit.defense = value;
            }
        }
        else if(attributes==Attributes.ATK)
        {
            if(unit !=null)
            {
                unit.damage = value;
            }
            else
            {
                value = 0;
                unit.damage = value;
            }
        }
        else if(attributes==Attributes.HP)
        {
            if(unit !=null)
            {
                unit.currentHP = value;
            }
            else
            {
                value = 0;
                unit.currentHP = value;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.GetComponent<Draggable>()!=null)
        {
            Attribute.GetComponent<TMP_Text>().color = Color.green;
            Value.GetComponent<TMP_Text>().color = Color.green;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.GetComponent<Draggable>()!=null)
        {
            Attribute.GetComponent<TMP_Text>().color = Color.white;
            Value.GetComponent<TMP_Text>().color = Color.white;
        }
    }

    public void initializeValue(int n,Unit punit)
    {
        value = n;
        unit = punit;
    }

}
