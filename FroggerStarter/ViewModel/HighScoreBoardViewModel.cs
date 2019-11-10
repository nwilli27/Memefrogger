using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using FroggerStarter.Annotations;
using FroggerStarter.Extensions;
using FroggerStarter.Model.Score;
using FroggerStarter.Serializer;
using FroggerStarter.Utility;

namespace FroggerStarter.ViewModel
{
    /// <summary>
    ///     HighScoreBoard view model.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class HighScoreBoardViewModel : INotifyPropertyChanged
    {
        #region Data members

        private readonly List<HighScore> highScoreList;

        private ObservableCollection<HighScore> highScores;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the return command.
        /// </summary>
        /// <value>
        ///     The return command.
        /// </value>
        public RelayCommand ReturnCommand { get; set; }

        /// <summary>
        ///     Gets or sets the clear scores command.
        /// </summary>
        /// <value>
        ///     The clear scores command.
        /// </value>
        public RelayCommand ClearScoresCommand { get; set; }

        /// <summary>
        ///     Gets or sets the high scores.
        /// </summary>
        /// <value>
        ///     The high scores.
        /// </value>
        public ObservableCollection<HighScore> HighScores
        {
            get => this.highScores;
            set
            {
                this.highScores = value;
                this.OnPropertyChanged();
                this.ClearScoresCommand.OnCanExecuteChanged();
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="HighScoreBoardViewModel" /> class.
        ///     Precondition: none
        ///     Post-condition: none
        /// </summary>
        public HighScoreBoardViewModel()
        {
            this.ReturnCommand = new RelayCommand(this.returnToStart, null);
            this.ClearScoresCommand = new RelayCommand(this.clearScores, this.canClearScores);

            this.highScoreList = Serializer<List<HighScore>>.ReadObjectFromFile("HighScoreBoard");

            this.HighScores = this.highScoreList.ToObservableCollection();
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Occurs when return selected.
        /// </summary>
        public event EventHandler ReturnSelected;

        private void returnToStart(object obj)
        {
            this.ReturnSelected?.Invoke(null, EventArgs.Empty);
        }

        private void clearScores(object obj)
        {
            this.highScoreList.Clear();
            this.HighScores = this.highScoreList.ToObservableCollection();
            Serializer<List<HighScore>>.WriteObjectToFile("HighScoreBoard", new List<HighScore>());
        }

        private bool canClearScores(object obj)
        {
            return this.highScoreList != null && this.highScoreList?.Count != 0;
        }

        /// <summary>
        ///     Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}