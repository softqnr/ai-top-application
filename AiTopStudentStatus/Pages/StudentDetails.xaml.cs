using AiTopStudentStatus.Models;

namespace AiTopStudentStatus.Pages;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class StudentDetails : ContentPage
{
    private Student _student;
    public Student Student
    {
        get => _student;
        set
        {
            _student = value;
            OnPropertyChanged(nameof(Student));
        }
    }

    public StudentDetails(Student student)
	{
		InitializeComponent();
        Student = student;
        BindingContext = Student;
    }
}