using AutoMapper;
using InsuranceBillingApi.Data;
using InsuranceBillingApi.Dtos;
using InsuranceBillingApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly InsuranceDbContext _context;
    private readonly IMapper _mapper;

    public ClientsController(InsuranceDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/clients
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientWithPoliciesDto>>> GetClients()
    {
        var clients = await _context.Clients
            .Include(c => c.Policies)
            .ToListAsync();

        var dtoList = _mapper.Map<List<ClientWithPoliciesDto>>(clients);
        return Ok(dtoList);
    }

    // GET: api/clients/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientWithPoliciesDto>> GetClient(int id)
    {
        var client = await _context.Clients
            .Include(c => c.Policies)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (client == null)
            return NotFound();

        var dto = _mapper.Map<ClientWithPoliciesDto>(client);
        return Ok(dto);
    }

    // POST: api/clients
    [HttpPost]
    public async Task<ActionResult<ClientWithPoliciesDto>> CreateClient(ClientDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = _mapper.Map<Client>(dto);
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<ClientWithPoliciesDto>(client);
        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, resultDto);
    }

    [HttpPost("with-policies")]
    public async Task<ActionResult<ClientWithPoliciesDto>> CreateClientWithPolicies(ClientCreateWithPoliciesDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = new Client
        {
            Name = dto.Name,
            Email = dto.Email,
            Policies = dto.Policies.Select(p => new InsurancePolicy
            {
                PolicyNumber = p.PolicyNumber,
                CoverageType = p.CoverageType,
                Premium = p.Premium,
                StartDate = p.StartDate,
                EndDate = p.EndDate
            }).ToList()
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        var resultDto = _mapper.Map<ClientWithPoliciesDto>(client);
        return CreatedAtAction(nameof(GetClient), new { id = client.Id }, resultDto);
    }

    // PUT: api/clients/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(int id, ClientDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var client = await _context.Clients.FindAsync(id);
        if (client == null)
            return NotFound();

        _mapper.Map(dto, client);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/clients/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(int id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
            return NotFound();

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
