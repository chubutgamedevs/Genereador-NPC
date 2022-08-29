using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSample : MonoBehaviour {
	public Image cbody;
	public Image cface;
	public Image chair;
	public Image ckit;
	public Sprite[] body;
	public Sprite[] face;
	public Sprite[] hair;
	public Sprite[] kit;
	public Color[] background;
	private Camera cam;
	public string nombre = "";

	void Start () {
		cam = GameObject.Find("Main Camera").GetComponent<Camera>();
		RandomizeCharacter();
	}
	
	public void RandomizeCharacter(){
		//		cbody.sprite = body[0];
		//Random.InitState(nombre.GetHashCode());
		cbody.sprite = body[Random.Range(0,body.Length)];
		cface.sprite = face[Random.Range(0,face.Length)];
		chair.sprite = hair[Random.Range(0,hair.Length)];
		ckit.sprite = kit[Random.Range(0,kit.Length)];

		ColorPelo();
		ColorBody();

		cam.backgroundColor = background[Random.Range(0,background.Length)];
	}
	private void ColorPelo()
    {
		float sat = Random.value;
		float val = 1f - sat / 2f;
		float hue = Random.value;
		chair.color = Color.HSVToRGB(hue, sat, val);
	}
	private void ColorBody()
    {
		float sat = -1 / 4f + 0.5f + Random.value / 2f;
		float val = 1f - sat / 2f;
		float hue = Random.Range(40, 60)/360f;

		if (Random.Range(0,100) == 5) 
        {
			hue = Random.Range(0, 360) / 360f;
        }

		cbody.color = Color.HSVToRGB(hue, sat, val);
	}

}
