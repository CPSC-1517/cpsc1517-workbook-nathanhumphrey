using Hockey.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorSampleApp.Components.Pages.Samples
{
	public partial class HockeyRoster
	{
		private List<HockeyPlayer>? Players { get; set; }
		private string? FeedbackMessage { get; set; }

		[Inject]
		public IWebHostEnvironment WebHostEnvironment { get; set; } = default!;

		protected override Task OnInitializedAsync()
		{
			Players = new();

			string csvFilePath = $@"{WebHostEnvironment.ContentRootPath}\Data\players.csv";

			using (StreamReader reader = new StreamReader(csvFilePath))
			{
				// Skip the first header record
				reader.ReadLine();

				string? currentLine;

				while ((currentLine = reader.ReadLine()) != null)
				{
					try
					{
						Players.Add(HockeyPlayer.Parse(currentLine));
					}
					catch (Exception e)
					{
						FeedbackMessage = e.Message;
					}
				}

				reader.Close();
			}

			return base.OnInitializedAsync();
		}
	}
}
