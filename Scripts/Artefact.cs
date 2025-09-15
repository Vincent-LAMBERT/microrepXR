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
    [AddComponentMenu("Artefact", 0)]
    [Serializable]
    public class Artefact
    {
        public GameObject gameObject;
        public TransformElements transformElements;
        public Behavior behavior;
        public Placeholder placeholder;
        public Command command;

        public Artefact(GameObject gameObject, TransformElements transformElements, Behavior behavior, Placeholder placeholder, Command command)
        {
            this.gameObject = gameObject;
            this.transformElements = transformElements;
            this.behavior = behavior;
            this.placeholder = placeholder;
            this.command = command;
        }

        public GameObject getGameObject() {
            return gameObject;
        }

        public TransformElements getTransformElements() {
            return transformElements;
        }

        public Behavior getBehavior()
        {
            return behavior;
        }

        public Placeholder getPlaceholder() {
            return placeholder;
        }

        public Command getCommand() {
            return command;
        }
    }
}