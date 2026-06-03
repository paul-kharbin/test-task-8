using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestTask.Infrastructure.Contact.Model;
using TestTask.Infrastructure.Contact.Services;
using TestTask.Web.Controllers.Bases;
using TestTask.Web.Models;

namespace TestTask.Web.Controllers.Api;

public sealed class CandidatesController(ICanditatesService canditatesService) : CandidatesAppControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCandidates([FromServices] IMapper mapper)
    {
        var candidates = await canditatesService.GetAllAsync();
        var result = candidates.Select(mapper.Map<CandidateResponse>).ToList();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCandidate([FromServices] IMapper mapper, CandidateAddRequest request)
    {
        var candidate = mapper.Map<Candidate>(request);

        candidate = await canditatesService.AddAsync(candidate);

        return Created($"/api/candidates/{candidate.Id}", mapper.Map<CandidateResponse>(candidate));
    }

    [HttpGet("analytics")]
    public async Task<IActionResult> GetAnalytics([FromServices] IMapper mapper)
    {
        var candidates = (await canditatesService.GetAllAsync()).OrderBy(c => c.DesiredSalary).ToList();
        var maxSalary = candidates.Max(c => c.DesiredSalary);
        var maxExperience = candidates.Max(c => c.ExperienceYears);
        var ratings = candidates
            .Select(c =>
            {
                // Чем меньше желаемая зарплата относительно максимальной, тем выше часть рейтинга за зарплату.
                var salaryScore = 1 - c.DesiredSalary / maxSalary;
                // Чем больше опыт относительно максимального опыта, тем выше часть рейтинга за опыт.
                var experienceScore = (decimal)c.ExperienceYears / maxExperience;
                // Итоговый рейтинг складывается из 40% оценки зарплаты и 60% оценки опыта.
                var score = (int)Math.Round(salaryScore * 40 + experienceScore * 60);

                return new CandidateRatingResponse
                {
                    Candidate = mapper.Map<CandidateResponse>(c),
                    Score = score
                };
            })
            .OrderByDescending(c => c.Score)
            .ToList();

        var candsCount = candidates.Count;

        return Ok(new AnalyticsResponse
        {
            CandidateCount = candsCount,
            AverageSalary = candsCount != 0 ? candidates.Average(x => x.DesiredSalary) : 0,
            AverageExperience = candsCount != 0 ? candidates.Average(x => x.ExperienceYears) : 0,
            Ratings = ratings
        });
    }
}
