using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "10b8d7435358b8dc22903da573f88e661cad3e17")]
public class Respawn : Component
{
	[ShowInEditor][ParameterSlider(Title = "respawn")] private WorldTrigger resp;   
	void Init()
	{
		resp.EventEnter.Connect(respawn_enter);
	}
	void respawn_enter(Node _node)
	{
		node.WorldPosition = new vec3(0f,0f,1f);
	}	
}