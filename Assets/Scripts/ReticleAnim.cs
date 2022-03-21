using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ReticleAnim : MonoBehaviour
{
	private Shooter _shooter;
	private Animation _animation;
	private void Start()
	{
		_animation = gameObject.GetComponent<Animation>();
		_shooter = GameManager.instance.player.GetComponentInChildren<PlayerController>().playerShooter;
		_shooter.OnGunShot += Shooter_OnGunShot;
	}

	private void Shooter_OnGunShot(object sender, EventArgs e)
	{
		_animation.Play();
	}
}
