using Xunit;

namespace RobotSimulator.UnitTests
{
    public class UnitTest1
    {
        readonly RobotSimulator _robotSimulator;

        Robot robot = new Robot();

        public UnitTest1()
        {
            _robotSimulator = new RobotSimulator();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        public void InitialisationTest(int x)
        {
            Location location = new Location()
            {
                X = x,
                Y = 0,
                F = "North"
            };

            robot = _robotSimulator.InitialiseRobot(location);

            Assert.True(robot.Initialised, "Robot Initialised");
        }

        [Theory]
        [InlineData(0)]
        public void MoveTest(int y)
        {
            Location location = new Location()
            {
                X = 0,
                Y = y,
                F = "NORTH"
            };

            robot.Location = location;
            robot.Initialised = true;

            var result = _robotSimulator.Move(robot);

            Assert.Equal(y + 1, result.Location.Y);
        }

        [Theory]
        [InlineData("NORTH")]
        public void LeftTest(string f)
        {
            Location location = new Location()
            {
                X = 0,
                Y = 0,
                F = f
            };

            robot.Location = location;
            robot.Initialised = true;

            var result = _robotSimulator.Left(robot);

            Assert.Equal("WEST", result.Location.F);
        }

        [Theory]
        [InlineData("NORTH")]
        public void RightTest(string f)
        {
            Location location = new Location()
            {
                X = 0,
                Y = 0,
                F = f
            };

            robot.Location = location;
            robot.Initialised = true;

            var result = _robotSimulator.Right(robot);

            Assert.Equal("EAST", result.Location.F);
        }
    }
}