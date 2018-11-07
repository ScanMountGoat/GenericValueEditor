namespace GenericValueEditor
{
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

        public EditorInfo EditorInfo { get; set; } = null;

        public TrackBarInfo TrackBarInfo { get; set; } = null;

        // Prevents the text box from updating the track bar.
        public bool EnableTrackBarUpdates { get; set; } = false;

        public override string ToString()
        {
            return valueObject.ToString();
        }
    }
}
