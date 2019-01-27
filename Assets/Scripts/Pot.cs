using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

    public int validColor;
    public bool isValid;    // Start is called before the first frame update
    public Lvl2Manager lvlManager;
    public int index;

    private SpriteRenderer rd;
    void Start()
    {
        isValid = false;
        rd = GetComponent<SpriteRenderer>();
        rd.color = lvlManager.colorList[index];
        Debug.Log(rd.color);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown("E"))
            {
                index++;
                if (index > 4)
                {
                    index = 0;
                    if (index == validColor)
                        isValid = true;
                }
                rd.color = lvlManager.colorList[index];
            }
        }
    }
}
