﻿using FluentAssertions;
using Stars.Core;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xunit;

namespace Stars.Tests
{
	public class JsonSerializationTests
	{
		[Fact]
		public void GameShouldDeserialize()
		{
			Game original = CreateGame();

			var restored = SerializeAndDeserialize(original);

			restored.Should().BeEquivalentTo(original);
		}

		[Fact]
		public void GalaxyShouldDeserialize()
		{
			Galaxy original = CreateGame().Galaxy;

			var restored = SerializeAndDeserialize(original);

			restored.Should().BeEquivalentTo(original);
		}

		[Theory]
		[InlineData(0)]
		[InlineData(500)]
		public void GalaxyBoundsShouldDeserialize(int size)
		{
			var original = new GalaxyBounds(size);

			var restored = SerializeAndDeserialize(original);

			Assert.Equal(original, restored);
		}

		[Theory]
		[InlineData(0, null)]
		[InlineData(1, "Name")]
		public void PlayerShouldDeserialize(int id, string name)
		{
			var original = new Player()
			{
				Id = id,
				Name = name,
			};

			var restored = SerializeAndDeserialize(original);

			restored.Should().BeEquivalentTo(original);
		}

		[Fact]
		public void PlanetShouldDeserialize()
		{
			var original = new Planet()
			{
				Id = 1,
				Name = "Name",
				Position = new Position(1, 1),
				Details = new PlanetDetails()
				{
					Environment = new Core.Environment(1, 2, 3),
					Minerals = new Minerals(4, 5, 6),
				},
				Settlement = new Settlement()
				{
					OwnerId = 1,
					Population = 1,
					ScannerRange = 1,
				},
			};

			var restored = SerializeAndDeserialize(original);

			restored.Should().BeEquivalentTo(original);
		}

		[Theory]
		[InlineData(0, 0)]
		[InlineData(1, 2)]
		public void PositionShouldDeserialize(int x, int y)
		{
			var original = new Position(x, y);

			var restored = SerializeAndDeserialize(original);

			Assert.Equal(original, restored);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(1, 2, 3)]
		public void SettlementShouldDeserialize(int owner, int population, int scanner)
		{
			var original = new Settlement()
			{
				OwnerId = owner,
				Population = population,
				ScannerRange = scanner,
			};

			var restored = SerializeAndDeserialize(original);

			restored.Should().BeEquivalentTo(original);
		}

		[Fact]
		public void PlanetDetailsShouldDeserialize()
		{
			var original = new PlanetDetails()
			{
				Environment = new Core.Environment(1, 2, 3),
				Minerals = new Minerals(4, 5, 6),
			};

			var restored = SerializeAndDeserialize(original);

			restored.Should().BeEquivalentTo(original);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(1, 2, 3)]
		public void EnvironmentShouldDeserialize(int g, int r, int t)
		{
			var original = new Core.Environment(g, r, t);

			var restored = SerializeAndDeserialize(original);

			Assert.Equal(original, restored);
		}

		[Theory]
		[InlineData(0, 0, 0)]
		[InlineData(1, 2, 3)]
		public void MineralsShouldDeserialize(int br, int gr, int ir)
		{
			var original = new Minerals(br, gr, ir);

			var restored = SerializeAndDeserialize(original);

			Assert.Equal(original, restored);
		}

		private static T SerializeAndDeserialize<T>(T original)
		{
			string json = JsonSerializer.Serialize(original);
			var restored = JsonSerializer.Deserialize<T>(json);

			return restored;
		}

		private static Game CreateGame()
		{
			var game = new Game()
			{
				Settings = new GameSettings(),

				Galaxy = new Galaxy()
				{
					Bounds = new GalaxyBounds(800),
					
					Planets = new List<Planet>()
					{
						new Planet()
						{
							Id = 1,
							Name = "Planet One",
							Position = new Position(100, 100),
							Details = new PlanetDetails()
							{ 
								Environment = new Core.Environment(10, 50, 20),
								Minerals = new Minerals(20, 30, 50),
							},
						},
					},

					Players = new List<Player>()
					{
						new Player()
						{
							Id = 1,
							Name = "Player One",
						},
					},
				},
				
				Turn = 1,
			};

			return game;
		}
	}
}