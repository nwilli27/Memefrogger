using System;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model;
using System.Collections.Generic;

namespace FroggerStarter.Controller
{
    /// <summary>
    ///     Manages all aspects of the game play including moving the player,
    ///     the Vehicles as well as lives and score.
    /// </summary>
    public class GameManager
    {
        #region Data members

        private const int BottomLaneOffset = 5;

        //TODO rename this to something maybe more descriptive?

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private readonly double highRoadYLocation;

        private Canvas gameCanvas;
        private Frog player;
        private RoadManager roadManager;
        private DispatcherTimer timer;
        private DispatcherTimer vehicleSpeedTimer;

        #endregion

        #region Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="GameManager" /> class.
        /// </summary>
        /// <param name="backgroundHeight">Height of the background.</param>
        /// <param name="backgroundWidth">Width of the background.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     backgroundHeight &lt;= 0
        ///     or
        ///     backgroundWidth &lt;= 0
        /// </exception>
        public GameManager(double backgroundHeight, double backgroundWidth, double highRoadYLocation)
        {
            //TODO precondition check
            if (backgroundHeight <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundHeight));
            }

            if (backgroundWidth <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(backgroundWidth));
            }

            this.backgroundHeight = backgroundHeight;
            this.backgroundWidth = backgroundWidth;
            this.highRoadYLocation = highRoadYLocation;

            this.setupGameTimer();
            this.setupVehicleSpeedTimer();
        }

        #endregion

        #region Methods

        private void setupGameTimer()
        {
            this.timer = new DispatcherTimer();
            this.timer.Tick += this.timerOnTick;
            this.timer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            this.timer.Start();
        }

        private void setupVehicleSpeedTimer()
        {
            this.vehicleSpeedTimer = new DispatcherTimer();
            this.vehicleSpeedTimer.Tick += this.vehicleSpeedTimerOnTick;
            this.vehicleSpeedTimer.Interval = new TimeSpan(0, 0, 0, 5, 0);
            this.vehicleSpeedTimer.Start();
        }

        /// <summary>
        ///     Initializes the game working with appropriate classes to play frog
        ///     and vehicle on game screen.
        ///     Precondition: background != null
        ///     Post-condition: Game is initialized and ready for play.
        /// </summary>
        /// <param name="gamePage">The game page.</param>
        /// <exception cref="ArgumentNullException">gameCanvas</exception>
        public void InitializeGame(Canvas gamePage)
        {
            this.gameCanvas = gamePage ?? throw new ArgumentNullException(nameof(gamePage));

            this.createAndPlacePlayer();
            this.createAndPlaceVehiclesInLanes();
        }

        private void createAndPlacePlayer()
        {
            this.player = new Frog();
            this.gameCanvas.Children.Add(this.player.Sprite);
            this.setPlayerToCenterOfBottomLane();
        }

        private void createAndPlaceVehiclesInLanes()
        {
            //TODO do something with starting/ending y locations.
            this.roadManager = new RoadManager(this.backgroundWidth, 105, 305, 5);
            
            this.roadManager.AddLaneOfVehicles(LaneDirection.Right, 2.5, VehicleType.Car, 3);
            this.roadManager.AddLaneOfVehicles(LaneDirection.Left, 2.0, VehicleType.SemiTruck, 2);
            this.roadManager.AddLaneOfVehicles(LaneDirection.Left, 1.5, VehicleType.Car, 3);
            this.roadManager.AddLaneOfVehicles(LaneDirection.Right, 1.0, VehicleType.SemiTruck, 3);
            this.roadManager.AddLaneOfVehicles(LaneDirection.Left, 0.5, VehicleType.Car, 2);

            this.placeVehiclesOnCanvas();
        }

        private void placeVehiclesOnCanvas()
        {
            foreach (var currentLane in this.roadManager)
            {
                foreach (Vehicle currentVehicle in currentLane)
                {
                    this.gameCanvas.Children.Add(currentVehicle.Sprite);
                }
            }
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = this.backgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = this.backgroundHeight - this.player.Height - BottomLaneOffset;
        }

        private void timerOnTick(object sender, object e)
        {
            // TODO Update game state, e.g., move Vehicles, check for collision, etc.
            this.roadManager.MoveAllVehicles();
            this.checkForPlayerToVehicleCollision();
        }

        private void vehicleSpeedTimerOnTick(object sender, object e)
        {
            this.roadManager.IncreaseSpeedOfVehicles();
        }

        private void checkForPlayerToVehicleCollision()
        {
            foreach (var currentLane in this.roadManager)
            {
                foreach (Vehicle currentVehicle in currentLane)
                {
                    if (this.player.HasCollidedWith(currentVehicle)) {
                        this.setPlayerToCenterOfBottomLane();
                        this.roadManager.SetAllVehiclesToDefaultSpeed();
                    }
                }
            }
        }

        /// <summary>
        ///     Moves the player to the left.
        ///     Precondition: none
        ///     Post-condition: player.X = player.X@prev - player.Width
        /// </summary>
        public void MovePlayerLeft()
        {
            this.player.MoveLeftWithBoundaryCheck(0);
        }
        
        /// <summary>
        ///     Moves the player to the right.
        ///     Precondition: none
        ///     Post-condition: player.X = player.X@prev + player.Width
        /// </summary>
        public void MovePlayerRight()
        {
            this.player.MoveRightWithBoundaryCheck(this.backgroundWidth);
        }

        /// <summary>
        ///     Moves the player up.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y@prev - player.Height
        /// </summary>
        public void MovePlayerUp()
        {
            if (this.hasPlayerMadeItToTheHighRoad())
            {
                this.setPlayerToCenterOfBottomLane();
            } else
            {
                this.player.MoveUpWithBoundaryCheck(this.highRoadYLocation);
            }
        }

        /// <summary>
        ///     Moves the player down.
        ///     Precondition: none
        ///     Post-condition: player.Y = player.Y@prev + player.Height
        /// </summary>
        public void MovePlayerDown()
        {
            this.player.MoveDownWithBoundaryCheck(this.backgroundHeight - BottomLaneOffset);
        }

        private bool hasPlayerMadeItToTheHighRoad()
        {
            return this.player.Y - this.player.SpeedY <= this.highRoadYLocation;
        }

        #endregion
    }
}