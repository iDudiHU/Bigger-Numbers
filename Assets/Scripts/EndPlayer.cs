using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlayer : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Target;
    public Transform CurrentTransform;
    public float speed = 10f;
    public float turnSpeed = 1f;
    public GameObject PlayerExplosionEffect;

    private Rigidbody rb;
    void Start()
    {
        Target = GameManager.instance.player.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity += transform.forward * speed * Time.deltaTime;

        var rocketTargetRot = Quaternion.LookRotation(Target.position - transform.position);

        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRot, turnSpeed));
    }
    private void OnCollisionEnter(Collision collision)
    {
		if(collision.gameObject.tag == "Player")
		{
            Instantiate(PlayerExplosionEffect, transform.position, transform.rotation, null);
            Destroy(gameObject);
		}
    }
}
