using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestTask.Web.Controllers.Bases;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public abstract class CandidatesAppControllerBase() : ControllerBase
{
}
