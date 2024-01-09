using GameItems.Entities;

namespace GameItems.Data
{
	public interface IFakeUserService
	{
		FakeUser CurrentUser { get; }
	}

}