using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Universidad
{
  [Collection("Universidad")]
  public class UniversidadTest : IDisposable
  {
    public UniversidadTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=universidad_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_Student_EmptyAtFirst()
    {
      //Arrange, Act
      int result = Student.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Equal_ReturnsTrueForSameName()
    {
      //Arrange, Act
      Student studentName1 = new Student("James Franco");
      Student studentName2 = new Student("James Franco");

      //Assert
      Assert.Equal(studentName1, studentName2);
    }

    [Fact]
    public void Test_SaveStudents_SavesToDatabase()
    {
      //Arrange
      Student student1 = new Student("johnny", true);
      student1.Save();

      //Act
      List<Student> allStudents = Student.GetAll();
      Console.WriteLine(allStudents);
      List<Student> testList = new List<Student>{student1};

      //Assert
      Assert.Equal(testList, allStudents);
    }

    public void Dispose()
    {
      Student.DeleteAll();
    //  Course.DeleteAll();
    }
  }
}
