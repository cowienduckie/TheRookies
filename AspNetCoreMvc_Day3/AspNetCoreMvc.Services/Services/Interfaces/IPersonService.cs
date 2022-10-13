using AspNetCoreMvc.Services.Models;

namespace AspNetCoreMvc.Services.Interfaces;

public interface IPersonService
{
    IEnumerable<PersonViewModel> GetAllPeople();
    PersonViewModel? GetPersonByIndex(int index);
    PersonEditModel? GetPersonEditModel(int index);
    void AddPerson(PersonCreateModel createModel);
    void EditPerson(int index, PersonEditModel editModel);
    PersonViewModel? DeletePerson(int index);
}