using FluentAssertions;
using Hockey.Data;

namespace HockeyTestProject
{
	public class HockeyPlayerTest
	{
		[Fact]
		public void HockeyPlayer_DefaultConstructor_CreatesGoodPlayer()
		{
			// arrange
			HockeyPlayer player;

			// act
			player = new HockeyPlayer();

			// assert
			player.Should().BeOfType<HockeyPlayer>();
		}

		[Fact]
		public void HockeyPlayer_HeightInInches_GoodSet()
		{
			// arrange
			HockeyPlayer player = new HockeyPlayer();
			int height = 72;
			int actual;

			// act
			player.HeightInInches = height; 
			actual = player.HeightInInches;

			// assert
			actual.Should().Be(height);
		}
	}
}