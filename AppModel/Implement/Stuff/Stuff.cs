using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AppModel.IF.Stuff;

namespace AppModel.Implement.Stuff
{
    /// <summary>モノ(抽象クラス)</summary>
    internal abstract class Stuff : IStuff
    {
        protected Stuff(int id, StuffState state)
        {
            Id = id;
            State = state;
        }

        /// <summary>モノID</summary>
        public int Id { get; }

        /// <summary>モノの状態</summary>
        public StuffState State
        {
            get => _state;
            set
            {
                _state = value;
                RaisePropertyChanged();
            }
        }
        private StuffState _state;

        /// <summary>指定したモノに対する距離を取得</summary>
        public abstract double GetDistance(IStuff targetStuff);

        /// <summary>接続判定</summary>
        public abstract bool Joint(IList<IStuff> stuffList);

        /// <summary>障害物判定</summary>
        public abstract bool Obstacle(IList<IStuff> stuffList);

        /// <summary>生成動作</summary>
        public abstract void Generating();

        #region INotifyPropertyChanged実装
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
