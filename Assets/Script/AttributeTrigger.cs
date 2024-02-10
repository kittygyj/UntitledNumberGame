using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AttributeTrigger : MonoBehaviour
{
    [SerializeField]TMP_Text Value;
    [SerializeField]TMP_Text Attribute;
    [SerializeField]int value;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Value.text = value.ToString();
    }
    public void Updatenumber(int number)
    {
        Debug.Log(number);
        value+=number;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<Draggable>()!=null)
        {
            Attribute.GetComponent<TMP_Text>().color = Color.green;
            Value.GetComponent<TMP_Text>().color = Color.green;
        }
    }

        private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.GetComponent<Draggable>()!=null)
        {
            Attribute.GetComponent<TMP_Text>().color = Color.white;
            Value.GetComponent<TMP_Text>().color = Color.white;
        }
    }

}
