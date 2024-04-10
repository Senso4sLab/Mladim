using CsvHelper;
using Mladim.Client.ViewModels.Survey;
using System.Globalization;

namespace Mladim.Client.Services.SurveyCsvGenerator;

//public abstract class SurveyResponseCsvFormat
//{
//    public abstract void GenerateCsvStream(CsvWriter writter);
    
//}


//public class SurveyRatingResponseCsvFormat : SurveyResponseCsvFormat
//{
//    private  SurveyRatingResponsesGroupedByQuestion RatingResponses { get; }
//    public SurveyRatingResponseCsvFormat(SurveyRatingResponsesGroupedByQuestion ratingResponses)
//    {
//        this.RatingResponses = ratingResponses; 
//    }

//    public override void GenerateCsvStream(CsvWriter writter)
//    {       
//        //List<SurveyParticipantRow> rows = new List<SurveyParticipantRow>();

//        //var writer = new StreamWriter(path);

//        //using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
//        //{           
//        //    foreach (var row in rows)
//        //    {
//        //        csv.WriteComment(RatingResponses.Question);
//        //        csv.NextRecord();
//        //        csv.WriteRecords(row.ParticipantsPerType);
//        //        csv.NextRecord();
//        //    }           
//        //}
//        //return writer.BaseStream;
//    }
//}





//public class SurveyBoleanResponseCsvFormat : SurveyResponseCsvFormat
//{
//    private SurveyBoleanResponsesGroupedByQuestion BoleanResponses { get; }
//    public SurveyBoleanResponseCsvFormat(SurveyBoleanResponsesGroupedByQuestion boleanResponses)
//    {
//        this.BoleanResponses = boleanResponses;
//    }   

//    public override void GenerateCsvStream(CsvWriter writter)
//    {
//        throw new NotImplementedException();
//    }
//}

