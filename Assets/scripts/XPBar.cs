using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
	public Slider slider;
	
	void Update()
	{
		slider.maxValue = GameState.XPtoLevel;
		slider.value = GameState.PlayerXP;

	}

	/*public void SetHealth(int health)
	{
		slider.value = health;

	}*/
}
