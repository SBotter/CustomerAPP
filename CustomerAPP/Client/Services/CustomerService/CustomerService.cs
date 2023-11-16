using CustomerAPP.Client.Pages;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using CustomerAPP.Client.Index;
using CustomerAPP.Client.Index.CustomerIndex;
using System.Xml;

namespace CustomerAPP.Client.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public CustomerService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Company> Companies { get; set; } = new List<Company>();

        public async Task CreateCustomer(Customer customer)
        {
            var result = await _http.PostAsJsonAsync("api/customer", customer);
            await SetCustomers(result);
        }

        public async Task DeleteCustomer(int id)
        {
            var result = await _http.DeleteAsync($"api/customer/{id}");
            await SetCustomers(result);
        }

        public async Task GetCompanies()
        {
            var result = await _http.GetFromJsonAsync<List<Company>>("api/customer/companies");
            if (result != null)
            {
                Companies = result;
            }
        }

        public async Task GetCustomers()
        {
            var result = await _http.GetFromJsonAsync<List<Customer>>("api/customer");
            if(result !=null)
            {
                Customers = result;
                //var index = new CustomerIndex();
                //index.AddCustomerToIndex(Customers);

                //searchTest
                //var query = "sergio";
                //var results = index.Search(query);
                
            }
        }

        public async Task<Customer> GetSingleCustomer(int id)
        {
            var result = await _http.GetFromJsonAsync<Customer>($"api/customer/{id}");
            if (result != null)
            {
               return result;
            }
            throw new Exception("Customer not found.");
        }

        public async Task UpdateCustomer(Customer customer)
        {
            var result = await _http.PutAsJsonAsync($"api/customer/{customer.Id}", customer);
            await SetCustomers(result);
        }
        private async Task SetCustomers(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Customer>>();
            if (response != null)
            {
                Customers = response;
            }
            _navigationManager.NavigateTo("customers");
        }
    }
}
