using UnityEngine;
using System.Collections;

public class FiveProgressBar : MonoBehaviour 
{
	public Judger m_judger;
	public UISprite[] m_results;
	
	protected int m_curCount = 0;

	// Use this for initialization
	void Start () 
	{
		foreach( UISprite spr in m_results )
		{
			spr.spriteName = "icon_unstrat";
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		ArrayList list = m_judger.GetResultList();
		
		if( list.Count > m_curCount )
		{
			int i = 0;
			foreach( bool result in list )
			{
				if( result )
				{
					m_results[i].spriteName = "icon_right";
				}
				else
				{
					m_results[i].spriteName = "icon_wrong";
				}
				
				i++;
			}
			
			m_curCount = list.Count;
		}
	}
}
