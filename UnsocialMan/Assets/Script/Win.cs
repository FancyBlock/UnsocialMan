using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {
	public UIPanel PanelText;
	public UIPanel Black;
	public AudioClip ClickSound;
	
	bool mAllowReturn = false;
	
	// Use this for initialization
	void Start () {
		Invoke("ShowText", 1);
	}
	
	// Update is called once per frame
	void Update () {
		if (mAllowReturn && Input.GetMouseButtonDown(0))
		{
			AudioSource.PlayClipAtPoint(ClickSound, Vector3.zero);
			
			mAllowReturn = false;
			TweenAlpha.Begin(Black.gameObject, 1, 1);
			Invoke("Load", 1);
		}
	}
	
	void ShowText()
	{
		TweenAlpha.Begin(PanelText.gameObject, 1, 1);
		mAllowReturn = true;
	}
	
	void Load()
	{
		Application.LoadLevel("Title");
	}
}
