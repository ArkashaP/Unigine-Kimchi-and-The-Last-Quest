using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "ffb668a0dbafe7edefd34b3499d2692c78974c2c")]
public class BatteriesCounter : Component
{
    private Gui gui;
    private WidgetLabel label;
    private Image batteryImage, starImage;
    [ShowInEditor][ParameterFile] private string texturePath;
    [ShowInEditor][ParameterFile] private string texturePath2;
    private WidgetSprite batterySprite;
    private WidgetSprite[] starSprites; // Массив для 10 звездочек
    public int battery1_count = 0;
    public int battery2_count = 0;
    private int currentStarCount = 0; // Текущее количество отображаемых звезд

    void Init()
    {
        gui = Gui.GetCurrent();
        
        // Настройка метки
        label = new WidgetLabel();
        label.Text = "";
        label.FontSize = 20;
        label.PositionX = 100;
        label.PositionY = 25;
        gui.AddChild(label, Gui.ALIGN_OVERLAP);
        
        // Настройка изображения батареи
        batteryImage = new Image(texturePath);
        if (batteryImage != null)
        {
            batterySprite = new WidgetSprite();
            batterySprite.SetImage(batteryImage);
            batterySprite.Width = 52;
            batterySprite.Height = 32;
            batterySprite.PositionX = 20;
            batterySprite.PositionY = 20;
            gui.AddChild(batterySprite, Gui.ALIGN_OVERLAP);
        }

        // Инициализация звездочек
        starImage = new Image(texturePath2);
        starSprites = new WidgetSprite[10];
        for (int i = 0; i < 10; i++)
        {
            if (starImage != null)
            {
                starSprites[i] = new WidgetSprite();
                starSprites[i].SetImage(starImage);
                starSprites[i].Width = 32; // Размер звездочки
                starSprites[i].Height = 32;
                starSprites[i].PositionX = 20 + i * 40; // Горизонтальное расположение с шагом 40
                starSprites[i].PositionY = gui.Height - 50; // Внизу экрана
                // Изначально не добавляем в GUI, добавим в Update при необходимости
            }
        }
    }

    void Update()
    {
        // Обновление текста
        label.Text = battery2_count.ToString() + " / 5";

        // Вычисляем, сколько звездочек должно быть видно (1 звездочка за каждые 2 батарейки)
        int targetStarCount = MathLib.Clamp(battery1_count / 2, 0, 10);

        // Если нужно показать больше звездочек
        while (currentStarCount < targetStarCount)
        {
            if (starSprites[currentStarCount] != null)
            {
                gui.AddChild(starSprites[currentStarCount], Gui.ALIGN_OVERLAP);
            }
            currentStarCount++;
        }

        // Если нужно убрать звездочки
        while (currentStarCount > targetStarCount)
        {
            currentStarCount--;
            if (starSprites[currentStarCount] != null)
            {
                gui.RemoveChild(starSprites[currentStarCount]);
            }
        }
    }

    void Shutdown()
    {
        // Очистка ресурсов
        if (label != null) label.DeleteLater();
        if (batterySprite != null) batterySprite.DeleteLater();
        for (int i = 0; i < 10; i++)
        {
            if (starSprites[i] != null) starSprites[i].DeleteLater();
        }
        if (batteryImage != null) batteryImage.DeleteLater();
        if (starImage != null) starImage.DeleteLater();
    }
}