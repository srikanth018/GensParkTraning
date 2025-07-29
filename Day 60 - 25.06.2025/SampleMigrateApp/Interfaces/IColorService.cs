using SampleMigrateApp.DTOs;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Interfaces
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetColors();
        Task<Color> GetColorById(int id);
        Task<Color> CreateColor(CreateColorDTO colorDTO);
        Task<Color> EditColor(int id, EditColorDTO colorDTO);
        Task<Color> DeleteColor(int id);
    }
}