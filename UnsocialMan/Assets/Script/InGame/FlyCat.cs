using UnityEngine;
using System.Collections;

public class FlyCat : MonoBehaviour 
{
	public const int STATE_IDLE = 0;
	public const int STATE_WAIT = 1;
	public const int STATE_FLY	= 2;
	
	public Camera m_camera;
	public float m_velocity = 1.0f;
	
	protected tk2dSprite m_img;
	protected int m_state;
	protected float m_waitTime;
	protected float m_speed;
	
	// Use this for initialization
	void Start () 
	{
		m_img = gameObject.GetComponent<tk2dSprite>();
		
		resetDir();
		
		m_state = STATE_IDLE;
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if( m_state == STATE_IDLE )
		{
			m_waitTime = Random.Range( 5.0f, 8.0f );
			m_state = STATE_FLY;
			resetDir();
		}
		else if( m_state == STATE_FLY )
		{
			gameObject.transform.Translate( m_speed * Time.deltaTime, 0, 0 );
			if( gameObject.transform.position.x < -2.0f ||
				gameObject.transform.position.x > 2.0f )
			{
				m_state = STATE_IDLE;
			}
		}
		else if( m_state == STATE_WAIT )
		{
			m_waitTime -= Time.deltaTime;
			
			if( m_waitTime <= 0.0f )
			{
				m_state = STATE_IDLE;
			}
		}
	}
	
	
	protected void resetDir()
	{
		gameObject.transform.position = new Vector3( gameObject.transform.position.x,
													Random.Range( m_camera.transform.position.y - 0.7f, 
																	m_camera.transform.position.y + 0.7f ),
													gameObject.transform.position.z );
		
		if( gameObject.transform.position.x > 0.0f )
		{
			m_img.scale = new Vector3( 1, 1, 1 );
			m_speed = -m_velocity;
		}
		else
		{
			m_img.scale = new Vector3( -1, 1, 1 );
			m_speed = m_velocity;
		}
	}
}
