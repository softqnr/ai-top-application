using AiTopStudentStatus.Models;
using AiTopStudentStatus.Pages;
using AiTopStudentStatus.Tools;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AiTopStudentStatus;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class MainPage : ContentPage
{
    private const string API_URL = "https://softqnrapi20230628122139.azurewebsites.net/api/Students";
    //private const string API_URL = "http://10.0.2.2:5170/api/Students";
    //private const string API_URL = "http://localhost:5170/api/Students";

    private string _classId;
    private string _listHash;
    private bool _isLoading;

    private ObservableCollection<Student> _studentsList = new ObservableCollection<Student>();
    public ObservableCollection<Student> StudentsList
    {
        get => _studentsList;
        set
        {
            _studentsList = value;
            OnPropertyChanged(nameof(StudentsList));
        }
    }

    public bool IsLoading
    {
        get => _isLoading;
        set 
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
            OnPropertyChanged(nameof(IsListEmpty));
        }
    }

    public bool IsListEmpty => !StudentsList.Any();

    public MainPage()
	{
        InitializeComponent();
        BindingContext = this;
        Application.Current.UserAppTheme = AppTheme.Light;
    }

    private async Task<bool> IsUpdateAvailable()
    {
        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("ListHash", _listHash);
        var response = await httpClient.GetAsync($"{API_URL}/CheckForUpdate/{_classId}");
        return bool.Parse(await response.Content.ReadAsStringAsync());
    }

    private async void ListenToService()
    {
        try
        {
            while (true)
            {
                if (!(await IsUpdateAvailable()))
                {
                    await Task.Delay(3000);
                    continue;
                }

                if (!StudentsList.Any())
                    IsLoading = true;

                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync($"{API_URL}/{_classId}");
                var json = await response.Content.ReadAsStringAsync();
                _listHash = Helpers.GetDeterministicHashCode(json);

                if (string.IsNullOrWhiteSpace(json) || json == "[null]" || json == "[]")
                {
                    StudentsList.Clear();
                    IsLoading = false;
                    await Task.Delay(3000);
                    continue;
                }

                var students = JsonConvert.DeserializeObject<Student[]>(json);
 
                var newlyAddedStudents = students.Where(x => !StudentsList.Any(y => y.StudentId == x.StudentId) && !string.IsNullOrWhiteSpace(x.ClassId));
                if (newlyAddedStudents.Any())
                    foreach (var item in newlyAddedStudents)
                    {
                        Vibrate(item);
                        StudentsList.Add(item);
                    }

                if (StudentsList.Any())
                {
                    // Remove students that aren't in income list
                    for (int i = StudentsList.Count - 1; i >=0; i--)
                    {
                        if (!students.Any(x => x.StudentId == StudentsList[i].StudentId))
                            StudentsList.RemoveAt(i);
                    }

                    foreach (var student in students)
                    {
                        var s = StudentsList.FirstOrDefault(x => x.StudentId.Equals(student.StudentId));

                        if (s != null)
                        {
                            if (s.BehaviouralState.Equals(student.BehaviouralState) &&
                                s.LearningState.Equals(student.LearningState) &&
                                s.EmotionalState.Equals(student.EmotionalState) &&
                                s.HeartRate.Equals(student.HeartRate)) 
                                continue;

                            if (string.IsNullOrWhiteSpace(student.ClassId))
                                StudentsList.Remove(s);
                            else
                            {
                                if (!s.BehaviouralState.Equals(student.BehaviouralState) || 
                                    !s.LearningState.Equals(student.LearningState))
                                {
                                    Vibrate(student);
                                }

                                s.BehaviouralState = student.BehaviouralState;
                                s.LearningState = student.LearningState;
                                s.EmotionalState = student.EmotionalState;
                                s.HeartRate = student.HeartRate;       
                            }
                        }
                    }
                }
                else
                {
                    foreach (var item in students)
                    {
                        Vibrate(item);
                        StudentsList.Add(item);
                    }
                }

                SortStudentsList();

                IsLoading = false;

                await Task.Delay(3000);
            }
        }
        catch (Exception)
        {
            //await DisplayAlert("Attention", ex.Message, "Close");
            await Task.Delay(3000);
            ListenToService();
        }
    }

    private void Vibrate(Student student)
    {
        if (student.BehaviouralState == 3 && student.LearningState == 3)
            Vibration.Default.Vibrate(TimeSpan.FromMilliseconds(1000));
    }

    private void SortStudentsList()
    {
        StudentsList = new ObservableCollection<Student>(StudentsList.OrderByDescending(x => x.BehaviouralState)
                                                                     .ThenByDescending(x => x.LearningState));
    }

    private async void OnButtonClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(Entry.Text))
        {
            await DisplayAlert("Attention", "Please enter class ID", "Close");
            return;
        }

        Entry.IsEnabled = false;
        Entry.IsEnabled = true;

        _classId = Entry.Text.Trim();

        FirstStack.IsVisible = false;
        MainStack.IsVisible = true;

        Title = $"Class {_classId}";
        Shell.SetNavBarIsVisible(this, true);

        ListenToService();
    }

    private async void ListViewStudentsItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (ListViewStudents.SelectedItem == null)
            return;

        var selectedStudent = ListViewStudents.SelectedItem as Student;
        await Navigation.PushAsync(new StudentDetails(selectedStudent));

        ListViewStudents.SelectedItem = null;
    }
}