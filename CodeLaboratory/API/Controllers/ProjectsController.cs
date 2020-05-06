using CodeLaboratory.Domain;
using CodeLaboratory.Enteties;
using CodeLaboratory.Services.Abstract;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeLaboratory.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/{Controller}")]
    public class ProjectsController : Controller
    {
        private readonly IProjectsService _projectService;


        public ProjectsController(IProjectsService projectService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _projectService.Get(id));
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _projectService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project model)
        {
            await _projectService.Create(model, User.Identity.Name);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Project model)
        {
            await _projectService.Update(model);
            return Ok(model);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Project model)
        {
            await _projectService.Delete(model);
            return Ok(model);
        }

        [HttpPost("join")]
        public async Task<IActionResult> Join(int projectId)
        {
            await _projectService.JoinToProject(projectId, User.Identity.Name);

            return Ok(await _projectService.Get(projectId));
        }
     }
}
