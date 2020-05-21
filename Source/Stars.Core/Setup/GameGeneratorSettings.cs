﻿using System.Collections.Generic;

namespace Stars.Core.Setup
{
	public class GameGeneratorSettings
	{
		public string GameName { get; set; }

		public GalaxyGeneratorSettings GalaxySettings { get; set; } = new GalaxyGeneratorSettings();

		public IList<string> PlayerNames { get; set; } = new List<string>();
	}
}
