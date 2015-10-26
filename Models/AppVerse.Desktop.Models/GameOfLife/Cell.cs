using Microsoft.Practices.Prism.Mvvm;

namespace AppVerse.Desktop.Models.GameOfLife
{
    public class Cell : BindableBase
    {
        #region Private members

        private LifeState _cellState;

        #endregion


        #region Properties

        public LifeState State
        {
            get
            {

                return _cellState;
            }
            set
            {
                SetProperty(ref this._cellState, value);


            }
        }

        public Coordinates CellCordinate { get; set; }

        public void InvertLifeState()
        {
            switch (State)
            {
                case LifeState.Alive:
                    State=LifeState.Dead;
                    break;

                case LifeState.Dead:
                    State = LifeState.Alive;

                    break;

            }
        }

        #endregion
    }
}
