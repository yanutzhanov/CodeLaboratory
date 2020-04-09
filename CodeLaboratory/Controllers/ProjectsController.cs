using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeLaboratory.Domain;
using CodeLaboratory.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CodeLaboratory.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IProjectsService _projectService;

        public ProjectsController(IProjectsService projectService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        }

        [AllowAnonymous]
        [Route("{controller}/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return View(await _projectService.Get(id));
        }

        [AllowAnonymous]
        [Route("{controller}")]
        public async Task<IActionResult> Get(string language)
        {
            var projects = await _projectService.GetAll();
            if (!string.IsNullOrEmpty(language))
            {
                projects = projects.Where(p => p.Language == language);
            }
            return View("Projects", projects);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project model)
        {
            await _projectService.Create(model, User.Identity.Name);
            return RedirectToAction("Get");
        }

        [HttpPost]
        public async Task<IActionResult> Update(Project model)
        {
            await _projectService.Update(model);
            return RedirectToAction("Get", new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Project model)
        {
            await _projectService.Delete(model);
            return RedirectToAction("Get");
        }

        [HttpPost]
        public async Task<IActionResult> Join(int projectId)
        {
            await _projectService.JoinToProject(projectId, User.Identity.Name);

            return RedirectToAction("Get", new { id = projectId });
        }
    }
}
