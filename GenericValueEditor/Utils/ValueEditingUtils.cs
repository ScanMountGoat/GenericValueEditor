using System;
using System.Collections.Generic;
using System.Reflection;

namespace GenericValueEditor.Utils
{
    /// <summary>
    /// Contains methods for editing member values stored in a <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    internal static class ValueEditingUtils
    {
        /// <summary>
        /// Initializes the values in <paramref name="valueByName"/> with the member values
        /// from <paramref name="objectToEdit"/>.
        /// </summary>
        /// <param name="objectToEdit">The object whose member values will be used</param>
        /// <param name="valueByName">The collection of values to update</param>
        public static void UpdateEditorValues(object objectToEdit, Dictionary<string, EditorValue> valueByName)
        {
            // Allow for calling this method multiple times.
            valueByName.Clear();

            InitializeEditableProperties(objectToEdit, valueByName);
            InitializeEditableFields(objectToEdit, valueByName);
        }

        /// <summary>
        /// Combines values with a common group name into a single list.
        /// </summary>
        /// <param name="valueByName">The unorganized values</param>
        /// <returns>A collection of values organized by group name</returns>
        public static Dictionary<string, List<EditorValue>> GroupValuesByGroupName(Dictionary<string, EditorValue> valueByName)
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

        public static void InitializeEditableProperties(object objectToEdit, Dictionary<string, EditorValue> valueByName)
        {
            foreach (var property in objectToEdit.GetType().GetProperties())
            {
                AddEditorValue(objectToEdit, property, valueByName);
            }
        }

        public static void InitializeEditableFields(object objectToEdit, Dictionary<string, EditorValue> valueByName)
        {
            foreach (var field in objectToEdit.GetType().GetFields())
            {
                AddEditorValue(objectToEdit, field, valueByName);
            }
        }

        public static void AddEditorValue(object objectToEdit, MemberInfo memberInfo, Dictionary<string, EditorValue> valueByName)
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
                    SetInitialValue(objectToEdit, editorValue, name, property, valueByName);
                else if (memberInfo is FieldInfo field)
                    SetInitialValue(objectToEdit, editorValue, name, field, valueByName);
            }
        }

        public static void SetInitialValue(object objectToEdit, EditorValue editorValue, string name, PropertyInfo property,
            Dictionary<string, EditorValue> valueByName)
        {
            editorValue.Value = property.GetValue(objectToEdit, null);
            valueByName.Add(name, editorValue);
            SetUpObjectUpdateOnValueChange(objectToEdit, property, editorValue);
        }

        public static void SetInitialValue(object objectToEdit, EditorValue editorValue, string name, FieldInfo field,
            Dictionary<string, EditorValue> valueByName)
        {
            editorValue.Value = field.GetValue(objectToEdit);
            valueByName.Add(name, editorValue);
            SetUpObjectUpdateOnValueChange(objectToEdit, field, editorValue);
        }

        public static void SetUpObjectUpdateOnValueChange(object objectToEdit, PropertyInfo property, EditorValue editorValue)
        {
            // Modify the object's property value.
            editorValue.OnValueChanged += (sender, args) =>
            {
                property.SetValue(objectToEdit, Convert.ChangeType(editorValue.Value, property.PropertyType), null);
            };
        }

        public static void SetUpObjectUpdateOnValueChange(object objectToEdit, FieldInfo field, EditorValue editorValue)
        {
            // Modify the object's property value.
            editorValue.OnValueChanged += (sender, args) =>
            {
                field.SetValue(objectToEdit, Convert.ChangeType(editorValue.Value, field.FieldType));
            };
        }
    }
}
