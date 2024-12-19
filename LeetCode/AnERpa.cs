using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Collections.Concurrent;
using System.Text;
using System.Data;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Globalization;
using System.Data.Odbc;

namespace LeetCode
{
    public static class AnERpa
    {
        public static char[] StartChars { get; set; } = [':', '.'];
        public static void ReadPdf()
        {
            var hdrs = new List<(string startAnchor, string endAnchor, string[] ignoreAnchors)>()
            {
                ("REMARKS","PREPARED BY",[])


                //("Buyer Name", "PO Date",[]),
                //("Supplier Name", "PO #",[]),
                //("Supplier Details", "Delivery Date",[]),
                //("PO Date", "Supplier Name",[]),
                //("PO #", "Supplier Details",[]),
                //("Delivery Date", "Delivery Mode",[]),
                //("Delivery Mode", "Payment Mode",[]),
                //("Payment Mode","Currency",[]),
                //("Currency","Season",[]),
                //("Season","SL#",[]),


            };
            var anchorList = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object>
                {
                    { "startAnchor", "Buyer Name" },
                    { "endAnchor", "PO Date" },
                    { "ignoreAnchors", new string[]{} }
                },
                new Dictionary<string, object>
                {
                    { "startAnchor", "PO #" },
                    { "endAnchor", "Supplier Details" },
                    { "ignoreAnchors", new string[] {} }
                },
                new Dictionary<string, object>
                {
                    { "startAnchor", "REMARKS" },
                    { "endAnchor", "PREPARED BY" },
                    { "ignoreAnchors", new string[] {} }
                }
            };
            var filePathList = new string[]
            {
                "D:\\Univogue\\Split\\1.pdf",
                "D:\\Univogue\\Split\\2.pdf",
                "D:\\Univogue\\Split\\3.pdf",
                "D:\\Univogue\\Split\\4.pdf",
                "D:\\Univogue\\Split\\5.pdf",
                "D:\\Univogue\\Split\\6.pdf",
                "D:\\Univogue\\Split\\7.pdf",
                "D:\\Univogue\\Split\\8.pdf",
                "D:\\Univogue\\Split\\9.pdf",
                "D:\\Univogue\\Split\\10.pdf",
                "D:\\Univogue\\Split\\11.pdf",
                "D:\\Univogue\\Split\\12.pdf",
                "D:\\Univogue\\Split\\13.pdf"
            };
            //var filePath = "C:\\Users\\aebislamk\\Downloads\\Brandix\\Brandix\\Carhartt\\2507507_AMERICAN AND EFIRED BANGLADESH LTD-BCB86505_ Purchase Order_20241105_102623.PDF";
            var filePath = "C:\\Users\\aebislamk\\Downloads\\FW_ Help from Robotics_Univouge\\RAW # 90441 THREAD PURCHASE ORDER FOR STYLE #HC00237,HC01000,HC10884,HC10897,HC70078.pdf";
            //ExtractHeaderInformation(filePath, filePathList, anchorList, StartChars,false,true);
            ExtractTables(filePath, filePathList, true);
        }
        public static Dictionary<string, object> ExtractHeaderInformation(string mainPath, string[] filePathList, List<Dictionary<string, object>> anchorList,
            char[] startChars, bool isHeaderInFirstPage, bool isHeaderInFirstAndLastPage)
        {
            var result = new Dictionary<string, object>();
            result.Add("successMsg", "");
            result.Add("errMsg", "");
            result.Add("response", new Dictionary<string, string>());
            var textBuilder = new StringBuilder();
            var headerData = new ConcurrentDictionary<string, string>();
            if (filePathList.Length < 4)//1,2,3,4
            {
                var pdfDocument = new Document(mainPath);
                foreach (var page in pdfDocument.Pages)
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
                };
            }
            else//5,6,7,....
            {
                var firstPath = filePathList[0];
                var lastPath = filePathList[filePathList.Length - 1];

                if (isHeaderInFirstAndLastPage)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Document pdfDocument = null;
                        if (i == 0)
                        {
                            pdfDocument = new Document(firstPath);

                        }
                        if (i == 1)
                        {
                            pdfDocument = new Document(lastPath);

                        }
                        var textAbsorber = new TextAbsorber
                        {
                            TextSearchOptions = new TextSearchOptions(true)
                        };
                        pdfDocument.Pages.First().Accept(textAbsorber);
                        textBuilder.AppendLine(textAbsorber.Text);
                    }
                }
                if (isHeaderInFirstPage)
                {
                    var pdfDocument = new Document(firstPath);
                    var textAbsorber = new TextAbsorber
                    {
                        TextSearchOptions = new TextSearchOptions(true)
                    };
                    pdfDocument.Pages.First().Accept(textAbsorber);
                    textBuilder.AppendLine(textAbsorber.Text);
                }
            }

            try
            {

                var formattedText = textBuilder.ToString().ToUpper(CultureInfo.InvariantCulture);

                foreach (var anchor in anchorList)
                {
                    var startAnchor = anchor["startAnchor"] as string;
                    var endAnchor = anchor["endAnchor"] as string;
                    var ignoreAnchors = anchor["ignoreAnchors"] as string[];

                    var startIndex = formattedText.IndexOf(startAnchor.ToUpper(CultureInfo.InvariantCulture), StringComparison.Ordinal);
                    var endIndex = formattedText.IndexOf(endAnchor.ToUpper(CultureInfo.InvariantCulture), startIndex + startAnchor.Length, StringComparison.Ordinal);

                    if (startIndex != -1 && endIndex != -1)
                    {
                        var dataStart = startIndex + startAnchor.Length;
                        var extractedData = formattedText.Substring(dataStart, endIndex - dataStart).Trim();
                        var dataBuilder = new StringBuilder(extractedData);
                        foreach (var ignoreAnchor in ignoreAnchors)
                        {
                            var ignoreIndex = dataBuilder.ToString().IndexOf(ignoreAnchor.ToUpper(CultureInfo.InvariantCulture), StringComparison.Ordinal);
                            while (ignoreIndex != -1)
                            {
                                var endOfIgnore = dataBuilder.ToString().IndexOf("\n", ignoreIndex, StringComparison.Ordinal);
                                if (endOfIgnore == -1)
                                    endOfIgnore = dataBuilder.Length;

                                dataBuilder.Remove(ignoreIndex, endOfIgnore - ignoreIndex);
                                ignoreIndex = dataBuilder.ToString().IndexOf(ignoreAnchor.ToUpper(CultureInfo.InvariantCulture), StringComparison.Ordinal);
                            }
                        }

                        for (var i = 0; i < dataBuilder.Length; i++)
                        {
                            if (dataBuilder.Length > 0 && char.IsWhiteSpace(dataBuilder[0]) || startChars.Contains(dataBuilder[0]))
                            {
                                dataBuilder.Remove(i, 1);
                                i = -1;
                            }
                        }

                        headerData[startAnchor] = dataBuilder.ToString().Trim();
                    }
                };

                var responseData = headerData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
                result["response"] = responseData;
                result["successMsg"] = "OK";
                result["errMsg"] = "";
                ExtractMerchantName(headerData["REMARKS PLEASE CONFIRM"]);
                return result;
            }
            catch (Exception ex)
            {
                result["response"] = new Dictionary<string, string>();
                result["successMsg"] = "";
                result["errMsg"] = "ERR: " + ex.Message;
                return result;
            }
        }

        private static string ExtractMerchantName(string formattedText)
        {
            var lines = formattedText.Split('\n');
            var lastLine = lines.LastOrDefault();
            lastLine = lastLine?.Trim() ?? "";
            var spaceCount = 0;
            var trackName = string.Empty;
            foreach (var n in lastLine)
            {
                if (char.IsWhiteSpace(n))
                {
                    spaceCount++;
                }

                if (spaceCount < 6)
                {
                    trackName += n;
                }

                if (spaceCount == 6)
                {
                    break;
                }
            }

            return trackName.Trim();
        }

        public static Dictionary<string, string> ExtractTables(string mainPath, string[] filePathList, bool hasSerialColumn = false, int serialStartsWith = 1)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("errMsg", "");
            dic.Add("successmsg", "");
            dic.Add("response", "");
            var extractedText = new StringBuilder();

            try
            {

                if (filePathList.Length < 4)//1,2,3,4
                {
                    var pdfDocument = new Document(mainPath);
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
                                    double? previousYIndent = null;

                                    foreach (var fragment in cell.TextFragments)
                                    {
                                        if (previousYIndent.HasValue && Math.Abs(fragment.Position.YIndent - previousYIndent.Value) > 1e-2)
                                        {
                                            cellText.Append(" ");
                                        }

                                        cellText.Append(fragment.Text);
                                        previousYIndent = fragment.Position.YIndent; // Update YIndent
                                    }
                                    rowText.Append($"{cellText.ToString().Trim()} | ");
                                }
                                extractedText.AppendLine(rowText.ToString());
                            }
                        }
                    }
                }
                else
                {
                    foreach (var path in filePathList)
                    {
                        var pdfDocument = new Document(path);
                        var page = pdfDocument.Pages.First();
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
                                    double? previousYIndent = null;

                                    foreach (var fragment in cell.TextFragments)
                                    {
                                        if (previousYIndent.HasValue && Math.Abs(fragment.Position.YIndent - previousYIndent.Value) > 1e-2)
                                        {
                                            cellText.Append(" ");
                                        }

                                        cellText.Append(fragment.Text);
                                        previousYIndent = fragment.Position.YIndent; // Update YIndent
                                    }
                                    rowText.Append($"{cellText.ToString().Trim()} | ");
                                }
                                extractedText.AppendLine(rowText.ToString());
                            }
                        }
                    }
                }

                string[] lines = extractedText.ToString().Split('\n');
                var result = new StringBuilder();
                if (hasSerialColumn)
                {
                    var lineNum = serialStartsWith;

                    foreach (var line in lines)
                    {
                        if (line.StartsWith(lineNum + " | "))
                        {
                            result.AppendLine(line);
                            lineNum++;
                        }
                    }
                    dic["successMsg"] = "OK";
                    dic["response"] = result.ToString();
                    return dic;
                }
                else
                {
                    dic["successMsg"] = "OK";
                    dic["response"] = extractedText.ToString();
                    return dic;
                }
            }
            catch (Exception ex)
            {
                dic["errMsg"] = "ERR: " + ex.Message;
                return dic;
            }

            //PrepareDetailsData(result.ToString());
            //Console.WriteLine(result.ToString());
        }
        public static DataTable PrepareDetailsData(string tableRawData)
        {
            var lines = tableRawData.Split("\n");
            var rawDataList = new List<PdfDetailsData>();
            var dataList = new List<ItemDetails>();

            foreach (var line in lines)
            {
                var columns = line.Split('|');
                if (columns.Length >= 8)
                {
                    var pdfDetail = new PdfDetailsData
                    {
                        Serial = columns[0].Trim(),
                        ItemName = columns[1].Trim(),
                        Style = columns[2].Trim(),
                        PO = columns[3].Trim(),
                        ItemColorSize = columns[4].Trim(),
                        Shade = columns[5].Trim(),
                        Unit = columns[6].Trim(),
                        Qty = columns[7].Trim()
                    };

                    rawDataList.Add(pdfDetail);
                }
            }

            var mappedData = new Dictionary<string, string>();
            foreach (var rawData in rawDataList)
            {
                var model = new ItemDetails();
                model.SameGroupIdentifier = rawData.PO;
                model.FinType = "REG";
                model.ExpShipDate = DateTime.Now.AddDays(10).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                try
                {
                    model.ItemQty = Convert.ToInt32(Convert.ToDecimal(rawData.Qty));
                }
                catch (Exception e)
                {
                    model.LineMessage += "Error Converting(Exception). ItemQty :" + rawData.Qty;
                }
                var texNumber = string.Empty;
                try
                {
                    texNumber = ExtractNumberAfterTex(rawData.Shade);
                }
                catch (Exception e)
                {
                    model.LineMessage += "Tex Extraction Issue(Exception). Tex:" + rawData.Shade;
                }

                try
                {
                    mappedData.TryGetValue(rawData.ItemName + texNumber, out var mappedTktWithFinType);
                    if (string.IsNullOrEmpty(mappedTktWithFinType))
                    {
                        var tktWithFinType = GetTkt(rawData.ItemName, texNumber);
                        if (tktWithFinType.Contains("ERROR"))
                        {
                            model.LineMessage += tktWithFinType;
                        }
                        else
                        {
                            mappedData.TryAdd(rawData.ItemName + texNumber, tktWithFinType);
                            model.TKT = tktWithFinType.Contains("-") ? tktWithFinType.Split('-')[0] : tktWithFinType;
                        }

                    }
                    else
                    {
                        model.TKT = mappedTktWithFinType.Contains("-") ? mappedTktWithFinType.Split('-')[0] : mappedTktWithFinType;
                        if (string.IsNullOrEmpty(model.TKT))
                        {
                            model.LineMessage += "TKT NOT FOUND. ITEM: " + rawData.ItemName;
                        }
                    }

                }
                catch (Exception e)
                {
                    model.LineMessage += "TKT NOT FOUND(Exception). ITEM: " + rawData.ItemName;
                }
                try
                {
                    mappedData.TryGetValue(model.TKT, out var mappedLength);
                    if (string.IsNullOrEmpty(mappedLength))
                    {
                        var length = GetStandardLength(model.TKT);
                        mappedData.TryAdd(model.TKT, length);
                        model.Length = !string.IsNullOrEmpty(length) ? length.PadLeft(5, '0') : "";
                    }
                    else
                    {
                        model.Length = !string.IsNullOrEmpty(mappedLength) ? mappedLength.PadLeft(5, '0') : "";
                    }


                    if (string.IsNullOrEmpty(model.Length))
                    {
                        model.LineMessage += "Length NOT FOUND. ITEM: " + rawData.ItemName + "; Shade:" + rawData.Shade;
                    }
                }
                catch (Exception e)
                {
                    model.LineMessage += "Length NOT FOUND(Exception). ITEM: " + rawData.ItemName + "; Shade:" + rawData.Shade;
                }

                try
                {
                    var shade = rawData.Shade.Split(' ', StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
                    shade = shade?.Trim() ?? "";
                    var validator = ShadeValidate(shade);
                    if (validator["successMsg"] == "OK")
                    {
                        model.Shade = validator["response"];
                    }
                    if (string.IsNullOrEmpty(model.Shade))
                    {
                        model.LineMessage += "Shade NOT FOUND. ITEM: " + rawData.ItemName;
                    }

                }
                catch (Exception e)
                {
                    model.LineMessage += "Shade not found(Exception). ITEM: " + rawData.ItemName;
                }
                dataList.Add(model);
            }
            var json = JsonConvert.SerializeObject(dataList);
            var datatable = JsonConvert.DeserializeObject<DataTable>(json);
            SavePI(dataList);
            return datatable ?? new DataTable();
        }

        private static void SavePI(List<ItemDetails> dataList)
        {
            var groupData = dataList.GroupBy(a => a.SameGroupIdentifier);
            using var connection = new OdbcConnection("Data Source=192.168.0.12\\anesql;Initial Catalog=N2017;Persist Security Info=True;User ID=sa;Password=aNe_#(abc321@#;Encrypt=False");
            connection.Open();
            using var transaction = connection.BeginTransaction();
            foreach (var gData in groupData)
            {
                var description = "headerDescription" + "#" + gData.Key;
                //insert header
                var mergedData = MergeItems(gData.ToList());
                foreach (var data in mergedData)
                {
                    //insertDetails 
                }
            }
        }

        public static Dictionary<string, string> ExtractEmailSubject(string emailSubject)
        {
            emailSubject = emailSubject.Trim();
            var colonIndex = emailSubject.IndexOf(':');
            if (colonIndex == -1)
                return new Dictionary<string, string>();
            var details = emailSubject.Substring(colonIndex + 1).Trim();
            var data = details.Split('/');
            var length = data.Length;
            var result = new Dictionary<string, string>();
            if (length > 0)
                result["CompanyName"] = data[0].Trim();
            if (length > 1)
                result["MerchantName"] = data[1].Trim();
            if (length > 2)
                result["Email"] = data[2].Trim();
            if (length > 3)
                result["BuyerCode"] = data[3].Trim();
            return result;
        }
        public static string GetTkt(string rawItemName, string tex)
        {
            var query = "EXEC N2017..[RPA_ProductInfoByDescriptionOfUnivogue] @rawItemName, @tex";
            using var connection = new SqlConnection("Data Source=192.168.0.12\\anesql;Initial Catalog=N2017;Persist Security Info=True;User ID=sa;Password=aNe_#(abc321@#;Encrypt=False");
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@rawItemName", rawItemName);
            command.Parameters.AddWithValue("@tex", tex);

            connection.Open();
            var result = command.ExecuteScalar();
            return result?.ToString() ?? "";
        }
        public static string GetStandardLength(string tkt)
        {
            var query = "select top 1 STDLEG from N2017..PLTKTLOT where active=1 and TKT='" + tkt + "'";
            using var connection = new SqlConnection("Data Source=192.168.0.12\\anesql;Initial Catalog=N2017;Persist Security Info=True;User ID=sa;Password=aNe_#(abc321@#;Encrypt=False");
            using var command = new SqlCommand(query, connection);
            connection.Open();
            var result = command.ExecuteScalar();
            return result?.ToString() ?? "";
        }
        public static string ExtractNumberAfterTex(string input)
        {
            var texIndex = input.ToUpper().IndexOf("TEX", StringComparison.Ordinal);
            if (texIndex == -1) return "No match found";
            var startIndex = texIndex + 3; // Skip "TEX"
            while (startIndex < input.Length && (input[startIndex] == '-' || input[startIndex] == ' '))
            {
                startIndex++;
            }

            var endIndex = startIndex;
            while (endIndex < input.Length && char.IsDigit(input[endIndex]))
            {
                endIndex++;
            }
            return startIndex < endIndex ? input.Substring(startIndex, endIndex - startIndex) : string.Empty;
        }

        public static Dictionary<string, string> ShadeValidate(string rawShade)
        {
            var dic = new Dictionary<string, string>
            {
              { "errMsg", "" },
             { "successMsg", "" },
             { "response", "" }
            };
            if (string.IsNullOrEmpty(rawShade))
            {
                dic["errMsg"] = "ERR: Empty Shade.";
                dic["response"] = "";
                return dic;
            }

            if (rawShade == "TBA" || rawShade == "TBC")
            {
                dic["successMsg"] = "OK";
                dic["response"] = "C0000";
                return dic;
            }
            var length = rawShade.Length;

            switch (length)
            {
                case < 5:
                    dic["errMsg"] = "ERR: Shade is less than 5 character.";
                    dic["response"] = "";
                    return dic;
                case 5:
                    dic["successMsg"] = "OK";
                    dic["response"] = rawShade;
                    return dic;
                case 6:
                    var first_Letter = char.IsLetter(rawShade[0]);
                    var last_Letter = char.IsLetter(rawShade[^1]);
                    var shade = string.Empty;
                    if (first_Letter)
                    {
                        shade = rawShade.Remove(0, 1);
                    }
                    if (last_Letter)
                    {
                        shade = rawShade.Remove(rawShade.Length - 1);
                    }

                    if (shade.Length == 5)
                    {
                        dic["successMsg"] = "OK";
                        dic["response"] = shade;
                        return dic;
                    }
                    if (shade.Length < 5)
                    {
                        dic["errMsg"] = "ERR: Shade contains first char letter and last char letter. Invalid Shade. Character count<5";
                        dic["response"] = "";
                        return dic;
                    }
                    if (shade.Length == 6)
                    {
                        var isAllNumber = rawShade.All(char.IsNumber);
                        if (isAllNumber)
                        {
                            dic["successMsg"] = "OK";
                            dic["response"] = rawShade;
                            return dic;
                        }
                        dic["errMsg"] = "ERR: Invalid Shade. 6 Character shade but All is not number;";
                        dic["response"] = "";
                        return dic;

                    }
                    break;
                case 7:
                    var fl = char.IsLetter(rawShade[0]);
                    var ll = char.IsLetter(rawShade[^1]);
                    var newShade = string.Empty;
                    if (fl)
                    {
                        newShade = rawShade.Remove(0);
                    }
                    if (ll)
                    {
                        newShade = rawShade.Remove(rawShade.Length - 1);
                    }
                    if (newShade.Length == 6)
                    {
                        var isAllNumber = rawShade.All(char.IsNumber);
                        if (isAllNumber)
                        {
                            dic["successMsg"] = "OK";
                            dic["response"] = rawShade;
                            return dic;
                        }
                        dic["errMsg"] = "ERR: Invalid Shade. 6 Character shade but All is not number;";
                        dic["response"] = "";
                        return dic;

                    }
                    dic["errMsg"] = "ERR: Invalid Shade.";
                    dic["response"] = "";
                    return dic;
            }

            return dic;
        }


        public static string GetBuyerIdByBuyerName(string buyerName)
        {
            string query = @"
                   DECLARE @BuyerShortCode nvarchar(150);
                   DECLARE @buyerId nvarchar(190);
                   SET @BuyerShortCode = @BuyerShortCodeParam;
                   
                   SET @buyerId = (
                       SELECT TOP 1 BUYERID 
                       FROM N2017.DBO.RPA_BUYERTRACE 
                       WHERE BUYERSHORTCODE =  @buyerName
                         AND [TRACEFOR] = 'UNIVOGUE'
                    );

                    SELECT ISNULL(@buyerId, 'ERROR:Buyer Id not found for ' + @BuyerShortCode) AS BuyerId;";
            using var connection =
                new SqlConnection(
                    "Data Source=192.168.0.12\\anesql;Initial Catalog=N2017;Persist Security Info=True;User ID=sa;Password=aNe_#(abc321@#;Encrypt=False");
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@buyerName", buyerName);
            connection.Open();
            var result = command.ExecuteScalar();
            return result?.ToString() ?? "ERR: Unexpected error occurred when buyer map.";
        }


        public static List<ItemDetails> MergeItems(List<ItemDetails> itemDetails)
        {
            var groupedItems = new Dictionary<string, ItemDetails>();
            foreach (var item in itemDetails)
            {
                var key = item.TKT + "-" + item.Shade + "-" + item.Length + "-" + item.FinType + "-" + item.ExpShipDate;
                if (groupedItems.ContainsKey(key))
                {
                    groupedItems[key].ItemQty += item.ItemQty;
                }
                else
                {
                    groupedItems[key] = item;
                }
            }

            return groupedItems.Values.ToList();
        }
    }

    public class ItemDetails
    {
        public string TKT { get; set; }
        public string Shade { get; set; }
        public string Length { get; set; }
        public string FinType { get; set; }
        //
        public string Description { get; set; }
        public string Location { get; set; }
        public double UnitPrice { get; set; }
        public int ItemQty { get; set; }
        public string ExpShipDate { get; set; }
        public string ShadeColor { get; set; }
        //
        public string LineMessage { get; set; }
        public string SameGroupIdentifier { get; set; }

    }
    public class PdfDetailsData
    {
        public string Serial { get; set; }
        public string ItemName { get; set; }
        public string Style { get; set; }
        public string PO { get; set; }
        public string ItemColorSize { get; set; }
        public string Shade { get; set; }
        public string Unit { get; set; }
        public string Qty { get; set; }
    }
}