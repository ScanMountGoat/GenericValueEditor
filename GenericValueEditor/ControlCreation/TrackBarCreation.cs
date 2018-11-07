using GenericValueEditor.Utils;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GenericValueEditor.ControlCreation
{
    internal static class TrackBarCreation
    {
        public static TrackBar AddTrackBar(TableLayoutPanel tableLayout, ValueEnums.ValueType type, int row, int col,
            string name, Dictionary<string, EditorValue> valueByName)
        {
            var trackBar = new TrackBar()
            {
                TickStyle = TickStyle.None,
                Anchor = AnchorStyles.Right | AnchorStyles.Left,
                Maximum = 100,
            };

            tableLayout.Controls.Add(trackBar, col, row);

            CreateScrollEvent(type, name, trackBar, valueByName);
            return trackBar;
        }

        private static void CreateScrollEvent(ValueEnums.ValueType type, string name, TrackBar control, Dictionary<string, EditorValue> valueByName)
        {
            var editorValue = valueByName[name];
            var min = editorValue.TrackBarInfo.Min;
            var max = editorValue.TrackBarInfo.Max;

            switch (type)
            {
                case ValueEnums.ValueType.Float:
                    // Set initial value.
                    TrackBarUtils.SetFloat((float)editorValue.Value, control, (float)min, (float)max);

                    control.Scroll += (sender, args) =>
                    {
                        editorValue.EnableTrackBarUpdates = false;
                        float newValue = TrackBarUtils.GetFloat(control, (float)min, (float)max);
                        editorValue.Value = newValue;
                    };
                    break;
                case ValueEnums.ValueType.Double:
                    // Set initial value.
                    TrackBarUtils.SetDouble((double)editorValue.Value, control, (double)min, (double)max);

                    control.Scroll += (sender, args) =>
                    {
                        editorValue.EnableTrackBarUpdates = false;
                        double newValue = TrackBarUtils.GetDouble(control, min, max);
                        editorValue.Value = newValue;
                    };
                    break;
                case ValueEnums.ValueType.Int:
                    // Set initial value.
                    TrackBarUtils.SetInt((int)editorValue.Value, control, (int)min, (int)max);

                    control.Scroll += (sender, args) =>
                    {
                        editorValue.EnableTrackBarUpdates = false;
                        int newIntValue = TrackBarUtils.GetInt(control, (int)min, (int)max);
                        editorValue.Value = newIntValue;
                    };
                    break;
                default:
                    break;
            }

            control.Leave += (sender, args) => { valueByName[name].EnableTrackBarUpdates = true; };
        }

    }
}
