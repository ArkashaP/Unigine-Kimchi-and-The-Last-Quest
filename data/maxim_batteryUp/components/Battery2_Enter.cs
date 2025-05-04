using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "4573f092ca1afb181b809fdb327cf28b04126e60")]
public class Battery2_Enter : Component
{
	[ShowInEditor][ParameterSlider(Title = "battery2")] private WorldTrigger bat2;  
	[ShowInEditor][ParameterSlider(Title = "Batteries Counter")] private Node batcountnode;
	[ShowInEditor][ParameterSlider(Title = "player")] private Node player;   
	[ShowInEditor][ParameterSlider(Title = "material")] private Material mymat;   
	[ShowInEditor][ParameterSlider(Title = "kap")] private ObjectMeshStatic test;   
	private BatteriesCounter batteriesCounter;
	private ObjectMeshStatic oms_node;
	public int countBat; 
	void Init()
	{
		// write here code to be called on component initialization
		oms_node = node as ObjectMeshStatic;
		batteriesCounter = batcountnode.GetComponent<BatteriesCounter>();
		if(bat2 != null)
			bat2.EventEnter.Connect(trigger2_enter);
	}

	void trigger2_enter(Node _node)
	{
		test.SetMaterial(mymat,0);
		batteriesCounter.battery2_count +=1;
		oms_node.SetViewportMask(0, 0);
		bat2.Enabled = false;
	}	

}