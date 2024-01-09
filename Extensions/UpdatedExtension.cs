using GameItems.Data;
using GameItems.Dto;
using GameItems.Entities;
using System.Linq;

namespace GameItems.Extensions
{
	public static class UpdatedExtension
	{
		public static UpdatedItem AsDto(this Item items)
		{
			return new UpdatedItem 
			{
				Price = items.Price,
				Description = items.Description,
			};
		}
		
	}
}
