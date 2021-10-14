using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnowlfakeGenerator
{
    public partial class Form1 : Form
    {

        private readonly int _width;
        private readonly int _height;
        private readonly Bitmap _bitmap;
        private Graphics _graphics;
        private List<Particle> _snowflake;
        public Form1()
        {
            InitializeComponent();
            _snowflake = new List<Particle>();
            _width = pic_Main.Width;
            _height = pic_Main.Height;
            timer1.Interval = 6;

            _bitmap = new Bitmap(_width, _height);
            _graphics = Graphics.FromImage(_bitmap);
            _graphics.TranslateTransform(_width / 2, _height / 2);
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Generate_Click(object sender, EventArgs e)
        {
            _snowflake.Clear();
            
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            timer1.Start();
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            var rng = new Random();
            var stX = _width / 4;
            var p = new Particle(new Point(stX, rng.Next(-250,250)));
            //Stop the timer when the new flakes cant move anymore
            if (p.Intersect(_snowflake))
            {
                timer1.Stop();
                return;
            }
            while (p.Position.X > 1 && p.Position.X <= stX && !p.Intersect(_snowflake))
            {
                p.Update(_height);
            }
            _snowflake.Add(p);
            DrawSnowflake();
        }

        private void DrawSnowflake()
        {
            _graphics.Clear(Color.Black);
            for (int j = 0; j < 2; j++)
            {
                _graphics.ScaleTransform(1f, -1f);
                for (int i = 0; i < 6; i++)
                {

                    _graphics.RotateTransform(60);
                    foreach (var particle in _snowflake)
                    {
                        particle.DrawSelf(ref _graphics);
                    }
                }

            }
           
            
            pic_Main.Image = _bitmap;
        }

        private int export_i = 0;
        private void btn_Export_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dialog.SelectedPath))
            {
                pic_Main.Image.Save($"{dialog.SelectedPath}\\snowflake_{export_i}.png");
                export_i++;
            }
        }
    }
}
