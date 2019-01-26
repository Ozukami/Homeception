using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Target;
    private int _keyNumber;
    private bool _open = true;

    private GameObject _key;

    // Start is called before the first frame update
    void Start()
    {
        GameObject cpy;

        if (cpy = Target.transform.Find("Key").gameObject)
        {
            _open = false;
            _key = cpy;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.GetComponent<PlayerController>().GetPickUpObjects().ContainsValue(this._key))
            {
                _open = true;
            }
        }
    }
}
