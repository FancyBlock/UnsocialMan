using UnityEngine;
using System.Collections;

public class Window : MonoBehaviour 
{
	public GameObject m_blackMask;
	public bool m_isFlash;
	
	protected float m_time;
	protected bool m_show;

	// Use this for initialization
	void Start () 
	{
		m_blackMask.SetActive( false );
		m_time = 0.0f;
		m_show = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( m_isFlash )
		{
			if( m_time >= 0.3f )
			{
				m_show = !m_show;
				m_blackMask.SetActive( m_show );
				
				m_time = 0.0f;
			}
			
			m_time += Time.deltaTime;
		}
	}
}
