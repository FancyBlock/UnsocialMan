using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour 
{
	public GameObject m_blackMask;
	public bool m_isFlash;
	
	//protected float m_time;
	//protected bool m_show;

	// Use this for initialization
	void Start () 
	{
		m_blackMask.SetActive( false );
		
		if (m_isFlash)
		{
			InvokeRepeating("StartRound", Random.Range(0.0f, 1.0f), 1);
		}
		
		//m_time = 0.0f;
		//m_show = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//if( m_isFlash )
		//{
		//	if( m_time >= 0.3f )
		//	{
		//		m_show = !m_show;
		//		m_blackMask.SetActive( m_show );
				
		//		m_time = 0.0f;
		//	}
			
		//	m_time += Time.deltaTime;
		//}
	}
	
	void StartRound()
	{
		Invoke("flashOn", 0.1f);
		Invoke("flashOn", 0.4f);
		Invoke("flashOn", 0.55f);
		
		//AudioClip clip = Resources.Load("Sound/window_flash") as AudioClip;
		//AudioSource.PlayClipAtPoint(clip, Vector3.zero, 0.3f);
	}
	
	void flashOn()
	{
		Invoke("flashOff", 0.1f);
		
		m_blackMask.SetActive(false);
	}
	
	void flashOff()
	{
		m_blackMask.SetActive(true);
	}
}
