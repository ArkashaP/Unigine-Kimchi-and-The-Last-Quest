using System;
using System.Collections;
using System.Collections.Generic;
using Unigine;


[Component(PropertyGuid = "42f51590e8ff3483dc12647fe6630cfba2fcbdcd")]
public class Shine : Component
{
	Node ShineBox;
	public Node Cat; // Добавил модификатор "public" для этой переменной, чтобы можно выбрать в ручную - кто игрок (в Unigine Editor)

	public Node Battery;	
	public ObjectMeshStatic boxMesh, batteryMesh;
	// public float ifps = Game.IFps; // Убрал эту строку, тк IFps это динамическое значение, и оно меняется в зависимости от кол-ва кадров. 
	public float emissionTimer;
	public float progress, targetEmission;
	
	// Добавлено
	private dvec3 targetDeltaPos = 0; 
	private bool isBatteryShown = false;

	private float TRIGGER_DISTANCE = 5.0f; //Триггер дистанции
	private float EMISSION_DURATION = 5.0f; //Длительность увлеичивания свечения
	private float MAX_EMISSION = 5.0f;     //максимальное значение свечения
	private float MIN_EMISSION = 0.0f;	 //минимальное значение свечения
	private float LERP_SPEED = 2.0f; 
	private float currentEmission;	

	void Init()
	{
		ShineBox = node as ObjectMeshStatic;
		// Cat = World.GetNodeByName("Cat"); // Убрано, тк кота мы назначаем в Unigine Editor
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
		double distance = MathLib.Distance(catPos, shineBoxPos);

		if(distance <= TRIGGER_DISTANCE)
		{
			targetEmission = MAX_EMISSION;
			if(Input.IsKeyDown(Input.KEY.E))
			{
				showItem(); // Если мы нажали Е, то включаем таймер отчета времени
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
			isBatteryShown = false;
		}

		if(isBatteryShown) {
			Battery.WorldPosition = MathLib.Lerp(
				Battery.WorldPosition,
				Battery.WorldPosition = ShineBox.WorldPosition + targetDeltaPos,
				4*Game.IFps
			);
		}else{
			Battery.Scale = MathLib.Lerp(
				Battery.Scale,
				0,
				5*Game.IFps
			);
		}
	}

	private void showItem()
    {
		if(!isBatteryShown) {
			isBatteryShown = true; 
			// Рандомное направление, но нужно чтобы в одном было. Оставил как пример.
			// float randX = MathLib.RandFloat(0.5f, 1f) * (MathLib.RandFloat(1,2) > 1.5 ? 1 : -1); 
			// float randY = MathLib.RandFloat(0.5f, 1f) * (MathLib.RandFloat(1,2) > 1.5 ? 1 : -1);
			targetDeltaPos = new vec3(1f, 0f, 0f);
			Battery.Scale = 0.5f;
			Battery.WorldPosition = ShineBox.WorldPosition;
		};
    }

}