using DemoCrudWithEF.Domain.Commands;
using DemoCrudWithEF.Domain.Entities;
using DemoCrudWithEF.Domain.Queries;
using DemoCrudWithEF.Domain.Repositories;
using DemoCrudWithEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tools.Results;

namespace DemoCrudWithEF.Controllers
{
    public class GroupeController(IGroupeRepository groupeRepository, IAlbumRepository albumRepository) : Controller
    {
        private readonly IGroupeRepository _groupeRepository = groupeRepository;
        private readonly IAlbumRepository _albumRepository = albumRepository;

        // GET: GroupeController
        public IActionResult Index()
        {
            Result<IEnumerable<Groupe>> result = _groupeRepository.Handle(new GetGroupesQuery());

            if (result.IsFailure)
            {
                ViewBag.ErrorMessage = result.Error.ToString();
                return View(Enumerable.Empty<Groupe>());
            }

            return View(result.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            Result<Groupe> result = await _groupeRepository.HandleAsync(new GetGroupeByIdQuery(id, true));

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error.ToString();
                return RedirectToAction("Index");
            }

            return View(new DetailsGroupForm() { Groupe = result.Data, AddAlbumForm = new AddAlbumForm() { GroupeId = result.Data.Id } });
        }

        [HttpPost]
        public async Task<IActionResult> AddAlbum(AddAlbumForm form)
        {
            if(ModelState.IsValid)
            {
                await _albumRepository.HandleAsync(new CreateAlbumCommand(form.Titre, form.Annee, form.GroupeId), CancellationToken.None);
            }

            return RedirectToAction("Details", new { id = form.GroupeId });
        }

        // GET: GroupeController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroupeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupeForm form)
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
        public async Task<IActionResult> Edit(int id)
        {
            Result<Groupe> result = await _groupeRepository.HandleAsync(new GetGroupeByIdQuery(id));

            if (result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error.ToString();
                return RedirectToAction("Index");
            }

            EditGroupeForm form = new EditGroupeForm() { Nom = result.Data.Nom };

            return View(form);
        }

        // POST: GroupeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EditGroupeForm form)
        {
            Result result = _groupeRepository.Handle(new UpdateGroupeCommand(id, form.Nom));

            if(result.IsFailure)
            {
                TempData["ErrorMessage"] = result.Error.ToString();
            }
            
            return RedirectToAction("Index");
        }        
    }
}
