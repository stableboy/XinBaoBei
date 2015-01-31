using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;

public class SqlServerHelper : DatabaseHelper
{
    private int _commandTimeout;
    private string _sqlConnStr;
    private SetOfBookType setOfBook = SetOfBookType.Offical;

    public SqlServerHelper(SetOfBookType sobType)
    {
        setOfBook = sobType;

        this._commandTimeout = 30;
        //this._sqlConnStr = ConfigurationSettings.AppSettings["SqlConnStr"].ToString();
        this._sqlConnStr = ConfigurationSettings.AppSettings[setOfBook.ToString()].ToString();

        if (ConfigurationSettings.AppSettings["CommandTimeout"] != null)
        {
            this._commandTimeout = int.Parse(ConfigurationSettings.AppSettings["CommandTimeout"].ToString());
        }

    }

    public SqlServerHelper(string SqlConnStr)
    {
        this._commandTimeout = 30;
        this._sqlConnStr = SqlConnStr;
    }

    public TableTypeParameter CreateParameter(string p_paramName, object p_paramValue)
    {
        return new TableTypeParameter { ParameterName = p_paramName, ParameterValue = p_paramValue };
    }

    public SqlParameter CreateParameter(string p_paramName, SqlDbType p_paramType, object p_paramSize, object p_paramValue, ParameterDirection p_paramDirection)
    {
        SqlParameter parameter = new SqlParameter(p_paramName, p_paramType);
        if (p_paramSize != null)
        {
            parameter.Size = int.Parse(p_paramSize.ToString());
        }
        if (p_paramValue != null)
        {
            parameter.Value = p_paramValue;
        }
        parameter.Direction = p_paramDirection;
        return parameter;
    }

    public int ExecuteNonQuery(CommandType p_CommandType, string p_CommandText, params TableTypeParameter[] p_Parameters)
    {
        int result = 0;
        SqlConnection connection = null;
        SqlCommand command = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };
            OpenConnection(connection);
            command = new SqlCommand(p_CommandText, connection)
            {
                CommandType = p_CommandType,
                CommandTimeout = this._commandTimeout
            };
            if ((p_Parameters != null) && (p_Parameters.Length != 0))
            {
                foreach (TableTypeParameter parameter in p_Parameters)
                {
                    command.Parameters.AddWithValue(parameter.ParameterName, parameter.ParameterValue);
                }
            }
            result = command.ExecuteNonQuery();
        }
        catch (Exception exception)
        {
            throw exception;
        }
        finally
        {
            if (command != null)
            {
                command.Dispose();
            }
            CloseConnection(connection);
        }
        return result;
    }

    private static void OpenConnection(SqlConnection connection)
    {
        if (connection.State != ConnectionState.Open)
        {
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("数据库连接失败,请联系管理员! SqlServer:{0}", connection.ConnectionString));
            }
        }
    }

    private static void CloseConnection(SqlConnection connection)
    {
        if (connection != null)
        {
            connection.Close();
        }
    }

    public int ExecuteNonQuery(CommandType p_CommandType, string p_CommandText, SqlParameter[][] p_Parameters)
    {
        int result = 0;
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlTransaction transaction = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            transaction = connection.BeginTransaction();
            for (int i = 0; i < p_Parameters.Length; i++)
            {
                command = new SqlCommand(p_CommandText, connection, transaction)
                {
                    CommandType = p_CommandType,
                    CommandTimeout = this._commandTimeout
                };
                if (p_Parameters[i] != null)
                {
                    if (p_Parameters[i].Length != 0)
                    {
                        foreach (SqlParameter parameter in p_Parameters[i])
                        {
                            command.Parameters.Add(parameter);
                        }
                    }
                    result = command.ExecuteNonQuery();
                }
            }
            transaction.Commit();
        }
        catch (Exception exception)
        {
            if (command != null
                && command.Transaction != null
                )
            {
                command.Transaction.Rollback();
            }
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
            if (transaction != null)
            {
                transaction.Dispose();
            }
        }
        return result;
    }

    public int ExecuteNonQuery(CommandType p_CommandType, string p_CommandText, TableTypeParameter[][] p_Parameters)
    {
        int result = 0;
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlTransaction transaction = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            transaction = connection.BeginTransaction();
            for (int i = 0; i < p_Parameters.Length; i++)
            {
                command = new SqlCommand(p_CommandText, connection, transaction)
                {
                    CommandType = p_CommandType,
                    CommandTimeout = this._commandTimeout
                };
                if (p_Parameters[i] != null)
                {
                    if (p_Parameters[i].Length != 0)
                    {
                        //foreach (SqlParameter parameter in p_Parameters[i])
                        //{
                        //    command.Parameters.Add(parameter);
                        //}
                        foreach (TableTypeParameter parameter in p_Parameters[i])
                        {
                            command.Parameters.AddWithValue(parameter.ParameterName, parameter.ParameterValue);
                        }
                    }
                    result = command.ExecuteNonQuery();
                }
            }
            transaction.Commit();
        }
        catch (Exception exception)
        {
            if (command != null
                && command.Transaction != null
                )
            {
                command.Transaction.Rollback();
            }
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
            if (transaction != null)
            {
                transaction.Dispose();
            }
        }
        return result;
    }

    public int ExecuteNonQuery(CommandType p_CommandType, string p_CommandText, params SqlParameter[] p_Parameters)
    {
        int result = 0;
        SqlConnection connection = null;
        SqlCommand command = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            command = new SqlCommand(p_CommandText, connection)
            {
                CommandType = p_CommandType,
                CommandTimeout = this._commandTimeout
            };
            if ((p_Parameters != null) && (p_Parameters.Length != 0))
            {
                foreach (SqlParameter parameter in p_Parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            result = command.ExecuteNonQuery();
        }
        catch (Exception exception)
        {
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
        }
        return result;
    }

    public object ExecuteScalar(CommandType p_CommandType, string p_CommandText, params TableTypeParameter[] p_Parameters)
    {
        object result = null;
        SqlConnection connection = null;
        SqlCommand command = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            command = new SqlCommand(p_CommandText, connection)
            {
                CommandType = p_CommandType,
                CommandTimeout = this._commandTimeout
            };
            if ((p_Parameters != null) && (p_Parameters.Length > 0))
            {
                foreach (TableTypeParameter parameter in p_Parameters)
                {
                    command.Parameters.AddWithValue(parameter.ParameterName, parameter.ParameterValue);
                }
            }
            result = command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
        }
        return result;
    }

    public object ExecuteScalar(CommandType p_CommandType, string p_CommandText, params SqlParameter[] p_Parameters)
    {
        object result = null;
        SqlConnection connection = null;
        SqlCommand command = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            command = new SqlCommand(p_CommandText, connection)
            {
                CommandType = p_CommandType,
                CommandTimeout = this._commandTimeout
            };
            if ((p_Parameters != null) && (p_Parameters.Length != 0))
            {
                foreach (SqlParameter parameter in p_Parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            result = command.ExecuteScalar();
        }
        catch (Exception exception)
        {
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
        }
        return result;
    }

    public void Fill(DataSet ds, CommandType p_CommandType, string p_CommandText, params TableTypeParameter[] p_Parameters)
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataAdapter adapter = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            command = new SqlCommand
            {
                Connection = connection,
                CommandText = p_CommandText,
                CommandType = p_CommandType,
                CommandTimeout = this._commandTimeout,
                Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
            };
            if ((p_Parameters != null) && (p_Parameters.Length > 0))
            {
                foreach (TableTypeParameter parameter in p_Parameters)
                {
                    command.Parameters.AddWithValue(parameter.ParameterName, parameter.ParameterValue);
                }
            }
            adapter = new SqlDataAdapter
            {
                SelectCommand = command
            };
            adapter.Fill(ds);
        }
        catch (Exception exception)
        {
            if(command != null
                && command.Transaction != null
                )
            {
                command.Transaction.Rollback();
            }
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
            if (adapter != null)
            {
                adapter.Dispose();
            }
        }
    }

    public void Fill(DataTable dt, CommandType p_CommandType, string p_CommandText, params TableTypeParameter[] p_Parameters)
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataAdapter adapter = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            command = new SqlCommand
            {
                Connection = connection,
                CommandText = p_CommandText,
                CommandType = p_CommandType,
                CommandTimeout = this._commandTimeout,
                Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
            };
            if ((p_Parameters != null) && (p_Parameters.Length > 0))
            {
                foreach (TableTypeParameter parameter in p_Parameters)
                {
                    command.Parameters.AddWithValue(parameter.ParameterName, parameter.ParameterValue);
                }
            }
            adapter = new SqlDataAdapter
            {
                SelectCommand = command
            };
            adapter.Fill(dt);
        }
        catch (Exception exception)
        {
            if (command != null
                && command.Transaction != null
                )
            {
                command.Transaction.Rollback();
            }
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
            if (adapter != null)
            {
                adapter.Dispose();
            }
        }
    }

    public void Fill(DataTable dt, CommandType p_CommandType, string p_CommandText, SqlParameter[] p_Parameters)
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataAdapter adapter = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            command = new SqlCommand
            {
                Connection = connection,
                CommandText = p_CommandText,
                CommandType = p_CommandType,
                CommandTimeout = this._commandTimeout,
                Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
            };
            if ((p_Parameters != null) && (p_Parameters.Length > 0))
            {
                foreach (SqlParameter parameter in p_Parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            adapter = new SqlDataAdapter
            {
                SelectCommand = command
            };
            adapter.Fill(dt);
        }
        catch (Exception exception)
        {
            if (command != null
                && command.Transaction != null
                )
            {
                command.Transaction.Rollback();
            }
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
            if (adapter != null)
            {
                adapter.Dispose();
            }
        }
    }

    public void Fill(DataSet ds, string[] p_TableNames, CommandType p_CommandType, string p_CommandText, SqlParameter[] p_Parameters)
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataAdapter adapter = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            command = new SqlCommand
            {
                Connection = connection,
                CommandText = p_CommandText,
                CommandType = p_CommandType,
                CommandTimeout = this._commandTimeout,
                Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
            };
            if ((p_Parameters != null) && (p_Parameters.Length > 0))
            {
                foreach (SqlParameter parameter in p_Parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            adapter = new SqlDataAdapter
            {
                SelectCommand = command
            };
            if ((p_TableNames != null) && (p_TableNames.Length > 0))
            {
                for (int i = 0; i < p_TableNames.Length; i++)
                {
                    adapter.TableMappings.Add("Table" + ((i == 0) ? string.Empty : i.ToString()), p_TableNames[i]);
                }
            }
            adapter.Fill(ds);
        }
        catch (Exception exception)
        {
            if (command != null
                && command.Transaction != null
                )
            {
                command.Transaction.Rollback();
            }
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
            if (adapter != null)
            {
                adapter.Dispose();
            }
        }
    }

    public void Fill(DataTable dt, CommandType p_CommandType, string p_CommandText, TableTypeParameter[] p_TableParams, SqlParameter[] p_SqlParams)
    {
        SqlConnection connection = null;
        SqlCommand command = null;
        SqlDataAdapter adapter = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };

            OpenConnection(connection);
            command = new SqlCommand
            {
                Connection = connection,
                CommandText = p_CommandText,
                CommandType = p_CommandType,
                CommandTimeout = this._commandTimeout,
                Transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted)
            };
            if ((p_TableParams != null) && (p_TableParams.Length > 0))
            {
                foreach (TableTypeParameter parameter in p_TableParams)
                {
                    command.Parameters.AddWithValue(parameter.ParameterName, parameter.ParameterValue);
                }
            }
            if ((p_SqlParams != null) && (p_SqlParams.Length > 0))
            {
                foreach (SqlParameter parameter2 in p_SqlParams)
                {
                    command.Parameters.Add(parameter2);
                }
            }
            adapter = new SqlDataAdapter
            {
                SelectCommand = command
            };
            adapter.Fill(dt);
        }
        catch (Exception exception)
        {
            if (command != null
                && command.Transaction != null
                )
            {
                command.Transaction.Rollback();
            }
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (command != null)
            {
                command.Dispose();
            }
            if (adapter != null)
            {
                adapter.Dispose();
            }
        }
    }

    public void SqlBulkCopy(DataTable dtSource, string tableName)
    {
        SqlConnection connection = null;
        System.Data.SqlClient.SqlBulkCopy copy = null;
        try
        {
            connection = new SqlConnection
            {
                ConnectionString = this._sqlConnStr
            };
            copy = new System.Data.SqlClient.SqlBulkCopy(connection)
            {
                BatchSize = dtSource.Rows.Count,
                BulkCopyTimeout = 30,
                DestinationTableName = tableName
            };
            copy.WriteToServer(dtSource);
        }
        catch (Exception exception)
        {
            throw exception;
        }
        finally
        {
            CloseConnection(connection);
            if (copy != null)
            {
                copy.Close();
            }
        }
    }
}
