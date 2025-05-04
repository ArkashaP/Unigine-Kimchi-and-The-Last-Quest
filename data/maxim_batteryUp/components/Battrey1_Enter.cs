using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unigine;

[Component(PropertyGuid = "ad8d73d92a2e0ac5ed58938abff80ad15e142031")]
public class Battrey1_Enter : Component
{
	[ShowInEditor][ParameterSlider(Title = "battery1")] private WorldTrigger bat1;   
	[ShowInEditor][ParameterSlider(Title = "island")] private WorldTrigger island;   
	[ShowInEditor][ParameterSlider(Title = "Batteries Counter")] private Node batcountnode;
	[ShowInEditor][ParameterSlider(Title = "respawn")] private WorldTrigger resp;   
	[ShowInEditor][ParameterSlider(Title = "player")] private Node player;   
	private BatteriesCounter batteriesCounter;
	private Respawn respawn;
	private ObjectMeshStatic oms_node;
	public int countBat; 
	private bool hasProcessedIsland; 
	void Init()
	{
		oms_node = node as ObjectMeshStatic;
        if (player != null)
        {
            respawn = player.GetComponent<Respawn>();
            if (respawn == null)
                Log.Error($"Node {player.Name} does not have a globalSpeed component!");
        }
        else
        {
            Log.Error("globalSpeedNode is not set in the editor.");
			return;
        }
		hasProcessedIsland = false;
		batteriesCounter = batcountnode.GetComponent<BatteriesCounter>();
		countBat = 0;
		if(island!=null)
		{
			island.EventEnter.Connect(triggerIsland_enter);
			island.EventLeave.Connect(triggerIsland_leave);
		}
		if(bat1 != null)
			bat1.EventEnter.Connect(trigger1_enter);

		if(resp != null)
		{
			resp.EventEnter.Connect(respawn_enter);
		}

	}
	void trigger1_enter(Node _node)
	{
		batteriesCounter.battery1_count +=1;
		oms_node.SetViewportMask(0, 0);
		bat1.Enabled = false;
	}


	void triggerIsland_enter(Node _node)
	{
		if(!hasProcessedIsland)
		{
			if(batteriesCounter.battery1_count >=2)
			{
				batteriesCounter.battery1_count -=2;
			}
			hasProcessedIsland = true;
		}

	}

	void triggerIsland_leave (Node _node) // это на случай, если у нас весь остров будет в триггере
	{
		hasProcessedIsland = false;
	}

	void respawn_enter(Node _node)
	{
		if (batteriesCounter.battery1_count==0)
		{
			if (respawn.timer >595) // через сколько секунд респавн
			{
				oms_node.SetViewportMask(1, 0);
				bat1.Enabled = true;
			}
		}
	}

	void Update()
	{
		respawn_enter(null);
	}
}