using BlazorIntus2.Shared.DataModel;
using BlazorIntus2.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BlazorIntus2.Server.Services
{
	public class OrderManager : IOrder
	{
		readonly DataContext db;

		public OrderManager(DataContext _db)
		{
			db = _db;
		}

		public List<Data.Order> GetOrdersDetails()
		{
			try
			{
                return db.Order.ToList();
            }
			catch
			{
				throw;
			}
		}

		public void AddOrder(Data.Order orders)
		{
			try
			{
				db.Order.Add(orders);
				db.SaveChanges();
			}
			catch
			{
				throw;
			}
		}


		public void UpdateOrder(Data.Order orders)
		{
			try
			{
				db.Entry(orders).State = EntityState.Modified;
				db.SaveChanges();
			}
			catch
			{
				throw;
			}
		}


		public Data.Order GetOrderData(int id)
		{
			try
			{
				Data.Order? orders = db.Order.Find(id);
				if (orders != null)
				{
					return orders;
				}
				else
				{
					throw new ArgumentNullException();
                }
			}
			catch
			{
				throw;
			}
		}


		public void DeleteOrder(int id)
		{
			try
			{
				Data.Order? orders = db.Order.Find(id);
				if (orders != null)
				{
					db.Order.Remove(orders);
					db.SaveChanges();
				}
				else
				{
					throw new ArgumentNullException();
				}
			}
			catch
			{
				throw;
			}
		}

	}
}