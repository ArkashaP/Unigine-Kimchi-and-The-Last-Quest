using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "ffb668a0dbafe7edefd34b3499d2692c78974c2c")]
public class BatteriesCounter : Component
{
	private Gui gui;
    private WidgetLabel label;
	public int battery1_count = 0;
	public int battery2_count = 0;
	void Init()
	{
        gui = Gui.GetCurrent();
        label = new WidgetLabel();
        label.Text = "";
        label.PositionX = 20;
        label.PositionY = 20;
        gui.AddChild(label, Gui.ALIGN_OVERLAP);
	}
	
	void Update()
	{
		label.Text = ("bat1: " + battery1_count.ToString() + "\nbat2: " + battery2_count.ToString());
	}
}