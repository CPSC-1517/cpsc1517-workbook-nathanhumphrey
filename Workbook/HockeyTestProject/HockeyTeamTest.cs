using FluentAssertions;
using Hockey.Data;
using System.Text.Json;

namespace HockeyTestProject
{
	public class HockeyTeamTest
	{
		// Constants for testing the team
		const string City = "Edmonton";
		const string Name = "Oilers";

		// Constants for a test HockeyPlayer
		const string FirstName = "Connor";
		const string LastName = "Brown";
		const string BirthPlace = "Toronto, ON, CAN";
		const int BirthYear = 1994;
		const int BirthMonth = 01;
		const int BirthDay = 14;
		const int HeightInInches = 72;
		const int WeightInPounds = 188;
		const int JerseyNumber = 28;
		const Position PlayerPosition = Position.Center;
		const Shot PlayerShot = Shot.Left;
		
		public HockeyPlayer CreateTestHockeyPlayer()
		{
			return new HockeyPlayer(FirstName, LastName, BirthPlace,
				new DateOnly(BirthYear, BirthMonth, BirthDay), WeightInPounds, HeightInInches,
				JerseyNumber, PlayerPosition, PlayerShot);
		}

		private static HockeyTeam CreateTestHockeyTeam(int numOfPlayers, int numOfGoalies = 0)
		{
			string firstName = "FirstName";
			string lastName = "LastName";
			string birthPlace = "BirthPlace";
			DateOnly dateOfBirth = new DateOnly(2000, 01, 01);
			int jerseyNumber = 0;
			int weight = 0;
			int height = 0;
			Shot shot = Shot.Left;

			int number = 0;

			HockeyTeam team = new HockeyTeam(City, Name);

			// Generate the test players
			for (number = 1; number <= numOfPlayers - numOfGoalies; number += 1)
			{
				Position position;

				if (number < 5)
				{
					position = Position.LeftWing;
				}
				else if (number < 9)
				{
					position = Position.RightWing;
				}
				else if (number < 13)
				{
					position = Position.Center;
				}
				else
				{
					position = Position.Defense;
				}

				team.AddPlayer(new HockeyPlayer($"{firstName}{number}", $"{lastName}{number}",
					$"{birthPlace}{number}", dateOfBirth.AddDays(1), weight + number, height + number,
					jerseyNumber += 1, position, shot));
			}

			// Create test goalies
			for (; number <= numOfPlayers; number += 1)
			{
				team.AddPlayer(new HockeyPlayer($"{firstName}{number}", $"{lastName}{number}",
					$"{birthPlace}{number}", dateOfBirth.AddDays(1), weight + number, height + number,
					jerseyNumber += 1, Position.Goalie, shot));
			}

			return team;
		}

		[Fact]
		public void HockeyTeam_Constructor_ReturnsHockeyTeam()
		{
			HockeyTeam actual = null;

			actual = new HockeyTeam(City, Name);

			actual.Should().NotBeNull();
			actual.Name.Should().Be(Name);
			actual.City.Should().Be(City);
		}

		[Theory]
		[InlineData("", Name)]
		[InlineData(" ", Name)]
		[InlineData(null, Name)]
		[InlineData(City, "")]
		[InlineData(City, " ")]
		[InlineData(City, null)]
		public void HockeyTeam_Constructor_ThrowsForEmptyCityOrName(string city, string name)
		{
			Action act = () => new HockeyTeam(city, name);

			act.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void HockeyTeam_AddPlayer_Success()
		{
			HockeyTeam team = CreateTestHockeyTeam(0);
			HockeyPlayer player = CreateTestHockeyPlayer();
			HockeyPlayer actual;

			team.AddPlayer(player);
			actual = team.Players.First();

			actual.Should().Be(player);
		}

		[Fact]
		public void HockeyTeam_AddPlayer_ThrowsForNullArgument()
		{
			HockeyTeam team = CreateTestHockeyTeam(0);

			Action act = () => team.AddPlayer(null);

			act.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void HockeyTeam_RemovePlayer_Success()
		{
			HockeyTeam team = CreateTestHockeyTeam(1);
			HockeyPlayer player = team.Players.First();
			int actual;

			team.RemovePlayer(player);
			actual= team.Players.Count;

			actual.Should().Be(0);
		}

		[Fact]
		public void HockeyTeam_RemovePlayer_ThrowsForNullArgument()
		{
			HockeyTeam team = CreateTestHockeyTeam(1);

			Action act = () => team.RemovePlayer(null);

			act.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void HockeyTeam_RemovePlayer_ThrowsForPlayerNotOnTeam()
		{
			HockeyTeam team = CreateTestHockeyTeam(1);
			HockeyPlayer player = CreateTestHockeyPlayer();

			Action act = () => team.RemovePlayer(player);

			act.Should().Throw<InvalidOperationException>();
		}

		[Theory]
		[InlineData(1)]
		[InlineData(2)]
		[InlineData(3)]
		public void HockeyTeam_TotalPlayers_ReturnsCorrectCount(int numOfPlayers)
		{
			HockeyTeam team = CreateTestHockeyTeam(numOfPlayers);
			int actual;

			actual = team.TotalPlayers;

			actual.Should().Be(numOfPlayers);
		}

		[Theory]
		[InlineData(19, 1, false)] // players low, goalies low
		[InlineData(19, 2, false)] // players low, goalies ok
		[InlineData(20, 1, false)] // players ok, goalies low
		[InlineData(20, 4, false)] // players ok, goalies high
		[InlineData(20, 2, true)]  // players min, goalies min
		[InlineData(20, 3, true)]  // players min, goalies max
		[InlineData(23, 2, true)]  // players max, goalies min
		[InlineData(23, 3, true)]  // players max, goalies max
		[InlineData(24, 1, false)]  // players high, goalies low
		[InlineData(24, 4, false)]  // players high, goalies high
		public void HockeyTeam_IsValidRoster_ReturnsCorrectValue(int numOfPlayers, int numOfGoalies, 
			bool expected)
		{
			HockeyTeam team = CreateTestHockeyTeam(numOfPlayers, numOfGoalies);
			bool actual;

			actual = team.IsValidRoster;

			actual.Should().Be(expected);
		}
	}
}
