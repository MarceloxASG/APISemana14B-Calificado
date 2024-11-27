using APISemana11A.Requests;

public class StudentListRequest
{
    public int GradeID { get; set; }
    public List<StudentIdRequest> Students { get; set; }
}
