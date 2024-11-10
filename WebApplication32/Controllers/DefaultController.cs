using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication32.Models;

namespace WebApplication32.Controllers
{

    public class DefaultController : Controller
    {
        RegistrationContext db = new RegistrationContext();

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Registration reg)
        {
             if (ModelState.IsValid)
            {
                if (checkifuserexists(reg) == false)
                {
                    db.registration.Add(reg);
                    int rec = db.SaveChanges();

                    if (rec > 0)
                    {
                        ViewBag.Success = "Registration Successful";
                    }
                    else
                    {
                        ViewBag.Error = "Error";
                    }
                }
                else
                {
                    ViewBag.Error = "Email Already Exists";
                }
             
                    


           
             
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public bool checkifuserexists(Registration userexit)
        {
          var exists =  db.registration.FirstOrDefault(u => u.Email == userexit.Email);

            if(exists == null)
            {
                return false;
            }
            return true;
        }
        [HttpPost]
        public ActionResult Login(Registration reg)
        {
            
             var login = db.registration.FirstOrDefault(u => u.Email == reg.Email && u.Password == reg.Password);
                if(login != null)
                {
                    return RedirectToAction("Movies");
                }
                ViewBag.Error = "Incorrect Email or Password";
               
  
            return View();
        }

        public ActionResult Movies()
        {
            MoviesViewModel movies = new MoviesViewModel();
            movies.listofmovies = db.movies.ToList();
            return View(movies);
        }
        [HttpPost]
        public ActionResult Movies(MoviesViewModel vm)
        {
            if (ModelState.IsValid)
            {
                db.movies.Add(vm.newmovie);
                int rec = db.SaveChanges();
                if (rec > 0)
                {
                    ViewBag.Success = "Movie Added";
                    return RedirectToAction("Movies");
                }
                else
                {
                    ViewBag.Error = "Erro Movie Not Added";
                }
            }

            MoviesViewModel movies = new MoviesViewModel();
            movies.listofmovies = db.movies.ToList();
            return View(movies);
        }

        public ActionResult Edit(int id)
        {
            var editmovie = db.movies.Find(id);
            if(editmovie == null)
            {
                return HttpNotFound();
            }
            return View(editmovie);
        }
        [HttpPost]
        public ActionResult Edit(Movies mv)
        {
            if (ModelState.IsValid)
            {
                var editmovie = db.movies.Find(mv.Id);

                if(editmovie == null)
                {
                    return HttpNotFound();
                }
                editmovie.Name = mv.Name;
                editmovie.Year = mv.Year;
                editmovie.Director = mv.Director;

                db.SaveChanges();

                return RedirectToAction("Movies");

            }

            return View();
        }
        public ActionResult Delete(int id)
        {
            var deletemovie = db.movies.Find(id);
            if (deletemovie == null)
            {
                return HttpNotFound();
            }
            return View(deletemovie);

        }
        [HttpPost]
        public ActionResult Delete(Movies mv)
        {
            var delmovie = db.movies.Find(mv.Id);
            if(delmovie == null)
            {
                return HttpNotFound();
            }

            db.movies.Remove(delmovie);
            db.SaveChanges();
            return RedirectToAction("Movies");
        }
    }
}