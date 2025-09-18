using System.Data;
using KursProjectdb.Model;
using Microsoft.Data.SqlClient;

namespace KursProjectdb.Services;

public class SpecializationService: BaseService<Specialization>
{
     public SpecializationService() : base()
    {
    }

    public override bool Add(Specialization obj)
    {
        bool IsAdded = false;
        if (obj.FacultyId == null || obj.SpecializationTitle == null)
            throw new ArgumentException("Не все поля заполнены");
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_InsertSpecializations";
            objSqlCommand.Parameters.AddWithValue("@FacultyId", obj.FacultyId);
            objSqlCommand.Parameters.AddWithValue("@SpecializationTitle", obj.SpecializationTitle);
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
            objSqlCommand.CommandText = "udp_DeleteSpecializations";
            objSqlCommand.Parameters.AddWithValue("@SpecializationId", id);
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

    public override List<Specialization> GetAll()
    {
        List<Specialization> list = new List<Specialization>();
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_GetAllSpecializations";
            objSqlCommand.Connection.Open();
            var ObjSqlDataReader = objSqlCommand.ExecuteReader();
            if (ObjSqlDataReader.HasRows)
            {
                Specialization objSpecialization = null;
                while (ObjSqlDataReader.Read())
                {
                    objSpecialization = new Specialization();
                    objSpecialization.Id = ObjSqlDataReader.GetInt32(0);
                    objSpecialization.FacultyId = ObjSqlDataReader.GetInt32(1);
                    objSpecialization.SpecializationTitle = ObjSqlDataReader.GetString(2);
                    objSpecialization.FacultyName = ObjSqlDataReader.GetString(3);
                  
                    
                    list.Add(objSpecialization);
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

    public override bool Update(Specialization obj)
    {
        bool IsUpdated = false;
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_UpdateSpecializations";
            objSqlCommand.Parameters.AddWithValue("@SpecializationId", obj.Id);
            objSqlCommand.Parameters.AddWithValue("@FacultyId", obj.FacultyId);
            objSqlCommand.Parameters.AddWithValue("@SpecializationTitle", obj.SpecializationTitle);
            

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