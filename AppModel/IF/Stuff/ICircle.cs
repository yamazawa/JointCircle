using System.Windows;

namespace AppModel.Stuff.IF
{
    /// <summary>円(モノ)</summary>
    public interface ICircle : IStuff
    {
        Point CenterPoint { get; set; }

        double Radious { get; set; }
    }
}
