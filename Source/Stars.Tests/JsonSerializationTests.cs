using FluentAssertions;
using Stars.Core;
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
					Population = new Population(1),
					Installations = new Installations
					{
						Scanner = 1,
					},
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
				Population = new Population(population),
				Installations = new Installations
				{
					Scanner = scanner,
				},
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

		[Fact]
		public void EmptyBuildQueueShouldDeserialize()
		{
			var original = new BuildQueue();

			var restored = SerializeAndDeserialize(original);

			restored.Should().BeEquivalentTo(original);
		}

		[Fact]
		public void PopulatedBuildQueueShouldDeserialize()
		{
			var original = new BuildQueue();
			original.Items.Add(new BuildQueueItem(BuildMenuItem.ColonyShip) { Invested = 10 });
			original.Items.Add(new BuildQueueItem(BuildMenuItem.ScoutShip));

			var restored = SerializeAndDeserialize(original);

			restored.Should().BeEquivalentTo(original);
		}

		[Fact]
		public void BuildQueueItemShouldDeserialize()
		{
			var original = new BuildQueueItem(BuildMenuItem.ScoutShip);

			var restored = SerializeAndDeserialize(original);

			restored.Should().BeEquivalentTo(original);
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
				Rules = new GameRules(),

				Players = new EntityStore<Player>
				{
					new Player()
					{
						Id = 1,
						Name = "Player One",
					},
				},

				Galaxy = new Galaxy()
				{
					Bounds = new GalaxyBounds(800),

					Planets = new EntityStore<Planet>
					{
						new Planet()
						{
							Id = 1,
							Name = "Planet One",
							Position = new Position(100, 100),
							Details = new PlanetDetails()
							{
								Environment = new Environment(10, 50, 20),
								Minerals = new Minerals(20, 30, 50),
							},
							Settlement = new Settlement()
							{
								OwnerId = 1,
								Population = new Population(10_000),
								Installations = new Installations
								{
									Scanner = 100,
								},
							},
						},
					},
				},

				Time = new StarDate(),
			};

			return game;
		}
	}
}
