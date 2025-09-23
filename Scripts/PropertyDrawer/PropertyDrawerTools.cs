using System.Numerics;
using System.Diagnostics;
using System.Globalization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using Microgestures;
using UnityEngine.UIElements;


#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
public class PropertyDrawerTools
{
    protected Rect currentPosition;
    
    bool horizontal = false;
    int initialHeight;
    int initialWidth;
    int initialX;
    int initialY;
    int maxHeight;


    public Rect getCurrentPosition() { return currentPosition; }
    public void setCurrentPosition(Rect currentPosition) { this.currentPosition = currentPosition; }

    public bool getHorizontal() { return horizontal; }
    public void setHorizontal(bool horizontal) { this.horizontal = horizontal; }

    public int getInitialHeight() { return initialHeight; }
    public void setInitialHeight(int initialHeight) { this.initialHeight = initialHeight; }

    public int getInitialWidth() { return initialWidth; }
    public void setInitialWidth(int initialWidth) { this.initialWidth = initialWidth; }

    public int getInitialX() { return initialX; }
    public void setInitialX(int initialX) { this.initialX = initialX; }

    public int getMaxHeight() { return maxHeight; }
    public void setMaxHeight(int maxHeight) { this.maxHeight = maxHeight; }

    public static int marginY = 5;
    public static int marginX = 5;

    public PropertyDrawerTools() {}

    public PropertyDrawerTools(Rect position) {
        this.currentPosition = position;
    }

    // public void copyStatus(PropertyDrawerTools tools) {
    //     setCurrentPosition(tools.getCurrentPosition());
    //     setHorizontal(tools.getHorizontal());
    //     setInitialHeight(tools.getInitialHeight());
    //     setInitialWidth(tools.getInitialWidth());
    //     setInitialX(tools.getInitialX());
    //     setMaxHeight(tools.getMaxHeight());
    // }

    public int maxHeightInProps(params SerializedProperty[] props) {
        int max = 0;
        int propHeight = 0;
        foreach (SerializedProperty prop in props) {
            propHeight = (int) EditorGUI.GetPropertyHeight(prop);
            if (propHeight>max) {
                max = propHeight;
            }
        }
        return max;
    }

    public Enum insertEnum(Enum e) {
        int width = (int) currentPosition.width;
        return insertEnum(e, width);
    }

    public Enum insertEnum(Enum e, float widthPart) {
        int width = (int) ((float) initialWidth*widthPart);
        return insertEnum(e, width);
    }

    public Enum insertEnum(Enum e, int width) {
        if (horizontal) {
            return insertEnumHorizontally(e, width);
        } else {
            return insertEnumVertically(e, width);
        }
    }

    private Enum insertEnumVertically(Enum e, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        currentPosition.height=initialHeight;
        Enum res = EditorGUI.EnumPopup(currentPosition, e);
        addVerticalMargin();
        currentPosition.width = initWidth;
        currentPosition.y+=initialHeight;
        return res;
    }

    private Enum insertEnumHorizontally(Enum e, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        addHorizontalMargins();
        Enum res = EditorGUI.EnumPopup(currentPosition, e);
        removeHorizontalMargins();
        currentPosition.x+=currentPosition.width;
        currentPosition.width = initWidth;
        return res;
    }

    public void insertLabel(string str) {
        int width = (int) currentPosition.width;
        if (widthToDefine) { width = (int) Math.Sqrt(str.Length*500); }
        insertLabel(str, width);
    }

    public void insertLabel(string str, float widthPart) {
        int width = (int) ((float) initialWidth*widthPart);
        insertLabel(str, width);
    }

    public void insertLabel(string str, int width) {
        if (horizontal) {
            insertLabelHorizontally(str, width);
        } else {
            insertLabelVertically(str, width);
        }
    }

    private void insertLabelVertically(string str, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        currentPosition.height=initialHeight;
        EditorGUI.LabelField(currentPosition, str);
        addVerticalMargin();
        currentPosition.width = initWidth;
        currentPosition.y+=initialHeight;
    }
    private void insertLabelHorizontally(string str, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        addHorizontalMargins();
        EditorGUI.LabelField(currentPosition, str);
        removeHorizontalMargins();
        currentPosition.x+=currentPosition.width;
        currentPosition.width = initWidth;
    }

    public int insertInt(int integer) {
        int width = (int) currentPosition.width;
        if (widthToDefine) { width = 50; }
        return insertInt(integer, width);
    }

    public int insertInt(int integer, float widthPart) {
        int width = (int) ((float) initialWidth*widthPart);
        return insertInt(integer, width);
    }

    public int insertInt(int integer, int width) {
        if (horizontal) {
            return insertIntHorizontally(integer, width);
        } else {
            return insertIntVertically(integer, width);
        }
    }

    private int insertIntVertically(int integer, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        currentPosition.height=initialHeight;
        integer = EditorGUI.IntField(currentPosition, integer);
        addVerticalMargin();
        currentPosition.width = initWidth;
        currentPosition.y+=initialHeight;
        return integer;
    }

    private int insertIntHorizontally(int integer, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        addHorizontalMargins();
        integer = EditorGUI.IntField(currentPosition, integer);
        removeHorizontalMargins();
        currentPosition.x+=currentPosition.width;
        currentPosition.width = initWidth;
        return integer;
    }

    public float insertFloat(float number) {
        int width = (int) currentPosition.width;
        if (widthToDefine) { width = 50; }
        return insertFloat(number, width);
    }

    public float insertFloat(float number, float widthPart) {
        int width = (int) ((float) initialWidth*widthPart);
        return insertFloat(number, width);
    }

    public float insertFloat(float number, int width) {
        if (horizontal) {
            return insertFloatHorizontally(number, width);
        } else {
            return insertFloatVertically(number, width);
        }
    }

    private float insertFloatVertically(float number, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        currentPosition.height=initialHeight;
        number = EditorGUI.FloatField(currentPosition, number);
        addVerticalMargin();
        currentPosition.width = initWidth;
        currentPosition.y+=initialHeight;
        return number;
    }

    private float insertFloatHorizontally(float number, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        addHorizontalMargins();
        number = EditorGUI.FloatField(currentPosition, number);
        removeHorizontalMargins();
        currentPosition.x+=currentPosition.width;
        currentPosition.width = initWidth;
        return number;
    }

    public Color insertColor(Color color) {
        int width = (int) currentPosition.width;
        if (widthToDefine) { width = 70; }
        return insertColor(color, width);
    }

    public Color insertColor(Color color, float widthPart) {
        int width = (int) ((float) initialWidth*widthPart);
        return insertColor(color, width);
    }

    public Color insertColor(Color color, int width) {
        if (horizontal) {
            return insertColorHorizontally(color, width);
        } else {
            return insertColorVertically(color, width);
        }
    }

    private Color insertColorVertically(Color color, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        currentPosition.height=initialHeight;
        color = EditorGUI.ColorField(currentPosition, color);
        addVerticalMargin();
        currentPosition.width = initWidth;
        currentPosition.y+=initialHeight;
        return color;
    }

    private Color insertColorHorizontally(Color color, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        addHorizontalMargins();
        color = EditorGUI.ColorField(currentPosition, color);
        removeHorizontalMargins();
        currentPosition.x+=currentPosition.width;
        currentPosition.width = initWidth;
        return color;
    }

    public string insertTextField(string str) {
        int width = (int) currentPosition.width;
        if (widthToDefine) { width = 100; }
        return insertTextField(str, width);
    }

    public string insertTextField(string str, float widthPart) {
        int width = (int) ((float) initialWidth*widthPart);
        return insertTextField(str, width);
    }

    public string insertTextField(string str, int width) {
        if (horizontal) {
            return insertTextFieldHorizontally(str, width);
        } else {
            return insertTextFieldVertically(str, width);
        }
    }

    private string insertTextFieldVertically(string str, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        currentPosition.height=initialHeight;
        str = EditorGUI.TextField(currentPosition, str);
        addVerticalMargin();
        currentPosition.width = initWidth;
        currentPosition.y+=initialHeight;
        return str;
    }

    private string insertTextFieldHorizontally(string str, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        addHorizontalMargins();
        str = EditorGUI.TextField(currentPosition, str);
        removeHorizontalMargins();
        currentPosition.x+=currentPosition.width;
        currentPosition.width = initWidth;
        return str;
    }

    public bool insertRadio(bool boolean) {
        int width = (int) currentPosition.width;
        if (widthToDefine) { width = 20; }
        return insertRadio(boolean, width);
    }

    public bool insertRadio(bool boolean, float widthPart) {
        int width = (int) ((float) initialWidth*widthPart);
        return insertRadio(boolean, width);
    }

    public bool insertRadio(bool boolean, int width) {
        if (horizontal) {
            return insertRadioHorizontally(boolean, width);
        } else {
            return insertRadioVertically(boolean, width);
        }
    }
    

    private bool insertRadioVertically(bool boolean, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        currentPosition.height = initialHeight;
        currentPosition.y += 3;
        boolean = EditorGUI.Toggle(currentPosition, boolean, EditorStyles.radioButton);
        currentPosition.y -= 3;
        addVerticalMargin();
        currentPosition.width = initWidth;
        currentPosition.y+=initialHeight;
        return boolean;
    }

    private bool insertRadioHorizontally(bool boolean, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        addHorizontalMargins();
        currentPosition.y += 3;
        boolean = EditorGUI.Toggle(currentPosition, boolean, EditorStyles.radioButton);
        currentPosition.y -= 3;
        removeHorizontalMargins();
        currentPosition.x+=currentPosition.width;
        currentPosition.width = initWidth;
        return boolean;
    }
    
    public bool insertToggle(bool boolean) {
        int width = (int) currentPosition.width;
        if (widthToDefine) { width = 20; }
        return insertToggle(boolean, width);
    }

    public bool insertToggle(bool boolean, float widthPart) {
        int width = (int) ((float) initialWidth*widthPart);
        return insertToggle(boolean, width);
    }

    public bool insertToggle(bool boolean, int width) {
        if (horizontal) {
            return insertToggleHorizontally(boolean, width);
        } else {
            return insertToggleVertically(boolean, width);
        }
    }

    private bool insertToggleVertically(bool boolean, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        currentPosition.height=initialHeight;
        boolean = EditorGUI.Toggle(currentPosition, boolean);
        addVerticalMargin();
        currentPosition.width = initWidth;
        currentPosition.y+=initialHeight;
        return boolean;
    }

    private bool insertToggleHorizontally(bool boolean, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        addHorizontalMargins();
        boolean = EditorGUI.Toggle(currentPosition, boolean);
        removeHorizontalMargins();
        currentPosition.x+=currentPosition.width;
        currentPosition.width = initWidth;
        return boolean;
    }

    public void insertNone(float integer) {
        if (horizontal) {
            currentPosition.x += (int) ((float) initialWidth*integer);
        } else {
            currentPosition.y += (int) ((float) initialHeight*integer);
        }
    }

    public void insertFields(params SerializedProperty[] props) {
        foreach (SerializedProperty prop in props) {
            insertField(prop);
        }
    }

    public void insertField(SerializedProperty prop) {
        int width = (int) currentPosition.width;
        if (widthToDefine) { width=initialWidth/3; }
        insertField(prop, width);
    }

    public void insertField(SerializedProperty prop, float widthPart) {
        int width = (int) ((float) initialWidth*widthPart);
        insertField(prop, width);
    }

    public void insertField(SerializedProperty prop, int width) {
        if (horizontal) {
            insertFieldHorizontally(prop, width);
        } else {
            insertFieldVertically(prop, width);
        }
    }

    private void insertFieldVertically(SerializedProperty prop, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        int propHeight = (int) EditorGUI.GetPropertyHeight(prop);
        currentPosition.height=propHeight;
        EditorGUI.PropertyField(currentPosition, prop, GUIContent.none, false);
        addVerticalMargin();
        currentPosition.width = initWidth;
        currentPosition.y+=propHeight;
    }

    private void insertFieldHorizontally(SerializedProperty prop, int width) {
        int initWidth = (int) currentPosition.width;
        currentPosition.width = width;
        addHorizontalMargins();
        EditorGUI.PropertyField(currentPosition, prop, GUIContent.none, false);
        removeHorizontalMargins();
        currentPosition.x+=currentPosition.width;
        currentPosition.width = initWidth;
    }

    public void insertFieldsHorizontally(params SerializedProperty[] props) {
        beginHorizontal(props);
        insertFields(props);
        endHorizontal();
    }

    public void beginHorizontal(params SerializedProperty[] props) {
        horizontal = true;
        
        initialHeight = (int) currentPosition.height;
        initialWidth = (int) currentPosition.width;
        initialX = (int) currentPosition.x;
        maxHeight = this.maxHeightInProps(props);

        currentPosition.height = maxHeight;
        currentPosition.width = initialWidth/props.Length;
        widthToDefine = false;
    }

    bool widthToDefine = false;

    public void initialize() {
        initialHeight = (int) currentPosition.height;
        initialWidth = (int) currentPosition.width;
        initialX = (int) currentPosition.x;
        initialY = (int) currentPosition.y;
    }

    public void beginHorizontal() {
        horizontal = true;
        
        maxHeight = 20;
        currentPosition.height = maxHeight;
        widthToDefine = true;
    }

    public void beginHorizontal(int mHeight) {
        horizontal = true;
        
        maxHeight = mHeight;
        currentPosition.height = maxHeight;
        widthToDefine = true;
    }

    public void beginHorizontal(int mHeight, int width) {
        horizontal = true;
        
        maxHeight = mHeight;
        currentPosition.height = maxHeight;
        currentPosition.width = width;
        widthToDefine = false;
    }
    
    public void endHorizontal() {
        horizontal = false;
        widthToDefine = false;
        
        currentPosition.x = initialX;
        currentPosition.height = initialHeight;
        currentPosition.width = initialWidth;
        currentPosition.y += maxHeight;
        addVerticalMargin();
    }

    public void addHorizontalMargins() {
        currentPosition.x += marginX;
        currentPosition.width -= marginX*2;
    }

    public void removeHorizontalMargins() {
        currentPosition.x -= marginX;
        currentPosition.width += marginX*2;
    }

    public void addVerticalMargin() {
        currentPosition.y += marginY;
    }
}
#endif