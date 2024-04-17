using Biblioteka.Models;
using Biblioteka.Context;
using Biblioteka.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Biblioteka.Repositories.DbImplementations;
using System.Diagnostics;

namespace Biblioteka.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly BibContext _context;
        public EmployeeRepository(BibContext context) : base(context)
        {
            _context = context;
        }
        public async Task Add(Employee employee)
        {
            /*var id = await GetLastId();
            employee.employeeId = id;*/

            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            /*try
            {
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.ToString());
            }*/

        }
        public Task<int> GetLastId()
        {
            var id = 1;
            var employees = GetAll();
            if (employees.Any())
                id = employees.Max(e => e.employeeId) + 1;
            return Task.FromResult(id);
        }

        public List<Employee> GetAll()
        {
            return _context.Employee
                .Include(a => a.employeeData)
                .Include(a => a.position)
                .ToList();
        }
        public Employee GetOne(int employeeid)
        {
            return _context.Employee
                .Include(a => a.employeeData)
                .Include(a => a.position).FirstOrDefault(a => a.employeeId == employeeid);
        }
        public Employee GetOneByMail(string email)
        {
            return _context.Employee
                .Include(a => a.employeeData)
                .Include(a => a.position).FirstOrDefault(a => a.email == email);
        }
        public Employee GetByMail(string email)
        {
            try
            {
                Debug.WriteLine("getbyemail function");
                return getAll().FirstOrDefault(employee => employee.email?.ToLowerInvariant() == email?.ToLowerInvariant());
            }
            catch (Exception ex)
            {
               
                Debug.WriteLine($"An error occurred while fetching the employee by email: {ex.Message}");
                return null;
            }
        }

        // do metody update, pomocnicza
        /* public Employee employeeToUpdate()
         {
             Employee employeeToUpdate = _context.Employee.Find(2);
             employeeToUpdate.name = "Jedrzej";
             employeeToUpdate.surname = "Brzeczyszczykiewicz";
             return employeeToUpdate;
         }*/

        public List<Employee> SearchEmployees(string searchTerm)
        {
            var query = _context.Employee
                .Include(a => a.employeeData)
                .Include(a => a.position)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                var searchTerms = searchTerm.Split(',').Select(term => term.Trim().ToLower()).ToArray();

                // Pobierz dane z bazy danych do pami�ci
                var employeesInMemory = query.ToList();

                var filteredEmployees = employeesInMemory
                    .Where(employee =>
                        searchTerms.All(searchTerm =>
                            (employee.name != null && employee.name.ToLower().Contains(searchTerm)) ||
                            (employee.surname != null && employee.surname.ToLower().Contains(searchTerm)) ||
                            (employee.email != null && employee.email.ToLower().Contains(searchTerm)) ||
                            (employee.employeeData != null &&
                                employee.employeeData.pesel != null && employee.employeeData.pesel.ToString().Contains(searchTerm))
                        )
                    )
                    .AsQueryable();

                // Zaktualizuj oryginalne zapytanie
                query = filteredEmployees.AsQueryable();
            }

            return query.ToList();
        }


    }
}