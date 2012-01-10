using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public class ActorInstanceLayer : Layer<ActorInstance>
	{
		private readonly Dictionary<Guid, ActorInstance> _actorInstancesById = new Dictionary<Guid, ActorInstance>();

		public ActorInstanceLayer(
			Size size,
			IEnumerable<ActorInstance> actorInstances)
			: base(size)
		{
			actorInstances.ThrowIfNull("actorInstances");

			actorInstances = actorInstances.ToArray();
			_actorInstancesById.Clear();
			foreach (ActorInstance actorInstance in actorInstances)
			{
				_actorInstancesById.Add(actorInstance.Id, actorInstance);
			}
			Tiles = actorInstances;
		}

		public IEnumerable<ActorInstance> ActorInstances
		{
			get
			{
				return Tiles;
			}
		}

		public ActorInstance this[Guid actorInstanceId]
		{
			get
			{
				return GetActorInstanceById(actorInstanceId);
			}
		}

		public ActorInstance GetActorInstanceById(Guid id)
		{
			return _actorInstancesById[id];
		}

		public IEnumerable<ActorInstance> GetActorInstancesByActorId(Guid actorId)
		{
			return _actorInstancesById.Values.Where(arg => arg.ActorId == actorId);
		}

		protected internal bool AddActorInstance(Board board, Player player, ActorInstance actorInstance)
		{
			board.ThrowIfNull("board");
			actorInstance.ThrowIfNull("actorInstance");

			Coordinate coordinate = actorInstance.Coordinate;
			Sprite foregroundSprite = board.ForegroundLayer[coordinate];
			ActorInstance existingActorInstance = board.ActorInstanceLayer[coordinate];

			if (foregroundSprite != null || existingActorInstance != null || player.Coordinate == coordinate)
			{
				return false;
			}

			SetTile(coordinate, actorInstance);

			return true;
		}
	}
}