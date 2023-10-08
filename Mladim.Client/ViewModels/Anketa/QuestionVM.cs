using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Mladim.Client.ViewModels.Anketa;

public abstract class QuestionVM
{
    public string Text { get; protected set;}

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
    public bool Answer { get; set; } = true;
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
