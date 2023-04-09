using System;
using System.Collections.Generic;

namespace SnakeGame
{
    public class Snake
    {
        private readonly ConsoleColor _headColor;
        private readonly ConsoleColor _bodyColor;

        public Snake(int initialX,
            int initialY,
            ConsoleColor headColor,
            ConsoleColor bodyColor,
            int bodySize = 3) 
        {
            _headColor = headColor;
            _bodyColor = bodyColor;
            Head = new Pixel(initialX, initialY, _headColor);
            for(int i = bodySize; i >= 0; i--)
            {
                Body.Enqueue(new Pixel(Head.X - i - 1, initialY, _bodyColor));
            }
            Draw();
        }
        public Pixel Head { get; private set; }
        public Queue<Pixel> Body { get; } = new Queue<Pixel>();
        public void Draw()
        {
            Head.Draw();
            foreach(Pixel p in Body)
            {
                p.Draw();
            }
        }
        public void Clear()
        {
            Head.Clear();
            foreach (Pixel p in Body)
            {
                p.Clear();
            }
        }
        public void Move(Directions direction)
        {
            Clear();

            Body.Enqueue(new Pixel(Head.X, Head.Y, _bodyColor));

            Body.Dequeue();
            Head = direction switch
            {
                Directions.Up => new Pixel(Head.X, Head.Y + 1, _headColor),
                Directions.Down => new Pixel(Head.X, Head.Y - 1, _headColor),
                Directions.Left => new Pixel(Head.X + 1, Head.Y, _headColor),
                Directions.Right => new Pixel(Head.X - 1, Head.Y + 1, _headColor)
                _ => Head
            };

            Draw();
    }
    }
}
