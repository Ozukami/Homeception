using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Body : MonoBehaviour
{
  [SerializeField] private float _bouncingValue;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update() {
    transform.localPosition = new Vector3(0, 0, transform.localPosition.z + ((Mathf.Sin(Time.time * 10) > 0) ? +_bouncingValue : -_bouncingValue));
  }
}
