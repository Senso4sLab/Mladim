using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Client.ViewModels.Anketa;

public abstract class QuestionVM
{
    public string Text { get; set;}

    protected QuestionVM(string text) =>
        this.Text = text;
}

public class RatingResponseQuestionVM : QuestionVM
{
    public double Answer { get; set; }
    private RatingResponseQuestionVM(string text) : base(text) { }   

    public static RatingResponseQuestionVM Create(string text) =>
        new RatingResponseQuestionVM(text);

}

public class BooleanResponseQuestionVM : QuestionVM
{
    public double Answer { get; set; }
    private BooleanResponseQuestionVM(string text) : base(text) { }

    public static BooleanResponseQuestionVM Create(string text) =>
        new BooleanResponseQuestionVM(text);

}

public class TextResponseQuestionVM : QuestionVM
{
    public string? Answer { get; set; }
    private TextResponseQuestionVM(string text) : base(text) { }

    public static TextResponseQuestionVM Create(string text) =>
        new TextResponseQuestionVM(text);

}



public class MultipleRatingQuestionsVM : QuestionVM
{
    public List<RatingResponseQuestionVM> RatingResponseQuestions = new List<RatingResponseQuestionVM>();    
    private MultipleRatingQuestionsVM(string text):base(text) { }

    public MultipleRatingQuestionsVM AddRatingButtonResponse(string response)
    {
        RatingResponseQuestions.Add(RatingResponseQuestionVM.Create(response));
        return this;
    }


    public static MultipleRatingQuestionsVM Create(string question)
    {
        return new MultipleRatingQuestionsVM(question);
    }
}


public class RadioButtonResponseVM : QuestionVM
{
    public int Answer { get; set; }

    private RadioButtonResponseVM(string text) : base(text) { }

    public static RadioButtonResponseVM Create(string text) =>
        new RadioButtonResponseVM(text);
}
