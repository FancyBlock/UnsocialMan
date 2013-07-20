using UnityEngine;
using System.Collections;

public class Lose : MonoBehaviour {
	public UIPanel Panel0;
	public UIPanel Panel1;
	public UIPanel Panel2;
	public UIPanel PanelGameOver;
	public UIPanel Black;
	
	public AudioClip ClickSound;
	public AudioClip BombSound;
	
	public UITexture BombTexture;
	public Texture2D[] BombTexs;
	
	bool mAllowReturn = false;

	// Use this for initialization
	void Start () {
		Invoke("StartBomb", 1.5f + 2);
		Invoke("ChangeBack", 2 + 2);
		Invoke("GameOver", 3 + 2);
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
	
	void ChangeBack()
	{
		Panel1.alpha = 0;
		Panel2.alpha = 1;
	}
	
	void GameOver()
	{
		TweenAlpha.Begin(PanelGameOver.gameObject, 1.0f, 1);
		mAllowReturn = true;
	}
	
	void Load()
	{
		Application.LoadLevel("Title");
	}
	
	void StartBomb()
	{
		AudioSource.PlayClipAtPoint(BombSound, Vector3.zero);
		InvokeRepeating("ShowNextBomb", 0, 0.12f);
		
		iTween.ShakePosition(Panel0.gameObject, new Vector3(0.1f, 0.1f, 0), 1.5f);
		iTween.ShakePosition(Panel1.gameObject, new Vector3(0.1f, 0.1f, 0), 1.5f);
		iTween.ShakePosition(Panel2.gameObject, new Vector3(0.1f, 0.1f, 0), 1.5f);
	}
	
	int mBombIndex = -1;
	void ShowNextBomb()
	{
		if (mBombIndex < BombTexs.Length - 1)
		{
			mBombIndex += 1;
			BombTexture.gameObject.SetActive(true);
			BombTexture.material.mainTexture = BombTexs[mBombIndex];
		}
		else
		{
			BombTexture.gameObject.SetActive(false);
		}
	}
}
