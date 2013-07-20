using UnityEngine;
using System.Collections;

public class Blood : MonoBehaviour 
{
	public Judger m_judger;
	
	protected tk2dSprite m_img;
	protected float m_alpha = 1.0f;
	protected bool m_fadeOut = false;

	// Use this for initialization
	void Start () 
	{
		m_img = gameObject.GetComponent<tk2dSprite>();
		
		StartCoroutine( "showBlood" );
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( m_fadeOut )
		{
			if( m_alpha <= 0.0f )
			{
				m_judger.EndCurLevel();
				
				Destroy( gameObject );
			}
			
			m_img.color = new Color( 1.0f, 1.0f, 1.0f, m_alpha );
			
			m_alpha -= 0.01f;
		}
	}
	
	protected IEnumerator showBlood()
	{
		TweenScale.Begin( gameObject, 0.1f, new Vector3( 2.0f, 2.0f, 2.0f ) ).from = new Vector3( 0.1f, 0.1f, 0.1f );
		yield return new WaitForSeconds( 0.5f );
		m_fadeOut = true;
	}
}
