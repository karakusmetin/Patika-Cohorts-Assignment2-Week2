using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Xml.Linq;
using System;
using GameItems.Entities;
using GameItems.Dto;
using GameItems.Data;
using GameItems.Extensions;
using GameItems.FakeUserLoginService;

namespace GameItems.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ItemController : ControllerBase
	{
		private readonly IItemsInterface items;

		public ItemController(IItemsInterface items)
		{
			this.items = items;
		}

		[HttpGet]
		public ActionResult<IEnumerable<Item>> GetUsers()
		{
			var result = items.GetAllItems();
			return Ok(result);
		}

		[HttpGet("list")]
		public ActionResult<IEnumerable<List<Item>>> GetList([FromQuery] string Name)
		{
			if (string.IsNullOrEmpty(Name))
				return BadRequest("İsim Boş Olamaz");

			var itemList = items.NameOfList(Name);
			if (itemList.Count == 0)
				return NotFound("İtem Listesi Bulunamadı");
			else
			{
				return Ok(itemList);
			}
			
		}

		//[HttpGet("sort")]
		//public IActionResult GetSortedProducts([FromQuery] string sortBy)
		//{
		//	if (string.IsNullOrEmpty(sortBy))
		//	{
		//		return BadRequest("Boş bırakılamaz");
		//	}

		//	switch (sortBy.ToLower())
		//	{
		//		case "name":
		//			return Ok(items.OrderBy(p => p.Name).ToList());
		//		case "price":
		//			return Ok(items.OrderBy(p => p.Price).ToList());
		//		default:
		//			return BadRequest("Name' veya 'Price' yazılmalı.");
		//	}
		//}

		[HttpGet("{id}")]
		public ActionResult<Item> GetById(int id)
		{
			Item result = items.GetItemsById(id);

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

		// POST: api/items
		[HttpPost]
		public ActionResult<CreateItem> Post([FromBody] CreateItem newItem)
		{
		
			Item createItem = new()
			{
				Id = items.GetCount(),
				Description = newItem.Description,
				Name = newItem.Name,
				Price = newItem.Price,
			};
			items.CrateItem(createItem);

			return Ok(createItem);
		}

		// PUT: api/items/1
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] UpdatedItem updatedItem)
		{
			var existingItem = items.UpdatedItem(id, updatedItem);

			if (existingItem == null)
			{
				return NotFound(); 
			}

			return Ok(existingItem);
		}

		[HttpPut("Query")]
		public IActionResult PutQuery(int id, string Name, string Description)
		{
			var result = items.UpdateItemQuery(id, Name, Description);

			if (result == null)
			{
				return NotFound(); 
			}

			return Ok(result); 
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{

			var status = items.DeleteItem(id);
			if (status ==true)
			{
				return Ok();
			}

			return NoContent(); 
		}
	}
}

