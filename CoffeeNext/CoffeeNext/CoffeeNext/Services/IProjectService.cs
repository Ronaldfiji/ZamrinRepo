using CoffeeNext.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeNext.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Projects>> GetProjectsAsync(string url);
        Task DeleteProject(string projectToDelete);
    }
}
