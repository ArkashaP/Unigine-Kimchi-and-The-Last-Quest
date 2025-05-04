using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "10b8d7435358b8dc22903da573f88e661cad3e17")]
public class Respawn : Component
{
	[ShowInEditor][ParameterSlider(Title = "respawn")] public WorldTrigger resp; 
	
	public float timer; // Текущее время
	public bool isTimerRunning = false;  
	void Init()
	{
		resp.EventEnter.Connect(respawn_enter);
	}
	void respawn_enter(Node _node)
	{
		node.WorldPosition = new vec3(-5f, 193f, 95f);
		isTimerRunning = true;
	}	
	void StartTimer(){
		timer += Game.IFps;
	}

	void StopTimer()
	{
		timer = 0.0f;
	}
	void Update(){
		if (isTimerRunning)
			StartTimer();
		if(timer >= 600.0f)
		{
			StopTimer();
			isTimerRunning = false;
		}
	}
}