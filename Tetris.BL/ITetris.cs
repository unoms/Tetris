using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.BL
{
    public interface ITetris
    {
        bool[,] Map { get; set; }
        void PlaceShapeOnMap();
        void MoveDown();
        void MoveRight();
        void MoveLeft();
        void Rotate();
        void SliceLine(int y);
        void ClearShape();
        void StartGame();
        bool ShapeFall { get; set; }
        bool Reached { get; set; }
        int Score { get; set; }
        int CurrentShape { get; set; }
        int xShape { get; set; }
        int yShape { get; set; }
        Shape[] shapes { get; set; }
        int IsFullLine();
        //Events
        event Action ShapeMove; 
        event Action CreateShape;
        event Action SlicedLine;
        event Action GameOver;
    }
}
