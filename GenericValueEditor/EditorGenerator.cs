﻿using System;
using System.Collections.Generic;
using System.Reflection;

namespace GenericValueEditor
{
    /// <summary>
    /// Contains methods to create controls for editing an object's member variables.
    /// </summary>
    /// <typeparam name="T">The reference type of the editable object. The member attributes 
    /// determine what editor controls are used.</typeparam>
    public class EditorGenerator<T> where T : class
    {
        private Dictionary<string, EditorValue> valueByName = new Dictionary<string, EditorValue>();

        /// <summary>
        /// The object whose members will be edited by the controls
        /// generated by <see cref="AddEditorControls(System.Windows.Forms.Control)"/>.
        /// </summary>
        public T ObjectToEdit
        {
            get { return objectToEdit; }
            set
            {
                objectToEdit = value;
                UpdateEditorValues(objectToEdit);
            }
        }
        private T objectToEdit = null;

        /// <summary>
        /// Creates a generator to edit the members of <paramref name="objectToEdit"/>.
        /// </summary>
        /// <param name="objectToEdit">The object used to generate the editor controls</param>
        public EditorGenerator(T objectToEdit)
        {
            ObjectToEdit = objectToEdit;
        }

        /// <summary>
        /// Adds editor controls to <paramref name="parent"/>. A <see cref="System.Windows.Forms.FlowLayoutPanel"/>
        /// is recommended.
        /// </summary>
        /// <param name="parent">The control to which the editor controls will be added</param>
        public void AddEditorControls(System.Windows.Forms.Control parent)
        {
            Dictionary<string, List<EditorValue>> editorValuesByGroup = GroupValuesByGroupName();

            foreach (var pair in editorValuesByGroup)
            {
                var spacer = AddGroupSpacer(parent, pair.Key);

                var addedControls = new List<System.Windows.Forms.Control>();
                foreach (var editorValue in pair.Value)
                {
                    addedControls.Add(ControlCreation.ValueControlCreation.AddPropertyControls(editorValue.EditorInfo.Name, editorValue.EditorInfo.Type, parent, valueByName));
                }

                ToggleControlVisiblityOnClick(spacer, addedControls);
            }
        }

        private void UpdateEditorValues(object objectToEdit)
        {
            // Allow for calling this method multiple times.
            valueByName.Clear();

            InitializeEditableProperties(objectToEdit);
            InitializeEditableFields(objectToEdit);
        }

        private static void ToggleControlVisiblityOnClick(System.Windows.Forms.Control spacer, List<System.Windows.Forms.Control> addedControls)
        {
            if (spacer != null)
            {
                spacer.Click += (sender, args) =>
                {
                    foreach (var control in addedControls)
                    {
                        control.Visible = !control.Visible;
                    }
                };
            }
        }

        private static System.Windows.Forms.Control AddGroupSpacer(System.Windows.Forms.Control parent, string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            var spacerButton = new System.Windows.Forms.Button()
            {
                Text = name,
                Width = parent.Width
            };
            parent.Controls.Add(spacerButton);
            return spacerButton;
        }

        private Dictionary<string, List<EditorValue>> GroupValuesByGroupName()
        {
            var editorValuesByGroup = new Dictionary<string, List<EditorValue>>();
            foreach (var pair in valueByName)
            {
                string groupName = pair.Value.EditorInfo.GroupName;

                if (!editorValuesByGroup.ContainsKey(groupName))
                    editorValuesByGroup.Add(groupName, new List<EditorValue>());

                editorValuesByGroup[groupName].Add(pair.Value);
            }

            return editorValuesByGroup;
        }

        private void InitializeEditableProperties(object objectToEdit)
        {
            foreach (var property in objectToEdit.GetType().GetProperties())
            {
                AddEditorValue(objectToEdit, property);
            }
        }

        private void InitializeEditableFields(object objectToEdit)
        {
            foreach (var field in objectToEdit.GetType().GetFields())
            {
                AddEditorValue(objectToEdit, field);
            }
        }

        private void AddEditorValue(object objectToEdit, MemberInfo memberInfo)
        {
            var editorValue = new EditorValue();
            string name = null;
            foreach (var attribute in memberInfo.GetCustomAttributes(true))
            {
                if (attribute is EditInfo info)
                {
                    name = info.Name;
                    editorValue.EditorInfo = info;
                }
                else if (attribute is TrackBarInfo trackBarInfo)
                {
                    editorValue.TrackBarInfo = trackBarInfo;
                }
            }

            // Ignore members with no attributes.
            if (name != null)
            {
                if (memberInfo is PropertyInfo property)
                    SetInitialValue(objectToEdit, editorValue, name, property);
                else if (memberInfo is FieldInfo field)
                    SetInitialValue(objectToEdit, editorValue, name, field);
            }
        }

        private void SetInitialValue(object objectToEdit, EditorValue editorValue, string name, PropertyInfo property)
        {
            editorValue.Value = property.GetValue(objectToEdit, null);
            valueByName.Add(name, editorValue);
            SetUpObjectUpdateOnValueChange(objectToEdit, property, editorValue);
        }

        private void SetInitialValue(object objectToEdit, EditorValue editorValue, string name, FieldInfo field)
        {
            editorValue.Value = field.GetValue(objectToEdit);
            valueByName.Add(name, editorValue);
            SetUpObjectUpdateOnValueChange(objectToEdit, field, editorValue);
        }

        private static void SetUpObjectUpdateOnValueChange(object objectToEdit, System.Reflection.PropertyInfo property, EditorValue editorValue)
        {
            // Modify the object's property value.
            editorValue.OnValueChanged += (sender, args) =>
            {
                property.SetValue(objectToEdit, Convert.ChangeType(editorValue.Value, property.PropertyType), null);
            };
        }

        private static void SetUpObjectUpdateOnValueChange(object objectToEdit, System.Reflection.FieldInfo field, EditorValue editorValue)
        {
            // Modify the object's property value.
            editorValue.OnValueChanged += (sender, args) =>
            {
                field.SetValue(objectToEdit, Convert.ChangeType(editorValue.Value, field.FieldType));
            };
        }
    }
}