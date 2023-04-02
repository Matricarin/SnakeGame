using static System.Console;
using SnakeGame;

#region Constants
const int MapWidth = 30;
const int MapHeight = 20;

const int ScreenWidth = MapWidth * 3;
const int ScreenHeight = MapHeight * 3;

const ConsoleColor BorderColor = ConsoleColor.Gray;
#endregion


#region WindowSettings
SetWindowSize(ScreenWidth, ScreenHeight);
SetBufferSize(ScreenWidth, ScreenHeight);
CursorVisible = false;

#endregion

#region MainLogic
DrawBorder();
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

#endregion