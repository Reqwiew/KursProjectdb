﻿

using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Protocols;

namespace KursProjectdb.Services;

public abstract class BaseService<T>
{
    protected SqlConnection objSqlconnection;
    protected SqlCommand objSqlCommand;
    public BaseService()
    {
        objSqlconnection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString);
        objSqlCommand = new SqlCommand();
        objSqlCommand.Connection = objSqlconnection;
        objSqlCommand.CommandType = CommandType.StoredProcedure;
    }
    public abstract List<T> GetAll();
    public abstract bool Add(T obj);
    public abstract bool Update(T obj);
    public abstract bool Delete(int id);
}