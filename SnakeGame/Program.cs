using static System.Console;
using System.Diagnostics;
using Figgle;
using SnakeGame;

#region Constants
const int MapWidth = 30;
const int MapHeight = 20;

const int ScreenWidth = MapWidth * 3;
const int ScreenHeight = MapHeight * 3;

const ConsoleColor BorderColor = ConsoleColor.Gray;
const ConsoleColor HeadColor = ConsoleColor.DarkGreen;
const ConsoleColor BodyColor = ConsoleColor.Green;
const ConsoleColor FoodColor = ConsoleColor.Red;

const int FrameMs = 200;
#endregion

#region WindowSettings
SetWindowSize(ScreenWidth, ScreenHeight);
SetBufferSize(ScreenWidth, ScreenHeight);
CursorVisible = false;

#endregion

#region MainLogic
while(true)
{
    StartGame();

    Thread.Sleep(1000);
    ReadKey();
}
ReadKey();
#endregion


#region ConsoleMethods
static void DrawBorder()
{
    for(int i = 0; i < MapWidth;  i++)
    {
        new Pixel(i, 0, BorderColor).Draw();
        new Pixel(i, MapHeight - 1, BorderColor).Draw();
    }

    for(int i = 0; i < MapHeight; i++)
    {
        new Pixel(0, i, BorderColor).Draw();
        new Pixel(MapWidth -1 , i, BorderColor).Draw();
    }
}

static Pixel GenFood(Snake snake)
{
    Pixel food;
    Random Rand = new Random();
    do
    {
        food = new Pixel(Rand.Next(1, MapWidth - 2), Rand.Next(1, MapHeight - 2), FoodColor);
    } while (snake.Head.X == food.X 
    && snake.Head.Y == food.Y
    || snake.Body.Any(b => b.X == food.X && b.Y == food.Y));

    return food;
}

static Directions ReadMovement(Directions currentDirection)
{
    if (!KeyAvailable)
        return currentDirection;

    ConsoleKey key = ReadKey(true).Key;

    currentDirection = key switch
    {
        ConsoleKey.UpArrow when currentDirection != Directions.Down => Directions.Up,
        ConsoleKey.DownArrow when currentDirection != Directions.Up => Directions.Down,
        ConsoleKey.RightArrow when currentDirection != Directions.Left => Directions.Right,
        ConsoleKey.LeftArrow when currentDirection != Directions.Right => Directions.Left,
        _ => currentDirection
    };

    return currentDirection;
}

static void StartGame()
{
    Clear();
    DrawBorder();
    Snake snake = new Snake(17, 10, HeadColor, BodyColor);
    Pixel food = GenFood(snake);
    food.Draw();
    int score = 0;
    Directions currentMovement = Directions.Right;
    int lagMs = 0;
    var sw = new Stopwatch();
    while (true)
    {
        sw.Restart();
        Directions oldMovement = currentMovement;
        while (sw.ElapsedMilliseconds <= FrameMs - lagMs)
        {
            if (oldMovement == currentMovement)
            {
                currentMovement = ReadMovement(currentMovement);
            }
        }
        sw.Restart();
        if(snake.Head.X == food.X && snake.Head.Y == food.Y)
        {
            snake.Move(currentMovement, true);
            food = GenFood(snake);
            food.Draw();
            score++;
            Task.Run(() => Beep(1200, 200));
        }
        else
        {
            snake.Move(currentMovement);
        }        

        if (snake.Head.X == 0
            || snake.Head.Y == 0
            || snake.Head.X == MapWidth - 1
            || snake.Head.Y == MapHeight - 1
            || snake.Body.Any(b => b.X == snake.Head.X && b.Y == snake.Head.Y))
        {
            break;
        }
        lagMs = (int)sw.ElapsedMilliseconds;
    }

    snake.Clear();
    food.Clear();
    SetCursorPosition(ScreenWidth / 3 + 10, ScreenHeight / 2);
    WriteLine("Game Over");
    Task.Run(() => Beep(200, 600));
    SetCursorPosition(ScreenWidth / 3 + 7, ScreenHeight / 2 + 5);
    Write($"Your score:{score}");
}

#endregion