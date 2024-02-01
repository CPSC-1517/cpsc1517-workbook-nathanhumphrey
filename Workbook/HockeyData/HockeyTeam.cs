using System.Xml.Linq;

namespace Hockey.Data
{
	/// <summary>
	/// 
	/// </summary>
	public class HockeyTeam
	{
		private const int _MinPlayers = 20;
		private const int _MaxPlayers = 23;
		private const int _MinGoalies = 2;
		private const int _MaxGoalies = 3;

		public string Name { get; private set; }

		public string City { get; private set; }

		public List<HockeyPlayer> Players { get; private set; } = new List<HockeyPlayer>();

		public int TotalPlayers => Players.Count;

		public bool IsValidRoster
		{
			get
			{
				// Get the number of goalies
				int totalGoalies = Players.FindAll(p => p.Position == Position.Goalie).Count;

				return TotalPlayers >= _MinPlayers && TotalPlayers <= _MaxPlayers
					&& totalGoalies >= _MinGoalies && totalGoalies <= _MaxGoalies;
			}
		}

		public HockeyTeam(string city, string name)
		{
			if (string.IsNullOrWhiteSpace(name)) 
			{
				throw new ArgumentException("Name cannot be empty");
			}
			if (string.IsNullOrWhiteSpace(city))
			{
				throw new ArgumentException("City cannot be empty");
			}

			Name = name.Trim();
			City = city.Trim();
		}

		public void AddPlayer(HockeyPlayer player)
		{
			if (player == null)
			{
				throw new ArgumentException("Player cannot be null.");
			}

			Players.Add(player);
		}

		public void RemovePlayer(HockeyPlayer player)
		{
			if (player == null)
			{
				throw new ArgumentException("Player cannot be null.");
			}

			if (!Players.Contains(player))
			{
				throw new InvalidOperationException($"Player {player} is not on the team.");
			}

			Players.Remove(player);
		}
	}
}
