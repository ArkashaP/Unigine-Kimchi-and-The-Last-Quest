using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "23cabb45a8ed0ab1578cc9bf9e243e3d911ba13d")]
public class BatteryWood : Component
{
    [ShowInEditor] private Gui realGui1, realGui2, realGui3, realGui4, realGui5;
	[ShowInEditor]private ObjectGui wood1;
	[ShowInEditor]private ObjectGui wood2;
	[ShowInEditor]private ObjectGui wood3;
	[ShowInEditor]private ObjectGui wood4;
	[ShowInEditor]private ObjectGui wood5;
	[ShowInEditor]private ObjectMeshStatic battrey1;
	[ShowInEditor]private ObjectMeshStatic battrey2;
	[ShowInEditor]private ObjectMeshStatic battrey3;
	[ShowInEditor]private ObjectMeshStatic battrey4;
	[ShowInEditor]private ObjectMeshStatic battrey5;
    [ShowInEditor][ParameterFile] private string texturePath;
    [ShowInEditor][ParameterFile] private string texturePath2;
	private WidgetSprite sprite1, sprite2, sprite3, sprite4, sprite5;
	private Image[] images;

	void Init()
	{
		realGui1 = wood1.GetGui();
		realGui2 = wood2.GetGui();
		realGui3 = wood3.GetGui();
		realGui4 = wood4.GetGui();
		realGui5 = wood5.GetGui();
		images = new[]
        {
            new Image(texturePath),
            new Image(texturePath2),
        };

		sprite1 = new WidgetSprite(realGui1)
        {
            Height = realGui1.Height,
            Width = realGui1.Width
        };

		sprite2 = new WidgetSprite(realGui2)
        {
            Height = realGui2.Height,
            Width = realGui2.Width
        };

		sprite3 = new WidgetSprite(realGui3)
        {
            Height = realGui3.Height,
            Width = realGui3.Width
        };

		sprite4 = new WidgetSprite(realGui4)
        {
            Height = realGui4.Height,
            Width = realGui4.Width
        };

		sprite5 = new WidgetSprite(realGui5)
        {
            Height = realGui5.Height,
            Width = realGui5.Width
        };

        sprite1.SetImage(images[0]);
        sprite2.SetImage(images[0]);
        sprite3.SetImage(images[0]);
        sprite4.SetImage(images[0]);
        sprite5.SetImage(images[0]);


        realGui1.AddChild(sprite1, Gui.ALIGN_OVERLAP | Gui.ALIGN_BACKGROUND | Gui.ALIGN_CENTER);
        realGui2.AddChild(sprite2, Gui.ALIGN_OVERLAP | Gui.ALIGN_BACKGROUND | Gui.ALIGN_CENTER);
        realGui3.AddChild(sprite3, Gui.ALIGN_OVERLAP | Gui.ALIGN_BACKGROUND | Gui.ALIGN_CENTER);
        realGui4.AddChild(sprite4, Gui.ALIGN_OVERLAP | Gui.ALIGN_BACKGROUND | Gui.ALIGN_CENTER);
        realGui5.AddChild(sprite5, Gui.ALIGN_OVERLAP | Gui.ALIGN_BACKGROUND | Gui.ALIGN_CENTER);
	}
	
	void Update()
	{
		if (battrey1.GetViewportMask(0)==0)
			sprite1.SetImage(images[1]);
		if (battrey2.GetViewportMask(0)==0)
			sprite2.SetImage(images[1]);
		if (battrey3.GetViewportMask(0)==0)
			sprite3.SetImage(images[1]);
		if (battrey4.GetViewportMask(0)==0)
			sprite4.SetImage(images[1]);
		if (battrey5.GetViewportMask(0)==0)
			sprite5.SetImage(images[1]);
	}
}