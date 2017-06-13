using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Universidad
{
  public class Student
  {
    private string _name;
    private bool _enrollment;
    private int _id;

    public Student(string Name, bool Enrollment = false, int Id = 0 )
    {
      _name = Name;
      _id = Id;
      _enrollment = Enrollment;
    }

    public override bool Equals(System.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool nameEquality = this.GetName() == newStudent.GetName();
        bool idEquality = this.GetId() == newStudent.GetId();
        bool enrollmentEquality = this.GetEnrollment() == newStudent.GetEnrollment();

        return (nameEquality && idEquality && enrollmentEquality);
      }
    }

      //Getters
      public string GetName()
      {
        return _name;
      }
      public bool GetEnrollment()
      {
        return _enrollment;
      }
      public int GetId()
      {
        return _id;
      }
      //Setters
      public void SetName(string newName)
      {
        _name = newName;
      }
      public void SetEnrollment(bool newEnrollment)
      {
        _enrollment = newEnrollment;
      }

      public static List<Student> GetAll() //GetAll
      {
        List<Student> allStudents = new List<Student>{};

        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("SELECT * FROM student;", conn);
        SqlDataReader  rdr = cmd.ExecuteReader();

        while (rdr.Read())
        {
          int studentId = rdr.GetInt32(0);
          string studentName = rdr.GetString(1);
          bool studentEnrollment = rdr.GetBoolean(2);
          Student newStudent = new Student(studentName, studentEnrollment, studentId);
          allStudents.Add(newStudent);
        }
        if (rdr != null)
        {
          rdr.Close();
        }
        if (conn != null)
        {
          conn.Close();
        }

        return allStudents;
      }

      public static void DeleteAll()
      {
        SqlConnection conn = DB.Connection();
        conn.Open();

        SqlCommand cmd = new SqlCommand("DELETE * FROM student");
        cmd.ExecuteNonQuery();
        conn.Close();
      }
    }
}
