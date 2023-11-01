namespace OnlineStudyShared.Message.Event;

public class CourseUpdateEvent
{
    public string CourseId { get; set; }
    public string NewCourseName { get; set; }
    public string NewCourseImage { get; set; }
    public decimal NewCoursePrice { get; set; }
}