﻿using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Collections.Concurrent;
using System.Text;

namespace LeetCode
{
    public static class ReadPdfGeneric
    {
        public static char[] StartChars { get; set; } = [':', '.'];
        public static void ReadPdf()
        {
            var hdrs = new List<(string startAnchor, string endAnchor, string[] ignoreAnchors)>()
            {
                //("Supplier Address","Currency",["E-Mail","Telephone/Telefax"])


                ("Buyer Name", "PO Date",[]),
                ("Supplier Name", "PO #",[]),
                ("Supplier Details", "Delivery Date",[]),
                ("PO Date", "Supplier Name",[]),
                ("PO #", "Supplier Details",[]),
                ("Delivery Date", "Delivery Mode",[]),
                ("Delivery Mode", "Payment Mode",[]),
                ("Payment Mode","Currency",[]),
                ("Currency","Season",[]),
                ("Season","SL#",[]),


            };
            //var filePath = "C:\\Users\\aebislamk\\Downloads\\Brandix\\Brandix\\Carhartt\\2507507_AMERICAN AND EFIRED BANGLADESH LTD-BCB86505_ Purchase Order_20241105_102623.PDF";
            var filePath = "D:\\Project\\7Robots\\Robot5_PdfScrap\\PDF\\2.pdf";
            ExtractHeaderInformation(filePath, hdrs, true);
            //ExtractTables(filePath);
        }
        public static Dictionary<string, string> ExtractHeaderInformation(string pdfPath, List<(string startAnchor, string endAnchor, string[] ignoreAnchors)> anchors, bool isHeaderInFirstPage)
        {
            var pdfDocument = new Document(pdfPath);
            var textBuilder = new StringBuilder();
            var headerData = new ConcurrentDictionary<string, string>();

            if (!isHeaderInFirstPage)
            {
                Parallel.ForEach(pdfDocument.Pages, page =>
                {
                    var textAbsorber = new TextAbsorber
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
                var textAbsorber = new TextAbsorber
                {
                    TextSearchOptions = new TextSearchOptions(true)
                };
                pdfDocument.Pages.First().Accept(textAbsorber);
                textBuilder.AppendLine(textAbsorber.Text);
            }

            var formattedText = textBuilder.ToString().ToUpper(System.Globalization.CultureInfo.InvariantCulture);

            Parallel.ForEach(anchors, (anchor) =>
            {
                var startIndex = formattedText.IndexOf(anchor.startAnchor.ToUpper(System.Globalization.CultureInfo.InvariantCulture), StringComparison.Ordinal);
                var endIndex = formattedText.IndexOf(anchor.endAnchor.ToUpper(System.Globalization.CultureInfo.InvariantCulture), startIndex + anchor.startAnchor.Length, StringComparison.Ordinal);

                if (startIndex != -1 && endIndex != -1)
                {
                    var dataStart = startIndex + anchor.startAnchor.Length;
                    var extractedData = formattedText.Substring(dataStart, endIndex - dataStart).Trim();
                    var dataBuilder = new StringBuilder(extractedData);
                    foreach (var ignoreAnchor in anchor.ignoreAnchors)
                    {
                        var ignoreIndex = dataBuilder.ToString().IndexOf(ignoreAnchor.ToUpper(System.Globalization.CultureInfo.InvariantCulture), StringComparison.Ordinal);
                        while (ignoreIndex != -1)
                        {
                            var endOfIgnore = dataBuilder.ToString().IndexOf("\n", ignoreIndex, StringComparison.Ordinal);
                            if (endOfIgnore == -1)
                                endOfIgnore = dataBuilder.Length;

                            dataBuilder.Remove(ignoreIndex, endOfIgnore - ignoreIndex);
                            ignoreIndex = dataBuilder.ToString().IndexOf(ignoreAnchor.ToUpper(System.Globalization.CultureInfo.InvariantCulture), StringComparison.Ordinal);
                        }
                    }

                    for (var i = 0; i < dataBuilder.Length; i++)
                    {
                        if (dataBuilder.Length > 0 && char.IsWhiteSpace(dataBuilder[0]) || StartChars.Contains(dataBuilder[0]))
                        {
                            dataBuilder.Remove(i, 1);
                            i = -1;
                        }
                    }

                    headerData[anchor.startAnchor] = dataBuilder.ToString().Trim();
                }
            });

            return headerData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static void ExtractTables(string pdfPath, bool hasSerialColumn = false, int serialStartsWith = 1)
        {
            var pdfDocument = new Document(pdfPath);
            var finalResult = new StringBuilder(); // To collect all the table data

            for (var pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
            {
                var page = pdfDocument.Pages[pageIndex];
                var absorber = new TableAbsorber();
                absorber.Visit(page);
                foreach (var table in absorber.TableList)
                {
                    foreach (var row in table.RowList)
                    {
                        var rowText = new StringBuilder();
                        foreach (var cell in row.CellList)
                        {
                            var cellText = new StringBuilder();
                            foreach (var fragment in cell.TextFragments)
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
            var lineNum = serialStartsWith;
            foreach (var line in lines)
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