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
            /// A <see cref="uint"/> value. 
            /// </summary>
            UintFlag,

            /// <summary>
            /// A <see cref="float"/> value. 
            /// </summary>
            Float,

            /// <summary>
            /// A <see cref="double"/> value.
            /// </summary>
            Double,

            /// <summary>
            /// An <see cref="int"/> value. 
            /// </summary>
            Int,

            /// <summary>
            /// A <see cref="bool"/> value.
            /// </summary>
            Bool,

            /// <summary>
            /// A <see cref="System.Enum"/> value.
            /// </summary>
            Enum,

            /// <summary>
            /// A <see cref="string"/> value.
            /// </summary>
            String
        }
    }
}
