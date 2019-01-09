namespace GenericValueEditor
{
    /// <summary>
    /// Stores the information for how a name/value pair should be edited.
    /// </summary>
    internal class EditorValue
    {
        public delegate void ValueChangedEventHandler(object sender, object value);
        public event ValueChangedEventHandler OnValueChanged;

        public object Value
        {
            get { return valueObject; }
            set
            {
                valueObject = value;
                OnValueChanged?.Invoke(this, value);
            }
        }
        object valueObject = null;

        public EditInfo EditorInfo { get; set; } = null;

        public TrackBarInfo TrackBarInfo { get; set; } = null;
        public bool EnableTrackBarUpdates { get; set; } = false;

        public override string ToString()
        {
            return valueObject.ToString();
        }
    }
}
