using UnityEngine;
using System.Collections;

public class InGameProgressBar : MonoBehaviour 
{
	public Judger m_judger;
	
	
	protected UISlider m_slider;
	
	void Start()
	{
		m_slider = gameObject.GetComponent<UISlider>();
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if( m_judger != null )
		{
			m_slider.sliderValue = ( CommonConstant.TIMEOUT_TIME - m_judger.TIME ) / CommonConstant.TIMEOUT_TIME;
		}
	}
}
