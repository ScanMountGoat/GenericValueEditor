using GenericValueEditor;

namespace ExampleProject
{
    class SampleClass
    {
        public int X { get; } = 5;

        [EditorInfo("int1", ValueEnums.ValueType.Int)]
        public int IntValue { get; set; } = -1;

        [EditorInfo("unsigned flag", ValueEnums.ValueType.UintFlag)]
        public uint UintValue { get; set; } = 2;

        [EditorInfo("float", ValueEnums.ValueType.Float)]
        public float FloatValue { get; set; } = 3.5f;

        [EditorInfo("string1", ValueEnums.ValueType.String)]
        public string TextValue { get; set; } = "sample text";

        public override string ToString()
        {
            return $"{X.ToString()} {IntValue.ToString()} {UintValue.ToString()} {FloatValue.ToString()} {TextValue}";
        }
    }
}
