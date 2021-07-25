using System.Windows;

namespace AppModel.Stuff.IF
{
    /// <summary>直線(モノ)</summary>
    interface ILine
    {
        /// <summary>点1</summary>
        Point Point1 { get; set; }

        /// <summary>点1</summary>
        Point Point2 { get; set; }
    }
}
