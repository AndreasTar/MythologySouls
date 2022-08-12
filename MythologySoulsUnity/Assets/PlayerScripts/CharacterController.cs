using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController: MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] float moveSpeed;
    [SerializeField] float axisSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float strafeSpeed;

    [Header("Physics")]
    [SerializeField] float gravity;
    [SerializeField] float jumpForce;

    [Header("External")]
    [SerializeField] Camera playerCamera;
    [SerializeField] LayerMask discludePlayer;

    [Header("Surface Control")]
    [SerializeField] Vector3 sensorLocal;
    [SerializeField] float surfaceSlideSpeed;
    [SerializeField] float slopeClimbSpeed;
    [SerializeField] float slopeDecendSpeed;

    bool grounded;
    float currentGravity;
    float HorizontalAxis => Input.GetAxis("Horizontal");
    float VerticalAxis => Input.GetAxis("Vertical");

    void Update()
    {

    }
    void OnDrawGizmos()
    {
        float radius = 0.2f;
        Vector3 worldSpace = transform.TransformPoint(sensorLocal);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(worldSpace, radius);
    }

}
