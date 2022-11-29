using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feature : MonoBehaviour
{
	private int adn;
	public Image imagen;
	public string pista;
	public FeatureData[] data;

	public int getAdn()
    {
		return adn;
    }
	public void SetADN(int cromo)
    {
		adn = cromo;
		imagen.sprite = data[adn].sprite;
		pista = data[adn].pista;
	}
	public int Randomize()
	{
		int i = Random.Range(0, data.Length);
		SetADN(i);
		return i;
	}
	public void Mutar()
	{
		int i = (adn + Random.Range(1, data.Length)) % data.Length;
		SetADN(i);
	}

	

	//private void ColorBody()
	//{
	//	float sat = -1 / 4f + 0.5f + Random.value / 2f;
	//	float val = 1f - sat / 2f;
	//	float hue = Random.Range(40, 60) / 360f;

	//	if (Random.Range(0, 100) == 5)
	//	{
	//		hue = Random.Range(0, 360) / 360f;
	//	}

	//	imagen.color = Color.HSVToRGB(hue, sat, val);
	//}
}	

