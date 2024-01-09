using GameItems.Dto;
using GameItems.Entities;
using GameItems.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace GameItems.Data
{
	public class ItemsListRepository : IItemsInterface
	{
		private readonly List<Item> items = new()
		{
			new Item{ Id= 0, Name="Sword",Price = 9, Description="Basic Weapon" },
			new Item{ Id= 1, Name="Sytche",Price = 20, Description = "Magical weapon" },
			new Item{ Id= 2, Name="Shield",Price = 19, Description = "Basic Armor" }
		};

		public void CrateItem(Item item)
		{
			items.Add(item);
		}
		public Boolean DeleteItem(int id)
		{
			var itemToRemove = items.FirstOrDefault(i => i.Id == id);
			if (itemToRemove != null)
			{
				items.Remove(itemToRemove);
				return true;
			}
			else
			{
				return false;
			}

		}
		public IEnumerable<Item> GetAllItems()
		{
			return items;
		}

		public List<Item> NameOfList(string name)
		{
			var itemList = items.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();
			return itemList;
		}

		public int GetCount()
		{
			return items.Count();
		}

		public Item GetItemsById(int id)
		{
			var item = items.FirstOrDefault(i => i.Id == id);
			return item;
		}

		public UpdatedItem UpdatedItem(int id, UpdatedItem item)
		{
			var existingItem = items.FirstOrDefault(i => i.Id == id);
			existingItem.Price = item.Price;
			existingItem.Description = item.Description;

			var updateditem =existingItem.AsDto();

			return updateditem;

		}

		public UpdatedItem UpdateItemQuery(int id, string Name, string description)
		{
			var existingItem = items.FirstOrDefault(i => i.Id == id);
			existingItem.Name = Name;
			existingItem.Description = description;

			UpdatedItem updatedItem = new()
			{
				Price = existingItem.Price,
				Description = existingItem.Description,
			};

			return updatedItem;
		}
	}
}
