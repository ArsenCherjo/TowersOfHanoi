﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowersOfHanoi
{
    public partial class TowerOfHanoi : Form
    {
        private List<string> moves = new List<string>();
        private List<Disks> TowerDisks = new List<Disks>();
        AnimateView animate = new AnimateView();
        int _DiskCount = 3;
        int diskHeight = 30;//Диск 30 в высоту
        int baseHeight = 20;//База 20 в высоту
        public TowerOfHanoi()
        {
            InitializeComponent();
            AnimateView.view = Panel;
            resetPanel();
        }
        /// <summary>
        /// Create disks on Panel
        /// </summary>
        private void populateDisks()
        {
            int i = 1;
            foreach (Disks disk in TowerDisks)
            {
                PictureBox panelBox = disk.box;
                panelBox.BackColor = colorSelector(disk);
                disk.width = 200 - (20 * i);
                panelBox.Width = disk.width;
                panelBox.Height = diskHeight;
                panelBox.Tag = disk.DiskNubmer;
                panelBox.BorderStyle = BorderStyle.FixedSingle;
                Point boxLocation = new Point(getDiskX(disk), (Panel.Height - baseHeight) - (diskHeight * i++));
                panelBox.Location = boxLocation;
                disk.box = panelBox;
                Panel.Controls.Add(disk.box);
            }
        }

        private int getDiskX(Disks disk)
        {
            int X = 0;
            int Peg = 0;
            switch (disk.peg)
            {
                case 'A': Peg = 1; break;
                case 'B': Peg = 2; break;
                case 'C': Peg = 3; break;
            }
            X = ((Panel.Width / 4) * Peg) - (int)(disk.width / 2);

            return X; 
        }

        private void resetPanel()
        {
            //Создать стержни для башни
            setupTower();
            Panel.Controls.Clear();
            TowerDisks = Enumerable.Range(1, _DiskCount).Select(i => new Disks() { DiskNubmer = i, peg = 'A', box = new PictureBox() }).OrderByDescending(i => i.DiskNubmer).ToList();
            //Поместить диски на панель
            populateDisks();
            //Установить начальное текстовое значение для наименьших возможных ходов
            lblCounter.Text = string.Format("Наименьшее количество ходов {0}", GetMoveCount(_DiskCount));
        }

        private int getDiskY(Disks disk)
        {
            int Y = 0;
            int stackNo = TowerDisks.Count(x => x.peg == disk.peg);
            Y = Panel.Height - baseHeight - (diskHeight * stackNo);
            return Y;
        }
        private Color colorSelector(Disks disk)
        {
            switch (disk.DiskNubmer)
            {
                case 1: return Color.Red;
                case 2: return Color.OrangeRed;
                case 3: return Color.Yellow;
                case 4: return Color.Green;
                case 5: return Color.Blue;
                case 6: return Color.Purple;
                case 7: return Color.LightBlue;
                default: return Color.Black;
            }
        }
        /// <summary>
        /// Button click event to solve tower
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BSolve_Click(object sender, EventArgs e)
        {
            resetPanel();
            bSolve.Enabled = false;
            moves.Clear();
            int NumberOfDisks = _DiskCount;   // Объявить количество дисков
            solveTower(NumberOfDisks);
            listMoves.DataSource = null;
            listMoves.DataSource = moves;     //Для решения башни
            bSolve.Enabled = true;
        }
        /// <summary>
        /// Setup tower values, process tower and display moves required
        /// </summary>
        /// <param name="numberOfDisks">Number of disks within the tower scenario</param>
        private void solveTower(int numberOfDisks)
        {
            char startPeg = 'A';                                // стартовая башня на выходе
            char endPeg = 'C';                                  // конечная башня на выходе
            char tempPeg = 'B';                                 // буферная башня на выходе
            //Решает башни рекурсией
            solveTowers(numberOfDisks, startPeg, endPeg, tempPeg);      
        }
        /// <summary>
        /// Recursive Method to solve Towers of Hanoi
        /// </summary>
        /// <param name="n">Disk to move</param>
        /// <param name="startPeg">Peg to take disk from</param>
        /// <param name="endPeg">Peg to move disk to</param>
        /// <param name="tempPeg">auxiliary peg</param>
        private void solveTowers(int n, char startPeg, char endPeg, char tempPeg)
        {
            if (n > 0)
            {
                solveTowers(n - 1, startPeg, tempPeg, endPeg);

                Disks currentDisk = TowerDisks.Find(x => x.DiskNubmer == n);
                currentDisk.peg = endPeg;

                //Анимирование
                animate.moveUp(currentDisk.box, 50);
                if (startPeg < endPeg)//Ход вправо
                    animate.moveRight(currentDisk.box, getDiskX(currentDisk));
                else //Ход влево
                    animate.moveLeft(currentDisk.box, getDiskX(currentDisk));
                animate.moveDown(currentDisk.box, getDiskY(currentDisk));

                //listBox (listMoves)
                string line = string.Format("Ход диска {0} с {1} на {2}", n, startPeg, endPeg);
                Console.WriteLine(line);
                moves.Add(line);
                solveTowers(n - 1, tempPeg, endPeg, startPeg);
            }
        }
        /// <summary>
        /// get the least amount of moves required to solve the tower
        /// </summary>
        /// <param name="numberOfDisks">Total number of disks in tower</param>
        /// <returns>Number of moves</returns>
        public static int GetMoveCount(int numberOfDisks)
        {
            double numberOfDisks_Double = numberOfDisks;
            return (int)Math.Pow(2.0, numberOfDisks_Double) - 1;
        }
        /// <summary>
        /// Value changed listener to set informational label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DiskCount_ValueChanged(object sender, EventArgs e)
        {
            _DiskCount = (int)DiskCount.Value;
            resetPanel();
        }
        /// <summary>
        /// Paint Base to panel1
        /// </summary>
        private void setupTower()
        {
            Panel.Paint += delegate
            {
                setBase();
            };
        }
        /// <summary>
        /// Draw base and pegs
        /// </summary>
        private void setBase()
        {
            SolidBrush sb = new SolidBrush(Color.RosyBrown);
            Graphics g = Panel.CreateGraphics();
            int topSpacing = 100;
            int width = 20;
            //Рисует нижнюю часть
            g.FillRectangle(sb, 0, Panel.Height - baseHeight, Panel.Width, baseHeight);
            //Рисует 1 стержень
            drawPeg(Panel, g, sb, 1, width, topSpacing);
            //Рисует 2 стержень
            drawPeg(Panel, g, sb, 2, width, topSpacing);
            //Рисует 3 стержень
            drawPeg(Panel, g, sb, 3, width, topSpacing);
        }
        /// <summary>
        /// Draw a peg 
        /// </summary>
        /// <param name="canvas">Panel to draw pegs on</param>
        /// <param name="g">panel.CreateGraphics</param>
        /// <param name="sb">SolidBrush</param>
        /// <param name="pegNo">Peg Number 1-3</param>
        /// <param name="pegWidth">Desired peg width</param>
        /// <param name="topSpacing">Spacing from the top</param>
        private void drawPeg(Panel canvas,Graphics g, SolidBrush sb, int pegNo,int pegWidth, int topSpacing)
        {
            g.FillRectangle(sb, ((int)(canvas.Width / 4) * pegNo)-(pegWidth/2), topSpacing, pegWidth, canvas.Height - topSpacing);
        }


    }
}