using KursProjectdb.Model;
using Microsoft.Data.SqlClient;

namespace KursProjectdb.Services;

public class AcademicPerformanceService : BaseService<AcademicPerformance>
{
    public AcademicPerformanceService() : base()
    {
    }

    public override bool Add(AcademicPerformance obj)
    {
        bool IsAdded = false;
        if (obj.Rating == null || obj.GroupId == null || obj.StudentId == null || obj.SubjectId == null)
            throw new ArgumentException("Не все поля заполнены");
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_InsertAcademicPerformances";
            objSqlCommand.Parameters.AddWithValue("@StudentId", obj.StudentId);
            objSqlCommand.Parameters.AddWithValue("@AcademicPlanId", obj.AcademicPlanId);
            objSqlCommand.Parameters.AddWithValue("@SubjectId", obj.SubjectId);
            objSqlCommand.Parameters.AddWithValue("@Term", obj.Term);
            objSqlCommand.Parameters.AddWithValue("@Rating", obj.Rating);
            objSqlCommand.Parameters.AddWithValue("@DateOffset", obj.DateOffset);
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
            objSqlCommand.CommandText = "udp_DeleteAcademicPerformances";
            objSqlCommand.Parameters.AddWithValue("@AcademicPerformanceId", id);
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

    public override List<AcademicPerformance> GetAll()
    {
        List<AcademicPerformance> list = new List<AcademicPerformance>();
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_GetAllAcademicPerformances";
            objSqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            objSqlCommand.Connection.Open();
            
            using (var reader = objSqlCommand.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AcademicPerformance performance = new AcademicPerformance
                        {
                            Id = reader.GetInt32(0),
                            StudentId = reader.GetInt32(1),
                            StudentFullName = reader.GetString(2),
                            AcademicPlanId = reader.GetInt32(3),
                            SubjectId = reader.GetInt32(4),
                            SubjectTitle = reader.GetString(5),
                            Term = reader.GetInt32(6),
                            Rating = reader.GetInt32(7),
                            DateOffset = reader.GetDateTime(8),
                            GroupId = reader.GetInt32(9),
                            GroupName = reader.GetString(10)
                        };
                        list.Add(performance);
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

    public override bool Update(AcademicPerformance obj)
    {
        bool IsUpdated = false;
        try
        {
            objSqlCommand.Parameters.Clear();
            objSqlCommand.CommandText = "udp_UpdateAcademicPerformances";
            objSqlCommand.Parameters.AddWithValue("@AcademyPerformanceId", obj.Id);
            objSqlCommand.Parameters.AddWithValue("@StudentId", obj.StudentId);
            objSqlCommand.Parameters.AddWithValue("@AcademicPlanId", obj.AcademicPlanId);
            objSqlCommand.Parameters.AddWithValue("@SubjectId", obj.SubjectId);
            objSqlCommand.Parameters.AddWithValue("@Term", obj.DateOffset);
            objSqlCommand.Parameters.AddWithValue("@Rating", obj.Rating);
            objSqlCommand.Parameters.AddWithValue("@DateOffset", obj.DateOffset);
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