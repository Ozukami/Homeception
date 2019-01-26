using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
<<<<<<< HEAD
  private bool _open = true;

  private GameObject _key;
  private bool _isOpen = false;
  private float _rotZ;
=======
  public GameObject Target;
  private int _keyNumber;
  private bool _open = true;

  private GameObject _key;
>>>>>>> 722bf1dfbab7687abe7065be8b0b8cd70daaa88a

  // Start is called before the first frame update
  void Start()
  {
<<<<<<< HEAD
    _rotZ = transform.localEulerAngles.z;
  }

  // Update is called once per frame
  void Update()
  {
    if (_isOpen && transform.localRotation.z > _rotZ - 0.5f) {
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
        _isOpen = doorStatus;
=======
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
>>>>>>> 722bf1dfbab7687abe7065be8b0b8cd70daaa88a
    }
  }
}
