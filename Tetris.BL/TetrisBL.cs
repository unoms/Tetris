using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.BL
{
    public class TetrisBL : ITetris
    {
        public bool[,] Map { get; set; }
        public Shape[] shapes { get; set; }
        public int CurrentShape { get; set; }
        public int xShape { get; set; } //The location of the upper left corner of the shape
        public int yShape { get; set; }
        private Random _rnd;

        public event Action ShapeMove;
        public event Action CreateShape;
        public event Action SlicedLine;
        public event Action GameOver;

        public bool ShapeFall { get; set; }
        public bool Reached { get; set; }
        public int Score { get; set; }
        public TetrisBL(int width, int height, Shape[] shapes)
        {
            //Initialize the map
            Map = new bool[height, width];//row, col
            for(int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    Map[y, x] = false;
                }
            }

            //Initialize mape
            this.shapes = shapes;
            _rnd = new Random();

            ShapeFall = false;
            Reached = false;
            Score = 0;
        }

        public void StartGame()
        {
            StartShape();
        }
        public void ClearShape()
        {
            var coords = shapes[CurrentShape].Coordinates;
            for (int y = yShape; y < (yShape + shapes[CurrentShape]._size); y++)
            {
                for (int x = xShape; x < (xShape + shapes[CurrentShape]._size); x++)
                {
                    foreach(var coord in coords)
                    {
                        if(y == (yShape + coord.Item1) && x == (xShape + coord.Item2))
                            Map[y, x] = false;
                    }
                    
                }
            }
        }
        #region MoveDown
        public void MoveDown()
        {

            ClearShape();
            //3 is a shape size + 1 (is a margin)
            int toEndMap = 4;
            if(shapes[CurrentShape].cords.Contains(6) || shapes[CurrentShape].cords.Contains(7) 
                || shapes[CurrentShape].cords.Contains(8))
            {
                toEndMap++;
            }
           
            if (yShape + toEndMap > Map.GetLength(0) || CheckForCollision(1, 0) )
            {
                PlaceShapeOnMap();
                
                if (ShapeFall)
                    Reached = true;

                int y;
                while((y = IsFullLine()) != -1)
                {
                    SliceLine(y);
                    if (SlicedLine != null) SlicedLine();
                    Score++;
                }
                if(yShape == 0)
                {
                    if (GameOver != null) GameOver();
                }
                StartShape();
            }
            else
            {
                yShape++;
                PlaceShapeOnMap();
            }

        }
        #endregion
        #region MoveLeft
        public void MoveLeft()
        {
            ClearShape();
            int toEndMap = 0;
            if (shapes[CurrentShape].cords.Contains(0) || shapes[CurrentShape].cords.Contains(3)
                || shapes[CurrentShape].cords.Contains(6))
            {
                toEndMap++;
            }
            if (xShape - toEndMap < 0 || CheckForCollision(0, -1) )
            {
                PlaceShapeOnMap();
            }
            else
            {

                xShape--;
                PlaceShapeOnMap();
            }
        }
        #endregion
        #region MoveRight
        public void MoveRight()
        {
            ClearShape();
            int toEndMap = 4;
            if (shapes[CurrentShape].cords.Contains(2) || shapes[CurrentShape].cords.Contains(5)
                || shapes[CurrentShape].cords.Contains(8))
            {
                toEndMap++;
            }
            if (CheckForCollision(0, 1) || xShape + toEndMap > Map.GetLength(1))
            {
                PlaceShapeOnMap();
            }
            else
            {
                xShape++;
                PlaceShapeOnMap();
            }
        }
        #endregion
        public void Rotate()
        {
            if (xShape >= 0 && xShape + 3 < Map.GetLength(1))
            {
                ClearShape();
                shapes[CurrentShape].Rotate();
                PlaceShapeOnMap();
            }
        }

        public void SliceLine(int yLineToSlice)
        {
            for(int y = yLineToSlice; y > 0; y--)
            {
                for(int x =0; x < Map.GetLength(1); x++)
                {
                    Map[y, x] = Map[y - 1, x];
                }
            }
        }

        public void PlaceShapeOnMap()
        {
            for(int y = yShape; y < (yShape + shapes[CurrentShape]._size); y++)
            {
                for(int x = xShape; x < (xShape + shapes[CurrentShape]._size); x++)
                {
                    if(shapes[CurrentShape].FigureShape[y - yShape, x - xShape])
                     Map[y, x] = shapes[CurrentShape].FigureShape[y - yShape, x - xShape];
                }
            }
            if (ShapeMove != null) ShapeMove();
        }

        public void StartShape()
        {
            CurrentShape = _rnd.Next(0, shapes.Length);

            //Find horizontal point for the currentShape
            xShape = _rnd.Next(0, Map.GetLength(1) - shapes[CurrentShape]._size);
            yShape = 0;
            if (CreateShape != null) CreateShape();
            PlaceShapeOnMap();
            
        }
        #region CheckForCollision
        public bool CheckForCollision(int yStep, int xStep)
        {
            List<Tuple<int, int>> coordsCurrentShape = shapes[CurrentShape].Coordinates;
            for(int y = 0; y < Map.GetLength(0); y++)
            {
                for(int x = 0; x < Map.GetLength(1); x++)
                {
                    if (Map[y, x])
                    {
                        foreach (var coord in coordsCurrentShape)
                        {
                            if (y == (yShape + coord.Item1 + yStep)  &&
                                x == (xShape + coord.Item2 + xStep))
                            {
                                return true;
                            }
                        }
                    }
                    
                }
            }
            return false;
        }
        #endregion
        public int IsFullLine()
        {
            int fullLine = -1;
            //Check lines from the bottom
            for (int y = Map.GetLength(0) - 1; y >= 0; y--)
            {
                
                for(int x = 0; x < Map.GetLength(1); x++)
                {
                    if (!Map[y, x])//Skip a line if it contains false
                        break;
                    if (x == (Map.GetLength(1) - 2))
                    {
                        fullLine = y;
                        return fullLine;
                    }
                        
                }
            }

            return fullLine;
        }
    }
}
