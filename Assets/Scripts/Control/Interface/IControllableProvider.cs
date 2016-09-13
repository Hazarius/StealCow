using Model;

namespace Control
{
	public interface IControllableProvider : IPositionProvider
	{
		void FreeControllableMember(IControllable member);
		IControllable Controllable { get; }
	}
}
