﻿using System;
using System.Collections.Generic;

namespace GenericValueEditor
{
    /// <summary>
    /// TODO: Better name for this class.
    /// Creates controls for editing an object.
    /// </summary>
    public class ObjectEditor
    {
        private Dictionary<string, EditorValue> valueByName = new Dictionary<string, EditorValue>();

        /// <summary>
        /// Creates a new editor object to edit the properties of <paramref name="objectToEdit"/>.
        /// </summary>
        /// <param name="objectToEdit"></param>
        public ObjectEditor(object objectToEdit)
        {
            InitializeProperties(objectToEdit);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parent">The control to which the editor controls will be added</param>
        public void AddEditorControls(System.Windows.Forms.Control parent)
        {
            foreach (var editorValue in valueByName)
            {
                ControlCreation.ValueControlCreation.AddPropertyControls(editorValue.Key, editorValue.Value.Type, parent, valueByName);
            }
        }

        private void InitializeProperties(object objectToEdit)
        {
            // Determine what types to use based on reflection and attributes for the given class.
            Type type = objectToEdit.GetType();
            foreach (var property in type.GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (!(attribute is EditorInfo))
                        continue;

                    EditorInfo info = (EditorInfo)attribute;

                    var editorValue = new EditorValue(property.GetValue(objectToEdit, null), info.Type);
                    valueByName.Add(info.Name, editorValue);

                    SetUpObjectUpdateOnValueChange(objectToEdit, property, editorValue);
                }
            }
        }

        private static void SetUpObjectUpdateOnValueChange(object objectToEdit, System.Reflection.PropertyInfo property, EditorValue editorValue)
        {
            // Modify the object's property value.
            editorValue.OnValueChanged += (sender, args) =>
            {
                property.SetValue(objectToEdit, Convert.ChangeType(editorValue.Value, property.PropertyType), null);
            };
        }
    }
}
