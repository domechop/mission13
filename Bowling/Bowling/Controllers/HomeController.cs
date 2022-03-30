using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Bowling.Models;
using Microsoft.EntityFrameworkCore;


namespace Bowling.Controllers
{
    public class HomeController : Controller
    {
      
        private BowlingDbContext _repo { get; set; }

        //constructor
        public HomeController(BowlingDbContext temp)
        {
            _repo = temp;
        }

        public IActionResult Index(string teamName)
        {
            ViewBag.TeamName = teamName ?? "Home";
            var blah = _repo.Bowlers
                .Where (x => x.Team.TeamName == teamName || teamName == null)
                .Include(x => x.Team)
                .ToList();

            return View(blah);
        }


        [HttpGet]
        public IActionResult Form()
        {

            ViewBag.Teams = _repo.Teams.ToList();

            return View("Form");
        }

        [HttpPost]
        public IActionResult Form(Models.Bowler b)
        {
           
            if (ModelState.IsValid)
            {
                b.BowlerID = _repo.Bowlers.Count() + 1;
                _repo.Add(b);
                _repo.SaveChanges();

                return RedirectToAction("Index");

            }

            else
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int bowlerId)
        {
            ViewBag.Added = false;
            ViewBag.Teams = _repo.Teams.ToList();

            var bowler = _repo.Bowlers.Single(bowler => bowler.BowlerID == bowlerId);

            return View("Form", bowler);
        }

        [HttpPost]
        public IActionResult Edit(Bowler bowler)
        {
            if (ModelState.IsValid)
            {
                _repo.Update(bowler);
                _repo.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bowler);
        }
        public IActionResult Delete(int bowlerId)
        {
            var bowler = _repo.Bowlers.Single(bowler => bowler.BowlerID == bowlerId);

            _repo.Bowlers.Remove(bowler);
            _repo.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
