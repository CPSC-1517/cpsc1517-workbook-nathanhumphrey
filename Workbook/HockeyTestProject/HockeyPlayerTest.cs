using FluentAssertions;
using Hockey.Data;
using System.Globalization;
using Xunit.Sdk;

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
		const int JerseyNumber = 28;
		const Position PlayerPosition = Position.Center;
		const Shot PlayerShot = Shot.Left;
		readonly int Age = (DateOnly.FromDateTime(DateTime.Now).DayNumber - new DateOnly(BirthYear, BirthMonth, BirthDay).DayNumber) / 365;
		readonly string ToStringValue = $"{FirstName},{LastName},Jan-14-1994,{BirthPlace.Replace(", ", "-")},{WeightInPounds},{HeightInInches},{PlayerPosition},{PlayerShot},{JerseyNumber}";


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
				JerseyNumber, PlayerPosition, PlayerShot);
		}

		[Fact]
		public void HockeyPlayer_GreedyConstructor_ReturnsHockeyPlayer()
		{
			// arrange
			HockeyPlayer actual;

			// act
			actual = new HockeyPlayer(FirstName, LastName, BirthPlace, 
				new DateOnly(BirthYear, BirthMonth, BirthDay), WeightInPounds, HeightInInches,
				JerseyNumber, PlayerPosition, PlayerShot);

			// assert
			actual.Should().NotBeNull();
			actual.FirstName.Should().Be(FirstName);
			actual.LastName.Should().Be(LastName);
			// TODO: complete checks for remaining properties
		}

		[Theory]
		[InlineData("", LastName, BirthPlace,
				BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches,
				JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be empty.")]
		[InlineData(" ", LastName, BirthPlace,
				BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches,
				JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be empty.")]
		[InlineData(null, LastName, BirthPlace,
				BirthYear, BirthMonth, BirthDay, WeightInPounds, HeightInInches,
				JerseyNumber, PlayerPosition, PlayerShot, "First name cannot be empty.")]
		// TODO: implement tests for the remaining properties
		public void HockeyPlayer_GreedyConstructor_ThrowsException(string firstName, string lastName,
			string birthPlace, int birthYear, int birthMonth, int birthDay, int weight,
			int height, int jerseyNumber, Position position, Shot shot, string errMsg)
		{
			// arrange
			Action act = () => new HockeyPlayer(firstName, lastName, birthPlace,
				new DateOnly(birthYear, birthMonth, birthDay), weight, height,
				jerseyNumber, position, shot);

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

		[Theory]
		[InlineData(1)]
		[InlineData(98)]
		public void HockeyPlayer_JerseyNumber_GoodSetAndGet(int expected)
		{
			HockeyPlayer player = CreateTestHockeyPlayer();
			
			player.JerseyNumber = expected;
			int actual = player.JerseyNumber;

			actual.Should().Be(expected);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(99)]
		public void HockeyPlayer_JerseyNumber_BadSetThrows(int value)
		{
			HockeyPlayer player = CreateTestHockeyPlayer();

			Action act = () => player.JerseyNumber = value;

			act.Should().Throw<ArgumentException>();
		}

		// ToString Test
		[Fact]
		public void HockeyPlayer_ToString_ReturnsCorrectValue()
		{
			HockeyPlayer player = CreateTestHockeyPlayer();

			string actual = player.ToString();

			actual.Should().Be(ToStringValue);
		}

		// Parse good test
		[Fact]
		public void HockeyPlayer_Parse_ParsesCorrectly()
		{
			HockeyPlayer actual = null;
			string line = ToStringValue;

			actual = HockeyPlayer.Parse(line);

			actual.Should().NotBeNull();
			// TODO: could also check the individual properties
		}

		// Parse bad tests (empty line, number of fields, and format)
		[Theory]
		[InlineData(null, "*Line cannot be empty or null.*")]
		[InlineData("", "*Line cannot be empty or null.*")]
		[InlineData(" ", "*Line cannot be empty or null.*")]
		public void HockeyPlayer_Parse_ThrowsForNullEmptyOrWhiteSpaceLine(string line, string errMsg)
		{
			Action act = () => HockeyPlayer.Parse(line);

			act.Should().Throw<ArgumentNullException>().WithMessage(errMsg);
		}

		[Theory]
		[InlineData("one,two,three,four,five,six,seven,eight", "Incorrect number of fields.")]
		[InlineData("one,two,three,four,five,six,seven,eight,nine,ten", "Incorrect number of fields.")]
		public void HockeyPlayer_Parse_ThrowsForInvalidNumberOfFields(string line, string errMsg)
		{
			Action act = () => HockeyPlayer.Parse(line);

			act.Should().Throw<InvalidDataException>().WithMessage(errMsg);
		}

		[Theory]
		[InlineData("one,two,three,four,five,six,seven,eight,nine", "Error parsing line")]
		public void HockeyPlayer_Parse_ThrowsForFormatError(string line, string errMsg)
		{
			Action act = () => HockeyPlayer.Parse(line);

			act.Should().Throw<FormatException>().WithMessage($"*{errMsg}*");
		}

		// TryParse true test
		[Fact]
		public void HockeyPlayer_TryParse_ParsesTrueCorrectly()
		{
			HockeyPlayer actual = null;
			bool result;

			result = HockeyPlayer.TryParse(ToStringValue, out actual);

			result.Should().BeTrue();
			actual.Should().NotBeNull();
		}

		// TryParse false test
		// TODO: implement this one on your own.
	}
}