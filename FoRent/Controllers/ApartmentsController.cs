﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoRent.Models;
using Microsoft.AspNetCore.Http;
using FoRent.Controllers;




namespace FoRent.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly FoRentContext _context;

        public ApartmentsController(FoRentContext context)
        {
            _context = context;
        }
        // GET: Apartments/EditControl
        public IActionResult EditControl(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.id = id;
            ViewBag.amenities = _context.Apartment.Where(a => a.Id == id).Select(a => a.Amenities.Id).First().ToString();
            ViewBag.policy = _context.Apartment.Where(a => a.Id == id).Select(a => a.Policy.Id).First().ToString();
            ViewBag.location = _context.Apartment.Where(a => a.Id == id).Select(a => a.Location.Id).First().ToString();
            ViewBag.image = _context.Apartment.Where(a => a.Id == id).Select(a => a.Image.Id).First().ToString();
            TempData["ApartmentId"] = id;
            return View();
        }
        // GET: Apartments
        public async Task<IActionResult> Index(string city, DateTime checkIn, DateTime checkOut, int adult, int child)
        {
            ViewBag.NumOfAdult = adult;
            ViewBag.NumOfChild = child;
            ViewBag.City = child;
            DateTime check = new DateTime();
            var databaseContext = _context.Apartment.Include(a => a.Amenities).Include(l => l.Location).Include(r => r.Renter).Include(p => p.Policy).Include(i=>i.Image);
            if (checkIn.Equals(check) && checkOut.Equals(check))
            {
                return View(await databaseContext.Where(p => p.Location.City.Contains(city) && ((p.Amenities.NumOfPersons) >= (adult + child))).ToListAsync());
            }
            else if (!(checkIn.Equals(check)) && checkOut.Equals(check))
            {
                checkOut = checkIn.AddDays(1);
            }
            else if ((checkIn.Equals(check)) && !(checkOut.Equals(check)))
            {
                TempData["ErrorDate"] = "*לא ניתן להזין צ'אק אאוט ללא צ'אק אין";
                return RedirectToAction("Home");
            }
            else if (!(checkIn.Equals(check)) && !(checkOut.Equals(check))&& (checkIn>checkOut))
            {
                TempData["ErrorDate"] = "*צ'אק אאוט חייב להיות מאוחר יותר מהצ'אק אין";
                return RedirectToAction("Home");
            }

            
            
             var result=  from a in _context.Apartment
                          join b in _context.ApartmentAvailability.Include(c=>c.Availability) on a.Id equals b.ApartmentId
                          where b.Availability.NotAvailable >= checkIn && b.Availability.NotAvailable <= checkOut
                          select a;
            result.Distinct();


            
            var query = from c in _context.Apartment
                        where !(from o in result
                                select o.Id).Contains(c.Id)
                        select c;
            
            
            return View(await query.Include(a => a.Amenities).Include(l => l.Location).Include(r => r.Renter).Include(p => p.Policy).Include(i => i.Image).Where(p => p.Location.City.Contains(city) && ((p.Amenities.NumOfPersons) >= (adult + child))).ToListAsync());
        }

        //public async Task<IActionResult> Index1(double price)
        //{

        //    ViewBag.PriceAdult = price;
        //    var order = from a in _context.Apartment
        //                orderby a.PriceAdult
        //                select a;



        //    return View( _context.Apartment.Include(a => a.PriceAdult));
        //}
        public async Task<IActionResult> Home()
        {
            return View(await _context.Apartment.Include(a => a.Amenities).Include(l => l.Location).Include(r => r.Renter).Include(p => p.Policy).Include(i => i.Image).ToListAsync());
        }




        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment.Include(a => a.Amenities).Include(p => p.Policy).Include(l => l.Location).Include(i => i.Image)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {

            return View();

        }

        // POST: Apartments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PriceAdult,PriceChild")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                var check = HttpContext.Session.GetString("Role");
                if (HttpContext.Session.GetString("username") != null)
                    if (HttpContext.Session.GetString("Role") == "FoRent.Models.Renter")
                     {
                    apartment.Renter = _context.Renter.Where(r => r.Username == HttpContext.Session.GetString("username")).FirstOrDefault();
                     }
                    else
                    {
                        var temp=_context.User.Where(r => r.Username == HttpContext.Session.GetString("username")).FirstOrDefault();
                        var renter = new Renter();
                        renter.FirstName = temp.FirstName;
                        renter.LastName = temp.LastName;
                        renter.Mail = temp.Mail;
                        renter.password = temp.password;
                        renter.Username = temp.Username;
                        renter.Phone = temp.Phone;
                        renter.Orders = temp.Orders;
                        _context.Add(renter);
                        _context.User.Remove(temp);
                        await _context.SaveChangesAsync();
                        apartment.Renter = _context.Renter.OrderByDescending(u => u.Id).FirstOrDefault();
                        HttpContext.Session.SetString("Role", apartment.Renter.GetType().ToString());
                    }
                else
                {
                    apartment.Renter = _context.Renter.OrderByDescending(u => u.Id).FirstOrDefault();
                }
                apartment.Location = _context.Location.OrderByDescending(u => u.Id).FirstOrDefault();
                apartment.Amenities = _context.ApartmentAmenities.OrderByDescending(u => u.Id).FirstOrDefault();
                apartment.Policy = _context.Policy.OrderByDescending(u => u.Id).FirstOrDefault();
                apartment.Image = _context.Image.OrderByDescending(u => u.Id).FirstOrDefault();


                _context.Add(apartment);
                await _context.SaveChangesAsync();
                TempData["Availability"] = apartment.Id;
              
                return RedirectToAction("Create","ApartmentAvailabilities");
            }
            ViewBag.Success = false;
            return View(apartment);

        }
  
            
        
        public async Task<IActionResult> SpecialIndex()
        {
            var result = from a in _context.Apartment
                         where a.Renter.Username== HttpContext.Session.GetString("username")
                         select a;
            result.DefaultIfEmpty();
            return View(await result.Include(a => a.Amenities).Include(l => l.Location).Include(r => r.Renter).Include(p => p.Policy).Include(i => i.Image).ToListAsync());
        }
        // GET: Apartments/EditControl
    

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment.SingleOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PriceAdult,PriceChild,Amenties,Policy,Location,Image,ApartmentAvailabilities")] Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
               

                return RedirectToAction("EditControl", "Apartments", new { id = id });
            }
            return View(apartment);
        }

        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartment
                .SingleOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _context.Apartment.Include(a => a.Amenities).Include(l => l.Location).Include(p => p.Policy).Include(i => i.Image).SingleOrDefaultAsync(m => m.Id == id);
            var location = _context.Location.Where(n => n.Id == apartment.Location.Id).FirstOrDefault();
            var amenities= _context.ApartmentAmenities.Where(n => n.Id == apartment.Amenities.Id).FirstOrDefault();
            var image = _context.Image.Where(n => n.Id == apartment.Image.Id).FirstOrDefault();
            var policy = _context.Policy.Where(n => n.Id == apartment.Policy.Id).FirstOrDefault();
            var available = _context.ApartmentAvailability.Where(n => n.ApartmentId == id).ToList();

            _context.Apartment.Remove(apartment);
            
            await _context.SaveChangesAsync();
           
            _context.Location.Remove(location);
            _context.ApartmentAmenities.Remove(amenities);
            _context.Image.Remove(image);
            _context.Policy.Remove(policy);
            foreach(ApartmentAvailability n in available)
            {
                _context.ApartmentAvailability.Remove(n);
            }
           
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Home));
        }

        public IActionResult Success()
        {

            return View();

        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartment.Any(e => e.Id == id);
        }
    }
}
