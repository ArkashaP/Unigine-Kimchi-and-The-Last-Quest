using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "ad8d73d92a2e0ac5ed58938abff80ad15e142031")]
public class Battrey1_Enter : Component
{
	[ShowInEditor][ParameterSlider(Title = "battery1")] private WorldTrigger bat1;   
	[ShowInEditor][ParameterSlider(Title = "battery2")] private WorldTrigger bat2;   
	[ShowInEditor][ParameterSlider(Title = "Batteries Counter")] private Node batcountnode;
	private BatteriesCounter batteriesCounter;
	public int countBat; 
	void Init()
	{
		batteriesCounter = batcountnode.GetComponent<BatteriesCounter>();
		countBat = 0;
		if(bat1 != null)
			bat1.EventEnter.Connect(trigger1_enter);
		if(bat2 != null)
			bat2.EventEnter.Connect(trigger2_enter);
	}
	void trigger1_enter(Node _node)
	{
		batteriesCounter.battery1_count +=1;
		node.Enabled = false;
	}
	void trigger2_enter(Node _node)
	{
		batteriesCounter.battery2_count +=1;
		node.Enabled = false;
	}
}