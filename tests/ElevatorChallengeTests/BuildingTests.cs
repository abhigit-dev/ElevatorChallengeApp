using ElevatorChallengeApp;
using Moq;

namespace ElevatorChallengeTests
{
    public class BuildingTests
    {
        [Fact]
        public void TestBuildingConstructor()
        {
            var building = new Building(10, 2, 5);
            Assert.Equal(10, building.Floors);
            Assert.Equal(2, building.Elevators.Count);
            Assert.Empty(building.PassengerQueue);
            Assert.Empty(building.PassengersOnFloor);
        }

        [Fact]
        public void TestCallElevator()
        {
            var mockElevator = new Mock<IElevator>();
            mockElevator.Setup(e => e.CurrentFloor).Returns(1);
            mockElevator.Setup(e => e.Passengers).Returns(new List<int>());
            mockElevator.Setup(e => e.MaxCapacity).Returns(5);

            var building = new Building(10, 1, 5);
            building.Elevators[0] = mockElevator.Object;

            building.CallElevator(5);

            mockElevator.Verify(e => e.MoveToFloor(5), Times.Once);
        }

        [Fact]
        public void TestAddPassengersToAvailableElevator()
        {
            var mockElevator = new Mock<IElevator>();
            mockElevator.Setup(e => e.CurrentFloor).Returns(1);
            mockElevator.Setup(e => e.Passengers).Returns(new List<int>());
            mockElevator.Setup(e => e.MaxCapacity).Returns(5);

            var building = new Building(10, 1, 5);
            building.Elevators[0] = mockElevator.Object;

            building.AddPassengersToAvailableElevator(1, 5, 3);

            mockElevator.Verify(e => e.MoveToFloor(1), Times.Once);
            mockElevator.Verify(e => e.AddNewPassengers(5, 3), Times.Once);
        }

        [Fact]
        public void TestDisplayElevatorStatuses()
        {
            var mockElevator = new Mock<IElevator>();
            mockElevator.Setup(e => e.DisplayStatus());

            var building = new Building(10, 1, 5);
            building.Elevators[0] = mockElevator.Object;

            building.DisplayElevatorStatuses();

            mockElevator.Verify(e => e.DisplayStatus(), Times.Once);
        }

        [Fact]
        public void TestDisplayPassengerCounts()
        {
            var building = new Building(10, 1, 5);
            building.PassengersOnFloor.Add(1, 5);
            building.PassengersOnFloor.Add(2, 3);

            var originalOut = Console.Out; // Save the original standard out

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                building.DisplayPassengerCounts();

                var result = sw.ToString().Trim();
                Assert.Equal("Floor 1: 5 passengers\r\nFloor 2: 3 passengers", result);
            }

            Console.SetOut(originalOut); // Reset the standard out
        }
    }
}
