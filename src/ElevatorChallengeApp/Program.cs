namespace ElevatorChallengeApp
{
    public class Program
    {
        #region Main Method
        static void Main(string[] args)
        {
            Building? building = null;

            while (true)
            {
                if (building == null)
                {
                    Console.WriteLine("Please enter a command:");
                    Console.WriteLine("1. Create a new building");
                    Console.WriteLine("2. Exit");
                }
                else
                {
                    Console.WriteLine("Please enter a command:");
                    Console.WriteLine("1. Call an elevator and add passengers");
                    Console.WriteLine("2. Display elevator statuses and Passenger waiting list");
                    Console.WriteLine("3. Exit");
                }

                var command = Console.ReadLine();

                if (building == null)
                {
                    switch (command)
                    {
                        case "1":
                            Console.WriteLine(
                                "Enter the number of floors, number of elevators, and elevator capacity, separated by commas:");
                            var buildingParams = Console.ReadLine().Split(',');
                            var floors = int.Parse(buildingParams[0]);
                            var elevators = int.Parse(buildingParams[1]);
                            var capacity = int.Parse(buildingParams[2]);
                            building = new Building(floors, elevators, capacity);
                            break;
                        case "2":
                            return;
                        default:
                            Console.WriteLine("Invalid command. Please try again.");
                            break;
                    }
                }
                else
                {
                    switch (command)
                    {
                        case "1":
                            Console.WriteLine("Enter the current floor, destination floor and number of passengers, separated by a comma:");
                            var elevatorParams = Console.ReadLine().Split(',');
                            var currentFloor = int.Parse(elevatorParams[0]);
                            var destinationFloor = int.Parse(elevatorParams[1]);
                            var numPassengers = int.Parse(elevatorParams[2]);
                            building.CallElevator(currentFloor);
                            building.AddPassengersToAvailableElevator(currentFloor, destinationFloor, numPassengers);
                            break;
                        case "2":
                            building.DisplayElevatorStatuses();
                            building.DisplayPassengerCounts();
                            break;
                        case "3":
                            return;
                        default:
                            Console.WriteLine("Invalid command. Please try again.");
                            break;
                    }
                }
            }
        }


        #endregion  Main Method
    }
}