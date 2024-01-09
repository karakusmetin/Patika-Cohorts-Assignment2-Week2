using GameItems.Dto;
using GameItems.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameItems.Data
{
	public interface IItemsInterface
	{
		Item GetItemsById(int id);
		IEnumerable<Item> GetAllItems();
		void CrateItem(Item item);
		Boolean DeleteItem(int id);
		List<Item> NameOfList(string name);
		int GetCount();
		UpdatedItem UpdatedItem(int id,UpdatedItem item);
	}
}
