using AspNetCoreMvc.DataAccess.Entities;
using AspNetCoreMvc.DataAccess.Interfaces;
using AspNetCoreMvc.Services.Interfaces;
using AspNetCoreMvc.Services.Models;

namespace AspNetCoreMvc.Services;

public class PersonService : IPersonService
{
    private readonly IPersonDataAccess _dataAccess;

    public PersonService(IPersonDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public IEnumerable<PersonViewModel> GetAllPeople()
    {
        var entities = _dataAccess.GetAllPeople();

        var viewModels = entities.Select(person => new PersonViewModel
        {
            FirstName = person.FirstName,
            LastName = person.LastName,
            Gender = person.Gender == 0 ? "Male" : "Female",
            DateOfBirth = person.DateOfBirth,
            PhoneNumber = person.PhoneNumber,
            BirthPlace = person.BirthPlace
        })
        .ToList();

        return viewModels;
    }

    public PersonEditModel? GetPersonEditModel(int index)
    {
        var entity = _dataAccess.GetPersonByIndex(index);

        if (entity == null) return null;

        var editModel = new PersonEditModel
        {
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Gender = entity.Gender == 0 ? "Male" : "Female",
            DateOfBirth = entity.DateOfBirth,
            PhoneNumber = entity.PhoneNumber,
            BirthPlace = entity.BirthPlace
        };

        return editModel;
    }

    public void AddPerson(PersonCreateModel createModel)
    {
        var newEntity = new Person(createModel.FirstName,
                                   createModel.LastName,
                                   createModel.Gender == "Male" ? 0 : 1,
                                   createModel.DateOfBirth,
                                   string.Empty,
                                   string.Empty);

        _dataAccess.AddPerson(newEntity);
    }

    public void EditPerson(int index, PersonEditModel editModel)
    {
        var entity = _dataAccess.GetPersonByIndex(index);

        if (entity == null) return;

        entity.FirstName = editModel.FirstName;
        entity.LastName = editModel.LastName;
        entity.Gender = editModel.Gender == "Male" ? 0 : 1;
        entity.DateOfBirth = editModel.DateOfBirth;
        entity.PhoneNumber = editModel.PhoneNumber ?? string.Empty;
        entity.BirthPlace = editModel.BirthPlace ?? string.Empty;

        _dataAccess.EditPerson(index, entity);
    }

    public void DeletePerson(int index)
    {
        _dataAccess.DeletePerson(index);
    }
}