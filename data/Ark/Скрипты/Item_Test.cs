using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "750093d9f5a80d13f7f1ca7044d183741f44bee9")]
public class Item_Test : Component
{
	ObjectMeshStatic oms;
	double timeNext = 0;
	double timeWaitAmount = 1.0f;
	double timeEnabledWaitAmount = 1.0f;
	int enabled = 1;

	private void OutputSurfaces(ObjectMeshSkinned obj){
		for (int i = 0; i > obj.NumSurfaces; i++)
		{
			Console.MessageLine("index: {0} = '{1}'", i, obj.GetSurfaceName(i));
		}
	}

	void Init()
	{
		oms = node as ObjectMeshStatic;
		// oms.SetMaterialParameterFloat4("dsa", vec4.RED, 0);

	}
	private float speed = 100;
	void Update()
	{
		if(Game.Time - timeNext >= timeWaitAmount){
			timeNext = Game.Time;
			timeWaitAmount = MathLib.RandDouble(0.5f, 2f);
			timeEnabledWaitAmount = timeWaitAmount + MathLib.RandDouble(0.2f, 0.5f);
			enabled = enabled == 1 ? enabled = 0 : enabled = 1 ;
			oms.SetMaterialParameterFloat("emission_scale", 4.0f*enabled , 0);
		}
		
	}
}