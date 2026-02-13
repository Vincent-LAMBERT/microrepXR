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
    public enum TextLocation  { Up, Down, Left, Right }

    [AddComponentMenu("Command", 0)]
    [Serializable]
    public class Command
    {
        public string text = "";
        public float fontSize = 20;
        public Color textColor = Color.white;
        public Color outlineColor = Color.black;
        public float outlineWidth = 0.2f;
        public TextLocation textLocation = TextLocation.Down;
        public TransformElements transformElements;

        public Command(string text, TextLocation textLocation)
        {
            this.text = text;
            this.textLocation = textLocation;
        }
    }
}