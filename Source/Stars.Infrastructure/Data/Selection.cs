using Stars.Core;
using System.Linq;

namespace Stars.Infrastructure.Data
{
	public struct Selection
	{
		public ISpaceObject SelectedObject { get; set; }

		public ISpaceObject[] Objects { get; set; }

		public bool IsSelected(ISpaceObject obj)
		{
			return obj.ObjectId == SelectedObject?.ObjectId;
		}

		public Selection TargetObject(IGalaxy galaxy, Position pos)
		{
			var previous = SelectedObject;
			var target = galaxy.ClosestSpaceObject(pos);

			if (target != null)
			{
				SelectedObject = target;
				Objects = galaxy.GetSpaceObjectsAt(target.Position).ToArray();

				if (previous?.Position == target.Position)
				{
					SelectedObject = Objects
						.SkipWhile(obj => obj.ObjectId != previous.ObjectId)
						.Skip(1)
						.FirstOrDefault();
				}
			}

			return this;
		}

		public Selection Refresh(IGalaxy galaxy)
		{
			if (SelectedObject != null)
			{
				SelectedObject = galaxy.GetSpaceObject(SelectedObject.ObjectId);

				Objects = SelectedObject != null
					? galaxy.GetSpaceObjectsAt(SelectedObject.Position).ToArray()
					: null;
			}

			return this;
		}

		public Selection SelectObject(string id)
		{
			SelectedObject = Objects?.SingleOrDefault(obj => obj.ObjectId == id);

			return this;
		}
	}
}
