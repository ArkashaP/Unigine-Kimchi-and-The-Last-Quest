using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "750093d9f5a80d13f7f1ca7044d183741f44bee9")]
public class Item_Test : Component
{
	void Init()
	{
		
	}
	private float speed = 100;
	void Update()
	{
		node.Rotate(0,0,speed*Game.IFps);
	}
}