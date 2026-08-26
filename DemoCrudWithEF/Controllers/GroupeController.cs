using DemoCrudWithEF.Domain.Commands;
using DemoCrudWithEF.Domain.Entities;
using DemoCrudWithEF.Domain.Repositories;
using DemoCrudWithEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tools.Results;

namespace DemoCrudWithEF.Controllers
{
    public class GroupeController(IGroupeRepository groupeRepository) : Controller
    {
        private readonly IGroupeRepository _groupeRepository = groupeRepository;

        // GET: GroupeController
        public ActionResult Index()
        {
            return View(Enumerable.Empty<Groupe>());
        }

        // GET: GroupeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GroupeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GroupeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateGroupeForm form)
        {
            if(!ModelState.IsValid)
            {
                return View(form);
            }

            Result result = _groupeRepository.Handle(new AddGroupeCommand(form.Nom));
            if(result.IsFailure)
            {
                ModelState.AddModelError("", result.Error.ToString());
                return View(form);
            }   
            
            return RedirectToAction("Index");

        }

        // GET: GroupeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GroupeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GroupeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GroupeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }

    public class X
    {
        public string? Nom { get; set; }
    }
}
