using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "ffb668a0dbafe7edefd34b3499d2692c78974c2c")]
public class BatteriesCounter : Component
{
	private Gui gui;
    private WidgetLabel label;
	public int battery1_count = 0;
	void Init()
	{

		// write here code to be called on component initialization
        gui = Gui.GetCurrent();
        label = new WidgetLabel();
        label.Text = "";
        label.PositionX = 20;
        label.PositionY = 20;
        gui.AddChild(label, Gui.ALIGN_OVERLAP);
	}
	
	void Update()
	{
		// write here code to be called before updating each render frame
		//battery1_count += battrey1_Enter.countBat;
		label.Text = ("bat1: " + battery1_count.ToString());
	}
}