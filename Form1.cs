﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048_kurs_
{
    
    public partial class Form1 : Form
    {
        private int[,] map = new int[4, 4];
        private Label[,] labels = new Label[4, 4];
        public PictureBox[,] pics = new PictureBox[4, 4];

        private int score = 0;
        private int bestscore = 0;

        public Form1()
        {
            InitializeComponent();

            this.KeyDown += new KeyEventHandler(_keyboardEvent);
            map[0, 0] = 1;
            map[0, 1] = 1;
            createMap();
            createPics();
            generateNewCube();
        }
        public void generateNewCube()
        {
            Random rnd = new Random();
            int a = rnd.Next(0, 4);
            int b = rnd.Next(0, 4);
            while (pics[a, b] != null) // перевірка чи є комірка вільна, то ми генеруємо комірку
            {

                a = rnd.Next(0, 4);
                b = rnd.Next(0, 4);
            }

                if (100 - rnd.Next(0, 100) > 10)
                {
                    map[a, b] = 1;
                    pics[a, b] = new PictureBox();
                    labels[a, b] = new Label();
                    labels[a, b].Text = "2";
                    labels[a, b].Size = new Size(50, 50);
                    labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
                    labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
                    pics[a, b].Controls.Add(labels[a, b]);
                    pics[a, b].Location = new Point(16 + b * 56, 92 + 56 * a);
                    pics[a, b].Size = new Size(50, 50);
                    pics[a, b].BackColor = Color.FloralWhite;
                    this.Controls.Add(pics[a, b]);
                    pics[a, b].BringToFront();
                }
                else
                {
                    map[a, b] = 1;
                    pics[a, b] = new PictureBox();
                    labels[a, b] = new Label();
                    labels[a, b].Text = "4";
                    labels[a, b].Size = new Size(50, 50);
                    labels[a, b].TextAlign = ContentAlignment.MiddleCenter;
                    labels[a, b].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
                    pics[a, b].Controls.Add(labels[a, b]);
                    pics[a, b].Location = new Point(16 + b * 56, 92 + 56 * a);
                    pics[a, b].Size = new Size(50, 50);
                    pics[a, b].BackColor = Color.AntiqueWhite;
                    this.Controls.Add(pics[a, b]);
                    pics[a, b].BringToFront();
                }
            
        }
        private void createPics()
        {
            pics[0, 0] = new PictureBox();
            labels[0, 0] = new Label();
            labels[0, 0].Text = "2";
            labels[0, 0].Size = new Size(50, 50);
            labels[0, 0].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 0].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[0, 0].Controls.Add(labels[0, 0]);
            pics[0, 0].Location = new Point(16, 92);
            pics[0, 0].Size = new Size(50, 50);
            pics[0, 0].BackColor = Color.FloralWhite;
            this.Controls.Add(pics[0, 0]);
            pics[0, 0].BringToFront();

            pics[0, 1] = new PictureBox();
            labels[0, 1] = new Label();
            labels[0, 1].Text = "4";
            labels[0, 1].Size = new Size(50, 50);
            labels[0, 1].TextAlign = ContentAlignment.MiddleCenter;
            labels[0, 1].Font = new Font(new FontFamily("Microsoft Sans Serif"), 15);
            pics[0, 1].Controls.Add(labels[0, 1]);
            pics[0, 1].Location = new Point(72, 92);
            pics[0, 1].Size = new Size(50, 50);
            pics[0, 1].BackColor = Color.AntiqueWhite;
            this.Controls.Add(pics[0, 1]);
            pics[0, 1].BringToFront();

        }

        private void createMap()
        {
            int kletki =0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    PictureBox pic = new PictureBox();
                    pic.Location = new Point(16 + 56 * j, 92 + 56 * i);
                    pic.Size = new Size(50, 50);
                    pic.BackColor = Color.LightGray;
                    this.Controls.Add(pic);
                }
            }

        }
        private void ChangeColor(int sum, int k, int j)
        {
            if (sum % 1024 == 0) pics[k, j].BackColor = Color.LightGoldenrodYellow;
            else if (sum % 512 == 0) pics[k, j].BackColor = Color.LightYellow;
            else if (sum % 256 == 0) pics[k, j].BackColor = Color.YellowGreen;
            else if (sum % 128 == 0) pics[k, j].BackColor = Color.Yellow;
            else if (sum % 64 == 0) pics[k, j].BackColor = Color.DarkRed;
            else if (sum % 32 == 0) pics[k, j].BackColor = Color.Red;
            else if (sum % 16 == 0) pics[k, j].BackColor = Color.Orange;
            else if (sum % 8 == 0) pics[k, j].BackColor = Color.OrangeRed;
            else pics[k, j].BackColor = Color.AntiqueWhite;
        }
        private void _keyboardEvent(object sender, KeyEventArgs e)
        {
            bool ifPicsMoved = false;


                switch (e.KeyCode.ToString())
                {
                    case "Right":
                        for (int k = 0; k < 4; k++)
                        {
                            for (int l = 2; l >= 0; l--)
                            {
                            // Перевірка правої сусідної комірки
                                if (map[k, l] == 1) // k- вверх, вниз, l- вліво, вправо
                                {
                                    for (int j = l + 1; j < 4; j++)
                                    {
                                    // обнуляємо ту комірку на якій нема комірки
                                        if (map[k, j] == 0)// j<4
                                        {
                                            ifPicsMoved = true;
                                            map[k, j - 1] = 0;
                                            map[k, j] = 1;
                                            pics[k, j] = pics[k, j - 1];
                                            pics[k, j - 1] = null;
                                            labels[k, j] = labels[k, j - 1];
                                            labels[k, j - 1] = null; 
                                            pics[k, j].Location = new Point(pics[k, j].Location.X + 56, pics[k, j].Location.Y);
                                        }
                                        else
                                        {
                                            int a = int.Parse(labels[k, j].Text);
                                            int b = int.Parse(labels[k, j - 1].Text);
                                            if (a == b)
                                            {
                                                ifPicsMoved = true;
                                                labels[k, j].Text = (a + b).ToString();
                                                score += (a + b);
                                                ChangeColor(a + b, k, j);
                                                label1.Text = "Очок: " + score;
                                                map[k, j - 1] = 0;
                                                this.Controls.Remove(pics[k, j - 1]);
                                                this.Controls.Remove(labels[k, j - 1]);
                                                pics[k, j - 1] = null;
                                                labels[k, j - 1] = null;

                                            }
                                        }

                                    }
                                }
                            }
                        }
                        break;
                    case "Left":
                        for (int k = 0; k < 4; k++)
                        {
                            for (int l = 1; l < 4; l++)
                            {
                                if (map[k, l] == 1)
                                {
                                    for (int j = l - 1; j >= 0; j--)
                                    {
                                        if (map[k, j] == 0)
                                        {
                                            ifPicsMoved = true;
                                            map[k, j + 1] = 0;
                                            map[k, j] = 1;
                                            pics[k, j] = pics[k, j + 1];
                                            pics[k, j + 1] = null;
                                            labels[k, j] = labels[k, j + 1];
                                            labels[k, j + 1] = null;
                                            pics[k, j].Location = new Point(pics[k, j].Location.X - 56, pics[k, j].Location.Y);
                                        }
                                        else
                                        {
                                            int a = int.Parse(labels[k, j].Text);
                                            int b = int.Parse(labels[k, j + 1].Text);
                                            if (a == b)
                                            {
                                                ifPicsMoved = true;
                                                labels[k, j].Text = (a + b).ToString();
                                                score += (a + b);
                                                ChangeColor(a + b, k, j);
                                                label1.Text = "Очок: " + score;
                                                map[k, j + 1] = 0;
                                                this.Controls.Remove(pics[k, j + 1]);
                                                this.Controls.Remove(labels[k, j + 1]);
                                                pics[k, j + 1] = null;
                                                labels[k, j + 1] = null;


                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "Down":
                        for (int k = 2; k >= 0; k--)
                        {
                            for (int l = 0; l < 4; l++)
                            {
                                if (map[k, l] == 1)
                                {
                                    for (int j = k + 1; j < 4; j++)
                                    {
                                        if (map[j, l] == 0)
                                        {
                                            ifPicsMoved = true;
                                            map[j - 1, l] = 0;
                                            map[j, l] = 1;
                                            pics[j, l] = pics[j - 1, l];
                                            pics[j - 1, l] = null;
                                            labels[j, l] = labels[j - 1, l];
                                            labels[j - 1, l] = null;
                                            pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y + 56);
                                        }
                                        else
                                        {
                                            int a = int.Parse(labels[j, l].Text);
                                            int b = int.Parse(labels[j - 1, l].Text);
                                            if (a == b)
                                            {
                                                ifPicsMoved = true;
                                                labels[j, l].Text = (a + b).ToString();
                                                score += (a + b);
                                                ChangeColor(a + b, j, l);
                                                label1.Text = "Очок: " + score;
                                                map[j - 1, l] = 0;
                                                this.Controls.Remove(pics[j - 1, l]);
                                                this.Controls.Remove(labels[j - 1, l]);
                                                pics[j - 1, l] = null;
                                                labels[j - 1, l] = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "Up":
                        for (int k = 1; k < 4; k++)
                        {
                            for (int l = 0; l < 4; l++)
                            {
                                if (map[k, l] == 1)
                                {
                                    for (int j = k - 1; j >= 0; j--)
                                    {
                                        if (map[j, l] == 0)
                                        {
                                            ifPicsMoved = true;
                                            map[j + 1, l] = 0;
                                            map[j, l] = 1;
                                            pics[j, l] = pics[j + 1, l];
                                            pics[j + 1, l] = null;
                                            labels[j, l] = labels[j + 1, l];
                                            labels[j + 1, l] = null;
                                            pics[j, l].Location = new Point(pics[j, l].Location.X, pics[j, l].Location.Y - 56);
                                        }
                                        else
                                        {
                                            int a = int.Parse(labels[j, l].Text);
                                            int b = int.Parse(labels[j + 1, l].Text);
                                            if (a == b)
                                            {
                                                ifPicsMoved = true;
                                                labels[j, l].Text = (a + b).ToString();
                                                score += (a + b);
                                                ChangeColor(a + b, j, l);
                                                label1.Text = "Очок: " + score;
                                                map[j + 1, l] = 0;
                                                this.Controls.Remove(pics[j + 1, l]);
                                                this.Controls.Remove(labels[j + 1, l]);
                                                pics[j + 1, l] = null;
                                                labels[j + 1, l] = null;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                }
                if (ifPicsMoved)
                    generateNewCube();

            
            if (map[0, 1] == 1 && map[0, 0]==1 && map[0, 2] == 1 && map[0, 3] == 1  && map[1, 0] == 1 && map[1, 1] == 1 && map[1, 2] == 1 && map[1, 3] == 1 && map[2, 1] == 1 && map[2, 2] == 1 && map[2, 3] == 1 && map[2, 0] == 1 && map[3, 0] == 1 && map[3, 1] == 1 && map[3, 2] == 1 && map[3, 3] == 1)
            {
                MessageBox.Show(score+" Очок вы набрали", "");

                Application.Restart();
                
            }

         }
 

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

            if (File.Exists("best.txt"))
            {

                using (FileStream fs = new FileStream("best.txt", FileMode.Open))
                {
                    using (StreamReader streamwriter = new StreamReader(fs))
                    {

                        try { bestscore = int.Parse(streamwriter.ReadToEnd()); }
                        catch { }
                            
                        
                    }
                }

            }
            else
            {
                using (FileStream fs = new FileStream("best.txt", FileMode.OpenOrCreate))
                {
                    using (StreamWriter streamReader = new StreamWriter(fs))
                    {
                        streamReader.WriteLine("");
                    }
                }
            }

            label2.Text = "Лучшие Очки: "+ bestscore.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            int currentbestscore = 0;
                using (FileStream fs = new FileStream("best.txt", FileMode.OpenOrCreate))
                {
                     using (StreamReader streamwriter = new StreamReader(fs))
                        {
                            try { currentbestscore = int.Parse(streamwriter.ReadToEnd()); }
                            catch { }
                     }
                }
            using (FileStream fs = new FileStream("best.txt", FileMode.OpenOrCreate))
            {
                using (StreamWriter streamReader = new StreamWriter(fs))
                {
                    if (bestscore < score)
                    {
                        streamReader.WriteLine(score);
                    }
                }
            }
        }
        
    }
    }

