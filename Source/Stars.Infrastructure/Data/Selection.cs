using Stars.Core;
using System;
using System.Linq;

namespace Stars.Infrastructure.Data
{
	public class Selection
	{
		public ISpaceObject SelectedObject { get; private set; }

		public ISpaceObject[] Objects { get; private set; }

		public event EventHandler StateChanged;

		public bool IsSelected(ISpaceObject obj)
		{
			return obj.ObjectId == SelectedObject?.ObjectId;
		}

		public void TargetObject(IGalaxy galaxy, Position pos)
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

				OnChange();
			}
		}

		public void Refresh(IGalaxy galaxy)
		{
			if (SelectedObject != null)
			{
				SelectedObject = galaxy.GetSpaceObject(SelectedObject.ObjectId);

				Objects = SelectedObject != null
					? galaxy.GetSpaceObjectsAt(SelectedObject.Position).ToArray()
					: null;

				OnChange();
			}
		}

		public void SelectObject(string id)
		{
			SelectedObject = Objects?.SingleOrDefault(obj => obj.ObjectId == id);

			OnChange();
		}

		private void OnChange()
		{
			StateChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
