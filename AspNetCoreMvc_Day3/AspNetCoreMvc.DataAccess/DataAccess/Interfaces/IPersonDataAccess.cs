using AspNetCoreMvc.DataAccess.Entities;

namespace AspNetCoreMvc.DataAccess.Interfaces;

public interface IPersonDataAccess
{
    IEnumerable<Person> GetAllPeople();
    Person? GetPersonByIndex(int index);
    void AddPerson(Person person);
    void EditPerson(int index, Person person);
    void DeletePerson(int index);
}