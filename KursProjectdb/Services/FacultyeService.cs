using KursProjectdb.Model;
using Microsoft.Data.SqlClient;

namespace KursProjectdb.Services;

public class FacultyeService: BaseService<Facultye>
{
     public FacultyeService() : base()
    {
    }

    public override bool Add(Facultye obj)
    {
        bool IsAdded = false;
        if (obj.FacultyName == null)
            throw new ArgumentException("Не все поля заполнены");
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_InsertFaculties";
            objSqlCommand.Parameters.AddWithValue("@FacultyName", obj.FacultyName);
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
            objSqlCommand.CommandText = "udp_DeleteFaculties";
            objSqlCommand.Parameters.AddWithValue("@FacultyId", id);
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

    public override List<Facultye> GetAll()
    {
        List<Facultye> list = new List<Facultye>();
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_GetAllFaculties";
            objSqlCommand.Connection.Open();
            var ObjSqlDataReader = objSqlCommand.ExecuteReader();
            if (ObjSqlDataReader.HasRows)
            {
                Facultye objFacultye = null;
                while (ObjSqlDataReader.Read())
                {
                    objFacultye = new Facultye();
                    objFacultye.Id = ObjSqlDataReader.GetInt32(0);
                    objFacultye.FacultyName = ObjSqlDataReader.GetString(1);
                    
                    
                    list.Add(objFacultye);
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

    public override bool Update(Facultye obj)
    {
        bool IsUpdated = false;
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_UpdateFaculties";
            objSqlCommand.Parameters.AddWithValue("@FacultyId", obj.Id);
            objSqlCommand.Parameters.AddWithValue("@FacultyName", obj.FacultyName);
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