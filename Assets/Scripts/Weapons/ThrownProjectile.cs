using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownProjectile : MonoBehaviour {

	public float throwPower;
	public Rigidbody2D body;
	public float rotateSpeed;

	void Start () 
	{
		body.velocity = new Vector2(Random.Range(-throwPower, throwPower), throwPower);
	}
	
	void Update () 
	{
		transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * 360f * Time.deltaTime * -(Mathf.Sign(body.velocity.x))));
	}
}
