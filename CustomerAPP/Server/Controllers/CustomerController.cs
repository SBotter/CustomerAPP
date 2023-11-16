
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerAPP.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _context;
        public CustomerController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            var customers = await _context.Customers
                .Include(c => c.Company)
                .ToListAsync();
            return Ok(customers);
        }
        [HttpGet("companies")]
        public async Task<ActionResult<List<Company>>> GetCompanies()
        {
            var companies = await _context.Companies.ToListAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Customer>>> GetSingleCustomer(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Company)
                .FirstOrDefaultAsync(c => c.Id == id);

            if(customer == null)
            {
                return NotFound("No Customer Found");
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<List<Customer>>> CreateCustomer(Customer customer)
        {
            customer.Company = null;
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return Ok(await GetDbCustomers());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Customer>>> UpdateCustomer(Customer customer, int id)
        {
            var dbCustomer = await _context.Customers
                .Include(c => c.Company)
                .FirstOrDefaultAsync (c => c.Id == id);

            if (dbCustomer == null)
                return NotFound("No Customer Found");

            dbCustomer.FirstName = customer.FirstName;
            dbCustomer.LastName = customer.LastName;
            dbCustomer.Email = customer.Email;
            dbCustomer.CompanyId = customer.CompanyId;

            await _context.SaveChangesAsync();
            return Ok(await GetDbCustomers());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Customer>>> DeleteCustomer(int id)
        {
            var dbCustomer = await _context.Customers
                .Include(c => c.Company)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (dbCustomer == null)
                return NotFound("No Customer Found");

            
            _context.Customers.Remove(dbCustomer);
            await _context.SaveChangesAsync();
            return Ok(await GetDbCustomers());
        }



        private async Task<List<Customer>> GetDbCustomers()
        {
            return await _context.Customers.Include(c => c.Company).ToListAsync();
        }
    }

}
