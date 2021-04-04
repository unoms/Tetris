using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.BL;

namespace Tetris
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Shape[] shapes = new Shape[] { new Square(), new Lightning1(),
               new Lightning2(), 
                new Line()
                ,new Rectangle()
            };
            ITetris tetris = new TetrisBL(14, 33, shapes);//14 33
            Application.Run(new Form1(tetris));
        }
    }
}
