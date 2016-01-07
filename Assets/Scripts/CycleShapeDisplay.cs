using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CycleShapeDisplay : MonoBehaviour {
	public Sprite [] sprites;
	public int defaultIndex;

	private int currentIndex;
	private Image image;

	// Use this for initialization
	void Start () {
		if (sprites.Length > 0)
		{
			if (defaultIndex < 0 || defaultIndex > sprites.Length)
			{
				defaultIndex = 0;
			}
			currentIndex = defaultIndex;
			
			image = GetComponent<Image>();
			image.sprite = sprites[currentIndex];
		} else
		{
			Debug.Log ("Cycling Shape Display missing elements of sprite array");
		}

	}

	public void NextShape()
	{
		if (++currentIndex == sprites.Length) {
			currentIndex = 0;
		}
		image.sprite = sprites[currentIndex];
	}

	public void PreviousShape()
	{
		if (--currentIndex == -1) {
			currentIndex = sprites.Length - 1;
		}
		image.sprite = sprites[currentIndex];
	}
}
