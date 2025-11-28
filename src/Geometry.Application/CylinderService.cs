using Geometry.Domain.CylinderModel;

namespace Geometry.Application;

public class CylinderService
{
    private ICylinderRepository _cylinderRepository;

    public CylinderService(ICylinderRepository cylinderRepository)
    {
        _cylinderRepository = cylinderRepository;
    }

    public async Task<Guid> Insert(Cylinder cylinder)
    {
        return await _cylinderRepository.Insert(cylinder);
    }

    public async Task<Cylinder?> ReadById(Guid id)
    {
        return await _cylinderRepository.ReadById(id);
    }
}
