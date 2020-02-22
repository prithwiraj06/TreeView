using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeView.BAL.Services;

namespace TreeView.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectTreeController : ControllerBase
    {
        private IProjectTreeService _projectTreeService;
        public ProjectTreeController(IProjectTreeService projectTreeService)
        {
            _projectTreeService = projectTreeService;
        }

        [HttpGet("GetProjectTreeData")]
        public IActionResult GetProjectTree()
        {
            return Ok(_projectTreeService.GetProjectTree());
        }
    }
}
