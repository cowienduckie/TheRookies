using AspNetCoreMvc.DataAccess.Entities;
using AspNetCoreMvc.DataAccess.Interfaces;

namespace AspNetCoreMvc.DataAccess;

public class StaticPersonDataAccess : IPersonDataAccess
{
    private static readonly List<Person> _people = new()
    {
        new Person("Minh", "Tran", 0, new DateTime(2000, 04, 24), "0962215871", "Ha Noi"),
        new Person("Nam", "Nguyen", 0, new DateTime(2000, 09, 24), "0962215871", "Hoa Binh"),
        new Person("Mai", "Tran", 1, new DateTime(2002, 04, 26), "0962215871", "Ha Noi"),
        new Person("Hoa", "Tran", 1, new DateTime(1999, 09, 21), "0962215871", "Phu Tho")
    };

    public IEnumerable<Person> GetAllPeople()
    {
        return _people;
    }

    public Person? GetPersonByIndex(int index)
    {
        return (index >= _people.Count || index < 0) ? null : _people[index];
    }

    public void AddPerson(Person person)
    {
        _people.Add(person);
    }

    public void EditPerson(int index, Person person)
    {
        _people[index] = person;
    }

    public void DeletePerson(int index)
    {
        if (index < _people.Count && index >= 0)
        {
            _people.RemoveAt(index);
        }
    }
}