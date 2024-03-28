using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BlazorIntus2.Shared;
using System;
using BlazorIntus2.Shared.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using BlazorIntus2.Server.Interfaces;
using static BlazorIntus2.Shared.DataModel.Data;
using Blazorise;


namespace BlazorIntus2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : Controller
    {
        private readonly DataContext db;
		private readonly IOrder _IOrder;

		public DataController(DataContext db, IOrder iOrder)
		{
			this.db = db;
            _IOrder = iOrder;
        }       

        [HttpGet]
		public async Task<List<Data.Order>> Get()
        {
			return await Task.FromResult(_IOrder.GetOrdersDetails());
		}

        [HttpGet("{id}")]
        [Route("/api/Data/GetOrd/{id}")]
        public IActionResult GetOrd(int id)
		{			
            try
            {
                Order order = _IOrder.GetOrderData(id);
                if (order != null)
                {
                    return Ok(order);
                }
                else 
                {
                    return StatusCode(500, new { error = "Oops" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { error = ex.Message });               
            }
        }

        [Route("/api/Data/GetOrderWindows/{id}")]
        public async Task<List<OrderWindow>> GetOrderWindows(int id)
        {
            return (List<OrderWindow>)db.OrderWindow.Where(z => z.OrderId == id).ToList();
        }

        [HttpPost]
        [Route("/api/Data/AddOrdEl")]
        public IActionResult AddOrdEl(OrderWindow orderWindow)
        {
            try
            {

                db.OrderWindow.Add(orderWindow);
                db.SaveChanges();                
                return Ok(orderWindow);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("GetOrderWindow/{orderId}/{windowId}")]
        public OrderWindow GetOrderWindow(int orderId, int windowId)
        {
            return db.OrderWindow.FirstOrDefault(ow => ow.OrderId == orderId && ow.WindowId == windowId);          
        }

        [HttpDelete]
        [Route("/api/Data/AddOrdElDexExistWin/{orderId}/{windowId}")]
        public IActionResult AddOrdElDexExistWin(int orderId, int windowId)
        {
            try
            {
                var ordw = db.OrderWindow.FirstOrDefault(ow => ow.WindowId == windowId && ow.OrderId == orderId);
                db.OrderWindow.Remove(ordw);
                db.SaveChanges();
                return Ok(ordw);
            }
            catch
            {
                throw;
            }
        }


        //Test for list
        [HttpPost]
        [Route("api/Data/UpdateOrderWindows")]
        public async Task<IActionResult> UpdateOrderWindows([FromBody] UpdateOrderWindowsModel  model)
        {
            foreach (var window in model.SelectedWindows)
            {
                var existingOrderWindow = await db.OrderWindow
                    .FirstOrDefaultAsync(ow => ow.OrderId == window.OrderId && ow.WindowId == window.WindowId);

                if (existingOrderWindow == null)
                {
                    db.OrderWindow.Add(window);
                }
            }

            foreach (var window in model.DeselectedWindows)
            {
                var existingOrderWindow = await db.OrderWindow
                    .FirstOrDefaultAsync(ow => ow.OrderId == window.OrderId && ow.WindowId == window.WindowId);

                if (existingOrderWindow != null)
                {
                    db.OrderWindow.Remove(existingOrderWindow);
                }
            }

            await db.SaveChangesAsync();

            return Ok();
        }


        public class UpdateOrderWindowsModel
        {
            public List<OrderWindow>? SelectedWindows { get; set; }
            public List<OrderWindow>? DeselectedWindows { get; set; }
        }

        [HttpPost]
		public void Post(Data.Order orders)
		{
			_IOrder.AddOrder(orders);
		}

		[HttpPut]
		public void Put(Data.Order orders)
		{
			_IOrder.UpdateOrder(orders);
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			_IOrder.DeleteOrder(id);
			return Ok();
		}

        [HttpGet]
        [Route("/api/Data/ListOrd")]
        public async Task<List<Data.Order>> ListW()
        {
            return await await Task.FromResult(db.Order.ToListAsync());
        }
    }
}

public static class ObjExt
{
    public static OrderWindow Clone(this OrderWindow myObj)
    {
        var myObjClone = new OrderWindow();
        myObjClone.Id = myObj.Id;
        myObjClone.OrderId = myObj.OrderId;
        //
        return myObjClone;
    }
}