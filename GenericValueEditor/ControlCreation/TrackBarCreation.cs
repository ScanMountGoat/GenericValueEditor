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
            switch (type)
            {
                case ValueEnums.ValueType.Float:
                    control.Scroll += (sender, args) =>
                    {
                        valueByName[name].EnableTrackBarUpdates = false;
                        float newValue = TrackBarUtils.GetFloat(control, 0, 1);
                        valueByName[name].Value = newValue;
                    };
                    break;
                case ValueEnums.ValueType.Int:
                    control.Scroll += (sender, args) =>
                    {
                        valueByName[name].EnableTrackBarUpdates = false;
                        int newIntValue = TrackBarUtils.GetInt(control, -128, 128);
                        valueByName[name].Value = newIntValue;
                    };
                    break;
                default:
                    break;
            }

            control.Leave += (sender, args) => { valueByName[name].EnableTrackBarUpdates = true; };
        }

    }
}
