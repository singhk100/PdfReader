using ExcelDataReader;
using Nancy.Json;
using PdfReader.Interface;
using PdfReader.Model;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace PdfReader
{
    public class JsonConverts : IJsonConvert
    {
        readonly Integration _integration;
        public JsonConverts(Integration integration)
        {
            _integration = integration;
        }
        public string Convert()
        {
            var filePath = @"C:\\Users\\keshav_kumar2\\Desktop\\Test.xlsx"; // Adjust the path to your Excel file
            //var jsonData = ConvertExcelToJson(filePath);
            //Console.WriteLine(jsonData);

            // Initialize the Excel package
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context
            var fileInfo = new FileInfo(filePath);

            using (var package = new ExcelPackage(fileInfo))
            {
                var workbook = package.Workbook;
                var worksheet = workbook.Worksheets["Data"]; // Assuming data is in the first worksheet

                var dataList = new List<dynamic>(); // List to hold the data

                // Assuming the first row contains column names
                var columnNames = new List<string>();
                foreach (var cell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    columnNames.Add(cell.Text);
                }
                columnNames.Add("policyName");
                columnNames.Add("SumAssured");

                // Iterate through the rows, starting from row 2
                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var dataItem = new Dictionary<string, object>();
                    for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
                    {
                        // Using dynamic objects for simplicity, consider creating a class to strongly type your data
                        //if (col > worksheet.Dimension.End.Column)
                        //    worksheet.Cells[row, col].Value = "";
                        dataItem[columnNames[col - 1]] = worksheet.Cells[row, col].Value;
                    }
                    dataList.Add(dataItem);
                }

                // Convert the list to JSON
                var json = JsonConvert.SerializeObject(dataList, Formatting.Indented);


                json = _integration.CallApi(json).Result;

                return json;
            }
        }
    }
}
