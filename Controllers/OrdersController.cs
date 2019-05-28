using lab6.Data;
using lab6.Models;
using lab6.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace lab6.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private STOContext _context;
        public OrdersController(STOContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        [Produces("application/json")]
        public List<OrdersViewModel> Get()
        {
            var list = _context.Orders.Include(s => s.Car).Include(e => e.Worker).Select(o =>
                new OrdersViewModel
                {
                    OrderID = o.OrderID,
                    CarID = o.CarID,
                    WorkerID = o.WorkerID,
                    Model = o.Car.Model,
                    FioWorker = o.Worker.FioWorker,
                    DateReceipt = o.DateReceipt,
                    DateCompletion = o.DateCompletion,
                });
            return list.OrderByDescending(t => t.OrderID).Take(20).ToList();
        }

        [HttpGet("cars")]
        [Produces("application/json")]
        public IEnumerable<Car> GetCars()
        {
            return _context.Cars.ToList();
        }

        [HttpGet("workers")]
        [Produces("application/json")]
        public IEnumerable<Worker> GetWorkers()
        {
            return _context.Workers.ToList();
        }


        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            OrdersViewModel order = _context.Orders.Include(s => s.Car).
                Include(e => e.Worker).Select(o =>
               new OrdersViewModel
               {
                   OrderID = o.OrderID,
                   WorkerID = o.WorkerID,
                   CarID = o.CarID,
                   Model = o.Car.Model,
                   FioWorker = o.Worker.FioWorker,
                   DateReceipt = o.DateReceipt,
                   DateCompletion = o.DateCompletion
               }).FirstOrDefault(x => x.OrderID == id);
            if (order == null)
                return NotFound();
            return new ObjectResult(order);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            _context.Orders.Add(order);
            _context.SaveChanges();
            return Ok(order);
        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }
            if (!_context.Orders.Any(x => x.OrderID == order.OrderID))
            {
                return NotFound();
            }

            _context.Update(order);
            _context.SaveChanges();


            return Ok(order);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Order order = _context.Orders.FirstOrDefault(x => x.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Ok(order);
        }
    }
}
