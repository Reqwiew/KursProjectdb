using System.Data;
using KursProjectdb.Model;
using Microsoft.Data.SqlClient;

namespace KursProjectdb.Services;

public class StudentService : BaseService<Student>
{
    public StudentService() : base()
    {
    }

    public override bool Add(Student obj)
    {
        bool IsAdded = false;
        if (obj.FullName == null || obj.Birthday == null || obj.Gender == null || obj.Gender == null)
            throw new ArgumentException("Не все поля заполнены");
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_InsertStudents";
            objSqlCommand.Parameters.AddWithValue("@FullName", obj.FullName);
            objSqlCommand.Parameters.AddWithValue("@Birthday", obj.Birthday);
            objSqlCommand.Parameters.AddWithValue("@Gender", obj.Gender);
            objSqlCommand.Parameters.AddWithValue("@GroupId", obj.GroupId);
            objSqlCommand.Connection.Open();
            int addRows = objSqlCommand.ExecuteNonQuery();
            IsAdded = addRows > 0;
        }
        catch (SqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            objSqlconnection.Close();
        }

        return IsAdded;
    }

    public override bool Delete(int id)
    {
        bool IsDeleted = false;
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_DeleteStudents";
            objSqlCommand.Parameters.AddWithValue("@StudentId", id);
            objSqlCommand.Connection.Open();
            int deleteRows = objSqlCommand.ExecuteNonQuery();
            IsDeleted = deleteRows > 0;
        }
        catch (SqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            objSqlconnection.Close();
        }

        return IsDeleted;
    }

    public override List<Student> GetAll()
    {
        List<Student> list = new List<Student>();
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_GetAllStudents"; 
            objSqlCommand.CommandType = CommandType.StoredProcedure; 
            objSqlCommand.Connection.Open();

            using (var ObjSqlDataReader = objSqlCommand.ExecuteReader())
            {
                if (ObjSqlDataReader.HasRows)
                {
                    Student objStudent = null;
                    while (ObjSqlDataReader.Read())
                    {
                        objStudent = new Student();
                        objStudent.Id = ObjSqlDataReader.GetInt32(0); 
                        objStudent.FullName = ObjSqlDataReader.GetString(1); 
                        objStudent.Birthday = ObjSqlDataReader.GetDateTime(2); 
                        objStudent.Gender = ObjSqlDataReader.GetString(3);
                        objStudent.GroupId = ObjSqlDataReader.GetInt32(4); 
                        objStudent.GroupName = ObjSqlDataReader.GetString(5); 
                        list.Add(objStudent);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            objSqlconnection.Close();
        }

        return list;
    }

    public override bool Update(Student obj)
    {
        bool IsUpdated = false;
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_UpdateStudents";
            objSqlCommand.Parameters.AddWithValue("@StudentId", obj.Id);
            objSqlCommand.Parameters.AddWithValue("@FullName", obj.FullName);
            objSqlCommand.Parameters.AddWithValue("@Birthday", obj.Birthday);
            objSqlCommand.Parameters.AddWithValue("@Gender", obj.Gender);
            objSqlCommand.Parameters.AddWithValue("@GroupId", obj.GroupId);
            objSqlCommand.Connection.Open();
            int updateRows = objSqlCommand.ExecuteNonQuery();
            IsUpdated = updateRows > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            objSqlconnection.Close();
        }

        return IsUpdated;
    }
}