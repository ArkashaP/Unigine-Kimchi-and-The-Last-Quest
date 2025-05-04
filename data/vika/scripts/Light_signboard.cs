using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Timers;
using Unigine;

[Component(PropertyGuid = "ef1102683c085c3b52acf9c4655eeb77a9fcd60e")]
public class Light_signboard : Component
{
	ObjectMeshStatic oms;
    double timeNext = 0;
    double timeWaitAmount = 1.0f;
    double timeEnabledWaitAmount = 1.0f;
    
    public Vector4 color_1 = new vec4();
	Boolean mode = true;
    int enabled = 1;

    void Init()
    {
        oms = node as ObjectMeshStatic;
    }
    private float speed = 100;
    void Update()
    {
        if(Game.Time - timeNext >= timeWaitAmount && mode){
            timeNext = Game.Time;
            timeWaitAmount = MathLib.RandDouble(0.2f, 0.6f);
            timeEnabledWaitAmount = timeWaitAmount + MathLib.RandDouble(0.2f, 0.5f);
            enabled = enabled == 1 ? enabled = 0 : enabled = 1 ;
			oms.SetMaterialParameterFloat4("emission_color", new vec4(10,10,255, 255), 0);
            oms.SetMaterialParameterFloat("emission_scale", 0.00001f*enabled , 0);
			mode = false;
			return;
        }
		if(Game.Time - timeNext >= timeWaitAmount && !mode){
            timeNext = Game.Time;
            timeWaitAmount = MathLib.RandDouble(0.5f, 2f);
            timeEnabledWaitAmount = timeWaitAmount + MathLib.RandDouble(0.2f, 0.5f);
            //enabled = enabled == 1 ? enabled = 0 : enabled = 1 ;
            //oms.SetMaterialParameterFloat("emission_scale", 4.0f*enabled , 0);
			//qwe.SetMaterialParameterFloat("emission_scale", 0.0001f, 0);
			oms.SetMaterialParameterFloat4("emission_color", new vec4(0,255,0, 255), 0);
			mode = true;
			return;
        }

    }
}