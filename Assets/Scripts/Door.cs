using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // public GameObject Target;
    // private int _keyNumber;
    private bool _open = true;

    private GameObject _key;

    private bool _isOpen = false;
    private float _rotZ;

    // Start is called before the first frame update
    void Start()
    {
        // GameObject cpy;

        // if (cpy = Target.transform.Find("Key").gameObject)
        // {
        //     _open = false;
        //     _key = cpy;
        // }
        _rotZ = transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("transform;localEuler.z = " + transform.localRotation.z);
        Debug.Log("Rot = " + _rotZ + 90);
        if (_isOpen && transform.localRotation.z > _rotZ - 0.5f) {
            Debug.Log(_rotZ);

            // transform.Rotate(transform.rotation.x, transform.rotation.y, transform.)

            // Quaternion newRotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);

            // Debug.Log("new rot => x: " + newRotation.x + "\ny: " + newRotation.y + "\nz: " + newRotation.z);
            // Debug.Log("x: " + transform.rotation.x + "\ny: " + transform.rotation.y + "\nz: " + transform.rotation.z);

            // transform.rotation = Quaternion.Slerp(transform.localRotation, newRotation, .05f);

            transform.Rotate(Vector3.forward * -50.0f * Time.deltaTime, Space.Self);
            
        }
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

    public  void setIsOpen(bool doorStatus) 
    {
        Debug.Log("Hello");
        _isOpen = doorStatus;
    }
}
