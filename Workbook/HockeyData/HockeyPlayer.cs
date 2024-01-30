using System.Globalization;
using Utils;

namespace Hockey.Data
{
	public class HockeyPlayer
	{
		// Fields
		private string _birthPlace;
		private DateOnly _dateOfBirth;
		private string _firstName;
		private string _lastName;
		private int _heightInInches;
		private int _weightInPounds;
		private int _jerseyNumber;

		// Constants
		private const int _MinJerseyNumber = 1;
		private const int _MaxJerseyNumber = 98;

		// Constructors

		public HockeyPlayer(string firstName, string lastName, string birthPlace, DateOnly dateOfBirth,
			int weightInPounds, int heightInInches, int jerseyNumber, Position position = Position.Center, Shot shot = Shot.Left)
		{
			FirstName = firstName;
			LastName = lastName;
			BirthPlace = birthPlace;
			HeightInInches = heightInInches;
			WeightInPounds = weightInPounds;
			JerseyNumber = jerseyNumber;
			Position = position;
			Shot = shot;
			DateOfBirth = dateOfBirth;
		}

		///// <summary>
		///// Default constructor, sets the first name to empty string, ....
		///// </summary>
		//public HockeyPlayer()
		//{
		//	_firstName = string.Empty;
		//	_lastName = string.Empty;
		//	_birthPlace = string.Empty;
		//	_dateOfBirth = new DateOnly();
		//	_heightInInches = 0;
		//	_weightInPounds = 0;
		//	Shot = Shot.Left;
		//	Position = Position.Center;
		//}

		// Properties

		public int JerseyNumber
		{
			get
			{
				return _jerseyNumber;
			}

			set
			{
				if (!Utilities.IsInRange(value, _MinJerseyNumber, _MaxJerseyNumber))
				{
					throw new ArgumentException($"Jersey number must be between {_MinJerseyNumber} and {_MaxJerseyNumber}.");
				}
				_jerseyNumber = value;
			}
		}

		public string FirstName
		{
			get
			{
				return _firstName;
			}

			private set
			{
				if (Utilities.IsNullEmptyOrWhiteSpace(value))
				{
					throw new ArgumentException("First name cannot be empty.");
				}
				_firstName = value;
			}
		}

		public string LastName
		{
			get
			{
				return _lastName;
			}

			private set
			{
				if (Utilities.IsNullEmptyOrWhiteSpace(value))
				{
					throw new ArgumentException("Last name cannot be empty.");
				}
				_lastName = value;
			}
		}

		public string BirthPlace
		{
			get
			{
				return _birthPlace;
			}

			private set
			{
				if (Utilities.IsNullEmptyOrWhiteSpace(value))
				{
					throw new ArgumentException("Birth place cannot be empty.");
				}
				_birthPlace = value;
			}
		}

		public int HeightInInches
		{
			get
			{
				return _heightInInches;
			}

			private set
			{
				if (Utilities.IsZeroOrNegative(value))
				{
					throw new ArgumentException("Height must be positive.");
				}

				_heightInInches = value;
			}
		}

		public int WeightInPounds
		{
			get
			{
				return _weightInPounds;
			}

			private set
			{
				if (Utilities.IsZeroOrNegative(value))
				{
					throw new ArgumentException("Weight must be positive.");
				}

				_weightInPounds = value;
			}
		}

		public DateOnly DateOfBirth
		{
			get 
			{
				return _dateOfBirth;
			}

			private set
			{
				if (Utilities.IsInTheFuture(value))
				{
					throw new ArgumentException("Date of birth cannot be in the future.");
				}
				_dateOfBirth = value;
			}
		}

		// Auto-implemented properties
		public Position Position { get; set; }

		public Shot Shot { get; set; }

		// Derived property
		public int Age => (DateOnly.FromDateTime(DateTime.Now).DayNumber - DateOfBirth.DayNumber) / 365;

		// Methods
		public override string ToString()
		{
			//TODO: complete for all properties
			return $"{FirstName},{LastName},{DateOfBirth.ToString("MMM-dd-yyy", CultureInfo.InvariantCulture)},{BirthPlace.Replace(", ", "-")},{WeightInPounds},{HeightInInches},{Position},{Shot},{JerseyNumber}";
		}

		public static HockeyPlayer Parse(string line)
		{
			HockeyPlayer player;

			if (Utilities.IsNullEmptyOrWhiteSpace(line)) 
			{
				throw new ArgumentNullException("line", "Line cannot be empty or null.");
			}

			// Split on the commas
			string[] fields = line.Split(',');

			if (fields.Length != 9)
			{
				throw new InvalidDataException("Incorrect number of fields.");
			}

			try
			{
				player = new HockeyPlayer(fields[0], fields[1], fields[3], 
					DateOnly.ParseExact(fields[2], "MMM-dd-yyy", CultureInfo.InvariantCulture),
					int.Parse(fields[4]), int.Parse(fields[5]), int.Parse(fields[8]), Enum.Parse<Position>(fields[6]),
					Enum.Parse<Shot>(fields[7]));
			}
			catch
			{
				throw new FormatException($"Error parsing line {line}");
			}

			return player;
		}
	}
}
