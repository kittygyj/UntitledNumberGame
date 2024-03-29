using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;
    public List<Collider2D> TriggerList = new List<Collider2D>();

    [SerializeField] public int number = 2;

    [SerializeField]TMP_Text NumberText;

    Collider2D currentCollidingObject;

    void Start() {
        //gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(0,5);
        //NumberText = gameObject.GetComponentInParent<TMP_Text>();
    }
    void Update()
    {
        NumberText.text = number.ToString();
        if(IsDragging)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity=new Vector2(0,0);
        }
        if(gameObject.transform.position.y<-20)
        {
            Destroy(gameObject);
        }
    }
    public void Drop()
    {
        if(TriggerList.Count>0)
        {
            foreach (var item in TriggerList)
            {
                item.gameObject.GetComponent<AttributeTrigger>().Updatenumber(number);
            }
            AudioManager.instance.Play("NumberPowerUp");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!TriggerList.Contains(other))
        {
            TriggerList.Add(other);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(TriggerList.Contains(other))
        {
            TriggerList.Remove(other);
        }
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
    }

    public void changenumber(int n)
    {
        number=n;
    }
}
