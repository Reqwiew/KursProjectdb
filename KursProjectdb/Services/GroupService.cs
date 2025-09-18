using KursProjectdb.Model;
using Microsoft.Data.SqlClient;

namespace KursProjectdb.Services;

public class GroupService: BaseService<Group>
{
     public GroupService() : base()
    {
    }

    public override bool Add(Group obj)
    {
        bool IsAdded = false;
    
        // Правильная проверка обязательных полей
        if (obj.GroupName == null || obj.SpecializationId == 0)
            throw new ArgumentException("Не все поля заполнены");
    
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_InsertGroups";
            objSqlCommand.Parameters.AddWithValue("@Course", obj.Course);
            objSqlCommand.Parameters.AddWithValue("@GroupName", obj.GroupName);
            objSqlCommand.Parameters.AddWithValue("@SpecializationId", obj.SpecializationId);
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
            objSqlCommand.CommandText = "udp_DeleteGroups";
            objSqlCommand.Parameters.AddWithValue("@GroupId", id);
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

    public override List<Group> GetAll()
    {
        List<Group> list = new List<Group>();
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_GetAllGroups";
            objSqlCommand.Connection.Open();
            var ObjSqlDataReader = objSqlCommand.ExecuteReader();
            if (ObjSqlDataReader.HasRows)
            {
                Group objGroup = null;
                while (ObjSqlDataReader.Read())
                {
                    objGroup = new Group();
                    objGroup.Id = ObjSqlDataReader.GetInt32(0);
                    objGroup.Course = ObjSqlDataReader.GetInt32(1);
                    objGroup.GroupName = ObjSqlDataReader.GetString(2);
                    objGroup.SpecializationId = ObjSqlDataReader.GetInt32(3);
                    objGroup.SpecializationTitle = ObjSqlDataReader.GetString(4);
                  
                    
                    list.Add(objGroup);
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

    public override bool Update(Group obj)
    {
        bool IsUpdated = false;
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_UpdateGroups";
            objSqlCommand.Parameters.AddWithValue("@GroupId", obj.Id);
            objSqlCommand.Parameters.AddWithValue("@Course", obj.Course);
            objSqlCommand.Parameters.AddWithValue("@GroupName", obj.GroupName);
            objSqlCommand.Parameters.AddWithValue("@SpecializationId", obj.SpecializationId);
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