using UnityEngine;
using System.Collections;

public class Curtain : MonoBehaviour 
{
	public Judger m_judger;
	
	protected tk2dAnimatedSprite m_anim;
	protected bool m_isOpen;

	
	// Use this for initialization
	void Start () 
	{
		m_anim = gameObject.GetComponent<tk2dAnimatedSprite>();
		m_isOpen = false;
	}
	
	
	void Update()
	{
		if( m_isOpen )
		{
			if( m_anim.IsPlaying( "OpenWindow" ) == false )
			{
				Destroy( gameObject );
			}
		}
	}
	
	
	// open the curtain
	void OnMouseDown()
	{
		if( m_isOpen == false )
		{
			m_judger.PlayOpenWindowSound();
			m_anim.Play();
			m_isOpen = true;
		}
	}
}
