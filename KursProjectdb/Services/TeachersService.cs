using KursProjectdb.Model;
using Microsoft.Data.SqlClient;

namespace KursProjectdb.Services;

public class TeacherService: BaseService<Teacher>
{
     public TeacherService() : base()
    {
    }

    public override bool Add(Teacher obj)
    {
        bool IsAdded = false;
        if (obj.Fullname== null)
            throw new ArgumentException("Не все поля заполнены");
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_InsertTeachers";
            objSqlCommand.Parameters.AddWithValue("@Fullname", obj.Fullname);

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
            objSqlCommand.CommandText = "udp_DeleteTeachers";
            objSqlCommand.Parameters.AddWithValue("@TeacherId", id);
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

    public override List<Teacher> GetAll()
    {
        List<Teacher> list = new List<Teacher>();
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_GetAllTeachers";
            objSqlCommand.Connection.Open();
            var ObjSqlDataReader = objSqlCommand.ExecuteReader();
            if (ObjSqlDataReader.HasRows)
            {
                Teacher objTeacher = null;
                while (ObjSqlDataReader.Read())
                {
                    objTeacher = new Teacher();
                    objTeacher.Id = ObjSqlDataReader.GetInt32(0);
                    objTeacher.Fullname = ObjSqlDataReader.GetString(1);
                    
                    list.Add(objTeacher);
                }
            }

            ObjSqlDataReader.Close();
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

    public override bool Update(Teacher obj)
    {
        bool IsUpdated = false;
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_UpdateTeachers";
            objSqlCommand.Parameters.AddWithValue("@TeacherId", obj.Id);
            objSqlCommand.Parameters.AddWithValue("@Fullname", obj.Fullname);
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