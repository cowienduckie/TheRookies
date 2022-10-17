using AspNetCoreAPi.Models;

namespace AspNetCoreAPi.Services;

public interface IPersonService
{
    IEnumerable<PersonModel> GetAll(string? name, string? gender, string? birthPlace);
    PersonModel? Create(PersonCreateModel createModel);
    PersonModel? Update(Guid id, PersonUpdateModel updateModel);
    bool Delete(Guid id);
    bool IsExist(Guid id);
}