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

        public int X { get; } = 5;

        [EditorInfo("int1", ValueEnums.ValueType.Int)]
        [TrackBarInfo(-128, 128)]
        public int IntValue { get; set; } = -1;

        [EditorInfo("unsigned flag", ValueEnums.ValueType.UintFlag)]
        public uint UintValue { get; set; } = 2;

        [EditorInfo("float1", ValueEnums.ValueType.Float)]
        [TrackBarInfo(-2, 1.5)]
        public float FloatValue1 { get; set; } = 3.5f;

        [EditorInfo("float2", ValueEnums.ValueType.Float)]
        public float floatField = 0.5f;

        [EditorInfo("string1", ValueEnums.ValueType.String)]
        public string TextValue { get; set; } = "sample text sample text sample text";

        [EditorInfo("enum1", ValueEnums.ValueType.Enum)]
        public SampleEnum EnumValue { get; set; } = SampleEnum.B;

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
