using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unigine;

[Component(PropertyGuid = "9bac5f31f947e210368166b1f65bbd152b169916")]
public class light_cat : Component
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
		ObjectMeshSkinned mesh = node as ObjectMeshSkinned;
		color_zone_mat = mesh.GetMaterial(surface_index);
		
	}
	private void OutputSurfaces(ObjectMeshSkinned obj){
		//Console.MessageLine("{0}", obj.NumSurfaces);
        for (int i = 0; i < obj.NumSurfaces; i++)
        {
            // Console.MessageLine("index: {0} = '{1}'", i, obj.GetSurfaceName(i));
        }
    }
	void Update()
	{
		if (color_zone_mat != null)
		{
			float k = (MathLib.Sin(Game.Time * changeSpeed) + 1) / 2.0f;
			color_zone_mat.SetParameterFloat4("emission_color", MathLib.Lerp(color1, color2, k));
			color_zone_mat.SetParameterFloat("emission_scale", MathLib.Lerp(scale_1, scale_1, k));

		}
	}
}