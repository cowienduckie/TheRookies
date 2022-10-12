using AspNetCoreMvc.Services.Models;

namespace AspNetCoreMvc.Services.Interfaces;

public interface IPersonService
{
    IEnumerable<PersonViewModel> GetAllPeople();
    PersonEditModel? GetPersonEditModel(int index);
    void AddPerson(PersonCreateModel createModel);
    void EditPerson(int index, PersonEditModel editModel);
    void DeletePerson(int index);
}