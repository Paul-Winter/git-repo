﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Snake: Figure
    {
        Direction direction;
        public Point head, tail;

        public Snake(Point tail, int length, Direction direction)
        {
            this.direction = direction;
            pList = new List<Point>();
            for (int i = 0; i < length; i++)
            {
                Point p = new Point(tail);
                p.Move(i, direction);
                pList.Add(p);
            }
        }

        internal void Move()
        {
            tail = pList.First();
            pList.Remove(tail);
            head = GetNextPoint();
            pList.Add(head);
            tail.Clear();
            head.Draw();
        }

        public Point GetNextPoint()
        {           
            head = pList.Last();
            head.sym = '%';
            Console.ForegroundColor = ConsoleColor.Green;
            head.Draw();
            Point nextPoint = new Point(head);
            nextPoint.Move(1, direction);
            Console.ForegroundColor = ConsoleColor.Magenta;
            nextPoint.sym = '&';
            return nextPoint;
        }

        internal bool IsHitTail()
        {
            head = pList.Last();
            for (int i = 0; i < pList.Count - 3; i++)
            {
                if (head.IsHit(pList[i]))
                    return true;
            }
            return false;
        }

        public void Handler(ConsoleKey key)
        {
            if (key == ConsoleKey.LeftArrow)
            {
                if (direction == Direction.RIGHT)
                    direction = Direction.RIGHT;
                else
                    direction = Direction.LEFT;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                if (direction == Direction.LEFT)
                    direction = Direction.LEFT;
                else
                    direction = Direction.RIGHT;
            }
            else if (key == ConsoleKey.UpArrow)
            {
                if (direction == Direction.DOWN)
                    direction = Direction.DOWN; 
                else
                    direction = Direction.UP;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                if (direction == Direction.UP)
                    direction = Direction.UP;
                else
                    direction = Direction.DOWN;
            }
        }

        internal bool Eat(Point food)
        {
            head = GetNextPoint();
            if (head.IsHit(food))
            {
                Console.WriteLine("\a");
                food.sym = head.sym;
                pList.Add(food);
                return true;
            }
            else
                return false;
        }
    }
}
