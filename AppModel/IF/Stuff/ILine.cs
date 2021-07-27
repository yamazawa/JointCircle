using AppModel.IF.Pile;
using System.Windows;

namespace AppModel.Stuff.IF
{
    /// <summary>直線(モノ)</summary>
    public interface ILine : IStuff
    {
        /// <summary>杭1</summary>
        IPile Pile1 { get; }

        /// <summary>杭2</summary>
        IPile Pile2 { get; }
    }
}
