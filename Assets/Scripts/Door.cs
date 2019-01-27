using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
  public bool isPaintingLocked;

  private GameObject _key;
  private bool _isOpen;
  private float _rotZ;

  private bool IsKeyLocked => _key != null;

  private void Awake () {
    if (transform.childCount == 0) return;
    GameObject child = transform.GetChild(0).gameObject;
    if (child.CompareTag("Key")) _key = child;
  }

  private void Start()
  {
    _rotZ = transform.localEulerAngles.z;
  }

  private void Update()
  {
    if (_isOpen && transform.localRotation.z > _rotZ - 0.5f) {
      transform.Rotate(Vector3.forward * -50.0f * Time.deltaTime, Space.Self);
    }
  }

  public void Open (bool playerHasKey) {
    if (isPaintingLocked) return;
    if (IsKeyLocked && !playerHasKey) return;
    _isOpen = true;
  }
}
