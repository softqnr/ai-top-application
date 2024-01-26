namespace AiTopStudentStatus.Models
{
    public class Student : Base
    {
        private int _studentId;
        private string _studentName;
        private string _classId;
        private int _behaviouralState;
        private int _learningState;
        private int _heartRate;
        private int _emotionalState;

        public int StudentId
        {
            get => _studentId;
            set => SetField(ref _studentId, value);
        }

        public string StudentName
        {
            get => _studentName;
            set => SetField(ref _studentName, value);
        }

        public string ClassId
        {
            get => _classId;
            set => SetField(ref _classId, value);
        }

        public int BehaviouralState
        {
            get => _behaviouralState;
            set => SetField(ref _behaviouralState, value);
        }

        public int LearningState
        {
            get => _learningState;
            set => SetField(ref _learningState, value);
        }

        public int HeartRate
        {
            get => _heartRate;
            set => SetField(ref _heartRate, value);
        }

        public int EmotionalState
        {
            get => _emotionalState;
            set => SetField(ref _emotionalState, value);
        }
    }
}