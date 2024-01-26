using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using StudentSample.DAO;
using StudentSample.Models;

namespace StudentSample.Export
{
    public class StudentExportExcel
    {
        private readonly StudentDAO sdao;
        #region #constructor
        public StudentExportExcel(StudentDAO dao)
        {
            //set dao
            sdao = dao;
        }
        #endregion

        #region #GenerateExcel
        public void GenerateExcel(string filepath)
        {
            // Create a new Excel document
            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create(filepath, SpreadsheetDocumentType.Workbook, true))
            {
                // Create the necessary parts and relationships for the spreadsheet
                WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(new SheetData());

                Sheets? sheets = spreadsheetDocument.WorkbookPart?.Workbook.AppendChild(new Sheets());

                // Create a sheet and give it a name
                Sheet sheet = new Sheet() { Id = spreadsheetDocument.WorkbookPart?.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Boro" };
                sheets?.Append(sheet);

                // Get the sheet data
                SheetData? sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

                // Create header row
                Row headerRow = new Row() { RowIndex = 1 };
                headerRow.Append(CreateCell("Roll No", 1, 1));
                headerRow.Append(CreateCell("Name", 2, 1));
                headerRow.Append(CreateCell("Score", 3, 1));
                if(sheetData == null){
                    throw new Exception();
                }
                sheetData.Append(headerRow);
                IEnumerable<Student> students = this.sdao.getAll();
                //// Create data rows
                int i = 1;
                foreach (Student student in students)
                {
                    int rowIdx = i + 1;
                    Row dataRow = new Row() { RowIndex = (uint)rowIdx };
                    dataRow.Append(CreateCell(student.no.ToString(), 1, rowIdx));
                    dataRow.Append(CreateCell(student.name, 2, rowIdx));
                    dataRow.Append(CreateCell(student.score.ToString(), 3, rowIdx));
                    sheetData.Append(dataRow);
                    i++;
                }

                // Save the changes to the spreadsheet document
                workbookPart.Workbook.Save();
            }
        }

        private Cell CreateCell(string text, int columnIndex,int rowIndex)
        {
            Cell cell = new Cell(new InlineString(new Text(text)))
            {
                DataType = CellValues.InlineString,
                CellReference = GetColumnName(columnIndex) + rowIndex.ToString()
            };
            return cell;
        }

        private string GetColumnName(int columnIndex)
        {
            int dividend = columnIndex;
            string columnName = string.Empty;

            while (dividend > 0)
            {
                int modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }
        #endregion
    }
}