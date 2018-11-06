namespace GenericValueEditor
{
    /// <summary>
    /// Describes how a class property or field should be edited.
    /// </summary>
    public class EditorInfo : System.Attribute
    {
        /// <summary>
        /// The name of the value group. Assigns this value to the default group when null or empty.
        /// </summary>
        public string GroupName { get; }

        /// <summary>
        /// The name to display for the value.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The type of the value, which affects what controls are used.
        /// </summary>
        public ValueEnums.ValueType Type { get; }

        /// <summary>
        /// Creates a new instance with the given settings.
        /// </summary>
        /// <param name="name">The name to display for the value.</param>
        /// <param name="type">The type of the value, which affects what controls are used.</param>
        /// <param name="groupName">The name of the value group. Assigns this value to the default group when null or empty.</param>
        public EditorInfo(string name, ValueEnums.ValueType type, string groupName = "")
        {
            GroupName = groupName;
            Name = name;
            Type = type;
        }
    }
}
