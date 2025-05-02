using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "5e2d14447d167f30234f1bcedad34d72377a0a3b")]
public class TestComp : Component
{
	void Init()
	{
		
		// Рандомный вектор от 0.5 до 2.0
		vec3 randVec3 = MathLib.RandVec3(
			new vec3(0.5f,0.5f,0.5f), // От
			new vec3(2f,2f,2f) // До
		);
	}
	
	void Update()
	{
		
	}
}