using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace SnakeGame
{
    internal struct Pixel
    {
        private const char PixelChar = '█';
        public int X { get; }
        public int Y { get; }
        public ConsoleColor Color { get; }
        public Pixel(int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            Color = color;
        }  
        public void Draw()
        {
            SetCursorPosition(X, Y);
            Write(PixelChar);
        }
        public void Clear()
        {
            SetCursorPosition(X, Y);
            Write(' ');
        }
    }
}
