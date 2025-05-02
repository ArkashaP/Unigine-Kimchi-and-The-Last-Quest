using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "10b8d7435358b8dc22903da573f88e661cad3e17")]
public class Respawn : Component
{
	void Init()
	{
		// write here code to be called on component initialization
		
	}
	
	void Update()
	{
		// write here code to be called before updating each render frame
		if(Input.IsKeyPressed(Input.KEY.G))
		{
			node.WorldPosition = new vec3(0f,0f,1f);
		}
	}
}