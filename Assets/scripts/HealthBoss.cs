using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBoss : MonoBehaviour
{

	public Slider slider;
	public BossActions bossActions;
	//public void SetMaxHealth(int health)
	void Update()
	{
		slider.maxValue = bossActions.MAX_HEALTH;
		slider.value = bossActions.health;

	}

	public void SetHealth(int health)
	{
		slider.value = health;

	}

}