using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "bb896825bb437ed9266bea9b3e067ea8a024228e")]
public class Move_battery : Component
{
	public float speed = 0.5f;
	public double position_up;
	public int direction = 1;

	void Init()
	{
		position_up = node.WorldPosition.z;
	}
	
	void Update()
	{
		node.WorldRotate(0.5f*direction, 0.0f, 0.0f);
		node.WorldRotate(0.0f, 0.5f*direction, 0.0f);
		node.WorldRotate(0.0f, 0.0f, 1.0f);

		node.WorldPosition = node.WorldPosition + new vec3(0, 0, 0.3) * direction * Game.IFps;
		if(node.WorldPosition.z - position_up >= 0.1)
		{
			direction = -1;
		}

		if(node.WorldPosition.z - position_up <= -0.1)
		{
			direction = 1;
		}
	}
}