using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FroggerStarter.Model
{
    class CollisionDetection
    {
        public static bool HasPlayerCollidedWithVehicle(GameObject player, GameObject vehicle)
        {
            return hasPlayerIntersectedVehicleXBoundary(player, vehicle) && 
                   isPlayerOnSameLaneAsVehicle(player, vehicle);
        }

        private static bool hasPlayerIntersectedVehicleXBoundary(GameObject player, GameObject vehicle)
        {
            var playerRightSide = player.X + player.Width;
            var vehicleRightSide = vehicle.X + vehicle.Width;

            var doesPlayerIntersectVehicleRightSide = playerRightSide > vehicle.X && playerRightSide < vehicleRightSide;
            var doesPlayerIntersectVehicleLeftSide = player.X > vehicle.X && player.X < vehicleRightSide;

            return doesPlayerIntersectVehicleRightSide || doesPlayerIntersectVehicleLeftSide;
        }

        private static bool isPlayerOnSameLaneAsVehicle(GameObject player, GameObject vehicle)
        {
            var playerBottomSide = player.Y + player.Height;
            var vehicleBottomSide = vehicle.Y + vehicle.Height;

            return player.Y <= vehicle.Y && playerBottomSide >= vehicleBottomSide;
        }
    }
}
