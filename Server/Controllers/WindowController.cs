using BlazorIntus2.Shared.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorIntus2.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WindowController : Controller
    {
        private readonly DataContext db;        

        public WindowController(DataContext db)
        {
            this.db = db;            
        }

        [HttpGet]
        [Route("/api/Window/ListW")]
        public async Task<List<Data.Window>> ListW()        
        {                        
            return await await Task.FromResult(db.Windows.ToListAsync());
        }

        [HttpGet]
        [Route("/api/Window/ListSubs")]
        public async Task<List<Data.SubElement>> ListSubs(int id)
        {                        
            return await await Task.FromResult(db.SubElements.Where(z => z.Window.Id == id).ToListAsync());
        }

        [HttpPost]
        public IActionResult AddWin(Data.Window window)
        {
            try
            {
                if (window.Id == 0)
                {
                    Console.WriteLine(window.ToString());
                    db.Windows.Add(window);
                    db.SaveChanges();
                    return Ok(window);                    
                }
                else 
                {
                    Console.WriteLine(window.ToString());
                    db.Windows.Update(window);
                    db.SaveChanges();
                    return Ok(window);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(window.ToString());
                return Json(ex);
            }
        }

        [HttpPost]
        [Route("/api/Window/AddSubEl")]
        public IActionResult AddSubEl(Data.SubElement se)
        {
            try
            {   
                
                db.SubElements.Add(se);
                db.SaveChanges();
                return Ok(se);
            }
            catch
            {
                throw;
            }
        }
        
        [Route("/api/Window/DelSubEl/{id}")]
        public IActionResult DelSubEl(int id)
        {
            try
            {
                var el = db.SubElements.Find(id);

                db.SubElements.Remove(el);
                db.SaveChanges();
                return Ok(el);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {            
            try
            {
                Data.Window? w = db.Windows.Find(id);
                if (w != null)
                {
                    return Ok(w);
                }
            }
            catch (Exception ex)
            {

                return Json(ex);                
            }

            //return NotFound();
            //return StatusCode(500, new { error = "Oops" });            
            var data = new {err = "alarm in GET"};
            return Json(data);                        
        }
        
        [HttpDelete("DelW/{id}")]        
        public IActionResult Del(int id)
        {
            try
            {
                Data.Window? w = db.Windows.Find(id);
                if (w != null)
                {
                    Console.WriteLine(w.ToString());
                    db.Windows.Remove(w);
                    db.SaveChanges();
                    return Ok();
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