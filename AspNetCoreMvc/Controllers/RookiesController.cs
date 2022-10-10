using AspNetCoreMvc.Helpers;
using AspNetCoreMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreMvc.Controllers;

[Route("NashTech/{controller}")]
public class RookiesController : Controller
{
    private static readonly List<PersonModel> _members = new()
    {
        new PersonModel("Minh", "Tran", Gender.Male, new DateTime(2000, 04, 24), "0962215871", "Ha Noi", false),
        new PersonModel("Nam", "Nguyen", Gender.Male, new DateTime(2000, 09, 24), "0962215871", "Hoa Binh", false),
        new PersonModel("Mai", "Tran", Gender.Female, new DateTime(2002, 04, 26), "0962215871", "Ha Noi", true),
        new PersonModel("Hoa", "Tran", Gender.Other, new DateTime(1999, 09, 21), "0962215871", "Phu Tho", true)
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
    public IActionResult GetMembersByBirthYear(int year, string compareType)
    {
        switch (compareType)
        {
            case "greaterThan":
                {
                    var birthYearGreaterThan = _members
                        .FindAll(member => member.DateOfBirth.Year > year);

                    return Json(birthYearGreaterThan);
                }

            case "equalTo":
                {
                    var birthYearEqualTo = _members
                        .FindAll(member => member.DateOfBirth.Year == year);

                    return Json(birthYearEqualTo);
                }

            case "lessThan":
                {
                    var birthYearLessThan = _members
                    .FindAll(member => member.DateOfBirth.Year < 2000);

                    return Json(birthYearLessThan);
                }

            default:
                {
                    return Content("Invalid query string!");
                }
        }
    }

    [Route("{action}")]
    public IActionResult GetMembersBornAfter2000()
    {
        return RedirectToAction("GetMembersByBirthYear", new { year = 2000, compareType = "greaterThan" });
    }

    [Route("{action}")]
    public IActionResult GetMembersBornIn2000()
    {
        return RedirectToAction("GetMembersByBirthYear", new { year = 2000, compareType = "equalTo" });
    }

    [Route("{action}")]
    public IActionResult GetMembersBornBefore2000()
    {
        return RedirectToAction("GetMembersByBirthYear", new { year = 2000, compareType = "lessThan" });
    }

    [Route("{action}")]
    public IActionResult ExportMembersData()
    {
        const string sheetName = "MembersData";

        var dataTable = ExcelHelper.ToDataTable(_members);
        var excelSheet = ExcelHelper.ExportExcel(dataTable, true, sheetName);

        return File(excelSheet, ExcelHelper.ContentType, $"{sheetName}.xlsx");
    }
}