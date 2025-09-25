using System.Numerics;
using System.Diagnostics;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using TMPro;
using UnityEngine;
using Microgestures;
using UnityEditor;
using UnityEngine.UIElements;

namespace Microgestures
{
    [AddComponentMenu("TransformElements", 0)]
    [Serializable]
    public class TransformElements
    {
        public float positionX;
        public float positionY;
        public float positionZ;
        public float rotationX;
        public float rotationY;
        public float rotationZ;

        public TransformElements(float positionX, float positionY, float positionZ, float rotationX, float rotationY, float rotationZ)
        {
            this.positionX = positionX;
            this.positionY = positionY;
            this.positionZ = positionZ;
            this.rotationX = rotationX;
            this.rotationY = rotationY;
            this.rotationZ = rotationZ;
        }
    }
}