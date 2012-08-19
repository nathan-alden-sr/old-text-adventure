using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers.Compact
{
	public class WorldSerializer
	{
		public static readonly WorldSerializer Instance = new WorldSerializer();

		private WorldSerializer()
		{
		}

		public byte[] Serialize(World world)
		{
			world.ThrowIfNull("world");

			var serializer = new CompactSerializer();

			serializer[0] = world.Id.ToByteArray();
			serializer[1] = BitConverter.GetBytes(world.Version);
			serializer[2] = Encoding.UTF8.GetBytes(world.Title);
			serializer[3] = PlayerSerializer.Instance.Serialize(world.StartingPlayer);

			var boardSerializer = new CompactSerializer();
			int boardIndex = 0;

			foreach (Board board in world.Boards)
			{
				boardSerializer[boardIndex++] = BoardSerializer.Instance.Serializer(board);
			}

			serializer[4] = boardSerializer.Serialize();

			var actorSerializer = new CompactSerializer();
			int actorIndex = 0;

			foreach (Actor actor in world.Actors)
			{
				actorSerializer[actorIndex++] = ActorSerializer.Instance.Serialize(actor);
			}

			serializer[5] = actorSerializer.Serialize();

			var messageSerializer = new CompactSerializer();
			int messageIndex = 0;

			foreach (Message message in world.Messages)
			{
				messageSerializer[messageIndex++] = MessageSerializer.Instance.Serialize(message);
			}

			serializer[6] = messageSerializer.Serialize();

			var timerSerializer = new CompactSerializer();
			int timerIndex = 0;

			foreach (Timer timer in world.Timers)
			{
				timerSerializer[timerIndex++] = TimerSerializer.Instance.Serialize(timer);
			}

			serializer[7] = timerSerializer.Serialize();

			var soundEffectSerializer = new CompactSerializer();
			int soundEffectIndex = 0;

			foreach (SoundEffect soundEffect in world.SoundEffects)
			{
				soundEffectSerializer[soundEffectIndex++] = SoundEffectSerializer.Instance.Serialize(soundEffect);
			}

			serializer[8] = soundEffectSerializer.Serialize();

			var songSerializer = new CompactSerializer();
			int songIndex = 0;

			foreach (Song song in world.Songs)
			{
				songSerializer[songIndex++] = SongSerializer.Instance.Serialize(song);
			}

			serializer[9] = songSerializer.Serialize();

			return serializer.Serialize();
		}

		public World Deserialize(byte[] serializedData)
		{
			serializedData.ThrowIfNull("serializedData");

			var serializer = new CompactSerializer(serializedData);
			var id = new Guid(serializer[0]);
			int version = BitConverter.ToInt32(serializer[1], 0);
			string title = Encoding.UTF8.GetString(serializer[2]);
			Player startingPlayer = PlayerSerializer.Instance.Deserialize(serializer[3]);
			var boardSerializer = new CompactSerializer(serializer[4]);
			IEnumerable<Board> boards = boardSerializer.FieldIndices.Select(arg => BoardSerializer.Instance.Deserialize(boardSerializer[arg]));
			var actorSerializer = new CompactSerializer(serializer[5]);
			IEnumerable<Actor> actors = actorSerializer.FieldIndices.Select(arg => ActorSerializer.Instance.Deserialize(actorSerializer[arg]));
			var messageSerializer = new CompactSerializer(serializer[6]);
			IEnumerable<Message> messages = messageSerializer.FieldIndices.Select(arg => MessageSerializer.Instance.Deserialize(messageSerializer[arg]));
			var timerSerializer = new CompactSerializer(serializer[7]);
			IEnumerable<Timer> timers = timerSerializer.FieldIndices.Select(arg => TimerSerializer.Instance.Deserialize(timerSerializer[arg]));
			var soundEffectSerializer = new CompactSerializer(serializer[8]);
			IEnumerable<SoundEffect> soundEffects = soundEffectSerializer.FieldIndices.Select(arg => SoundEffectSerializer.Instance.Deserialize(soundEffectSerializer[arg]));
			var songSerializer = new CompactSerializer(serializer[9]);
			IEnumerable<Song> songs = songSerializer.FieldIndices.Select(arg => SongSerializer.Instance.Deserialize(songSerializer[arg]));

			return new World(id, version, title, startingPlayer, boards, actors, messages, timers, soundEffects, songs);
		}
	}
}