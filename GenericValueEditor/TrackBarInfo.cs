using System;
using System.Windows.Forms;

namespace GenericValueEditor
{
    /// <summary>
    /// Describes how a class property or field should be edited using a <see cref="TrackBar"/>.
    /// </summary>
    public class TrackBarInfo : Attribute
    {
        /// <summary>
        /// The actual value for <see cref="TrackBar.Minimum"/>.
        /// </summary>
        public double Min { get; }

        /// <summary>
        /// The actual value for <see cref="TrackBar.Maximum"/>.
        /// </summary>
        public double Max { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="min">The actual value for <see cref="TrackBar.Minimum"/>.</param>
        /// <param name="max">The actual value for <see cref="TrackBar.Maximum"/>.</param>
        public TrackBarInfo(double min, double max)
        {
            Min = min;
            Max = max;
        }
    }
}
