using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace AiTopStudentStatus.Api.Models
{
    public class Student
    {
        [JsonProperty("studentId")]
        public int StudentId { get; set; }

        [JsonProperty("studentName")]
        [StringLength(50)]
        public string StudentName { get; set; }

        [JsonProperty("classId")]
        [StringLength(10)]
        public string ClassId { get; set; }

        [JsonProperty("behaviouralState")]
        public int BehaviouralState { get; set; }

        [JsonProperty("learningState")]
        public int LearningState { get; set; }

        [JsonProperty("heartRate")]
        public int HeartRate { get; set; }

        [JsonProperty("emotionalState")]
        public int EmotionalState { get; set; }
    }
}