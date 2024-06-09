namespace ElevatorChallengeApp
{
    public class HighSpeedElevator : IElevator
    {
        // The special "cloud" floor
        public int? CloudFloor { get; set; }  

        public int CurrentFloor { get; set; }
        public Direction Direction { get; set; }
        public List<int> Passengers { get; set; }
        public Dictionary<int, int> PassengerDestinations { get; set; }
        public int MaxCapacity { get; set; }
        public int ElevatorNumber { get; set; }
        public DateTime TimeOfLastDropOff { get; }

        public void AddNewPassengers(int destinationFloor, int numPassengers)
        {
            throw new NotImplementedException();
        }

        public void MoveToFloor(int floor)
        {
            // If the elevator is going to the cloud floor or is currently at the cloud floor,
            // it can only go directly to the ground floor or from the ground floor to the cloud floor.
            if (CloudFloor.HasValue && (floor == CloudFloor.Value || CurrentFloor == CloudFloor.Value))
            {
                if (floor != 0 && CurrentFloor != 0)
                {
                    Console.WriteLine($"High-speed elevator {ElevatorNumber} can only go directly to the ground floor from the cloud floor.");
                    return;
                }
            }
        }

        public void DisplayStatus()
        {
            throw new NotImplementedException();
        }
    }
}