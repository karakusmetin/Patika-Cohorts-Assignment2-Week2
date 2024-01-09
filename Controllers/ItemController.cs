using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Xml.Linq;
using System;
using GameItems.Entities;

namespace GameItems.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class ItemController : ControllerBase
	{
		private readonly List<Item> items = new()
		{
			new Item{ Id= 0, Name="Sword",Price = 9, Description="Basic Weapon" },
			new Item{ Id= 1, Name="Sytche",Price = 20, Description = "Magical weapon" },
			new Item{ Id= 2, Name="Shield",Price = 19, Description = "Basic Armor" }
		};

		[HttpGet]
		public ActionResult<IEnumerable<Item>> Get()
		{
			return Ok(items);
		}

		[HttpGet("list")]
		public ActionResult<IEnumerable<List<Item>>> GetList([FromQuery] string Name)
		{
			if (string.IsNullOrEmpty(Name))
				return BadRequest("İsim Boş Olamaz");
			
			var itemList = items.Where(x => x.Name.Contains(Name, StringComparison.OrdinalIgnoreCase)).ToList();
			if (itemList.Count == 0)
				return NotFound("İtem Listesi Bulunamadı");
			else
			{
				return Ok(itemList);
			}
			
		}

		[HttpGet("sort")]
		public IActionResult GetSortedProducts([FromQuery] string sortBy)
		{
			if (string.IsNullOrEmpty(sortBy))
			{
				return BadRequest("Boş bırakılamaz");
			}

			switch (sortBy.ToLower())
			{
				case "name":
					return Ok(items.OrderBy(p => p.Name).ToList());
				case "price":
					return Ok(items.OrderBy(p => p.Price).ToList());
				default:
					return BadRequest("Name' veya 'Price' yazılmalı.");
			}
		}

		[HttpGet("{id}")]
		public ActionResult<Item> GetById(int id)
		{
			var item = items.FirstOrDefault(i => i.Id == id);

			if (item == null)
			{
				return NotFound(); // 404 Not Found
			}

			return Ok(item);
		}

		// POST: api/items
		[HttpPost]
		public ActionResult<Item> Post([FromBody] Item newItem)
		{
			newItem.Id = items.Count + 1;
			items.Add(newItem);

			return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
		}

		// PUT: api/items/1
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] Item updatedItem)
		{
			var existingItem = items.FirstOrDefault(i => i.Id == id);

			if (existingItem == null)
			{
				return NotFound(); // 404 Not Found
			}

			existingItem.Name = updatedItem.Name;
			existingItem.Description = updatedItem.Description;

			return NoContent(); // 204 No Content
		}
		
		[HttpPut("Query")]
		public IActionResult PutQuery(int id, string Name,string Description)
		{
			var existingItem = items.FirstOrDefault(i => i.Id == id);

			if (existingItem == null)
			{
				return NotFound(); // 404 Not Found
			}

			existingItem.Name = Name;
			existingItem.Description = Description;

			return NoContent(); // 204 No Content
		}

		// DELETE: api/items/1
		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			var itemToRemove = items.FirstOrDefault(i => i.Id == id);

			items.Remove(itemToRemove);

			return Ok(); 
		}

		// PATCH: api/items/1
		[HttpPatch("{id}")]
		public IActionResult Patch(int id, [FromBody] JsonPatchDocument<Item> patchDocument)
		{
			var item = items.FirstOrDefault(i => i.Id == id);

			if (item == null)
			{
				return NotFound(); // 404 Not Found
			}

			patchDocument.ApplyTo(item, ModelState);

			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState); // 400 Bad Request
			}

			return NoContent(); // 204 No Content
		}
	}
}

