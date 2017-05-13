using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Class to demonstrate how to use UI components
public class UIManager : MonoBehaviour 
{
	static GameObject Panel_Summary;
	static Text Text_House1;
	static Text Text_House2;
	static Text Text_House3;
	static Text Text_House4;

	// Use this for initialization
	void Start () {
		Panel_Summary = transform.FindChild("Panel_Summary").gameObject;
		Text_House1 = transform.FindChild("Panel_Summary/Text_House1").GetComponent<Text>();
		Text_House2 = transform.FindChild("Panel_Summary/Text_House2").GetComponent<Text>();
		Text_House3 = transform.FindChild("Panel_Summary/Text_House3").GetComponent<Text>();
		Text_House4 = transform.FindChild("Panel_Summary/Text_House4").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PanelSummaryDisplay(bool show)
	{
		
		Text_House1.text = CityBuilder.numApartment [1].ToString();
		Text_House2.text = CityBuilder.numApartment [2].ToString();
		Text_House3.text = CityBuilder.numApartment [3].ToString();
		Text_House4.text = CityBuilder.numApartment [4].ToString();

		Panel_Summary.SetActive (show);

	}
}
