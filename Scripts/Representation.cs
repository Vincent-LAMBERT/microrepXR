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
using UnityEngine.UIElements;
using MixedReality.Toolkit;
// using UnityEngine.XR.Hands; // new for Handedness and XRHandJointID

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
[CustomEditor(typeof(Representation))]
public class RepresentationEditor : Editor
{
    SerializedProperty handProp;
    SerializedProperty artefactsProp;

    void OnEnable()
    {
        handProp = serializedObject.FindProperty("hand");
        artefactsProp = serializedObject.FindProperty("artefacts");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(handProp, new GUIContent("Hand"));
        EditorGUILayout.PropertyField(artefactsProp, new GUIContent("Artefacts"));

        serializedObject.ApplyModifiedProperties();
    }
}
#endif

namespace Microgestures
{
    [AddComponentMenu("Representation", 0)]
    [System.Serializable]
    public class Representation : MonoBehaviour
    {
        public Hand hand;
        public Artefact[] artefacts;
    
        public Representation(Handedness handedness, Artefact[] artefacts) {
            this.hand = new Hand(handedness);
            this.artefacts = artefacts;
        }

        void Start()
        {
            hand.initialize(artefacts);
            hand.instantiate(this.transform);
        }
        void Update()
        {
            hand.update(state);
        }
        private bool state;

        public void setActive() {
            state = true;
        }

        public void setInactive() {
            state = false;
        }
        public VisualTreeAsset m_InspectorXML;
    }
}