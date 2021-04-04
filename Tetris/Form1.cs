using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tetris.BL;

namespace Tetris
{
    public partial class Form1 : Form
    {
        //Size
        private int widthOfForm = 400;
        private int heightthOfForm = 700;
        private int square = 20;
        private ITetris tetrisBL;
        private Timer timer;
        private List<PictureBox> shapeCurrent;
        public Form1(ITetris tetris)
        {
            InitializeComponent();
            tetrisBL = tetris;
           
            this.Paint += Form1_Paint;
            this.KeyDown += Form1_KeyDown;
            tetrisBL.ShapeMove += TetrisBL_ShapeMove;
            tetrisBL.CreateShape += TetrisBL_CreateShape;
            tetrisBL.SlicedLine += TetrisBL_SlicedLine;
            tetrisBL.GameOver += TetrisBL_GameOver;
            this.DoubleBuffered = true;
            this.Width = widthOfForm;
            this.Height = heightthOfForm;
            this.lbl_show_score.Text = "0";
            Init();
        }

        private void TetrisBL_GameOver()
        {
            timer.Stop();
            MessageBox.Show("Game over.\nYou score: " + tetrisBL.Score);
            this.Close();
        }

        private void TetrisBL_SlicedLine()
        {
            PrintMap();
        }

        private void TetrisBL_CreateShape()
        {
            List<Tuple<int,int>> cords = tetrisBL.shapes[tetrisBL.CurrentShape].GetCoordinates();
            shapeCurrent = new List<PictureBox>();
            for(int i = 0; i < cords.Count; i++)
            {
                PictureBox pictureBox = new PictureBox
                {
                    //Additional square is a margin. Check the grid
                    Location = new Point(
                        (tetrisBL.xShape + cords[i].Item2) * square + square, 
                        (tetrisBL.yShape + cords[i].Item1) * square + square),
                    Size = new Size(square, square),
                    BackColor = Color.Red
                };
                Controls.Add(pictureBox);
                shapeCurrent.Add(pictureBox);
            }

            
        }

        private void TetrisBL_ShapeMove()
        {
            if(shapeCurrent != null)
            {
                List<Tuple<int, int>> cords = tetrisBL.shapes[tetrisBL.CurrentShape].GetCoordinates();
                for (int i = 0; i < shapeCurrent.Count; i++)
                {
                    shapeCurrent[i].Location = new Point(
                            (tetrisBL.xShape + cords[i].Item2) * square + square,
                            (tetrisBL.yShape + cords[i].Item1) * square + square);

                }
            }
          
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Up":
                    tetrisBL.Rotate();
                    break;
                case "Down":
                    if (!tetrisBL.ShapeFall)
                    {
                        timer.Stop();
                        timer.Interval = 1;
                        timer.Start();
                        tetrisBL.ShapeFall = true;
                        tetrisBL.Reached = false;
                    }
                    break;
                case "Left":
                    tetrisBL.MoveLeft();
                    break;
                case "Right":
                    tetrisBL.MoveRight();
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            CreateGrid(e);
        }

        private void Init()
        {
            Invalidate();
            tetrisBL.StartGame();
            timer = new Timer();
            timer.Interval = 200;
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tetrisBL.MoveDown();
            lbl_show_score.Text = tetrisBL.Score.ToString();

            //Logic for speedy falling
            if (tetrisBL.ShapeFall && tetrisBL.Reached)
            {
                tetrisBL.ShapeFall = false;
                tetrisBL.Reached = false;
                timer.Stop();
                timer.Interval = 200;
                timer.Start();
            }
        }

        private void PrintMap()
        {
            //Clear all controls
            foreach (Control pb in this.Controls.OfType<PictureBox>().ToList())
            {
                Controls.Remove(pb);
                pb.Dispose();
            }

            for (int y = 0; y < tetrisBL.Map.GetLength(0); y++)
            {
                for (int x = 0; x < tetrisBL.Map.GetLength(1); x++)
                {

                    if (tetrisBL.Map[y, x])
                    {
                        PictureBox pictureBox = new PictureBox
                        {
                            //Additional square is a margin. Check the grid
                            Location = new Point(x * square +square, y * square + square),
                            Size = new Size(square, square),
                            BackColor = Color.Red
                        };
                        Controls.Add(pictureBox);
                    }
                }
            }
        }

        private void CreateGrid(PaintEventArgs e)
        {
            //Vertical lines
            for(int x = 1; x < 15; x++)
            {
                e.Graphics.DrawLine(new Pen(Color.Black, 2),
                    square * x , square, square * x, tetrisBL.Map.GetLength(0) * square);
            }

            //Horizontal lines
            for (int y = 1; y < 34; y++)
                e.Graphics.DrawLine(new Pen(Color.Black, 2),
                    square, square * y, tetrisBL.Map.GetLength(1) * square, square * y);
        }
    }
}
