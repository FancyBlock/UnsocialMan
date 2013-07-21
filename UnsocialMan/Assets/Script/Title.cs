using UnityEngine;
using System.Collections;

public class Title : MonoBehaviour {
	public UIPanel TitlePanel;
	public UIPanel CutscenePanel;
	
	public UISprite[] Cutscenes;
	
	public AudioClip ClickSound;
	
	bool mGameStart = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!mGameStart && Input.GetMouseButtonDown(0))
		{
			mGameStart = true;
			CurtSceneStart();
			
			AudioSource.PlayClipAtPoint(ClickSound, Vector3.zero);
		}
	}
	
	void CurtSceneStart()
	{
		TweenAlpha.Begin(TitlePanel.gameObject, 0.5f, 0);
		
		Invoke("CurtScene0", 0.5f);
		Invoke("CurtScene1", 2.0f);
		Invoke("CurtScene2", 5.0f);
		Invoke("CurtScene3", 8.0f);
		Invoke("CurtScene4", 12.0f);
		Invoke("CurtScene5", 15.0f);
		Invoke("CurtScene6", 18.0f);
		Invoke("CurtScene7", 21.0f);
		Invoke("CurtSceneEnd", 24.0f);
	}
	
	void CurtScene0()
	{
		TweenAlpha.Begin(CutscenePanel.gameObject, 0.5f, 1);
	}
	
	void CurtScene1()
	{
		for (int i = 0; i < 6; i++)
		{
			TweenAlpha.Begin(Cutscenes[i].gameObject, 1.0f, i == 0 ? 1 : 0);
		}
	}
	
	void CurtScene2()
	{
		for (int i = 0; i < 6; i++)
		{
			TweenAlpha.Begin(Cutscenes[i].gameObject, 1.0f, i == 1 ? 1 : 0);
		}
	}
	
	void CurtScene3()
	{
		for (int i = 0; i < 6; i++)
		{
			TweenAlpha.Begin(Cutscenes[i].gameObject, 1.0f, i == 2 ? 1 : 0);
		}
	}
	
	void CurtScene4()
	{
		for (int i = 0; i < 6; i++)
		{
			TweenAlpha.Begin(Cutscenes[i].gameObject, 1.0f, i == 3 ? 1 : 0);
		}
	}
	
	void CurtScene5()
	{
		for (int i = 0; i < 6; i++)
		{
			TweenAlpha.Begin(Cutscenes[i].gameObject, 1.0f, i == 4 ? 1 : 0);
		}
	}
	
	void CurtScene6()
	{
		for (int i = 0; i < 6; i++)
		{
			TweenAlpha.Begin(Cutscenes[i].gameObject, 1.0f, i == 5 ? 1 : 0);
		}
	}
	
	void CurtScene7()
	{
		for (int i = 0; i < 6; i++)
		{
			TweenAlpha.Begin(Cutscenes[i].gameObject, 1.0f, 0);
		}
	}
	
	void CurtSceneEnd()
	{
		GlobalWork.CurrentBuilding = 1;
		GlobalWork.LastLevelRestTime = 0.0f;
		GlobalWork.CurrentTimeOut = 5.0f;
		
		Application.LoadLevel("InGame");
	}
}
