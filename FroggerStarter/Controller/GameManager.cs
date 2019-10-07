using System;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FroggerStarter.Model;

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
        private const int TopLaneOffset = 50;

        private readonly double backgroundHeight;
        private readonly double backgroundWidth;
        private Canvas gameCanvas;
        private Frog player;
        private LaneManager laneManager;
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
        public GameManager(double backgroundHeight, double backgroundWidth)
        {
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
            this.vehicleSpeedTimer.Tick += this.speedTimerOnTick;
            this.vehicleSpeedTimer.Interval = new TimeSpan(0, 0, 0, 10, 0);
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
            this.laneManager = new LaneManager();

            //TODO do something about the hard coded 305, 255, etc.
            var lane1 = new Lane(305, this.backgroundWidth, 1, LaneDirection.Left);
            var lane2 = new Lane(255, this.backgroundWidth, 2, LaneDirection.Right);
            var lane3 = new Lane(205, this.backgroundWidth, 3, LaneDirection.Left);
            var lane4 = new Lane(155, this.backgroundWidth, 4, LaneDirection.Left);
            var lane5 = new Lane(105, this.backgroundWidth, 5, LaneDirection.Right);

            this.laneManager.Add(lane1);
            this.laneManager.Add(lane2);
            this.laneManager.Add(lane3);
            this.laneManager.Add(lane4);
            this.laneManager.Add(lane5);

            var vehicle1Lane1 = new Vehicle(VehicleType.Car);
            var vehicle2Lane1 = new Vehicle(VehicleType.Car);
            lane1.Add(vehicle1Lane1);
            lane1.Add(vehicle2Lane1);
            this.gameCanvas.Children.Add(vehicle1Lane1.Sprite);
            this.gameCanvas.Children.Add(vehicle2Lane1.Sprite);

            var semitruck1Lane2 = new Vehicle(VehicleType.SemiTruck);
            var semitruck2Lane2 = new Vehicle(VehicleType.SemiTruck);
            var semitruck3Lane2 = new Vehicle(VehicleType.SemiTruck); 
            lane2.Add(semitruck1Lane2);
            lane2.Add(semitruck2Lane2);
            lane2.Add(semitruck3Lane2);
            this.gameCanvas.Children.Add(semitruck1Lane2.Sprite);
            this.gameCanvas.Children.Add(semitruck2Lane2.Sprite);
            this.gameCanvas.Children.Add(semitruck3Lane2.Sprite);

            var vehicle1Lane3 = new Vehicle(VehicleType.Car);
            var vehicle2Lane3 = new Vehicle(VehicleType.Car);
            var vehicle3Lane3 = new Vehicle(VehicleType.Car);
            lane3.Add(vehicle1Lane3);
            lane3.Add(vehicle2Lane3);
            lane3.Add(vehicle3Lane3);
            this.gameCanvas.Children.Add(vehicle1Lane3.Sprite);
            this.gameCanvas.Children.Add(vehicle2Lane3.Sprite);
            this.gameCanvas.Children.Add(vehicle3Lane3.Sprite);

            var semitruck1Lane4 = new Vehicle(VehicleType.SemiTruck);
            var semitruck2Lane4 = new Vehicle(VehicleType.SemiTruck);
            lane4.Add(semitruck1Lane4);
            lane4.Add(semitruck2Lane4);
            this.gameCanvas.Children.Add(semitruck1Lane4.Sprite);
            this.gameCanvas.Children.Add(semitruck2Lane4.Sprite);

            var vehicle1Lane5 = new Vehicle(VehicleType.Car);
            var vehicle2Lane5 = new Vehicle(VehicleType.Car);
            var vehicle3Lane5 = new Vehicle(VehicleType.Car);
            lane5.Add(vehicle1Lane5);
            lane5.Add(vehicle2Lane5);
            lane5.Add(vehicle3Lane5);
            this.gameCanvas.Children.Add(vehicle1Lane5.Sprite);
            this.gameCanvas.Children.Add(vehicle2Lane5.Sprite);
            this.gameCanvas.Children.Add(vehicle3Lane5.Sprite);

            //TODO had to cast int here, maybe not best route
            /*foreach (Lane currentLane in this.laneManager)
            {
                foreach (Vehicle currentVehicle in currentLane)
                {
                    this.gameCanvas.Children.Add(currentVehicle.Sprite);
                }
            }*/
        }

        private void setPlayerToCenterOfBottomLane()
        {
            this.player.X = this.backgroundWidth / 2 - this.player.Width / 2;
            this.player.Y = this.backgroundHeight - this.player.Height - BottomLaneOffset;
        }

        private void timerOnTick(object sender, object e)
        {
            // TODO Update game state, e.g., move Vehicles, check for collision, etc.
            this.laneManager.MoveAllVehicles();

        }

        private void speedTimerOnTick(object sender, object e)
        {
            this.laneManager.IncreaseSpeedOfVehicles();
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
            this.player.MoveUpWithBoundaryCheck(TopLaneOffset);
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

        #endregion
    }
}