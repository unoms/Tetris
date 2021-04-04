using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.BL
{
    public abstract class Shape
    {
        public int _size { get; }
        public bool[,] FigureShape;
        public List<Tuple<int, int>> Coordinates;
        public List<int> cords;
        public Shape(int size = 3)
        {
            _size = size;
        }
        public List<Tuple<int,int>> GetCoordinates()
        {
            List<Tuple<int, int>> coordinates = new List<Tuple<int, int>>();
            int coordinate = 0;
            for (int i = 0; i < FigureShape.GetLength(0); i++)
            {
                for (int j = 0; j < FigureShape.GetLength(1); j++)
                {
                    if (FigureShape[i, j])
                    {
                        coordinates.Add(new Tuple<int, int>(i, j));
                    }
                    coordinate++;
                }

            }
            return coordinates;
        }

        public List<int> GetCords()
        {
            List<int> cords = new List<int>();
            int index = 0;
            for (int i = 0; i < FigureShape.GetLength(0); i++)
            {
                for (int j = 0; j < FigureShape.GetLength(1); j++)
                {
                    if (FigureShape[i, j])
                    {
                        cords.Add(index);
                    }
                    index++;
                }
            }
            return cords;
        }

        public virtual void Rotate()
        {

            for (int y = 0; y < _size / 2; y++)
            {
                for (int x = y; x < _size - 1 - y; x++)
                {
                    bool tmp = FigureShape[y, x];
                    FigureShape[y, x] = FigureShape[_size - x - 1,  y];
                    FigureShape[_size - x - 1, y] = FigureShape[_size - y - 1, _size - x - 1];
                    FigureShape[_size - y - 1, _size - x - 1] = FigureShape[x, _size - y - 1];
                    FigureShape[x, _size - y -1] = tmp;

                }
            }

            Coordinates = GetCoordinates();
            //New
            cords = GetCords();
        }
    }

    public class Square : Shape
    {
        public Square(int size = 3) : base(size)
        {
            FigureShape = new bool[,]
            {
                {true, true, true },
                {true, true, true },
                {true, true, true }
            };

            Coordinates = GetCoordinates();
            //New
            cords = GetCords();
        }

        public override void Rotate()
        {
        }
    }

    public class Lightning1 : Shape
    {
        public Lightning1(int size = 3) : base(size)
        {
            FigureShape = new bool[,]
            {
                {true, false, false },
                {true, true, true },
                {false, false, true }
            };
            Coordinates = GetCoordinates();
            //New
            cords = GetCords();
        }
    }

    public class Lightning2 : Shape
    {
        public Lightning2(int size = 3) : base(size)
        {
            FigureShape = new bool[,]
            {
                {false, false, true },
                {true, true, true },
                {true, false, false }
            };
            Coordinates = GetCoordinates();
            //New
            cords = GetCords();
        }
    }

    public class Line : Shape
    {
        public Line(int size = 3) : base(size)
        {
            FigureShape = new bool[,]
            {
                {false, true, false },
                {false, true, false },
                {false, true, false }
            };
            Coordinates = GetCoordinates();
            //New
            cords = GetCords();
        }
    }

    public class Rectangle : Shape
    {
        public Rectangle(int size = 3) : base(size)
        {
            FigureShape = new bool[,]
            {
                {false, true, false },
                {true, true, true },
                {false, false, false }
            };
            Coordinates = GetCoordinates();
            //New
            cords = GetCords();
        }
    }
}
