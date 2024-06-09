namespace ElevatorChallengeApp
{
    public interface IElevator
    {
        int CurrentFloor { get; set; }
        Direction Direction { get; set; }
        List<int> Passengers { get; set; }
        Dictionary<int, int> PassengerDestinations { get; set; }
        int MaxCapacity { get; set; }
        int ElevatorNumber { get; set; }
        DateTime TimeOfLastDropOff { get; }

        void AddNewPassengers(int destinationFloor, int numPassengers);
        void MoveToFloor(int floor);
        void DisplayStatus();
    }
}
