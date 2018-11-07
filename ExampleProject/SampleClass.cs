using GenericValueEditor;

namespace ExampleProject
{
    class SampleClass
    {
        public enum SampleEnum
        {
            A,
            B,
            C,
            D
        }

        // A value that shouldn't be edited.
        public int X { get; set; } = 5;

        [EditorInfo("Enable Experimental Feature", ValueEnums.ValueType.Bool)]
        public bool EnableFeature { get; set; }

        [EditorInfo("Name", ValueEnums.ValueType.String)]
        public string TextValue { get; set; } = "sample text";

        [EditorInfo("Index", ValueEnums.ValueType.Int)]
        [TrackBarInfo(-128, 128)]
        public int IntValue { get; set; } = -1;

        [EditorInfo("Flags", ValueEnums.ValueType.UintFlag)]
        public uint UintValue { get; set; } = 2;

        [EditorInfo("X", ValueEnums.ValueType.Float, groupName: "Position")]
        [TrackBarInfo(-2, 1.5)]
        public float FloatValue1 { get; set; } = 3.5f;

        [EditorInfo("Y", ValueEnums.ValueType.Float, groupName: "Position")]
        public float FloatValue2 { get; set; } = 0.5f;

        [EditorInfo("Z", ValueEnums.ValueType.Double, groupName: "Position")]
        [TrackBarInfo(0, 1)]
        public double DoubleValue { get; set; } = 0.5;

        [EditorInfo("Type", ValueEnums.ValueType.Enum, groupName: "Misc")]
        public SampleEnum enumValue = SampleEnum.B;

        public override string ToString()
        {
            string result = "";
            foreach (var property in typeof(SampleClass).GetProperties())
            {
                result += $"{property.Name}: {property.GetValue(this, null).ToString()} | ";
            }
            return result;
        }
    }
}
