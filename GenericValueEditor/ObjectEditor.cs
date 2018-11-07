using System;
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
            InitializeEditableProperties(objectToEdit);
            InitializeEditableFields(objectToEdit);
        }

        /// <summary>
        /// Adds controls to <paramref name="parent"/> to edit all the marked properties and fields of the given object.
        /// </summary>
        /// <param name="parent">The control to which the editor controls will be added</param>
        public void AddEditorControls(System.Windows.Forms.Control parent)
        {
            Dictionary<string, List<EditorValue>> editorValuesByGroup = GroupValuesByGroupName();

            foreach (var pair in editorValuesByGroup)
            {
                AddGroupSpacer(parent, pair.Key);

                foreach (var editorValue in pair.Value)
                {
                    ControlCreation.ValueControlCreation.AddPropertyControls(editorValue.EditorInfo.Name, editorValue.EditorInfo.Type, parent, valueByName);
                }
            }
        }

        private static void AddGroupSpacer(System.Windows.Forms.Control parent, string name)
        {
            if (string.IsNullOrEmpty(name))
                return;

            var spacerButton = new System.Windows.Forms.Button()
            {
                Text = name,
                Width = parent.Width
            };
            parent.Controls.Add(spacerButton);
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
            // Determine what types to use based on reflection and attributes for the given class.
            Type type = objectToEdit.GetType();
            foreach (var property in type.GetProperties())
            {
                // TODO: Account for multiple attributes of the same type.
                var editorValue = new EditorValue();
                string name = null;
                foreach (var attribute in property.GetCustomAttributes(true))
                {
                    if (attribute is EditorInfo)
                    {
                        var info = (EditorInfo)attribute;
                        name = info.Name;
                        editorValue.EditorInfo = info;
                    }
                    else if (attribute is TrackBarInfo)
                    {
                        editorValue.TrackBarInfo = (TrackBarInfo)attribute;
                    }
                }

                // Ignore properties with no attributes.
                if (name != null)
                {
                    // Initialize the value.
                    editorValue.Value = property.GetValue(objectToEdit, null);

                    valueByName.Add(name, editorValue);

                    SetUpObjectUpdateOnValueChange(objectToEdit, property, editorValue);
                }
            }
        }

        private void InitializeEditableFields(object objectToEdit)
        {
            // Determine what types to use based on reflection and attributes for the given class.
            Type type = objectToEdit.GetType();
            foreach (var field in type.GetFields())
            {
                // TODO: Account for multiple attributes of the same type.
                var editorValue = new EditorValue();
                string name = null;
                foreach (var attribute in field.GetCustomAttributes(true))
                {
                    if (attribute is EditorInfo)
                    {
                        var info = (EditorInfo)attribute;
                        name = info.Name;
                        editorValue.EditorInfo = info;
                    }
                    else if (attribute is TrackBarInfo)
                    {
                        editorValue.TrackBarInfo = (TrackBarInfo)attribute;
                    }
                }

                // Ignore fields with no attributes.
                if (name != null)
                {
                    // Initialize the value.
                    editorValue.Value = field.GetValue(objectToEdit);

                    valueByName.Add(name, editorValue);

                    SetUpObjectUpdateOnValueChange(objectToEdit, field, editorValue);
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
