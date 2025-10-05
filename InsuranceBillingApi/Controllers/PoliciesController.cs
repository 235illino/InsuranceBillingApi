using AutoMapper;
using InsuranceBillingApi.Data;
using InsuranceBillingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PoliciesController : ControllerBase
{
    private readonly InsuranceDbContext _context;
    private readonly IMapper _mapper;

    public PoliciesController(InsuranceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/policies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsurancePolicyDto>>> GetPolicies()
    {
        var policies = await _context.Policies.ToListAsync();
        var dtoList = _mapper.Map<List<InsurancePolicyDto>>(policies);
        return Ok(dtoList);
    }

    // GET: api/policies/5
    [HttpGet("{id}")]
    public async Task<ActionResult<InsurancePolicyDto>> GetPolicy(int id)
    {
        var policy = await _context.Policies.FindAsync(id);
        if (policy == null)
            return NotFound();

        var dto = _mapper.Map<InsurancePolicyDto>(policy);
        return Ok(dto);
    }

    // POST: api/policies
    [HttpPost]
    public async Task<ActionResult<InsurancePolicyDto>> CreatePolicy(InsurancePolicyDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var policy = _mapper.Map<InsurancePolicy>(dto);
        _context.Policies.Add(policy);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<InsurancePolicyDto>(policy);
        return CreatedAtAction(nameof(GetPolicy), new { id = policy.Id }, resultDto);
    }

    // PUT: api/policies/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePolicy(int id, InsurancePolicyDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var policy = await _context.Policies.FindAsync(id);
        if (policy == null)
            return NotFound();

        _mapper.Map(dto, policy);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/policies/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePolicy(int id)
    {
        var policy = await _context.Policies.FindAsync(id);
        if (policy == null)
            return NotFound();

        _context.Policies.Remove(policy);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
