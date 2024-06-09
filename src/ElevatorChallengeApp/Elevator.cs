namespace ElevatorChallengeApp
{
    public class Elevator : IElevator
    {
        #region Properties

        public virtual int CurrentFloor { get; set; }
        public Direction Direction { get; set; }
        public virtual  List<int> Passengers { get; set; }
        public Dictionary<int, int> PassengerDestinations { get; set; }
        public int MaxCapacity { get; set; }
        public int ElevatorNumber { get; set; }
        public DateTime TimeOfLastDropOff { get; private set; }

        #endregion Properties

        #region Constructor

        public Elevator(int maxCapacity, int elevatorNumber)
        {
            CurrentFloor = 0;
            Direction = Direction.Stationary;
            Passengers = new List<int>();
            PassengerDestinations = new Dictionary<int, int>();
            MaxCapacity = maxCapacity;
            ElevatorNumber = elevatorNumber;
            TimeOfLastDropOff = DateTime.Now;
        }

        #endregion Constructor

        #region Public Methods

        public virtual void AddNewPassengers(int destinationFloor, int numPassengers)
        {
            if (Passengers.Count + numPassengers > MaxCapacity)
            {
                Console.WriteLine($"Elevator {ElevatorNumber}: is full or will be overloaded. Can't add more passengers.");
                return;
            }

            for (int i = 0; i < numPassengers; i++)
            {
                var passenger = Passengers.Count + 1;
                Passengers.Add(passenger);
                PassengerDestinations.Add(passenger, destinationFloor);
            }
            Console.WriteLine($"Elevator {ElevatorNumber}: {numPassengers} passengers added. Current passengers: {Passengers.Count}. Destination Floor: {destinationFloor}");
            MoveToFloor(destinationFloor);
        }

        public virtual void MoveToFloor(int floor)
        {
            if (CurrentFloor == floor)
            {
                Console.WriteLine($"Elevator {ElevatorNumber}: is already at floor {floor}. Current direction: {Direction}. Current passengers: {Passengers.Count}");
                return;
            }

            if (Passengers.Count <= MaxCapacity)
            {
                Direction = floor > CurrentFloor ? Direction.Up : Direction.Down;
                Console.WriteLine($"Elevator {ElevatorNumber}: moving to floor {floor}. Current direction: {Direction}. Current passengers: {Passengers.Count}");

                CurrentFloor = floor;
                Direction = Direction.Stationary;
                Console.WriteLine($"Elevator {ElevatorNumber}: moved to floor {CurrentFloor}. Current direction: {Direction}. Current passengers: {Passengers.Count}");
                RemovePassengersAtDestination(floor);
            }
            else
            {
                Console.WriteLine($"Elevator {ElevatorNumber}: is overloaded. Can't move to the specified floor.");
            }
        }

        public virtual void DisplayStatus()
        {
            Console.WriteLine($"Elevator {ElevatorNumber}: at floor {CurrentFloor}. Current direction: {Direction}. Current passengers: {Passengers.Count}");
        }

        #endregion Public Methods

        #region Private Methods

        private void RemovePassengersAtDestination(int floor)
        {
            var passengersAtDestination = PassengerDestinations.Where(p => p.Value == floor).ToList();

            foreach (var passenger in passengersAtDestination)
            {
                Passengers.Remove(passenger.Key);
                PassengerDestinations.Remove(passenger.Key);
            }

            Console.WriteLine($"Elevator {ElevatorNumber}: {passengersAtDestination.Count} passengers removed at floor {floor}. Current passengers: {Passengers.Count}");
        }

        #endregion Private Methods
    }
}
