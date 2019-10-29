using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Holds the implementation for a single frame.
    /// </summary>
    /// <seealso cref="FroggerStarter.Model.GameObject" />
    public class Frame : GameObject
    {

        private bool isVisible;

        public bool IsVisible
        {
            get => this.isVisible;
            set
            {
                if (value)
                {
                    this.HasBeenPlayed = true;
                }
                this.isVisible = value;
                this.changeSpriteVisibility();
            }
        }

        public bool HasBeenPlayed { get; private set; } = false;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Frame"/> class.
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        public Frame(BaseSprite sprite)
        {
            this.Sprite = sprite;
            this.IsVisible = false;
        }

        public void ResetStatus()
        {
            this.isVisible = false;
            this.HasBeenPlayed = false;
        }

        //TODO maybe move this to gameobject? Same implementation in home
        public void changeSpriteVisibility()
        {
            this.Sprite.Visibility = this.isVisible ? Visibility.Visible : Visibility.Collapsed;
        }

    }
}
