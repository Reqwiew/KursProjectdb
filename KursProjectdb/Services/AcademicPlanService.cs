using KursProjectdb.Model;
using Microsoft.Data.SqlClient;

namespace KursProjectdb.Services;

public class AcademicPlanService: BaseService<AcademicPlan>
{
     public AcademicPlanService() : base()
    {
    }

    public override bool Add(AcademicPlan obj)
    {
        bool IsAdded = false;
        if (obj.SpecializationId == 0 || obj.SubjectId == 0 || obj.Term == 0 || 
            obj.TeacherId == 0 || obj.Hours == 0 || string.IsNullOrEmpty(obj.Form))
            throw new ArgumentException("Не все поля заполнены");
        
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_InsertAcademiPlan";
            objSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objSqlCommand.Parameters.AddWithValue("@SpecializationId", obj.SpecializationId);
            objSqlCommand.Parameters.AddWithValue("@SubjectId", obj.SubjectId);
            objSqlCommand.Parameters.AddWithValue("@Term", obj.Term);
            objSqlCommand.Parameters.AddWithValue("@TeacherId", obj.TeacherId);
            objSqlCommand.Parameters.AddWithValue("@Hours", obj.Hours);
            objSqlCommand.Parameters.AddWithValue("@Form", obj.Form);
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
            objSqlCommand.CommandText = "udp_DeleteAcademicPlans";
            objSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objSqlCommand.Parameters.AddWithValue("@AcademicPlanId", id);
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

    public override List<AcademicPlan> GetAll()
    {
        List<AcademicPlan> list = new List<AcademicPlan>();
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_GetAllAcademicPlans";
            objSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objSqlCommand.Connection.Open();
            
            using (var reader = objSqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AcademicPlan academicPlan = new AcademicPlan
                        {
                            Id = reader.GetInt32(0),
                            SpecializationId = reader.GetInt32(1),
                            SpecializationTitle = reader.GetString(2), 
                            SubjectId = reader.GetInt32(3), 
                            SubjectName = reader.GetString(4),
                            Term = reader.GetInt32(5),
                            TeacherId = reader.GetInt32(6), 
                            TeacherFullName = reader.GetString(7),
                            Hours = reader.GetInt32(8), 
                            Form = reader.GetString(9) 
                        };
                        list.Add(academicPlan);
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

    public override bool Update(AcademicPlan obj)
    {
        bool IsUpdated = false;
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_UpdateAcademicPlans";
            objSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objSqlCommand.Parameters.AddWithValue("@AcademicPlanId", obj.Id);
            objSqlCommand.Parameters.AddWithValue("@SpecializationId", obj.SpecializationId);
            objSqlCommand.Parameters.AddWithValue("@SubjectId", obj.SubjectId);
            objSqlCommand.Parameters.AddWithValue("@Term", obj.Term);
            objSqlCommand.Parameters.AddWithValue("@TeacherId", obj.TeacherId);
            objSqlCommand.Parameters.AddWithValue("@Hours", obj.Hours);
            objSqlCommand.Parameters.AddWithValue("@Form", obj.Form);
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