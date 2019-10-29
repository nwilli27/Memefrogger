using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using FroggerStarter.Enums;
using FroggerStarter.View.Sprites;
using System.Collections;
using FroggerStarter.Factory;

namespace FroggerStarter.Model
{
    /// <summary>
    ///     Class holds the implementation for the Frog dying animation
    /// </summary>
    public class Animation : IEnumerable<Frame>
    {

        public EventHandler<AnimationIsFinishedEventArgs> AnimationFinished;

        public bool IsAnimationFinished => this.animationFrames.All(frame => frame.HasBeenPlayed && !frame.IsVisible);

        private DispatcherTimer animateTimer;
        private IList<Frame> animationFrames;

        public Animation(AnimationType animationType)
        {
            this.createFramesFromAnimationSprites(animationType);
        }

        public void SetFrameLocations(double x, double y)
        {
            foreach (var frame in this.animationFrames)
            {
                frame.X = x;
                frame.Y = y;
            }
        }

        public void Start()
        {
            this.animateTimer = new DispatcherTimer();
            this.animateTimer.Tick += this.showNextFrame;
            this.animateTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);

            this.animationFrames[0].IsVisible = true;
            this.animateTimer.Start();
        }

        private void createFramesFromAnimationSprites(AnimationType animationType)
        {
            this.animationFrames = new List<Frame>();
            var listOfSprites = AnimationFactory.CreateAnimationSprites(animationType);

            foreach (var sprite in listOfSprites)
            {
                this.animationFrames.Add(new Frame(sprite));
            }
        }

        private void showNextFrame(object sender, object e)
        {
            var firstVisibleFrame = this.animationFrames.First(frame => frame.IsVisible);
            var firstInvisibleFrame = this.animationFrames.FirstOrDefault(frame => !frame.IsVisible && !frame.HasBeenPlayed);

            if (firstVisibleFrame != null)
            {
                firstVisibleFrame.IsVisible = false;
            }

            if (firstInvisibleFrame != null)
            {
                firstInvisibleFrame.IsVisible = true;
            }

            this.checkIfAnimationIsFinished();
        }

        private void checkIfAnimationIsFinished()
        {
            if (this.IsAnimationFinished)
            {
                this.animationFrames.ToList().ForEach(frame => frame.ResetStatus());
                this.animateTimer.Stop();
                var animationDone = new AnimationIsFinishedEventArgs() { AnimationIsOver = true };
                this.AnimationFinished?.Invoke(this, animationDone);
            }
        }

        public IEnumerator<Frame> GetEnumerator()
        {
            return this.animationFrames.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.animationFrames.GetEnumerator();
        }
    }

    public class AnimationIsFinishedEventArgs : EventArgs
    {
        public bool AnimationIsOver { get; set; }
    }
}
