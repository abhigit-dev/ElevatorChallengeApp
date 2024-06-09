using ElevatorChallengeApp;
namespace ElevatorChallengeTests
{
    public class ElevatorTests
    {
        [Fact]
        public void TestConstructor()
        {
            var elevator = new Elevator(10, 1);
            Assert.Equal(0, elevator.CurrentFloor);
            Assert.Equal(Direction.Stationary, elevator.Direction);
            Assert.Empty(elevator.Passengers);
            Assert.Empty(elevator.PassengerDestinations);
            Assert.Equal(10, elevator.MaxCapacity);
            Assert.Equal(1, elevator.ElevatorNumber);
        }

        [Fact]
        public void TestAddNewPassengers()
        {
            var elevator = new Elevator(10, 1);
            elevator.AddNewPassengers(5, 3);
            Assert.Equal(5, elevator.CurrentFloor);
            Assert.Equal(Direction.Stationary, elevator.Direction);
        }

        [Fact]
        public void TestMoveToFloor()
        {
            var elevator = new Elevator(10, 1);
            elevator.MoveToFloor(3);
            Assert.Equal(3, elevator.CurrentFloor);
            Assert.Equal(Direction.Stationary, elevator.Direction);
        }

        [Fact]
        public void TestDisplayStatus()
        {
            var elevator = new Elevator(10, 1);
            elevator.AddNewPassengers(5, 3);
            elevator.DisplayStatus();
        }

        [Fact]
        public void TestRemovePassengersAtDestination()
        {
            var elevator = new Elevator(10, 1);
            elevator.AddNewPassengers(5, 3);
            elevator.MoveToFloor(5);
            Assert.Empty(elevator.Passengers);
            Assert.Empty(elevator.PassengerDestinations);
        }
    }
}
