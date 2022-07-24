using DataAcessLayer.Models.NationModels;
using System.Collections.Generic;

namespace DataAcessLayer.Repositories.NationRepository
{
    public interface INationRepository
    {
        Nation GetNationById(int id);
        List<Nation> GetAllNations();
    }
}
