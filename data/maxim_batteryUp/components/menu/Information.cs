using System.Collections;
using System.Collections.Generic;
using Unigine;

[Component(PropertyGuid = "5456cc7115d472b899297af3d1d25a9ed94d7c7d")]
public class Information : Component
{
    // Путь к JSON-файлу с информацией, задается в редакторе
    [ShowInEditor]
    [ParameterFile(Filter = ".json")]
    private string jsonFile = "null";

    // Ссылка на компонент главного меню, задается в редакторе
    [ShowInEditor]
    private UIMainMenu mainMenu = null;

    // Основной контейнер для интерфейса
    private WidgetVBox VBox;

    // Фоновое изображение
    private WidgetSprite background;

    // Метки для отображения заголовка, контента и номера блока
    private WidgetLabel titleLabel, contentLabel, numberLabel;

    // Изображение для текущего блока
    private WidgetSprite blockImage;

    // Кнопки для перехода к следующему блоку и выхода
    private WidgetButton nextButton, exitButton;

    // Индекс текущего блока
    private int currentBlockIndex;

    // Объекты для работы с JSON
    private Json json, jsonBlocks;

    // Структура для хранения данных блока
    struct Block
    {
        public string title; // Заголовок блока
        public string content; // Контент блока (строка)
        public string image; // Путь к изображению (или null)
    }

    // Список блоков
    private List<Block> blocks = new List<Block>();

    // Инициализация компонента
    public void Init()
    {
        // Проверяем, что WindowManager.MainWindow доступен
        if (WindowManager.MainWindow == null)
        {
            Log.Error("MainWindow is not initialized.\n");
            return;
        }

        json = new Json(); // Создаем объект для работы с JSON
        CreateUI(); // Создаем основной интерфейс
        LoadBlocks(); // Загружаем блоки из JSON
    }

    // Обновление каждый кадр
    public void Update()
    {
        // Проверяем нажатие пробела для перелистывания
        if (Input.IsKeyDown(Input.KEY.SPACE) && VBox != null && !VBox.Hidden)
        {
            NextBlock(); // Вызываем логику перелистывания
        }
    }

    // Проверка, активен ли интерфейс (true, если скрыт)
    public bool GetActiveInfo()
    {
        return VBox.Hidden;
    }

    // Запуск отображения информации
    public void StartInformation()
    {
        if (blocks.Count == 0)
        {
            Log.Error("No blocks loaded. Check JSON file.\n");
            return;
        }

        if (VBox == null)
        {
            Log.Error("VBox is not initialized.\n");
            return;
        }

        VBox.Hidden = false; // Показываем интерфейс
        currentBlockIndex = 0; // Начинаем с первого блока
        if (nextButton != null)
        {
            nextButton.Enabled = true; // Включаем кнопку "Далее"
            nextButton.Hidden = false; // Делаем кнопку "Далее" видимой
            nextButton.Text = "Далее"; // Сбрасываем текст кнопки
        }
        ShowBlock(); // Показываем первый блок
    }

    // Отображение текущего блока
    private void ShowBlock()
    {
        if (currentBlockIndex < 0 || currentBlockIndex >= blocks.Count)
        {
            Log.Error("Invalid block index: {0}\n", currentBlockIndex);
            return;
        }

        if (titleLabel == null || contentLabel == null || numberLabel == null)
        {
            Log.Error("UI labels are not initialized.\n");
            return;
        }

        Block block = blocks[currentBlockIndex];
        titleLabel.Text = block.title ?? "No Title"; // Устанавливаем заголовок
        contentLabel.Text = block.content ?? "No Content"; // Устанавливаем контент
        numberLabel.Text = (currentBlockIndex + 1) + "/" + blocks.Count; // Номер блока

        // Отображаем изображение, если оно указано
        if (blockImage != null)
        {
            if (!string.IsNullOrEmpty(block.image) && block.image != "null")
            {
                blockImage.Texture = block.image;
                blockImage.Hidden = false;
            }
            else
            {
                blockImage.Hidden = true; // Скрываем изображение, если оно не задано
            }
        }
    }

    // Переход к следующему блоку
    private void NextBlock()
    {
        if (currentBlockIndex == blocks.Count - 2)
        {
            if (nextButton != null)
                nextButton.Text = "Завершить"; // Меняем текст перед последним блоком
        }

        if (currentBlockIndex == blocks.Count - 1)
        {
            // Закрываем интерфейс и показываем главное меню
            if (mainMenu != null)
            {
                //mainMenu.ShowMenu(); // Показываем главное меню
            }
            else
            {
                Log.Error("UIMainMenu is not assigned.\n");
            }
            if (VBox != null)
                VBox.Hidden = true; // Скрываем интерфейс
        }
        else
        {
            currentBlockIndex++; // Переходим к следующему блоку
            ShowBlock(); // Показываем новый блок
            if (nextButton != null)
                nextButton.Enabled = true; // Включаем кнопку после отображения
        }
    }

    // Загрузка блоков из JSON-файла
    private void LoadBlocks()
    {
        // Проверяем, задан ли путь к JSON-файлу
        if (string.IsNullOrEmpty(jsonFile) || jsonFile == "null")
        {
            Log.Error("JSON file path is not set or invalid: {0}\n", jsonFile);
            return;
        }

        // Проверяем, успешно ли загружен JSON
        if (json.Load(jsonFile) == 0)
        {
            Log.Error("Failed to load JSON file: {0}\n", jsonFile);
            return;
        }

        jsonBlocks = json.GetChild("blocks"); // Получаем узел "blocks"
        if (jsonBlocks == null)
        {
            Log.Error("No，皆 'blocks' node found in JSON file: {0}\n", jsonFile);
            return;
        }

        // Проходим по всем блокам в JSON
        for (int i = 0; i < jsonBlocks.GetNumChildren(); i++)
        {
            Json jsonBlock = jsonBlocks.GetChild(i);
            if (jsonBlock == null)
            {
                Log.Error("Invalid JSON block at index {0}\n", i);
                continue;
            }

            string title = jsonBlock.Read("title") ?? "No Title"; // Читаем заголовок
            string content = jsonBlock.Read("content") ?? "No Content"; // Читаем контент
            string image = jsonBlock.Read("image") ?? null; // Читаем путь к изображению

            // Добавляем блок в список
            blocks.Add(new Block
            {
                title = title,
                content = content,
                image = image
            });
        }
    }

    // Создание основного интерфейса
    private void CreateUI()
    {
        VBox = new WidgetVBox(); // Создаем вертикальный контейнер
        if (VBox == null)
        {
            Log.Error("Failed to create VBox.\n");
            return;
        }
        VBox.Width = 900;
        VBox.Height = 600;

        background = new WidgetSprite(); // Фон
        if (background == null)
        {
            Log.Error("Failed to create background sprite.\n");
            return;
        }
        background.Width = 900;
        background.Height = 600;
        background.Texture = "data/maxim_batteryUp/ui/infoBackground.png";
        background.SetPosition(0, 0);

        titleLabel = new WidgetLabel(); // Метка для заголовка
        if (titleLabel == null)
        {
            Log.Error("Failed to create titleLabel.\n");
            return;
        }
        titleLabel.Height = 80;
        titleLabel.Width = 800;
        titleLabel.Text = "Заголовок";
        titleLabel.FontSize = 30;
        titleLabel.SetPosition(50, 50);
        titleLabel.FontColor = new vec4(1.0f, 1.0f, 1.0f, 1.0f);
        titleLabel.FontWrap = 1; // Перенос текста

        contentLabel = new WidgetLabel(); // Метка для контента
        if (contentLabel == null)
        {
            Log.Error("Failed to create contentLabel.\n");
            return;
        }
        contentLabel.Height = 200; // Уменьшаем высоту, чтобы освободить место для изображения
        contentLabel.Width = 800;
        contentLabel.Text = "Контент";
        contentLabel.FontSize = 20;
        contentLabel.SetPosition(50, 150);
        contentLabel.FontColor = new vec4(1.0f, 1.0f, 1.0f, 1.0f);
        contentLabel.FontWrap = 1; // Перенос текста

        blockImage = new WidgetSprite(); // Изображение для блока
        if (blockImage == null)
        {
            Log.Error("Failed to create blockImage sprite.\n");
            return;
        }
        blockImage.Width = 300;
        blockImage.Height = 150;
        blockImage.SetPosition(300, 360); // Размещаем под текстом
        blockImage.Hidden = true; // Изначально скрыто

        numberLabel = new WidgetLabel(); // Метка для номера блока
        if (numberLabel == null)
        {
            Log.Error("Failed to create numberLabel.\n");
            return;
        }
        numberLabel.Width = 70;
        numberLabel.Height = 40;
        numberLabel.Text = "1/4";
        numberLabel.FontSize = 30;
        numberLabel.SetPosition(415, 527);
        numberLabel.FontColor = new vec4(1.0f, 1.0f, 1.0f, 1.0f);

        // Кнопка "Далее" для перехода к следующему блоку
        nextButton = new WidgetButton();
        if (nextButton == null)
        {
            Log.Error("Failed to create nextButton.\n");
            return;
        }
        nextButton.Width = 220;
        nextButton.Height = 70;
        nextButton.Text = "Далее";
        nextButton.FontSize = 25;
        nextButton.FontColor = new vec4(1.0f, 1.0f, 1.0f, 1.0f);
        nextButton.SetPosition(630, 511);
        nextButton.ButtonColor = new vec4(0.39f, 0.39f, 0.50f, 1.0f);
        nextButton.EventClicked.Connect(NextBlock);

        // Кнопка "Закрыть" для выхода
        exitButton = new WidgetButton();
        if (exitButton == null)
        {
            Log.Error("Failed to create exitButton.\n");
            return;
        }
        exitButton.Width = 220;
        exitButton.Height = 70;
        exitButton.Text = "Закрыть";
        exitButton.FontSize = 25;
        exitButton.FontColor = new vec4(1.0f, 1.0f, 1.0f, 1.0f);
        exitButton.SetPosition(50, 511);
        exitButton.ButtonColor = new vec4(0.39f, 0.39f, 0.50f, 1.0f);
        exitButton.EventClicked.Connect(() =>
        {
            if (mainMenu != null)
            {
                mainMenu.ShowMenu(); // Показываем главное меню
            }
            else
            {
                Log.Error("UIMainMenu is not assigned.\n");
            }
            if (VBox != null)
                VBox.Hidden = true; // Скрываем интерфейс
        });

        // Добавляем элементы в контейнер VBox
        VBox.AddChild(background, Gui.ALIGN_OVERLAP);
        VBox.AddChild(titleLabel, Gui.ALIGN_OVERLAP);
        VBox.AddChild(contentLabel, Gui.ALIGN_OVERLAP);
        VBox.AddChild(blockImage, Gui.ALIGN_OVERLAP);
        VBox.AddChild(numberLabel, Gui.ALIGN_OVERLAP);
        VBox.AddChild(nextButton, Gui.ALIGN_OVERLAP);
        VBox.AddChild(exitButton, Gui.ALIGN_OVERLAP);

        // Добавляем контейнер в главное окно, центрируем
        WindowManager.MainWindow.AddChild(VBox, Gui.ALIGN_OVERLAP | Gui.ALIGN_CENTER);

        if (nextButton != null)
            nextButton.Enabled = true; // Кнопка "Далее" изначально включена
        if (VBox != null)
            VBox.Hidden = true; // Изначально интерфейс скрыт
    }
}