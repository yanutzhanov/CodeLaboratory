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
        private readonly IUsersService _usersService;
        public ProjectsController(IProjectsService projectService, IUsersService usersService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        [AllowAnonymous]
        [Route("{controller}/{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            return View("Project", await _projectService.Get(id));
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
            if (User.Identity.IsAuthenticated)
                projects = projects.Where(p => !p.Users.Select(u => u.Login).ToList().Contains(User.Identity.Name));
            projects = projects.Where(p => !p.Finished);
            return View("Projects", projects);
        }

        [HttpGet]
        public IActionResult CreateProject(Project model)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProjectPost(Project model)
        {
            await _projectService.Create(model, User.Identity.Name);
            return RedirectToAction("CurrentProjects", "Account", new { login = User.Identity.Name });
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
        public async Task<IActionResult> JoinToProject(int projectId)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    await _projectService.JoinToProject(projectId, User.Identity.Name);
                    return RedirectToAction("GetProject", new { id = projectId });
                }
                catch
                {
                    return BadRequest();
                }
            }
            return RedirectToAction("Login", "Account");
            
        }
    }
}
