using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using Unigine;

[Component(PropertyGuid = "8c74dbb0d2d8e3e36c0dc386a0cc6ac7eeb64c35")]
public class Blink_light_signboard : Component
{
	public float changeSpeed = 1.5f;
	public int surface_index = 0;

	[ParameterColor]
    public vec4 color1;
	
	[ParameterSlider(Min = 1f, Max = 4f)]
	public float scale_1 = 1f;
	
	[ParameterColor]
	public vec4 color2;

	
	[ParameterSlider(Min = 1f, Max = 4f)]
	public float scale_2 = 1f;

	Material color_zone_mat;

	void Init()
	{
		ObjectMeshStatic mesh = node as ObjectMeshStatic;
		color_zone_mat = mesh.GetMaterial(surface_index);
	}

	void Update()
	{

		if (color_zone_mat != null)
		{
			float k = (MathLib.Sin(Game.Time * changeSpeed) + 1) / 2.0f;
			color_zone_mat.SetParameterFloat4("emission_color", MathLib.Lerp(color1, color2, k));
			color_zone_mat.SetParameterFloat("emission_scale", MathLib.Lerp(scale_1, scale_2, k));

		}
	}
}