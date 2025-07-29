using SampleMigrateApp.DTOs;
using SampleMigrateApp.Interfaces;
using SampleMigrateApp.Models;

namespace SampleMigrateApp.Services
{
    public class ColorService : IColorService
    {
        private readonly IRepository<int, Color> _colorRepo;

        public ColorService(IRepository<int, Color> colorRepo)
        {
            _colorRepo = colorRepo;
        }

        public async Task<Color> CreateColor(CreateColorDTO colorDTO)
        {
            var newColor = new Color
            {
                Color1 = colorDTO.Color1
            };

            var created = await _colorRepo.Add(newColor);
            return created;
        }

        public async Task<Color> DeleteColor(int id)
        {
            var color = await _colorRepo.Get(id);
            if (color == null)
            {
                throw new KeyNotFoundException($"Color with ID {id} not found.");
            }

            await _colorRepo.Delete(id);
            return color;
        }

        public async Task<Color> EditColor(int id, EditColorDTO colorDTO)
        {
            var existingColor = await _colorRepo.Get(id);
            if (existingColor == null)
            {
                throw new KeyNotFoundException($"Color with ID {id} not found.");
            }

            existingColor.Color1 = colorDTO.Color1;
            var updated = await _colorRepo.Update(id, existingColor);
            return updated;
        }

        public async Task<Color> GetColorById(int id)
        {
            var color = await _colorRepo.Get(id);
            if (color == null)
            {
                throw new KeyNotFoundException($"Color with ID {id} not found.");
            }

            return color;
        }

        public async Task<IEnumerable<Color>> GetColors()
        {
            return await _colorRepo.GetAll();
        }
    }
}
