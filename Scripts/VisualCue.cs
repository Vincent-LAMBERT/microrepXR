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
    [AddComponentMenu("VisualCue", 0)]
    [Serializable]
    public class VisualCue
    {
        public GameObject gameObject;
        public Behavior behavior;
        public Placeholder placeholder;

        public VisualCue(GameObject gameObject, Behavior behavior, Placeholder placeholder)
        {
            this.gameObject = gameObject;
            this.behavior = behavior;
            this.placeholder = placeholder;
        }

        public GameObject getGameObject() {
            return gameObject;
        }

        public Behavior getBehavior() {
            return behavior;
        }

        public Placeholder getPlaceholder() {
            return placeholder;
        }
    }
}