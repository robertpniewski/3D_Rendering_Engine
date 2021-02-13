﻿using Editor3D.Shapes;
using Editor3D.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor3D
{
    public partial class EditorForm : Form, IDisplayer
    {
        private const bool SHOULD_RENDER_LINES = true;
        private const int FRAMES_PER_SECOND = 20;
        private const Shading shading = Shading.Flat;

        private Color[] colors = { Color.Red, Color.Blue, Color.Green };
        private int currentColorIndex = 0;
        private Bitmap bitmap;
        private List<Camera> cameras = new List<Camera>();
        private List<Light> lights = new List<Light>();
        private int currentCameraIndex;
        private double i = 0;
        private Color currentColor = Color.Black;
        private double[,] zBuffor;

        public EditorForm()
        {
            InitializeComponent();
            PrepareCameras();
            PrepareLights();
            //RenderGraphicsPeriodically();
            RenderGraphics();
        }

        private void PrepareCameras()
        {
            Vector cameraPosition = new Vector(20, 20, 10, 1);
            Vector observedPosition = new Vector(0, 0, 0, 1);
            double nearPlane = 1; // 15
            double farPlane = 100; // 45
            double fieldOfView = Math.PI / 4;
            double aspect = pictureBox1.Width / pictureBox1.Height;
            cameras.Add(new Camera(cameraPosition, observedPosition,
                nearPlane, farPlane, fieldOfView, aspect));
            currentCameraIndex = 0;
        }

        private void PrepareLights()
        {
            lights.Add(new Light(Color.White, Color.White, new Vector(12, 12, 5, 1)));
        }

        private void PrepareBitmap()
        {
            if (bitmap != null)
            {
                bitmap.Dispose();
            }
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            zBuffor = new double[pictureBox1.Width, pictureBox1.Height];
            //for (int i = 0; i < pictureBox1.Width; ++i)
            //    for (int j = 0; j < pictureBox1.Height; ++j)
            //        zBuffor[i, j] = double.MinValue;
        }

        private void RenderGraphicsPeriodically()
        {
            Timer timer = new Timer();
            int millisecondsInOneSecond = 1000;
            timer.Interval = millisecondsInOneSecond / FRAMES_PER_SECOND;
            timer.Tick += RenderGraphics;
            timer.Start();
        }

        private void RenderGraphics(object sender, EventArgs e)
        {
            RenderGraphics();
        }

        private void RenderGraphics()
        {
            PrepareBitmap();
            //RenderCuboid(3, 4, 5, new Vector(i - 50, i - 30, (i / 3) - 20, 1), Color.Blue);
            //RenderCuboid(1, 1, 1, new Vector(12, 12, 5, 1), Color.Red);
            RenderCuboid(3, 4, 5, new Vector(i + 3, i + 5, (i / 3) + 4, 1), Color.Cyan);
            i += 0.05;
            pictureBox1.Refresh();
            // TODO: Add other objects
        }

        private void RenderCuboid(int x, int y, int z, Vector position, Color color)
        {
            Cuboid cuboid = new Cuboid(x, y, z, position, color);
            cuboid.Render(this, GeneratePipelineInfo());
        }

        private PipelineInfo GeneratePipelineInfo()
        {
            Camera currentCamera = cameras[currentCameraIndex];
            return new PipelineInfo(currentCamera.GetViewMatrix(),
                currentCamera.GetProjectionMatrix(), bitmap.Width, bitmap.Height,
                currentCamera.GetForwardDirection(), SHOULD_RENDER_LINES, lights,
                currentCamera.GetPosition());
        }

        /**
         * UNUSED
         */
        private void RenderColor(object sender, EventArgs e)
        {
            for (int i = 0; i < bitmap.Width; ++i)
            {
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    bitmap.SetPixel(i, j, colors[currentColorIndex]);
                }
            }
            ++currentColorIndex;
            if (currentColorIndex > 2)
            {
                currentColorIndex = 0;
            }
            pictureBox1.Refresh();
        }

        public void Display(int x, int y, double z, Color color)
        {
            if (x >= 0 && x < bitmap.Width && y >= 0 && y < bitmap.Height)
            {
                if (z >= zBuffor[x, y])
                {
                    bitmap.SetPixel(x, y, color); // TODO: X and Z seem to be swapped
                    zBuffor[x, y] = z;
                }
            }
        }

        public void SetColor(Color color)
        {
            this.currentColor = color;
        }

        public Shading GetShading()
        {
            return shading;
        }
    }
}
