﻿/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using System.Collections;

namespace BaiyiGame.JumpMrQ
{
	public class ObstacleDestroyer : MonoBehaviour
	{
		void OnTriggerEnter2D(Collider2D other)
		{
			gameObject.SetActive (false);
			transform.position = Vector3.zero;
		}
	}
}