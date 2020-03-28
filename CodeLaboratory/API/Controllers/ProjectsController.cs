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
        public IActionResult Get(int id)
        {
            return Ok(_projectService.Get(id));
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_projectService.GetAll());
        }

        [HttpPost]
        public IActionResult Create(Project model)
        {
            _projectService.Create(model, User.Identity.Name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Project model)
        {
            _projectService.Update(model);
            return Ok(model);
        }

        [HttpDelete]
        public IActionResult Delete(Project model)
        {
            _projectService.Delete(model);
            return Ok(model);
        }

        [HttpPost("join")]
        public IActionResult Join(int projectId)
        {
            _projectService.JoinToProject(projectId, User.Identity.Name);

            return Ok(_projectService.Get(projectId));
        }
     }
}
