using System.Data;
using System.Drawing;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace AspNetCoreMvc.Helpers;

public static class ExcelHelper
{
    public static string ContentType =>
        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    public static DataTable ToDataTable<T>(List<T> items)
    {
        var dataTable = new DataTable(typeof(T).Name);
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in properties)
        {
            dataTable.Columns.Add(prop.Name,
                Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
        }

        foreach (var item in items)
        {
            var values = new object?[properties.Length];

            for (var i = 0; i < properties.Length; i++)
            {
                values[i] = properties[i].GetValue(item, null);
            }

            dataTable.Rows.Add(values);
        }

        return dataTable;
    }

    public static byte[] ExportExcel(
        DataTable dataTable,
        bool showSequenceNo,
        string sheetName = "")
    {
        byte[] result;

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            sheetName = string.IsNullOrEmpty(sheetName) ? "NewSheet" : sheetName;

            var workSheet = package.Workbook.Worksheets.Add(sheetName);

            const int startRowFrom = 1;

            if (showSequenceNo)
            {
                var dataColumn = dataTable.Columns.Add("#", typeof(int));
                var index = 1;

                dataColumn.SetOrdinal(0);

                foreach (DataRow item in dataTable.Rows)
                {
                    item[0] = index;
                    index++;
                }
            }

            workSheet.Cells["A" + startRowFrom].LoadFromDataTable(dataTable, true);

            // Format DateTime date to dd/MM/yyyy
            for (var i = 0; i < dataTable.Columns.Count; i++)
            {
                if (dataTable.Columns[i].DataType == typeof(DateTime))
                {
                    workSheet.Column(i + 1).Style.Numberformat.Format = "dd/MM/yyyy";
                }
            }

            // Sheet styling
            using (var range = workSheet.Cells)
            {
                range.Style.Font.Size = 12;
            }

            // Header styling
            using (var range = workSheet.Cells[startRowFrom, 1, startRowFrom, dataTable.Columns.Count])
            {
                range.Style.Font.Color.SetColor(Color.White);
                range.Style.Font.Bold = true;
                range.Style.Font.Size = 12;
                range.Style.HorizontalAlignment =ExcelHorizontalAlignment.Center;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.DarkGreen);
            }

            // Cells and Border styling
            if (dataTable.Rows.Count > 0)
            {
                using var range = workSheet.Cells[startRowFrom + 1, 1, startRowFrom + dataTable.Rows.Count,
                    dataTable.Columns.Count];

                range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                range.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                range.Style.Border.Top.Color.SetColor(Color.Black);
                range.Style.Border.Bottom.Color.SetColor(Color.Black);
                range.Style.Border.Left.Color.SetColor(Color.Black);
                range.Style.Border.Right.Color.SetColor(Color.Black);
            }

            // Auto fit cells content
            const int maxCellWidth = 150;

            for (var columnIndex = 1; columnIndex <= dataTable.Columns.Count; columnIndex++)
            {
                var columnCells = workSheet.Cells[workSheet.Dimension.Start.Row, columnIndex,
                    workSheet.Dimension.End.Row, columnIndex];

                var maxLength = columnCells.Max(cell => cell?.ToString().Length ?? 0);

                if (maxLength < maxCellWidth)
                {
                    workSheet.Column(columnIndex).AutoFit();
                }
                else
                {
                    workSheet.Column(columnIndex).Width = maxCellWidth;
                    workSheet.Column(columnIndex).Style.WrapText = true;
                }
            }

            result = package.GetAsByteArray();
        }

        return result;
    }
}