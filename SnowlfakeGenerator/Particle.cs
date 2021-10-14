using System;
using System.Collections.Generic;
using System.Drawing;

namespace SnowlfakeGenerator
{
    class Particle
    {
        public Point Position { get; set; }
        private int radius = 4;
        public Particle(Point startingPosition)
        {
            Position = startingPosition;
        }

        public void DrawSelf(ref Graphics graphics)
        {
            var brush = new SolidBrush(Color.Snow);
            graphics.FillEllipse(brush,Position.X - radius,Position.Y - radius,radius*2,radius*2);
        }

        public void Update(int height)
        {
            var rng = new Random();
            int newX = Position.X - 1;
            int newY = Position.Y + rng.Next(-15,15);

            int constraint = height / 4;
            newY = Math.Min(newY, constraint);
            newY = Math.Max(newY, -constraint);
            
            Position = new Point(newX, newY);
        }

        public bool Intersect(List<Particle> particles)
        {
            foreach (var particle in particles)
            {
                if (Dis(Position, particle.Position) < radius + particle.radius)
                {
                    return true;
                }
            }

            return false;
        }

        private float Dis(Point a, Point b)
        {
            return MathF.Sqrt(MathF.Pow((b.X - a.X), 2) + MathF.Pow((b.Y - a.Y), 2));
        }
    }
}