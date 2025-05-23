using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpObject : MonoBehaviour
{
    public float jumpPower = 30f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Rigidbody>(out Rigidbody target))
        {
            Vector3 targetVelocity = target.velocity;
            targetVelocity.y = 0;
            target.velocity = targetVelocity;
            target.AddForce(Vector3.up * jumpPower , ForceMode.Impulse);
        }
    }
}
