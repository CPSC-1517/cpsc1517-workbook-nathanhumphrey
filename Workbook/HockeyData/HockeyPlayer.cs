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
		//private Position _position;
		//private Shot _shot;

		// Constructors

		public HockeyPlayer(string firstName, string lastName, string birthPlace, DateOnly dateOfBirth,
			int weightInPounds, int heightInInches, Position position = Position.Center, Shot shot = Shot.Left)
		{
			FirstName = firstName;
			LastName = lastName;
			Position = position;
			// TODO: complete for the rest
		}

		/// <summary>
		/// Default constructor, sets the first name to empty string, ....
		/// </summary>
		public HockeyPlayer()
		{
			_firstName = string.Empty;
			_lastName = string.Empty;
			_birthPlace = string.Empty;
			_dateOfBirth = new DateOnly();
			_heightInInches = 0;
			_weightInPounds = 0;
			// TODO: update the Shot property like the Position
			// _shot = Shot.Left;
			Position = Position.Center;
		}

		// Properties
		public string FirstName
		{
			get
			{
				return _firstName;
			}

			set
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

			set
			{
				if (Utilities.IsNullEmptyOrWhiteSpace(value))
				{
					throw new ArgumentException("Last name cannot be empty.");
				}
				_lastName = value;
			}
		}

		public int HeightInInches
		{
			get
			{
				return _heightInInches;
			}

			set
			{
				if (Utilities.IsZeroOrNegative(value))
				{
					throw new ArgumentException("Height must be positive.");
				}

				_heightInInches = value;
			}
		}

		// Auto-implemented property
		public Position Position { get; set; }

		// Methods
		public override string ToString()
		{
			//TODO: complete for all properties
			return $"{FirstName},{LastName},{Position},...";
		}
	}
}
