using UnityEngine;
using System.Collections;

public class DropedMan : MonoBehaviour 
{
	//---------------- public properties ----------------
	
	public Judger m_judger;
	public GameObject m_bloodMockup;
	
	//---------------- public functions ------------------

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		float xOffset = 0.0f;
		if( gameObject.transform.position.x > 0.75f )
		{
			xOffset = CommonConstant.DROP_VELOCITY * Time.deltaTime * 1.2f;
		}
		else if( gameObject.transform.position.x < -0.75f )
		{
			xOffset = -CommonConstant.DROP_VELOCITY * Time.deltaTime * 1.2f;
		}
			
		gameObject.transform.Translate( xOffset,  CommonConstant.DROP_VELOCITY * Time.deltaTime, 0 );
		
		float bottomLine = CommonConstant.BOTTOM_LINE + ( m_judger.CUR_LEVEL_CNT - 1.0f ) * 2.0f;
		if( gameObject.transform.position.y <=  bottomLine  )
		{
			m_judger.EndCurLevel();
			
			Destroy( gameObject );
		}
	}
	
	// kill the man
	void OnMouseDown()
	{
		m_judger.PlayHitSound();
		
		// kill the drop man
		GameObject blood = (GameObject)Instantiate( m_bloodMockup, gameObject.transform.position, Quaternion.identity );
		blood.GetComponent<Blood>().m_judger = m_judger;
		
		Destroy( gameObject );
	}
	
}
