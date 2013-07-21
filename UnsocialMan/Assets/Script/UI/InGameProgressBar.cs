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
			m_slider.sliderValue = ( GlobalWork.CurrentTimeOut - m_judger.TIME ) / GlobalWork.CurrentTimeOut;
		}
	}
}
