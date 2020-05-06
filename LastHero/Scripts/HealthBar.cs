using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour
{
	float maxValue = 100;
	public Color color = Color.red;
	public Slider slider;
	public PlayerScript player;

	private static float current;
	public float Current
	{
		get
		{
			return player.player.Health * 100 / player.player.MaxHealth;
		}
		set
		{			
			if (current < 0)
				current = 0;
			else if (current > maxValue)
				current = maxValue;
			else
				current = value;

		}
	}
	void Start()
	{
		slider.fillRect.GetComponent<Image>().color = color;
		slider.maxValue = maxValue;
		slider.minValue = 0;
		Current = maxValue;
		UpdateUI();
	}

	void Update()
	{
		slider.value = Current;
	}

	void UpdateUI()
	{
		
		RectTransform rect = slider.GetComponent<RectTransform>();
		int rectDeltaX = Screen.width / 10;
		float rectPosX = Screen.width / 2;	
		//GUI.Slider(new Rect(rectPosX, Screen.height * 0.65f, rectDeltaX, rect.sizeDelta.y), 100, 100, 0, 100, new GUIStyle(), new GUIStyle(), false, 1);
		slider.direction = Slider.Direction.LeftToRight;
		rect.sizeDelta = new Vector2(rectDeltaX, rect.sizeDelta.y);
		rect.position = new Vector3(rectPosX, Screen.height * 0.65f, player.transform.position.z);

	}
}