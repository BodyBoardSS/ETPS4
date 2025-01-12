﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WikiAPI.Models;

namespace WikiAPI.Controllers
{
    [Route("api/v1/[controller]")]
    public class SupplierController : Controller
    {
        private readonly WikiSalesDbContext _context;

        public SupplierController(WikiSalesDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Supplier> GetSupplier()
        {
            return _context.Supplier.ToList();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetSupplierById(int id)
        {
            var sup = this._context.Supplier.SingleOrDefault(ct => ct.supId == id);
            if (sup != null)
            {
                return Ok(sup);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{name}")]
        public IActionResult GetSupplierByName(string name)
        {
            var info = this._context.Supplier.SingleOrDefault(ct => ct.supTradename == name);

            if (info == null)
            {
                return new NoContentResult();
            }
            else
            {
                return Ok(info);
            }
        }

        [HttpPost]
        public IActionResult AddSupplier([FromBody] Supplier sup)
        {

            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }

            this._context.Supplier.Add(sup);
            this._context.SaveChanges();
            return Created($"suppplier/{sup.supId}", sup);
        }

        [HttpPut("{id}")]
        public IActionResult PutSupplier(int id, [FromBody] Supplier sup)
        {

            var target = _context.Supplier.FirstOrDefault(ct => ct.supId == id);
            if (target == null)
            {
                return NotFound();
            }
            else
            {
                target.supId = sup.supId;
                target.supTradename = sup.supTradename;
                target.supPerId = sup.supPerId;

                _context.Supplier.Update(target);
                _context.SaveChanges();
                return new NoContentResult();
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSupplier(int id)
        {
            var target = _context.Supplier.FirstOrDefault(ct => ct.supId == id);
            if (!this.ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _context.Supplier.Remove(target);
                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
