using BlazorIntus2.Shared.DataModel;
namespace BlazorIntus2.Server.Interfaces
{
	public interface IOrder
	{
		public List<Data.Order> GetOrdersDetails();
		public void AddOrder(Data.Order orders);		
		public Data.Order GetOrderData(int id);
		public void UpdateOrder(Data.Order orders);
		public void DeleteOrder(int id);
	}
}