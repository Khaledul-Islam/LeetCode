using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Collections.Concurrent;
using System.Text;
using static Aspose.Pdf.Artifacts.Pagination.PageNumber;

namespace LeetCode
{
    public static class ReadPdfGeneric
    {
        public static char[] StartChars { get; set; } = [':', '.'];
        public static void ReadPdf()
        {
            var hdrs = new List<(string startAnchor, string endAnchor)>()
            {
                ("Buyer Name", "PO Date"),
                ("Supplier Name", "PO #"),
                ("Supplier Details", "Delivery Date"),
                ("PO Date", "Supplier Name"),
                ("PO #", "Supplier Details"),
                ("Delivery Date", "Delivery Mode"),
                ("Delivery Mode", "Payment Mode"),
                ("Payment Mode","Currency"),
                ("Currency","Season"),
                ("Season","SL#"),


            };
            ExtractHeaderInformation("C:\\Users\\Administrator\\Downloads\\Documents\\2.pdf", hdrs, true);
            //ExtractTables("C:\\Users\\Administrator\\Downloads\\Documents\\2.pdf");
        }
        public static Dictionary<string, string> ExtractHeaderInformation(string pdfPath, List<(string startAnchor, string endAnchor)> anchors, bool isHeaderInFirstPage)
        {
            Document pdfDocument = new Document(pdfPath);
            var textBuilder = new StringBuilder();
            var headerData = new ConcurrentDictionary<string, string>();

            if (!isHeaderInFirstPage)
            {
                Parallel.ForEach(pdfDocument.Pages, page =>
                {
                    TextAbsorber textAbsorber = new TextAbsorber
                    {
                        TextSearchOptions = new TextSearchOptions(true)
                    };
                    page.Accept(textAbsorber);
                    lock (textBuilder)
                    {
                        textBuilder.AppendLine(textAbsorber.Text);
                    }
                });
            }
            else
            {
                TextAbsorber textAbsorber = new TextAbsorber
                {
                    TextSearchOptions = new TextSearchOptions(true)
                };
                pdfDocument.Pages.First().Accept(textAbsorber);
                textBuilder.AppendLine(textAbsorber.Text);
            }

            var formattedText = textBuilder.ToString().ToUpper(System.Globalization.CultureInfo.InvariantCulture);
            Parallel.ForEach(anchors, (anchor) =>
            {
                int startIndex = formattedText.IndexOf(anchor.startAnchor.ToUpper(System.Globalization.CultureInfo.InvariantCulture));
                int endIndex = formattedText.IndexOf(anchor.endAnchor.ToUpper(System.Globalization.CultureInfo.InvariantCulture), startIndex + anchor.startAnchor.Length);

                if (startIndex != -1 && endIndex != -1)
                {
                    int dataStart = startIndex + anchor.startAnchor.Length;
                    string parsedData = formattedText.Substring(dataStart, endIndex - dataStart).Trim();
                    var dataBuilder = new StringBuilder(parsedData);
                    for (int i = 0; i < dataBuilder.Length; i++)
                    {
                        if (dataBuilder.Length > 0 && char.IsWhiteSpace(dataBuilder[0]) || StartChars.Contains(dataBuilder[0]))
                        {
                            dataBuilder.Remove(i, 1);
                            i = -1;
                        }
                    }
                    headerData[anchor.startAnchor] = dataBuilder.ToString();
                }
            });

            return headerData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static void ExtractTables(string pdfPath, bool hasSerialColumn = false, int serialStartsWith = 1)
        {
            Document pdfDocument = new Document(pdfPath);
            StringBuilder finalResult = new StringBuilder(); // To collect all the table data

            for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                Page page = pdfDocument.Pages[pageIndex];
                TableAbsorber absorber = new TableAbsorber();
                absorber.Visit(page);
                foreach (AbsorbedTable table in absorber.TableList)
                {
                    foreach (AbsorbedRow row in table.RowList)
                    {
                        StringBuilder rowText = new StringBuilder();
                        foreach (AbsorbedCell cell in row.CellList)
                        {
                            var cellText = new StringBuilder();
                            foreach (TextFragment fragment in cell.TextFragments)
                            {
                                cellText.Append(fragment.Text);
                            }
                            rowText.Append($"{cellText.ToString().Trim()} | ");
                        }
                        finalResult.AppendLine(rowText.ToString().TrimEnd(' ', '|'));
                    }
                }
            }
            string[] lines = finalResult.ToString().Split('\n');
            int lineNum = serialStartsWith;
            foreach (string line in lines)
            {
                if (line.StartsWith($"{lineNum} | "))
                {
                    Console.WriteLine(line);
                    lineNum++;
                }
            }

        }
    }
}
//public static DataTable ExtractTableData(string pdfPath, List<string> tableHeaders)
//{
//    var dataTable = new DataTable();
//    foreach (var header in tableHeaders)
//    {
//        dataTable.Columns.Add(header); // Create columns for the headers
//    }

//    using (var pdfReader = new PdfReader(pdfPath))
//    using (var pdfDoc = new PdfDocument(pdfReader))
//    {
//        for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
//        {
//            string pageText = PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));
//            string[] lines = pageText.Split('\n'); // Split text by line
//            bool tableStarted = false;

//            foreach (string line in lines)
//            {
//                // Check if the line contains table headers
//                if (tableHeaders.TrueForAll(header => line.Contains(header)))
//                {
//                    tableStarted = true;
//                    continue;
//                }

//                if (tableStarted)
//                {
//                    // Split the line by spaces (or tabs) to extract columns
//                    string[] cells = line.Split(' '); // Use regex if cells are irregularly spaced
//                    if (cells.Length >= tableHeaders.Count)
//                    {
//                        var row = dataTable.NewRow();
//                        for (int j = 0; j < tableHeaders.Count; j++)
//                        {
//                            row[j] = cells[j].Trim(); // Assign each cell to the respective column
//                        }
//                        dataTable.Rows.Add(row);
//                    }
//                }
//            }
//        }
//    }

//    return dataTable;
//}