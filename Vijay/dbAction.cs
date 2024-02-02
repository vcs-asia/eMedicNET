using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Vijay
{
    public class dbAction
    {
        string dbType = "";
        string conString = "";
        objDL objdl = new objDL();
        public dbAction(string _dbType, string _conString)
		{
			this.dbType = _dbType;
			this.conString = _conString;
		}
        public objDL returnList(string sQry)
        {
            this.objdl.dataSet = new DataSet();
            MySqlConnection mySqlConnection = null;
            try
            {
                mySqlConnection = new MySqlConnection(this.conString);
                MySqlCommand mySqlCommand = null;
                mySqlCommand = new MySqlCommand(sQry, mySqlConnection)
                {
                    CommandTimeout = 0
                };
                mySqlConnection.Open();
                (new MySqlDataAdapter(mySqlCommand)).Fill(this.objdl.dataSet);
                if (this.objdl.dataSet.Tables[0].Rows.Count <= 0)
                {
                    this.objdl.flaG = false;
                    this.objdl.Msg = "ERROR! No records found.";
                }
                else
                {
                    this.objdl.flaG = true;
                    this.objdl.Msg = "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                this.objdl.flaG = false;
                this.objdl.Msg = string.Concat("ERROR:", ex.Message);
            }
            finally
            {
                mySqlConnection.Close();
            }
            return this.objdl;
        }
        public string run(string sQry, string userID)
        {
            string str = "";
            MySqlConnection mySqlConnection = new MySqlConnection(this.conString);
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();

            MySqlTransaction transaction = null;
            try
            {
                mySqlConnection.Open();
                transaction = mySqlConnection.BeginTransaction();
                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.Transaction = transaction;

                mySqlCommand.CommandText = sQry;
                mySqlCommand.ExecuteNonQuery();

                mySqlCommand.CommandText = "INSERT INTO RECORD_LOG(REC_LOGID, REC_ADTTM, REC_QUERY) VALUES('" + userID + "', NOW(), '" + sQry.Replace("'","`") + "')";
                mySqlCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception exception)
            {
                str = string.Concat("ERROR:", exception.Message);
                transaction.Rollback();
            }
            finally
            {
                mySqlConnection.Close();
            }
            return str;
        }
        public string runStoredProcedure(string sProcedure, List<dbParam> objParam, List<dbParam> objCondition)
        {
            string str = "";
            MySqlConnection mySqlConnection = new MySqlConnection(this.conString);
            try
            {
                MySqlCommand mySqlCommand = new MySqlCommand()
                {
                    Connection = mySqlConnection,
                    CommandText = sProcedure,
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 0
                };
                MySqlCommand mySqlCommand1 = mySqlCommand;
                if (objParam.Count > 0)
                {
                    for (int i = 0; i < objParam.Count; i++)
                    {
                        if (!(objParam[i].dType == "L"))
                        {
                            mySqlCommand1.Parameters.AddWithValue(string.Concat('@', objParam[i].col), objParam[i].val);
                        }
                        else
                        {
                            mySqlCommand1.Parameters.AddWithValue(string.Concat('@', objParam[i].col), objParam[i].image);
                        }
                    }
                }
                mySqlConnection.Open();
                str = mySqlCommand1.ExecuteNonQuery().ToString();
            }
            catch (Exception exception)
            {
                str = string.Concat("ERROR:", exception.ToString());
            }
            finally
            {
                mySqlConnection.Close();
            }
            return str;
        }
        public string commitValuesToDB(string sTable, string sAction, List<dbParam> objCondition, List<dbParam> objParam, string userID)
        {
            int i;
            string[] item;
            string str;
            string str1 = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            string str5 = "";

            if (sAction == "INSERT")
            {
                for (i = 0; i < objParam.Count; i++)
                {
                    str2 = string.Concat(str2, ",", objParam[i].col);
                    str3 = string.Concat(str3, ", @", objParam[i].col);
                }
                str2 = str2.Substring(1);
                str3 = str3.Substring(1);
                item = new string[] { "INSERT INTO ", sTable, " (", str2, ") VALUES(", str3, ")" };
                str5 = string.Concat(item);
            }
            else if (sAction == "UPDATE")
            {
                for (i = 0; i < objParam.Count; i++)
                {
                    str = str2;
                    item = new string[] { str, ",", objParam[i].col, "= @", objParam[i].col };
                    str2 = string.Concat(item);
                }
                for (i = 0; i < objCondition.Count; i++)
                {
                    str = str4;
                    item = new string[] { str, "AND ", objCondition[i].col, "=@", objCondition[i].col };
                    str4 = string.Concat(item);
                }
                str2 = str2.Substring(1);
                str4 = str4.Substring(3);
                item = new string[] { "UPDATE ", sTable, " SET ", str2, " WHERE ", str4 };
                str5 = string.Concat(item);
            }

            MySqlConnection mySqlConnection = new MySqlConnection(this.conString);
            MySqlCommand mySqlCommand = mySqlConnection.CreateCommand();
            mySqlCommand.CommandTimeout = 0;
            MySqlTransaction transaction = null;

            try
            {
                mySqlConnection.Open();
                transaction = mySqlConnection.BeginTransaction();

                mySqlCommand.Connection = mySqlConnection;
                mySqlCommand.Transaction = transaction;
                mySqlCommand.CommandText = str5;

                for (i = 0; i < objParam.Count; i++)
                {
                    if (!(objParam[i].dType == "L"))
                    {
                        mySqlCommand.Parameters.AddWithValue(string.Concat("@", objParam[i].col), objParam[i].val);
                    }
                    else
                    {
                        mySqlCommand.Parameters.AddWithValue(string.Concat("@", objParam[i].col), objParam[i].image);
                    }
                }
                if (objCondition != null)
                {
                    for (i = 0; i < objCondition.Count; i++)
                    {
                        mySqlCommand.Parameters.AddWithValue(string.Concat("@", objCondition[i].col), objCondition[i].val);
                    }
                }
                mySqlCommand.ExecuteNonQuery();

                string strDetails = "";

                for (int valI = 0; valI < objParam.Count; valI++ )
                {
                    strDetails += objParam[valI].val.ToString().Replace("'","`") + ",";
                }

                mySqlCommand.CommandText = "INSERT INTO RECORD_LOG(REC_LOGID, REC_ADTTM, REC_QUERY) VALUES('" + userID + "', NOW(), '" + sAction + " " + sTable + " " + strDetails + "')";
                mySqlCommand.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception exception)
            {
                str1 = string.Concat("ERROR:", exception.ToString());
                transaction.Rollback();
            }
            finally
            {
                mySqlConnection.Close();
            }
            return str1;
        }
        public bool checkRecordLockStatus(int xID, string xTable, int uID)
        {
            bool bStatus = false;
            objDL objdl = new objDL();

            string query = "SELECT COUNT(1) FROM record_lock_info WHERE TBLNAME = '" + xTable + "' AND USER_ID != '" + uID + "' AND ID = '" + xID + "'";
            objdl = returnList(query);
            if (objdl.flaG==true &&  Convert.ToInt32(objdl.dataSet.Tables[0].Rows[0][0].ToString()) > 0)
                bStatus = true;
            return bStatus;
        }
        public string lockRecord(int xID, string xTable, int uID, string userID)
        {
            string msg = "";

            string query = "INSERT INTO RECORD_LOCK_INFO(TBLNAME, USER_ID, ID) VALUES('" +xTable + "', '" + uID + "', '" + xID + "')";
            msg = run(query, userID);
            return msg;
        }
        public int getLatestID(string sTable, string sCol)
        {
            int intID = 0;

            MySqlConnection con = new MySqlConnection(conString);
            try
            {
                MySqlCommand cmd = new MySqlCommand("SELECT MAX(" + sCol + ") FROM " + sTable, con);
                con.Open();

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader[0].ToString() == "")
                        {
                            intID = 1;
                        }
                        else
                        {
                            intID = reader.GetInt32(0) + 1;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.ToString());
            }
            finally
            {
                con.Close();
            }

            return intID;
        }
        public string saveCollection(List<objData> objdata, List<string> gID)
        {
            string msg = "";
            bool saveFlag;
            string condition;
            int mainID = 0;
            List<string> uQuery = new List<string>();
            List<string> dQuery = new List<string>();

            try
            {
                if (gID[0] == "" || gID[0] == "0")
                {
                    mainID = getLatestID(gID[1], gID[2]);
                }
                for (int i = 0; i < objdata.Count; i++)
                {
                    condition = "";
                    saveFlag = true;
                    if (objdata[i].KeyCol.Count > 0)
                    {
                        condition = " WHERE ";
                        for (int xInc = 0; xInc < objdata[i].KeyCol.Count; xInc++)
                        {
                            if (xInc == objdata[i].KeyCol.Count - 1)
                            {
                                condition += objdata[i].KeyCol[xInc] + " = '" + objdata[i].KeyVal[xInc] + "'";
                                if (objdata[i].KeyVal[xInc] == "0")
                                    saveFlag = false;
                            }
                            else
                            {
                                condition += objdata[i].KeyCol[xInc] + " = '" + objdata[i].KeyVal[xInc] + "' AND ";
                                if (objdata[i].KeyVal[xInc] == "0")
                                    saveFlag = false;
                            }
                        }
                    }

                    string sQuery = "";
                    if (objdata[i].Delete == true)
                    {
                        sQuery = "DELETE FROM " + objdata[i].xTable + condition;
                        dQuery.Add(sQuery);
                        sQuery = "";
                        saveFlag = false;
                    }

                    for (int index = 0; index < objdata[i].CValue.Count; index++)
                    {
                        string columns = "";
                        string vcolumn = "";
                        if (saveFlag == false)
                        {
                            sQuery = "INSERT INTO " + objdata[i].xTable + " (";
                            for (int seq = 0; seq < objdata[i].Column.Count; seq++)
                            {
                                if (seq == objdata[i].Column.Count - 1)
                                {
                                    columns += objdata[i].Column[seq];
                                    vcolumn += String.Concat("@", objdata[i].Column[seq]);
                                }
                                else
                                {
                                    columns += String.Concat(objdata[i].Column[seq], ",");
                                    vcolumn += String.Concat("@" + objdata[i].Column[seq], ",");
                                }
                            }

                            sQuery += String.Concat(columns, ") VALUES(",  vcolumn, ")");
                        }
                        else
                        {
                            sQuery = "UPDATE " + objdata[i].xTable + " SET ";
                            for (int seq = 0; seq < objdata[i].Column.Count; seq++)
                            {
                                if (seq == objdata[i].Column.Count - 1)
                                {
                                    columns += String.Concat(objdata[i].Column[seq], " = @", objdata[i].Column[seq]);
                                }
                                else
                                {
                                    columns += String.Concat(objdata[i].Column[seq], " = @", objdata[i].Column[seq], ",");
                                }
                            }
                            sQuery += String.Concat(columns, condition);
                        }
                        uQuery.Add(sQuery);
                    }
                }
                //DB Operation
                MySqlConnection con = new MySqlConnection(conString);
                MySqlCommand cmd = con.CreateCommand();

                MySqlTransaction transaction = null;
                try
                {
                    con.Open();
                    transaction = con.BeginTransaction();
                    cmd.Connection = con;
                    cmd.Transaction = transaction;
                    foreach (string qry in dQuery)
                    {
                        cmd.CommandText = qry;
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "INSERT INTO RECORD_LOG(REC_LOGID, REC_ADTTM, REC_QUERY) VALUES('" + gID[3] + "', NOW(), '" + qry.Replace("'","`") + "')";
                        cmd.ExecuteNonQuery();
                    }
                    int queryIncrement = 0;
                    for (int i = 0; i < objdata.Count; i++)
                    {
                        foreach (List<string> eachValue in objdata[i].CValue)
                        {
                            string queryValues = "";
                            cmd.Parameters.Clear();
                            cmd.CommandText = uQuery[queryIncrement];
                            for (int nInc = 0; nInc < eachValue.Count; nInc++)
                            {
                                if ((eachValue[0]=="0" || eachValue[0]=="") && nInc==0)
                                {
                                    cmd.Parameters.AddWithValue(String.Concat("@", objdata[i].Column[nInc]), mainID);
                                    queryValues += eachValue[0].Replace("'","`") + ",";
                                }
                                else
                                {
                                    cmd.Parameters.AddWithValue(String.Concat("@", objdata[i].Column[nInc]), eachValue[nInc]);
                                    queryValues += eachValue[nInc].Replace("'", "`") + ",";
                                }
                            }
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = "INSERT INTO RECORD_LOG(REC_LOGID, REC_ADTTM, REC_QUERY) VALUES('" + gID[3] + "', NOW(), '" + uQuery[queryIncrement].Replace("'","`") + " " + queryValues + "')";
                            cmd.ExecuteNonQuery();

                            queryIncrement++;
                        }
                    }
                    transaction.Commit();
                }
                catch(Exception ex)
                {
                    msg += String.Concat("DB-ERROR: ", ex.ToString(), "\n");
                    try
                    {
                        transaction.Rollback();
                    }
                    catch(Exception rEx)
                    {
                        msg += String.Concat("ROLLBACK-ERROR:", rEx.ToString());
                    }
                }
                finally
                {
                    con.Close();
                }
            }
            catch(Exception ex)
            {
                msg += String.Concat("MAIN-ERROR: ", ex.ToString(), "\n");
            }
            return msg;
        }
    }
}
