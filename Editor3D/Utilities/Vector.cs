﻿using System;

namespace Editor3D.Utilities
{
    internal class Vector
    {
        public double x, y, z, w;

        public Vector(double x, double y, double z, double w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        internal Vector CloneMoved(double a, double b, double c)
        {
            return new Vector(x + a, y + b, z + c, w);
        }

        internal void Render(IDisplayer displayer, PipelineInfo info)
        {
            Vector worldVector = info.GetModelMatrix().MultipliedBy(this);
            Vector viewVector = info.GetViewMatrix().MultipliedBy(worldVector);
            Vector ndcVector = info.GetProjectionMatrix().MultipliedBy(viewVector).DivideByW();
            Vector screenVector = ndcVector.InScreenSpace(info.GetScreenWidth(), info.GetScreenHeight());
            displayer.Display(screenVector.x, screenVector.y);
        }

        private Vector InScreenSpace(double width, double height)
        {
            double screenX = ((x + 1) * width) / 2;
            double screenY = ((y + 1) * height) / 2;
            double screenZ = (z + 1) / 2;
            return new Vector(screenX, screenY, screenZ, 1);
        }

        private Vector DivideByW()
        {
            x /= w;
            y /= w;
            z /= w;
            w = 1;
            return this;
        }

        internal Vector CrossProduct(Vector vector)
        {
            double resultX = (y * vector.z) - (z * vector.y);
            double resultY = (z * vector.x) - (x * vector.z);
            double resultZ = (x * vector.y) - (y * vector.x);
            return new Vector(resultX, resultY, resultZ, 0);
        }

        internal Vector DirectionTo(Vector vector)
        {
            return new Vector(vector.x - x, vector.y - y, vector.z - z, 0);
        }

        internal Vector Normalize()
        {
            double magnitude = Magnitude();
            x /= magnitude;
            y /= magnitude;
            z /= magnitude;
            return this;
        }

        internal Vector NegatedWithoutW()
        {
            return new Vector(-x, -y, -z, w);
        }

        internal double DistanceTo(Vector pos)
        {
            return Math.Sqrt(((pos.x - x) * (pos.x - x)) + ((pos.y - y) * (pos.y - y)) + ((pos.z - z) * (pos.z - z)));
        }

        internal double Magnitude()
        {
            return Math.Sqrt((x * x) + (y * y) + (z * z));
        }

        internal Vector Clone()
        {
            return new Vector(x, y, z, w);
        }
    }
}
