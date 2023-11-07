using Frontend.Models;

namespace Frontend.Service;

public interface IApiService
{
    Task<IEnumerable<Employee>> GetAllEmployees();
    Task<Employee> GetEmployeeById(int id);
    Task CreateEmployee(EmployeeDto employeeDto);
    Task UpdateEmployee(int postId, EmployeeUpdateDto employeeUpdateDto);
    Task DeleteEmployeeById(int id);
}