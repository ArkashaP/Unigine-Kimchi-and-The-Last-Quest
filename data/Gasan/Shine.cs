using System.Collections;
using System.Collections.Generic;
using Unigine;


[Component(PropertyGuid = "42f51590e8ff3483dc12647fe6630cfba2fcbdcd")]
public class Shine : Component
{
	Node ShineBox;
	public Node Cat; // Добавил модификатор "public" для этой переменной, чтобы можно выбрать в ручную - кто игрок (в Unigine Editor)

	public float randomTimeMax = 2.0f; // Время таймера Max
	public float randomTimeMin = 0.5f; // Время таймера Min
	public Node Battery;	
	public ObjectMeshStatic boxMesh, batteryMesh;
	// public float ifps = Game.IFps; // Убрал эту строку, тк IFps это динамическое значение, и оно меняется в зависимости от кол-ва кадров. 
	public float emissionTimer;
	public float progress, targetEmission;
	
	// Добавлено
	private float randomTime; // Время для работы таймера
	private float timerCounter = 0f; // Время для работы таймера
	private bool timerActivated = false; // Активен ли таймер

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
				enableTimer(); // Если мы нажали Е, то включаем таймер отчета времени
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

		// Проверяем если дошли до нужного времени
		if(timerActivated && timerCounter >= randomTime){ // Если таймер активен и время достигло времени randomTime, то...
			timerActivated = false; // Выключаем таймер 
			Battery.WorldPosition = ShineBox.WorldPosition + new dvec3(1f, 1f, 0f); // Перемещаем обьект 
		}

		// Считаем время если включен таймер
		if(timerActivated) timerCounter += Game.IFps; // При активации таймера, timerCounter начнет считать время
		
	}

	private void enableTimer()
    {
		if(!timerActivated) {
			timerCounter = 0f; // Сбрасываем счетчик времени (текущее значение таймера)
			randomTime = MathLib.RandFloat(randomTimeMin, randomTimeMax); // Выбираем рандомное время чтобы потом в это время произошла остановка
			timerActivated = true; // Включаем таймер
		};
    }

}