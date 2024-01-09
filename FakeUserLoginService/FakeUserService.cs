using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using GameItems.Data;
using GameItems.Entities;

namespace GameItems.FakeUserLoginService
{	public class FakeUserService : IFakeUserService
	{
		//IsAuthenticated kısmı el ile değiştirilerek giriş engellenilebilir
		public FakeUser CurrentUser { get; } = new FakeUser { IsAuthenticated=true};
	}
}
