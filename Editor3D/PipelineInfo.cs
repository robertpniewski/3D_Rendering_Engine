﻿using Editor3D.Utilities;
using System;
using System.Collections.Generic;

namespace Editor3D
{
    internal class PipelineInfo
    {
        private Matrix rotationMatrix;
        private Matrix translationMatrix;
        private readonly Matrix viewMatrix;
        private readonly Matrix projectionMatrix;
        private readonly int screenWidth;
        private readonly int screenHeight;
        private readonly Vector forwardDirection;
        private readonly bool shouldRenderLines;
        private readonly List<Light> lights;
        private readonly Vector cameraPosition;

        internal PipelineInfo(Matrix viewMatrix, Matrix projectionMatrix,
            int screenWidth, int screenHeight, Vector forwardDirection,
            bool shouldRenderLines, List<Light> lights, Vector cameraPosition)
        {
            this.viewMatrix = viewMatrix;
            this.projectionMatrix = projectionMatrix;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.forwardDirection = forwardDirection;
            this.shouldRenderLines = shouldRenderLines;
            this.lights = lights;
            this.cameraPosition = cameraPosition;
        }

        internal Matrix GetRotationMatrix()
        {
            return rotationMatrix;
        }

        internal Matrix GetTranslationMatrix()
        {
            return translationMatrix;
        }

        internal Matrix GetViewMatrix()
        {
            return viewMatrix;
        }

        internal Matrix GetProjectionMatrix()
        {
            return projectionMatrix;
        }

        internal int GetScreenWidth()
        {
            return screenWidth;
        }

        internal int GetScreenHeight()
        {
            return screenHeight;
        }

        internal Vector GetCameraPosition()
        {
            return cameraPosition;
        }

        internal Vector GetForwardDirection()
        {
            return forwardDirection;
        }

        internal List<Light> GetLights()
        {
            return lights;
        }

        internal void SetRotationMatrix(Matrix matrix)
        {
            this.rotationMatrix = matrix;
        }

        internal void SetTranslationMatrix(Matrix matrix)
        {
            this.translationMatrix = matrix;
        }

        internal bool ShouldRenderLines()
        {
            return shouldRenderLines;
        }
    }
}
