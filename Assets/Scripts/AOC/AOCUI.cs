using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AOCUI : Singleton<AOCUI>
{

	[Header("References")]
	public Text DayText;
	public Image Star1;
	public Image Star2;
	public RawImage Part1ComputeInput;
	public RawImage Part1ComputeOutput;

	[Header("Stuff")]
	[Range(1, 25)]
	public int CurrentDay;
	private int _LastCurrentDay;


	void Update()
	{
		if (_LastCurrentDay != CurrentDay)
		{
			_LastCurrentDay = CurrentDay;
			UpdateUI();
		}
	}

	private void UpdateUI()
	{
		DayText.text = $"Day {CurrentDay}";
		var score = AOCScore.Instance.Scores[CurrentDay - 1];
		switch (score)
		{
			case AOCScore.DayScore.NoStar:
				SetStars(false, false);
				break;
			case AOCScore.DayScore.OneStar:
				SetStars(true, false);
				break;
			case AOCScore.DayScore.TwoStars:
				SetStars(true, true);
				break;
		}
	}

	private void SetStars(bool firstStartOn, bool secondStartOn)
	{
		Star1.gameObject.SetActive(firstStartOn);
		Star2.gameObject.SetActive(secondStartOn);
	}
}
