﻿/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using System.Collections;

/// <summary>
/// Class in charge to manage the diamonds
/// </summary>
namespace BaiyiGame.JumpMrQ
{
	public class DiamondLogic : MonoBehaviorHelper 
	{
		Transform p;

		void Update()
		{
			if(p == null)
				p = playerManager.transform;

			var d = Vector2.Distance(p.position,transform.position);
			if(d < 1.0f)
			{
				if(gameManager != null)
                    PlayerData.diamond ++;

				gameObject.SetActive(false);
			}
		}
	}
}