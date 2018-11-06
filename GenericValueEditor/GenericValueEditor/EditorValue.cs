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

        public ValueEnums.ValueType Type { get; }

        public EditorValue(object value, ValueEnums.ValueType type)
        {
            valueObject = value;
            Type = type;
        }

        public override string ToString()
        {
            return valueObject.ToString();
        }
    }
}
