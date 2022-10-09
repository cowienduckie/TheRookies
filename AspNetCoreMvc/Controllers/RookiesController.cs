using AspNetCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc.Controllers;

[Route("NashTech/{controller}")]
public class RookiesController : Controller
{
    private static readonly List<Person> _members = new()
    {
        new Person("Minh", "Tran", Gender.Male, new DateTime(2000, 04, 24), "0962215871", "Ha Noi", false),
        new Person("Nam", "Nguyen", Gender.Male, new DateTime(2000, 09, 24), "0962215871", "Hoa Binh", false),
        new Person("Mai", "Tran", Gender.Female, new DateTime(2002, 04, 26), "0962215871", "Ha Noi", true),
        new Person("Hoa", "Tran", Gender.Other, new DateTime(1999, 09, 21), "0962215871", "Phu Tho", true)
    };

    [Route("{action}")]
    public IActionResult GetMaleMembers()
    {
        var maleMembers = _members
            .FindAll(member => member.Gender == Gender.Male);

        return Json(maleMembers);
    }

    [Route("{action}")]
    public IActionResult GetOldestMember()
    {
        int maxAge = _members.Max(member => member.Age);

        var oldestMember = _members
            .First(member => member.Age == maxAge);

        return Json(oldestMember);
    }

    [Route("{action}")]
    public IActionResult GetAllFullNames()
    {
        var fullNames = _members
            .Select(member => member.FullName)
            .ToList();

        return Json(fullNames);
    }

    [Route("{action}")]
    public IActionResult GetMembersGroupByBirthYear(string compareTo2000)
    {
        if (compareTo2000 == "greater")
        {
            return RedirectToAction("GetMembersBirthYearGreaterThan2000");
        }

        if (compareTo2000 == "equal")
        {
            return RedirectToAction("GetMembersBirthYearEqualTo2000");
        }

        if (compareTo2000 == "less")
        {
            return RedirectToAction("GetMembersBirthYearLessThan2000");
        }

        return Content("Invalid query string!");
    }

    [Route("{action}")]
    public IActionResult GetMembersBirthYearGreaterThan2000()
    {
        var birthYearGreaterThan2000 = _members
            .FindAll(member => member.DateOfBirth.Year > 2000);

        return Json(birthYearGreaterThan2000);
    }

    [Route("{action}")]
    public IActionResult GetMembersBirthYearEqualTo2000()
    {
        var birthYearEqualTo2000 = _members
            .FindAll(member => member.DateOfBirth.Year == 2000);

        return Json(birthYearEqualTo2000);
    }

    [Route("{action}")]
    public IActionResult GetMembersBirthYearLessThan2000()
    {
        var birthYearLessThan2000 = _members
            .FindAll(member => member.DateOfBirth.Year < 2000);

        return Json(birthYearLessThan2000);
    }

    [Route("{action}")]
    public IActionResult ExportMembersData()
    {
        const string fileName = "MembersData.xlsx";
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot/exports/", fileName);

        var file = System.IO.File.ReadAllBytes(filePath);

        return File(file,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }
}