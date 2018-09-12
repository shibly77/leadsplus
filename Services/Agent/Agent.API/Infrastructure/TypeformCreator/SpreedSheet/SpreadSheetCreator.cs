//using Google.Apis.Auth.OAuth2;
//using Google.Apis.Services;
//using Google.Apis.Sheets.v4;
//using Google.Apis.Sheets.v4.Data;
//using Google.Apis.Util.Store;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;

//namespace TypeFormIntegration
//{

//    public class SpreadSheetCreator
//    {
//        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
//        static string ApplicationName = "agnt sheet";
       
//        private ConsoleLogger _logger;
//        private UserCredential _credential;
//        private SheetsService sheetsService;
//        private string _spreadSheetId;
//        private string _range;

//        private readonly string _spreadSheetName;
//        private readonly string _workSheetName;
//        private readonly IList<object> _headerValues;
//        private readonly IList<object> _initialValues;

//        public SpreadSheetCreator(SpreadSheetCreateOptions options)
//        {
//            _logger = new ConsoleLogger();

//            _spreadSheetName = options.SpreadSheetName;
//            _workSheetName = options.WorkSheetName;
//            _headerValues = options.headerValues;
//            _initialValues = options.InitialValues;          
//        }

//        public void Create()
//        {
//            //Load creds
//            LoadCredentials();            

//            //Create service
//            CreateSheetsService();

//            // TODO : Check if spreadsheet exists
//            //Create spreadshit and work sheet
//            CreateSpreadSheet(_spreadSheetName, _workSheetName);

//            // TODO : Handle errors
//            //InsertHeader
//            InsertHeader();

//            //TODO : Validate values
//            //Inser first row
//            InsertFirstRow();
//        }
//        private void CreateSheetsService() => sheetsService = new SheetsService(new BaseClientService.Initializer()
//        {
//            HttpClientInitializer = _credential,
//            ApplicationName = ApplicationName,
//        });
//        private void LoadCredentials()
//        {

//            using (var stream =
//                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
//            {
//                string credPath = "token.json";
//                _credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
//                    GoogleClientSecrets.Load(stream).Secrets,
//                    Scopes,
//                    "user",
//                    CancellationToken.None,
//                    new FileDataStore(credPath, true)).Result;
//                Console.WriteLine("Credential file saved to: " + credPath);
//            }

//        }
//        private void CreateSpreadSheet(string spreadSheetName, string worksheetName)
//        {

//            Spreadsheet createRequest = new Spreadsheet
//            {
//                Properties = new SpreadsheetProperties
//                {
//                    //NOTE : Name using agent's identity 
//                    Title = spreadSheetName
//                }
//            };


//            var sheet = new Sheet
//            {
//                Properties = new SheetProperties
//                {
//                    //NOTE : This will follow type form name
//                    Title = worksheetName
//                }
//            };

//            createRequest.Sheets = new List<Sheet>
//            {
//                sheet
//            };


//            _logger.Log(Type.Info, $"Sending request for {spreadSheetName} -> {_workSheetName} to google docs");
//            var request = sheetsService.Spreadsheets.Create(createRequest);
//            var response = request.Execute();
//            _logger.Log(Type.Info, $"Action completed ! {response.SpreadsheetId} is created");

//            _spreadSheetId = response.SpreadsheetId;

//        }
//        private void InsertHeader()
//        {
//            string sheet = _workSheetName;

//            CreateRange(sheet);

//            var valueRange = new ValueRange
//            {
//                Values = new List<IList<object>> { _headerValues }
//            };

//            _logger.Log(Type.Info, "Trying to insert header.");
//            var appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, _spreadSheetId, _range);
//            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

//            var appendReponse = appendRequest.Execute();
//            _logger.Log(Type.Info, "Completed inserting header.");


//            HighLightHeader();

//        }
//        private void CreateRange(string sheet)
//        {
//            char rangeStart = 'A';
//            char rangeEnd = 'A';
//            int totValues = _headerValues.Count;
//            for (int i = 0; i < totValues; i++)
//            {
//                rangeEnd = rangeStart++;
//            }

//            _range = $"{sheet}!{rangeStart}:{rangeEnd}";
//        }
//        private void HighLightHeader()
//        {
//            Spreadsheet spr = sheetsService.Spreadsheets.Get(_spreadSheetId).Execute();
//            Sheet sh = spr.Sheets.Where(s => s.Properties.Title == _workSheetName).FirstOrDefault();
//            int sheetId = (int)sh.Properties.SheetId;

//            //define cell color
//            var userEnteredFormat = new CellFormat()
//            {

//                TextFormat = new TextFormat()
//                {
//                    Bold = true
//                }
//            };
//            BatchUpdateSpreadsheetRequest bussr = new BatchUpdateSpreadsheetRequest();

//            //create the update request for cells from the first row
//            var updateCellsRequest = new Request()
//            {
//                RepeatCell = new RepeatCellRequest()
//                {
//                    Range = new GridRange()
//                    {
//                        SheetId = sheetId,
//                        StartColumnIndex = 0,
//                        StartRowIndex = 0,
//                        EndColumnIndex = 28,
//                        EndRowIndex = 1
//                    },
//                    Cell = new CellData()
//                    {
//                        UserEnteredFormat = userEnteredFormat
//                    },
//                    Fields = "UserEnteredFormat(TextFormat)"
//                }
//            };
//            bussr.Requests = new List<Request>
//            {
//                updateCellsRequest
//            };

//            var bur = sheetsService.Spreadsheets.BatchUpdate(bussr, _spreadSheetId);
//            bur.Execute();

//        }
//        private void InsertFirstRow()
//        {
//            string sheet = _workSheetName;
//            var valueRange = new ValueRange
//            {
//                Values = new List<IList<object>> { _initialValues }
//            };

//            _logger.Log(Type.Info, "Trying to insert first row.");

//            var appendRequest = sheetsService.Spreadsheets.Values.Append(valueRange, _spreadSheetId, _range);
//            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
//            var appendReponse = appendRequest.Execute();

//            _logger.Log(Type.Info, "Completed inserting first row.");

//        }
//    }
//}
