using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TowersWindows
{
    public partial class Form1 : Form
    {
        private List<string> moves = new List<string>();
        private List<Disks> _TowerDisks = new List<Disks>();
        AnimateView animate = new AnimateView();
        int _DiskCount = 3;
        int diskHeight = 30;
        int baseHeight = 20;//Base is 20 high
        public Form1()
        {
            InitializeComponent();
            AnimateView.view = panel1;
            resetPanel();
        }
        /// <summary>
        /// Create disks on Panel
        /// </summary>
        private void populateDisks()
        {
            int ii = 1;
            foreach (Disks disk in _TowerDisks)
            {
                PictureBox panelBox = disk.box;
                panelBox.BackColor = colorSelector(disk);
                disk.width = 200 - (20 * ii);
                panelBox.Width = disk.width;
                panelBox.Height = diskHeight;
                panelBox.Tag = disk.DiskNo;
                panelBox.BorderStyle = BorderStyle.FixedSingle;
                Point boxLocation = new Point(getDiskX(disk), (panel1.Height - baseHeight) - (diskHeight * ii++));
                panelBox.Location = boxLocation;
                disk.box = panelBox;
                panel1.Controls.Add(disk.box);
            }
        }
        /// <summary>
        /// Get X position for Disk 
        /// </summary>
        /// <param name="disk">Disk to check</param>
        /// <returns>X position for Disk</returns>
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
            X = ((panel1.Width / 4) * Peg) - (int)(disk.width / 2);

            return X; 
        }
        /// <summary>
        /// Recreates the Panel
        /// </summary>
        private void resetPanel()
        {
            //Create pegs for Tower
            setupTower();
            panel1.Controls.Clear();
            _TowerDisks = Enumerable.Range(1, _DiskCount).Select(i => new Disks() { DiskNo = i, peg = 'A', box = new PictureBox() }).OrderByDescending(i => i.DiskNo).ToList();
            //Place Disks on panel
            populateDisks();
            //Set initial text value for least possible moves
            lblCounter.Text = string.Format("Best possible moves {0}", GetMoveCount(_DiskCount));
        }
        /// <summary>
        /// Get Disk Y position
        /// </summary>
        /// <param name="disk">Disk to check</param>
        /// <returns>Y Position for Disk</returns>
        private int getDiskY(Disks disk)
        {
            int Y = 0;
            int stackNo = _TowerDisks.Count(x => x.peg == disk.peg);
            Y = panel1.Height - baseHeight - (diskHeight * stackNo);
            return Y;
        }
        private Color colorSelector(Disks disk)
        {
            switch (disk.DiskNo)
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
        private void BtnSolve_Click(object sender, EventArgs e)
        {
            resetPanel();
            btnSolve.Enabled = false;
            moves.Clear();
            //listMoves.Items.Clear();                            // Clear List of moves
            int NumberOfDisks = _DiskCount;                     // Declare number of disks
            solveTower(NumberOfDisks);
            listMoves.DataSource = null;
            listMoves.DataSource = moves;                            // Solve tower
            btnSolve.Enabled = true;
        }
        /// <summary>
        /// Setup tower values, process tower and display moves required
        /// </summary>
        /// <param name="numberOfDisks">Number of disks within the tower scenario</param>
        private void solveTower(int numberOfDisks)
        {
            char startPeg = 'A';                                // start tower in output
            char endPeg = 'C';                                  // end tower in output
            char tempPeg = 'B';                                 // temporary tower in output
            //Solve towers using recursive method
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

                Disks currentDisk = _TowerDisks.Find(x => x.DiskNo == n);
                currentDisk.peg = endPeg;

                //Animate
                animate.moveUp(currentDisk.box, 50);
                if (startPeg < endPeg)//Move Right
                    animate.moveRight(currentDisk.box, getDiskX(currentDisk));
                else //move Left
                    animate.moveLeft(currentDisk.box, getDiskX(currentDisk));
                animate.moveDown(currentDisk.box, getDiskY(currentDisk));

                //Format line
                string line = string.Format("Move disk {0} from {1} to {2}", n, startPeg, endPeg);
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
            panel1.Paint += delegate
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
            Graphics g = panel1.CreateGraphics();
            int topSpacing = 100;
            int width = 20;
            //Draw bottom bar
            g.FillRectangle(sb, 0, panel1.Height - baseHeight, panel1.Width, baseHeight);
            //Draw Peg 1
            drawPeg(panel1, g, sb, 1, width, topSpacing);
            //Draw Peg 2
            drawPeg(panel1, g, sb, 2, width, topSpacing);
            //Draw Peg 3
            drawPeg(panel1, g, sb, 3, width, topSpacing);
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