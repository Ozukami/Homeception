using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerAccSpeed = 1f;
    [SerializeField] private float _playerDecSpeed = 0.8f;
    [SerializeField] private float _playerMaxSpeed = 5f;

    private Rigidbody _playerRigidBody;
    private Vector3 _playerMovement;
    public Dictionary<string, GameObject> _pickupObjects;
    private UnityEngine.AI.NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        _playerRigidBody = GetComponent<Rigidbody>();
        _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _playerMovement = Vector3.zero;
        _pickupObjects = new Dictionary<string, GameObject>();
    }

    private void Update()
    {
        _playerMovement = new Vector3(
          (Input.GetAxis("Horizontal") > 0.2f || Input.GetAxis("Horizontal") < -0.2f ? Input.GetAxis("Horizontal") : 0),
          0,
          (Input.GetAxis("Vertical") > 0.2f || Input.GetAxis("Vertical") < -0.2f ? Input.GetAxis("Vertical") : 0)
        );

        Debug.DrawLine(transform.position, transform.position - new Vector3(
            _playerRigidBody.velocity.x,
            0,
            _playerRigidBody.velocity.z
          ),
        Color.blue);
    }

    private void FixedUpdate()
    {

        if (Math.Abs(_playerMovement.x) > 0.02f || Math.Abs(_playerMovement.z) > 0.02f)
            _playerRigidBody.velocity = _playerMovement * _playerAccSpeed;
        else
            _playerRigidBody.velocity *= _playerDecSpeed;
        _playerRigidBody.velocity = Vector3.ClampMagnitude(_playerRigidBody.velocity, _playerMaxSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            this._pickupObjects.Add(other.gameObject.name[0].ToString(), other.gameObject);
        }
        else if (other.gameObject.CompareTag("Bed"))
        {
            this._navMeshAgent.SetDestination(other.gameObject.transform.Find("Night Light").gameObject.transform.position);
        }
    }

    public Dictionary<string, GameObject> GetPickUpObjects()
    {
        return this._pickupObjects;
    }
}
