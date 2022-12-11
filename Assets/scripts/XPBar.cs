using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : MonoBehaviour
{
	//handles the UI for the XP bar
	public Slider slider;
	
	void Update()
	{
		slider.maxValue = GameState.XPtoLevel;
		slider.value = GameState.PlayerXP;

	}

}
