using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

	public Slider slider;
	public PlayerActions playerActions;
	//public void SetMaxHealth(int health)
	void Update()
	{
		slider.maxValue = playerActions.MAX_HEALTH;
		slider.value = playerActions.health;


	}

	public void SetHealth(int health)
	{
		slider.value = health;

	}

}