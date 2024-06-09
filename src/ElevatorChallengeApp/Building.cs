namespace ElevatorChallengeApp
{
    public class Building
    {
        #region Properties

        public List<IElevator> Elevators { get; set; }
        public int Floors { get; set; }
        public Dictionary<int, int> PassengerQueue { get; set; }
        public Dictionary<int, int> PassengersOnFloor { get; set; }

        #endregion Properties

        #region Constructor

        public Building(int floors, int elevators, int elevatorCapacity)
        {
            Floors = floors;
            Elevators = new List<IElevator>(Enumerable.Range(0, elevators)
                .Select(i => new Elevator(elevatorCapacity, i + 1)));
            PassengerQueue = new Dictionary<int, int>();
            PassengersOnFloor = new Dictionary<int, int>();
            Console.WriteLine(
                $"A new building has been created with {Floors} floors, {Elevators.Count} elevators, each with a capacity of {elevatorCapacity}.");
        }

        #endregion Constructor

        #region Public Methods

        public void CallElevator(int floor)
        {
            var nearestElevator = Elevators.OrderBy(e => Math.Abs(e.CurrentFloor - floor))
                .FirstOrDefault(e => e.Passengers.Count < e.MaxCapacity);

            if (nearestElevator != null)
            {
                nearestElevator.MoveToFloor(floor);
            }
            else
            {
                Console.WriteLine("No available elevator to service the request.");
            }
        }

        public void AddPassengersToAvailableElevator(int currentFloor, int destinationFloor, int numPassengers)
        {
            PassengerQueue.TryAdd(destinationFloor, 0);

            PassengerQueue[destinationFloor] += numPassengers;

            while (numPassengers > 0)
            {
                var availableElevator = Elevators.OrderBy(e => Math.Abs(e.CurrentFloor - currentFloor))
                    .FirstOrDefault(e => e.Passengers.Count < e.MaxCapacity && e.CurrentFloor != destinationFloor);

                if (availableElevator != null)
                {
                    var passengersToAdd = Math.Min(numPassengers,
                        availableElevator.MaxCapacity - availableElevator.Passengers.Count);
                    // Only print the message if passengers were actually added
                    availableElevator.MoveToFloor(currentFloor); // Move to the current floor to pick up passengers
                    availableElevator.AddNewPassengers(destinationFloor, passengersToAdd);
                    numPassengers -= passengersToAdd;
                    PassengerQueue[destinationFloor] -= passengersToAdd; // Update the queue
                }
                else
                {
                    Console.WriteLine("No available elevator to add passengers.");
                    break;
                }
            }
            // Remove the floor from the queue if there are no more passengers waiting
            if (PassengerQueue[destinationFloor] != 0) return;
            PassengerQueue.Remove(destinationFloor);
        }

        public void DisplayElevatorStatuses()
        {
            foreach (var t in Elevators)
            {
                t.DisplayStatus();
            }
        }

        public void DisplayPassengerCounts()
        {
            foreach (var floor in PassengersOnFloor.Keys.OrderBy(floor => floor))
            {
                Console.WriteLine($"Floor {floor}: {PassengersOnFloor[floor]} passengers");
            }
        }

        #endregion Public Methods
    }
}