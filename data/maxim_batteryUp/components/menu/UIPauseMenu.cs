using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "fa83f41ab081174b4ce7e3835641262b1a011fa9")]
public class UIPauseMenu : Component
{
    // Путь к файлу интерфейса .ui, отображается в редакторе
    [ShowInEditor]
    [ParameterFile(Filter = ".ui")]
    private string file = "null";

    // Ссылка на компонент информации (не используется в коде, возможно, задел на будущее)
    [ShowInEditor]
    private Information info;

    [ShowInEditor]
    private UIMainMenu uimain;

    // Флаг, указывающий, работает ли мир (задается в редакторе)
    [ShowInEditor]
    private bool worldWork;

    // Объект интерфейса пользователя
    private UserInterface ui;

    // Главное меню (виджет), публичное для доступа из других скриптов
    public Widget pMainMenu;

    // Флаг видимости меню
    public bool menuBool;

    // Ссылки на кнопки в виде спрайтов
    private WidgetSprite pWButtonExit, pWButtonInformation;

    // Скрывает меню, устанавливая флаг и скрывая виджет
    private void HideMenu()
    {
        menuBool = false;
        pMainMenu.Hidden = true; // Скрываем главное меню
    }

    // Показывает меню, устанавливая флаг и делая виджет видимым
    public void ShowMenu()
    {
        menuBool = true;
        pMainMenu.Hidden = false; // Делаем меню видимым
    }

    // Выход из приложения
    private void Exit()
    {
        Engine.Quit(); // Завершает работу движка Unigine
    }

    // Инициализация компонента
    private void Init()
    {
        menuBool = false; // Меню изначально видимо
        ui = new UserInterface(Gui.GetCurrent(), file); // Загружаем интерфейс из .ui файла
        System.Console.WriteLine(ui.FindWidget("mainMenu")); // Выводим ID виджета в консоль для отладки
        pMainMenu = ui.GetWidget(ui.FindWidget("mainMenu")); // Получаем виджет главного меню

        var mainWindowSize = WindowManager.MainWindow.Size; // Размер главного окна
        var px = pMainMenu.Width; // Ширина меню
        var py = pMainMenu.Height; // Высота меню

        // Настройка кнопки "Выход"
        pWButtonExit = (WidgetSprite)ui.GetWidget(ui.FindWidget("exit"));
        pWButtonExit.EventEnter.Connect(ChangeCoverButtonEnter, pWButtonExit); // При наведении мыши
        pWButtonExit.EventLeave.Connect(ChangeCoverButtonLeave, pWButtonExit); // При уходе мыши
        pWButtonExit.EventClicked.Connect(Exit); // При клике вызывает выход

        // Настройка кнопки "Информация"
        pWButtonInformation = (WidgetSprite)ui.GetWidget(ui.FindWidget("information"));
        pWButtonInformation.EventEnter.Connect(ChangeCoverButtonEnter, pWButtonInformation);
        pWButtonInformation.EventLeave.Connect(ChangeCoverButtonLeave, pWButtonInformation);
        pWButtonInformation.EventClicked.Connect(StartInformation); // Запуск тестирования

        // Добавляем главное меню в интерфейс, центрируем его
        Gui.GetCurrent().AddChild(pMainMenu, Gui.ALIGN_OVERLAP | Gui.ALIGN_CENTER);

        // Если мир уже загружен, убираем первый дочерний элемент (возможно, старое меню)
        if (worldWork)
        {
            Gui.GetCurrent().RemoveChild(Gui.GetCurrent().GetChild(0));
        }

        // Обработка изменения размера окна
        EngineWindowViewport ewv = WindowManager.MainWindow;
        ewv.EventResized.Connect(() =>
        {
            var newMainWindowSize = WindowManager.MainWindow.Size;
            var newPx = pMainMenu.Width;
            var newPy = pMainMenu.Height;
            // Центрируем меню при изменении размера окна
            pMainMenu.SetPosition(newMainWindowSize.x / 2 - newPx / 2, newMainWindowSize.y / 2 - newPy / 2);
        });
    }

    private void StartInformation()
    {
        menuBool = false;
    }

    // Изменение текстуры кнопки при наведении мыши (анимация hover)
    private void ChangeCoverButtonEnter(WidgetSprite widget)
    {
        widget.SetLayerTexCoord(0, new vec4(0.0f, 0.5f, 1.0f, 1f)); // Смещаем текстуру для эффекта
    }

    // Возврат текстуры кнопки в исходное состояние при уходе мыши
    private void ChangeCoverButtonLeave(WidgetSprite widget)
    {
        widget.SetLayerTexCoord(0, new vec4(0.0f, 0f, 1.0f, 0.5f)); // Возвращаем исходную текстуру
    }

    // Обновление каждый кадр
    private void Update()
    {
        // Управление видимостью меню через флаг menuBool
        if (menuBool)
        {
            pMainMenu.Hidden = false; // Показываем меню
        }
        else
        {
            pMainMenu.Hidden = true; // Скрываем меню
        }

        // Переключение видимости меню по клавише ESC, если активно тестирование или мир
        if (Input.IsKeyDown(Input.KEY.ESC) && (info.GetActiveInfo() || worldWork)&& uimain.menuBool == false)
        {
            menuBool = !menuBool; // Инвертируем состояние меню
        }
    }
}