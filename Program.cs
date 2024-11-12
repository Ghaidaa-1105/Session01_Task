using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEEE_TASKS
{ 
  public class Student
  {
      public string Name { get; set; }
      public int Score { get; set; }

      public Student(string _Name, int _Score)
      {
        Name = _Name;
        Score = _Score;
      }
  }


     public delegate bool ScoreCriteria(Student student);

     public delegate double CalcAverageScore(List<Student> students);


       public static class ScoreProcessor
       {
          public static List<Student> FilterStudentsByCriteria ( this List<Student> students, ScoreCriteria scoreCriteria)
          {
              var filteredStudents = new List<Student>();
              foreach (var student in students)
                if (scoreCriteria(student))
                    filteredStudents.Add(student);

                       return filteredStudents;
          }

          public static double CalculateAverageScore(List<Student> students, CalcAverageScore calcAverageScore)
          {
            double totalScore = 0;
            foreach (var student in students)
            {
                totalScore += student.Score;
            }
            return totalScore / students.Count;
            
          }
       }


       public static class StudentExtensions
       {
          public static void PrintStudentList(this List<Student> students)
          {
              foreach (var student in students)
                 Console.WriteLine($"Name: {student.Name}, Score: {student.Score}");
          }

          public static Student TopScorer(this List<Student> students)
          {
              if (students == null || students.Count == 0)
                 return null;

             Student topScorer = students[0];
              foreach (var student in students)
                if (student.Score > topScorer.Score)
                  topScorer = student;

                     return topScorer;
          }
       }

    internal class Program
    {
        static void Main(string[] args)
        {
           
            var students = new List<Student>
            {
                new Student("Ahmed", 85),
                new Student("Mohamed", 70),
                new Student("Ebrahim", 92),
                new Student("Mai", 68),
                new Student("Eyad", 55),
            };

           
            ScoreCriteria passCriteria = student => student.Score >= 60;
            ScoreCriteria failCriteria = student => student.Score < 60;


            var passingStudents = students.FilterStudentsByCriteria(passCriteria);
            Console.WriteLine("Passing Students:");
            passingStudents.PrintStudentList();

            Console.WriteLine( "--------------------------------");

            var failingStudents = ScoreProcessor.FilterStudentsByCriteria(students, failCriteria);
            Console.WriteLine("Failing Students:");
            failingStudents.PrintStudentList();

            Console.WriteLine("--------------------------------");

            CalcAverageScore averageScoreCalc = studentList =>
            {
                double totalScore = 0;
                foreach (var student in studentList)
                {
                    totalScore += student.Score;
                }
                return totalScore / studentList.Count;
            };

            Console.WriteLine("--------------------------------");

            var topScorer = students.TopScorer();
            if (topScorer != null)
                Console.WriteLine($"Top Scorer: {topScorer.Name} the Score: {topScorer.Score}");

            Console.ReadKey();
        }
    }
}