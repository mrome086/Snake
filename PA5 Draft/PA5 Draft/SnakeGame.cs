using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PA5_Draft
{
    internal delegate void EatApple();
    internal delegate void HitAnObstacle();
    internal delegate void HitItself();
    enum Direction { 
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
    class Point
    {
        internal int X { get; set; }
        internal int Y { get; set; }
        internal Point(int aX, int aY)
        {
            X = aX;
            Y = aY;
        }
    }
    class LineSeg
    {
        internal Point Start { get; set; }
        internal Point End { get; set; }
        internal LineSeg(Point p1, Point p2)
        {
            Start = new Point(p1.X, p1.Y);
            End = new Point(p2.X, p2.Y);

        }
    }
    class SnakeGame
    {
        internal const int AppleSize = 10;
        internal event EatApple EatAndGrow;
        internal event HitAnObstacle HitWallAndLose;
        internal event HitItself HitSnakeAndLose;
        private Point Start { get; }
        internal List<LineSeg> SnakeBody { get; }
        internal List<LineSeg> Obstacles { get; }
        internal List<Point> Apples { get; }
        private Size FieldSize { get; }

        private Direction CurrentDirection { get; set; }
        private void AddApple(int AppleCount) {
            var RandomGenerator = new Random();
            for (int i = 0; i < AppleCount; i++)
            {
                int X = RandomGenerator.Next(FieldSize.Width-2);
                int Y = RandomGenerator.Next(FieldSize.Height-2);
                Point P = new Point(X + 1, Y + 1);
                bool Acceptable = true;
                foreach (LineSeg Obstacle in Obstacles)
                    if (Intersect(P, Obstacle))
                        Acceptable = false;
                foreach (LineSeg Body in SnakeBody)
                    if (Intersect(P, Body))
                        Acceptable = false;
                foreach (Point Q in Apples)
                    if (Math.Abs(P.X - Q.X)<=AppleSize/2 && Math.Abs(P.Y -Q.Y)<=AppleSize/2)
                        Acceptable = false;
                if (Acceptable)
                    Apples.Add(P);
                else
                    i--;
            }
        }
        public SnakeGame(System.Drawing.Point startingPoint, int Length, int AppleCount, Size GameFieldSize)
        {
            Start = new Point(startingPoint.X, startingPoint.Y);
            FieldSize = GameFieldSize;
            SnakeBody = new List<LineSeg>();
            Point End = new Point(Start.X + Length, Start.Y);
            SnakeBody.Add(new LineSeg(Start, End));
            CurrentDirection = Direction.RIGHT;
            Obstacles = new List<LineSeg>();
            Obstacles.Add(new LineSeg(new Point(1, 1), new Point(1, GameFieldSize.Height-1)));
            Obstacles.Add(new LineSeg(new Point(1, GameFieldSize.Height-1), new Point(GameFieldSize.Width-1, GameFieldSize.Height-1)));
            Obstacles.Add(new LineSeg(new Point(GameFieldSize.Width-1, GameFieldSize.Height-1), new Point(GameFieldSize.Width-1, 1)));
            Obstacles.Add(new LineSeg(new Point(1, 1), new Point(GameFieldSize.Width - 1, 1)));
            Obstacles.Add(new LineSeg(new Point(150, 90), new Point(90, 150)));
            Obstacles.Add(new LineSeg(new Point(470, 90), new Point(530, 150)));
            Obstacles.Add(new LineSeg(new Point(90, 470), new Point(150, 530)));
            Obstacles.Add(new LineSeg(new Point(470, 530), new Point(530, 470)));
            Apples = new List<Point>();
            AddApple(AppleCount);

        }
        internal void Move(int Step, Direction NewDirection)
        {
            if (NewDirection == Direction.UP && CurrentDirection == Direction.DOWN)//illigal
                return;
            if (NewDirection == Direction.DOWN && CurrentDirection == Direction.UP)//illigal
                return;
            if (NewDirection == Direction.LEFT && CurrentDirection == Direction.RIGHT)//illigal
                return;
            if (NewDirection == Direction.RIGHT && CurrentDirection == Direction.LEFT)//illigal
                return;
            if (NewDirection != CurrentDirection)//Turn...
                SnakeBody.Add(new LineSeg(SnakeBody[SnakeBody.Count - 1].End, SnakeBody[SnakeBody.Count - 1].End));
            CurrentDirection = NewDirection;
            Move(Step);
        }
        internal void Move(int Step)
        {
            Point From = new Point(SnakeBody[SnakeBody.Count - 1].End.X, SnakeBody[SnakeBody.Count - 1].End.Y);
            switch (CurrentDirection)
            {
                case Direction.LEFT:
                    SnakeBody[SnakeBody.Count - 1].End.X -= Step;
                    break;
                case Direction.RIGHT:
                    SnakeBody[SnakeBody.Count - 1].End.X += Step;
                    break;
                case Direction.UP:
                    SnakeBody[SnakeBody.Count - 1].End.Y -= Step;
                    break;
                case Direction.DOWN:
                    SnakeBody[SnakeBody.Count - 1].End.Y += Step;
                    break;
            }
            Point To = new Point(SnakeBody[SnakeBody.Count - 1].End.X, SnakeBody[SnakeBody.Count - 1].End.Y);
            bool WillGrow = false;
            for (int x = Math.Min(From.X,To.X); x <= Math.Max(From.X,To.X); x++)
                for (int y = Math.Min(From.Y, To.Y); y <= Math.Max(From.Y, To.Y); y++)
                {
                   if(x==From.X && y==From.Y)
                        continue;
                    Point P = new Point(x, y);
                    foreach (LineSeg Obstacle in Obstacles)
                        if (Intersect(P, Obstacle))
                        {
                            SnakeBody[SnakeBody.Count - 1].End = P;
                            if (HitWallAndLose != null)
                                HitWallAndLose();
                            Console.WriteLine("Hit the obstacle, P is "+P.X+", "+P.Y+", and Obstacle is"+
                                Obstacle.Start.X+","+Obstacle.Start.Y+") and "+Obstacle.End.X+", "+Obstacle.End.Y);
                            return;
                        }
                    for (int i = 0; i < SnakeBody.Count - 1; i++)
                        if (Intersect(P, SnakeBody[i]))
                        {
                            SnakeBody[SnakeBody.Count - 1].End = P;
                            if (HitSnakeAndLose != null)
                                HitSnakeAndLose();
                            Console.WriteLine("Hit the body: P is "+P.X+","+P.Y+"body ends at" + SnakeBody[i].End.X+", "+SnakeBody[i].End.Y);
                            return;
                        }
                    int Removed = -1;
                    for (int i = 0; i < Apples.Count; i++)
                        if (Math.Abs(Apples[i].X - P.X) <= AppleSize / 2 &&
                            Math.Abs(Apples[i].Y - P.Y) <= AppleSize / 2)
                        {
                            Removed = i;
                            WillGrow = true;
                            break;
                        }
                    if (Removed >= 0)
                    {
                        Apples.RemoveAt(Removed);
                        AddApple(1);
                        if (EatAndGrow != null)
                            EatAndGrow();
                    }
                }
            if (WillGrow)
                return;
            if (SnakeBody[0].Start.X == SnakeBody[0].End.X)
            {
                if (Math.Abs(SnakeBody[0].Start.Y - SnakeBody[0].End.Y) > Step)
                    SnakeBody[0].Start.Y += (SnakeBody[0].Start.Y > SnakeBody[0].End.Y) ? (-Step) : Step;
                else
                    SnakeBody.RemoveAt(0);
            }
            else
            {
                if (Math.Abs(SnakeBody[0].Start.X - SnakeBody[0].End.X) > Step)
                    SnakeBody[0].Start.X += (SnakeBody[0].Start.X > SnakeBody[0].End.X) ? (-Step) : Step;
                else
                    SnakeBody.RemoveAt(0);
            }
        }
        private static bool Intersect(Point p, LineSeg l)
        {
            double dx = l.End.X - l.Start.X;
            double dy = l.End.Y - l.Start.Y;
            if (dx == 0)
                return p.X == l.Start.X && p.Y >= Math.Min(l.Start.Y, l.End.Y) && p.Y <= Math.Max(l.Start.Y, l.End.Y);
            if(dy == 0)
                return p.Y == l.Start.Y && p.X >= Math.Min(l.Start.X, l.End.X) && p.X <= Math.Max(l.Start.X, l.End.X);
            double slope = dy / dx;
            if (p.X < Math.Min(l.Start.X, l.End.X) || p.X > Math.Max(l.Start.X, l.End.X))
                return false;
            if (p.Y < Math.Min(l.Start.Y, l.End.Y) || p.Y > Math.Max(l.Start.Y, l.End.Y))
                return false;
            return Math.Abs(slope * (p.X - l.Start.X) - p.Y + l.Start.Y)<=Math.Abs(slope);
        }
    }
}
