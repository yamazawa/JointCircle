using AppModel.IF.Singleton;
using AppModel.Implement.Singleton;

namespace AppModel
{
    public static class SingletonAccessor
    {
        public static IGame GetGame()
        {
            return new Game();
        }
    }
}
