using System;
using System.Windows.Forms;

namespace GenericValueEditor.Utils
{
    /// <summary>
    /// Contains methods for converting between the value of a <see cref="TrackBar"/> and numeric types.
    /// </summary>
    public static class TrackBarUtils
    {
        /// <summary>
        /// Gets the value of <paramref name="trackBar"/> by remapping <see cref="TrackBar.Value"/>
        /// based on the given closed interval.
        /// </summary>
        /// <param name="trackBar">The track bar value to convert</param>
        /// <param name="minimum">The actual value for <see cref="TrackBar.Minimum"/></param>
        /// <param name="maximum">The actual value for <see cref="TrackBar.Maximum"/></param>
        /// <returns>The converted value of <paramref name="trackBar"/> based on the given closed interval</returns>
        public static float GetFloat(TrackBar trackBar, float minimum, float maximum)
        {
            return RemapValue(trackBar.Value, trackBar.Minimum, trackBar.Maximum, minimum, maximum);
        }

        /// <summary>
        /// Sets the value of <paramref name="trackBar"/> by remapping <paramref name="newValue"/> 
        /// based on the given closed interval.
        /// </summary>
        /// <param name="newValue">The new value to set</param>
        /// <param name="trackBar">The track bar whose value will be set</param>
        /// <param name="minimum">The actual value for <see cref="TrackBar.Minimum"/></param>
        /// <param name="maximum">The actual value for <see cref="TrackBar.Maximum"/></param>
        public static void SetFloat(float newValue, TrackBar trackBar, float minimum, float maximum)
        {
            int newSliderValue = (int)(RemapValue(newValue, minimum, maximum, trackBar.Minimum, trackBar.Maximum));
            // Values outside the displayable range of the track bar are set to the track bar's min or max value. 
            // The track bar's maximum only defines the precision. 
            newSliderValue = Math.Min(newSliderValue, trackBar.Maximum);
            newSliderValue = Math.Max(newSliderValue, trackBar.Minimum);
            trackBar.Value = newSliderValue;
        }

        /// <summary>
        /// Gets the value of <paramref name="trackBar"/> by remapping <see cref="TrackBar.Value"/>
        /// based on the given closed interval.
        /// </summary>
        /// <param name="trackBar">The track bar value to convert</param>
        /// <param name="minimum">The actual value for <see cref="TrackBar.Minimum"/></param>
        /// <param name="maximum">The actual value for <see cref="TrackBar.Maximum"/></param>
        /// <returns>The converted value of <paramref name="trackBar"/> based on the given closed interval</returns>
        public static int GetInt(TrackBar trackBar, int minimum, int maximum)
        {
            return RemapValue(trackBar.Value, trackBar.Minimum, trackBar.Maximum, minimum, maximum);
        }

        /// <summary>
        /// Sets the value of <paramref name="trackBar"/> by remapping <paramref name="newValue"/> 
        /// based on the given closed interval.
        /// </summary>
        /// <param name="newValue">The new value to set</param>
        /// <param name="trackBar">The track bar whose value will be set</param>
        /// <param name="minimum">The actual value for <see cref="TrackBar.Minimum"/></param>
        /// <param name="maximum">The actual value for <see cref="TrackBar.Maximum"/></param>
        public static void SetInt(int newValue, TrackBar trackBar, int minimum, int maximum)
        {
            int newSliderValue = RemapValue(newValue, minimum, maximum, trackBar.Minimum, trackBar.Maximum);
            // Values outside the displayable range of the track bar are set to the track bar's min or max value. 
            // The track bar's maximum only defines the precision. 
            newSliderValue = Math.Min(newSliderValue, trackBar.Maximum);
            newSliderValue = Math.Max(newSliderValue, trackBar.Minimum);
            trackBar.Value = newSliderValue;
        }

        private static float RemapValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
        {
            return outputMin + (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin);
        }

        private static int RemapValue(int value, int inputMin, int inputMax, int outputMin, int outputMax)
        {
            return outputMin + (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin);
        }
    }
}
