using KursProjectdb.Model;
using Microsoft.Data.SqlClient;

namespace KursProjectdb.Services;

public class SubjectService: BaseService<Subject>
{
     public SubjectService() : base()
    {
    }

    public override bool Add(Subject obj)
    {
        bool IsAdded = false;
        if (obj.SubjectTitle == null)
            throw new ArgumentException("Не все поля заполнены");
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_InsertSubjects";
            objSqlCommand.Parameters.AddWithValue("@SubjectTitle", obj.SubjectTitle);

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
            objSqlCommand.CommandText = "udp_DeleteSubjects";
            objSqlCommand.Parameters.AddWithValue("@SubjectId", id);
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

    public override List<Subject> GetAll()
    {
        List<Subject> list = new List<Subject>();
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_GetAllSubjects";
            objSqlCommand.Connection.Open();
            var ObjSqlDataReader = objSqlCommand.ExecuteReader();
            if (ObjSqlDataReader.HasRows)
            {
                Subject objSubject = null;
                while (ObjSqlDataReader.Read())
                {
                    objSubject = new Subject();
                    objSubject.Id = ObjSqlDataReader.GetInt32(0);
                    objSubject.SubjectTitle = ObjSqlDataReader.GetString(1);
                    Console.WriteLine(objSubject.Id.ToString(), objSubject.SubjectTitle);
                    list.Add(objSubject);
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

    public override bool Update(Subject obj)
    {
        bool IsUpdated = false;
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_UpdateSubjects";
            objSqlCommand.Parameters.AddWithValue("@SubjectId", obj.Id);
            objSqlCommand.Parameters.AddWithValue("@SubjectTitle", obj.SubjectTitle);
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