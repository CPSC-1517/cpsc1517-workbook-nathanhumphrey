using FluentAssertions;
using Hockey.Data;

namespace HockeyTestProject
{
	public class HockeyPlayerTest
	{
		// Constants for a test HockeyPlayer
		const string FirstName = "Connor";
		const string LastName = "Brown";
		const string BirthPlace = "Toronto, ON, CAN";
		const int BirthYear = 1994;
		const int BirthMonth = 01;
		const int BirthDay = 14;
		const int HeightInInches = 72;
		const int WeightInPounds = 188;
		const Position PlayerPosition = Position.Center;
		const Shot PlayerShot = Shot.Left;
		readonly int Age = (DateOnly.FromDateTime(DateTime.Now).DayNumber - new DateOnly(BirthYear, BirthMonth, BirthDay).DayNumber) / 365;

		// Used to quick test the Age calculation
		//[Fact]
		//public void Age_IsCorrect()
		//{
		//	Age.Should().Be(30);
		//}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public HockeyPlayer CreateTestHockeyPlayer()
		{
			return new HockeyPlayer(FirstName, LastName, BirthPlace,
				new DateOnly(BirthYear, BirthMonth, BirthDay), WeightInPounds, HeightInInches,
				PlayerPosition, PlayerShot);
		}

		[Fact]
		public void HockeyPlayer_GreedyConstructor_ReturnsHockeyPlayer()
		{
			// arrange
			HockeyPlayer actual;

			// act
			actual = new HockeyPlayer(FirstName, LastName, BirthPlace, 
				new DateOnly(BirthYear, BirthMonth, BirthDay), WeightInPounds, HeightInInches,
				PlayerPosition, PlayerShot);

			// assert
			actual.Should().NotBeNull();
			actual.FirstName.Should().Be(FirstName);
			actual.LastName.Should().Be(LastName);
			// TODO: complete checks for remaining properties
		}

		[Theory]
		[InlineData("", LastName, BirthPlace,
				BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches,
				PlayerPosition, PlayerShot, "First name cannot be empty.")]
		[InlineData(" ", LastName, BirthPlace,
				BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches,
				PlayerPosition, PlayerShot, "First name cannot be empty.")]
		[InlineData(null, LastName, BirthPlace,
				BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches,
				PlayerPosition, PlayerShot, "First name cannot be empty.")]
		// TODO: implement tests for the remaining properties
		public void HockeyPlayer_GreedyConstructor_ThrowsException(string firstName, string lastName,
			string birthPlace, int birthYear, int birthMonth, int birthDay, int weight,
			int height, Position position, Shot shot, string errMsg)
		{
			// arrange
			Action act = () => new HockeyPlayer(firstName, lastName, birthPlace,
				new DateOnly(birthYear, birthMonth, birthDay), weight, height,
				position, shot);

			// act/assert
			act.Should().Throw<ArgumentException>().WithMessage(errMsg);
		}

		[Fact]
		public void HockeyPlayer_Age_ReturnsCorrectAge()
		{
			// arrange
			HockeyPlayer player = CreateTestHockeyPlayer();
			int actual;

			// act
			actual = player.Age;

			// assert
			actual = Age;
		}
	}
}