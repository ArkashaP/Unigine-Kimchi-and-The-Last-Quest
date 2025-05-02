using System.Collections;
using System.Collections.Generic;
using Unigine;


[Component(PropertyGuid = "42f51590e8ff3483dc12647fe6630cfba2fcbdcd")]
public class Shine : Component
{
	Node ShineBox;
	Node Cat;

	public Random random = new Random();
	public Node Battery;	
	public ObjectMeshStatic boxMesh, batteryMesh;
	public float ifps = Game.IFps;
	public float emissionTimer;
	public float progress, targetEmission;
	
	//[ShowInEditor] 
	private float TRIGGER_DISTANCE = 5.0f; //Триггер дистанции
	private float EMISSION_DURATION = 5.0f; //Длительность увлеичивания свечения
	private float MAX_EMISSION = 5.0f;     //максимальное значение свечения
	private float MIN_EMISSION = 0.0f;	 //минимальное значение свечения
	private float LERP_SPEED = 2.0f; 
	private float currentEmission;	


	void Init()
	{
		ShineBox = node as ObjectMeshStatic;
		Cat = World.GetNodeByName("Cat");
		currentEmission = boxMesh.GetMaterialParameterFloat("emission_scale", 0);
	}
	
	void Update()
	{
		progress = MathLib.Saturate(emissionTimer / EMISSION_DURATION);
		

		dvec3 shineBoxPos = ShineBox.WorldPosition;
		//Персонаж
		dvec3 catPos = Cat.WorldPosition;
		dvec3 direction = Cat.GetWorldDirection(MathLib.AXIS.Y);
		
		boxMesh = ShineBox as ObjectMeshStatic;
		double distance = MathLib.Length(catPos - shineBoxPos);

		if(distance <= TRIGGER_DISTANCE)
		{
			targetEmission = MAX_EMISSION;
			if(Input.IsKeyDown(Input.KEY.E))
			{
				addBattery(Battery, ShineBox);
			}	
		}
		else
		{
			targetEmission = MIN_EMISSION;
		}

		// Плавно интерполируем текущее значение к целевому
		currentEmission = MathLib.Lerp(currentEmission, targetEmission, LERP_SPEED * Game.IFps);
		// Устанавливаем параметр материала
		boxMesh.SetMaterialParameterFloat("emission_scale", currentEmission, 0);
		


		//Cat
		if(Input.IsKeyPressed(Input.KEY.UP))
		{
			Cat.WorldPosition += direction * 0.1;
		}

		if(Input.IsKeyPressed(Input.KEY.DOWN))
		{
			Cat.WorldPosition -= direction * 0.1;
		}

		if(Input.IsKeyPressed(Input.KEY.LEFT))
		{
			Cat.Rotate(0.0f, 0.0f, 1);
		}

		if(Input.IsKeyPressed(Input.KEY.RIGHT))
		{
			Cat.Rotate(0.0f, 0.0f, -1);
		}

		if(MathLib.Length(Cat.WorldPosition - Battery.WorldPosition) <= 1)
		{
			Battery.WorldPosition += new dvec3(0f, 0f, 1000f);
		}


	}

	private void addBattery(Node obj, Node parent)
    {
		//float randomFloat = (float)(random.Double() * 1.5); // рандомное появление батарейки сделать не поулчилось(((
		obj.WorldPosition = parent.WorldPosition + new dvec3(1f, 1f, 0f);
    }

}