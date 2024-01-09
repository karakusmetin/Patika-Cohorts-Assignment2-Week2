using GameItems.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GameItems.FakeUserLoginService
{
	public class AuthorizeFakeUserAttribute : Attribute, IAuthorizationFilter
	{
		private readonly IFakeUserService _fakeUserService;

		public AuthorizeFakeUserAttribute(IFakeUserService fakeUserService)
		{
			_fakeUserService = fakeUserService;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!_fakeUserService.CurrentUser.IsAuthenticated)
			{
				// Kullanıcı girişi yapılmamışsa yetkilendirme hatası ver.
				context.Result = new UnauthorizedResult();
			}
		}
	}
}
