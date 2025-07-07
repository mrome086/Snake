using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PA5_Draft
{
    public partial class MainForm : Form
    {

        public static MainForm instance;
        Apples apples = new Apples();

        SoundPlayer player = new SoundPlayer("Eat.wav");
        SoundPlayer player1 = new SoundPlayer("Bonk.wav");
        SoundPlayer player2 = new SoundPlayer("Crash.wav");
        Label lab1;

        private int Step = 1;
        private readonly SnakeGame Game;
        public int NumberOfApples = 1;
        private int appleEaten;
        
        private bool pause = true;
        private bool unpause = false;
        public MainForm()
        {
            instance = this;
            Application.Run(new Apples());
            InitializeComponent();
            Game = new SnakeGame(new System.Drawing.Point((Field.Width - 20) / 2, Field.Height / 2), 40, NumberOfApples, Field.Size);
            Field.Image = new Bitmap(Field.Width, Field.Height);
            Game.EatAndGrow += Game_EatAndGrow;
            Game.HitWallAndLose += Game_HitWallAndLose;
            Game.HitSnakeAndLose += Game_HitSnakeAndLose;
            lab1 = label3;

        }





        private void Game_HitWallAndLose()
        {
            mainTimer.Stop();
            Field.Refresh();

            lab1.Text = Convert.ToString(appleEaten);
            label2.Text = "Number of Apples eaten";
            player1.Play();
        }
        private void Game_HitSnakeAndLose()
        {
            mainTimer.Stop();
            Field.Refresh();

            lab1.Text = Convert.ToString(appleEaten);
            label2.Text = "Number of Apples eaten";
            player2.Play();
        }

        private void Game_EatAndGrow()
        {

            appleEaten++;
            player.Play();

            if (appleEaten % 10 == 0) { Step++; }
            if (Step > 10) { Step = 10; }
            progressBar1.Maximum = 10;
            progressBar1.Minimum = 0;
            if (appleEaten % 10 == 0) { progressBar1.Value++; }



        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            Game.Move(Step);
            Field.Invalidate();

        }






        private void Field_Paint(object sender, PaintEventArgs e)
        {

            Random rand = new Random();
            int one = rand.Next(0, 255);
            int two = rand.Next(0, 255);


            Pen PenForObstacles = new Pen(Color.FromArgb(40, 40, 40), 2);
            Pen PenForSnake = new Pen(Color.FromArgb(100, 100, 100), 2);
            Brush BackGroundBrush = new SolidBrush(Color.FromArgb(150, 250, 150));
            Brush AppleBrush = new SolidBrush(Color.FromArgb(255, one, two));




            using (Graphics g = Graphics.FromImage(Field.Image))
            {
                g.FillRectangle(BackGroundBrush, new Rectangle(0, 0, Field.Width, Field.Height));
                foreach (Point Apple in Game.Apples)
                    g.FillEllipse(AppleBrush, new Rectangle(Apple.X - SnakeGame.AppleSize / 2, Apple.Y - SnakeGame.AppleSize / 2,
                                                                 SnakeGame.AppleSize, SnakeGame.AppleSize));
                foreach (LineSeg Obstacle in Game.Obstacles)
                    g.DrawLine(PenForObstacles, new System.Drawing.Point(Obstacle.Start.X, Obstacle.Start.Y)
                        , new System.Drawing.Point(Obstacle.End.X, Obstacle.End.Y));
                foreach (LineSeg Body in Game.SnakeBody)
                    g.DrawLine(PenForSnake, new System.Drawing.Point((int)Body.Start.X, (int)Body.Start.Y)
                        , new System.Drawing.Point((int)Body.End.X, (int)Body.End.Y));
            }
        }



        private void Snakes_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    Game.Move(Step, Direction.UP);
                    break;
                case Keys.Down:
                    Game.Move(Step, Direction.DOWN);
                    break;
                case Keys.Left:
                    Game.Move(Step, Direction.LEFT);
                    break;
                case Keys.Right:
                    Game.Move(Step, Direction.RIGHT);
                    break;
            }
        }

        private void Field_Click(object sender, EventArgs e)
        {
           
            if (pause == true)
            {
                Step = 0;
                pause = false;
            }
            else
            {
                
                pause = true;
              
            }
           
            
            if (unpause == true)
            {
                Step = 1;
                unpause = false;

            }
            else
            {
                unpause = true;
            
            }

            
        }
    }
}
