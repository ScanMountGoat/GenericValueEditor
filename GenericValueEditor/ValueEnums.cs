namespace GenericValueEditor
{
    /// <summary>
    /// Contains enums for setting the display and editor settings for values.
    /// </summary>
    public static class ValueEnums
    {
        /// <summary>
        /// Determines what controls will be used to display and edit the value. This should match the value's type. 
        /// </summary>
        public enum ValueType
        {
            /// <summary>
            /// A <see cref="uint"/> value. Creates a label and text box.
            /// </summary>
            UintFlag,

            /// <summary>
            /// A <see cref="float"/> value. Creates a label, text box, and slider.
            /// </summary>
            Float,

            /// <summary>
            /// An <see cref="int"/> value. Creates a label, text box, and slider.
            /// </summary>
            Int,
            /// <summary>
            /// A <see cref="bool"/> value. Creates a check box.
            /// </summary>
            Bool,
            /// <summary>
            /// A <see cref="System.Enum"/> value. Creates a combo box for all possible enumerations.
            /// </summary>
            Enum,
            /// <summary>
            /// A <see cref="string"/> value. Creates a label and text box.
            /// </summary>
            String
        }
    }
}
