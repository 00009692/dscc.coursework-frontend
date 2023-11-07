using System.Text;
using System.Text.Json;
using Frontend.Models;

namespace Frontend.Service;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiBaseUrl;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    
    public ApiService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiBaseUrl = configuration["ApiBaseUrl"];
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    
    public async Task<IEnumerable<Employee>> GetAllEmployees()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>(_apiBaseUrl + $"Employees");
    }

    public async Task<Employee> GetEmployeeById(int id)
    {
        return await _httpClient.GetFromJsonAsync<Employee>(_apiBaseUrl + $"Employees/{id}");
    }
    
    public async Task CreateEmployee(EmployeeDto employeeDto)
    {
        var json = JsonSerializer.Serialize(employeeDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(_apiBaseUrl + "Employees", content);
        response.EnsureSuccessStatusCode();
    }
    
    public async Task UpdateEmployee(int employeeId, EmployeeUpdateDto employeeDto)
    {
        var json = JsonSerializer.Serialize(employeeDto);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync(_apiBaseUrl + "Employees/" + employeeId, content);
        response.EnsureSuccessStatusCode();
    }
    
    // Method to delete a post by ID
    public async Task DeleteEmployeeById(int id)
    {
        var response = await _httpClient.DeleteAsync(_apiBaseUrl + $"Employees/{id}");
        response.EnsureSuccessStatusCode();
    }
}