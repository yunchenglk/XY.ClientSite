using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace StudentInfo.Help
{
    public class MyExcelUtls
    {
        #region 取文件的扩展名
        /// <summary>
        /// 获取文件拓展名
        /// </summary>
        /// <param name="FileName">文件名称</param>
        /// <returns>string</returns>
        public static string GetExtFileTypeName(string FileName)
        {
            string sFile = FileName;// myFile.PostedFile.FileName;
            sFile = sFile.Substring(sFile.LastIndexOf("\\") + 1);
            sFile = sFile.Substring(sFile.LastIndexOf(".")).ToLower();
            return sFile;
        }
        #endregion

        #region 检查一个文件是不是2007版本的Excel文件
        /// <summary>
        /// 检查一个文件是不是2007版本的Excel文件
        /// </summary>
        /// <param name="FileName">文件名称</param>
        /// <returns>bool</returns>
        public static bool IsExcel2007(string FileName)
        {
            bool r;
            switch (GetExtFileTypeName(FileName))
            {
                case ".xls":
                    r = false;
                    break;
                case ".xlsx":
                    r = true;
                    break;
                default:
                    throw new Exception("你要检查" + FileName + "是2007版本的Excel文件还是之前版本的Excel文件，但是这个文件不是一个有效的Excel文件。");

            }
            return r;
        }

        #endregion

        #region Excel的连接串
        //Excel的连接串
        //2007和之前的版本是有区别的，但是新的可以读取旧的

        /// <summary>
        /// Excel文件在服务器上的OLE连接字符串
        /// </summary>
        /// <param name="excelFile">Excel文件在服务器上的路径</param>
        /// <param name="no_HDR">第一行不是标题：true;第一行是标题：false;</param>
        /// <returns>String</returns>
        public static String GetExcelConnectionString(string excelFile, bool no_HDR)
        {

            try
            {
                if (no_HDR)
                {
                    if (IsExcel2007(excelFile))
                    {
                        return "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + excelFile + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'"; //此连接可以操作.xls与.xlsx文件
                    }
                    else
                    {
                        return "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + excelFile + ";Extended Properties='Excel 8.0; HDR=NO; IMEX=1'"; //此连接只能操作Excel2007之前(.xls)文件

                    }
                }
                else
                {
                    return GetExcelConnectionString(excelFile);
                }
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        /// <summary>
        /// Excel文件在服务器上的OLE连接字符串
        /// </summary>
        /// <param name="excelFile">Excel文件在服务器上的路径</param>
        /// <returns>String</returns>
        public static String GetExcelConnectionString(string excelFile)
        {
            try
            {
                if (IsExcel2007(excelFile))
                {
                    return "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + excelFile + ";Extended Properties='Excel 12.0;  IMEX=1'"; //此连接可以操作.xls与.xlsx文件
                }
                else
                {
                    return "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + excelFile + ";Extended Properties='Excel 8.0;  IMEX=1'"; //此连接只能操作Excel2007之前(.xls)文件

                }
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        /// <summary>
        /// Excel文件在服务器上的OLE连接字符串
        /// </summary>
        /// <param name="excelFile">Excel文件在服务器上的路径</param>
        /// <returns>String</returns>
        public static String GetExcelConnectionStringByWrite(string excelFile)
        {
            try
            {
                if (IsExcel2007(excelFile))
                {
                    return "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + excelFile + ";Extended Properties='Excel 12.0;'"; //此连接可以操作.xls与.xlsx文件
                }
                else
                {
                    return "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + excelFile + ";Extended Properties='Excel 8.0;'"; //此连接只能操作Excel2007之前(.xls)文件

                }
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        #endregion

        #region 读取Excel中的所有表名
        //读取Excel中的所有表名
        //读取Excel文件时，可能一个文件中会有多个Sheet，因此获取Sheet的名称是非常有用的

        /// <summary>
        /// 根据Excel物理路径获取Excel文件中所有表名,列名是TABLE_NAME
        /// </summary>
        /// <param name="excelFile">Excel物理路径</param>
        /// <returns>DataTable</returns>
        public static System.Data.DataTable GetExcelSheetNames2DataTable(string excelFile)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

            try
            {
                string strConn = GetExcelConnectionString(excelFile);
                objConn = new OleDbConnection(strConn);
                objConn.Open();
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dt == null)
                {
                    return null;
                }
                return dt;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }

        /// <summary>
        /// 根据Excel物理路径获取Excel文件中所有表名
        /// </summary>
        /// <param name="excelFile">Excel物理路径</param>
        /// <returns>String[]</returns>
        public static String[] GetExcelSheetNames(string excelFile)
        {
            System.Data.DataTable dt = null;

            try
            {

                dt = GetExcelSheetNames2DataTable(excelFile);
                if (dt == null)
                {
                    return null;
                }
                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                return excelSheets;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
        }
        /// <summary>
        /// 根据Excel物理路径获取Excel文件中所有表名
        /// </summary>
        /// <param name="excelFile">Excel物理路径</param>
        /// <returns>String[]</returns>
        public static List<string> GetExcelSheetNames2List(string excelFile)
        {
            List<string> l = new List<string>();
            try
            {
                if (File.Exists(excelFile))//如果文件不存在，就不用检查了，一定是0个表的
                {
                    string[] t = GetExcelSheetNames(excelFile);
                    foreach (string s in t)
                    {
                        string ss = s;
                        if (ss.LastIndexOf('$') > 0)
                        {
                            ss = ss.Substring(0, ss.Length - 1);
                        }
                        l.Add(ss);
                    }
                }
                return l;
            }
            catch (Exception ee)
            {
                throw ee;
            }

        }
        #endregion

        #region Sheet2DataTable
        /// <summary>
        /// 获取Excel文件中指定SheetName的内容到DataTable
        /// </summary>
        /// <param name="FileFullPath">Excel物理路径</param>
        /// <param name="SheetName">SheetName</param>
        /// <param name="no_HDR">第一行不是标题：true;第一行是标题：false;</param>
        /// <returns>DataTable</returns>
        public static DataTable GetExcelToDataTableBySheet(string FileFullPath, string SheetName, bool no_HDR)
        {
            try
            {
                return GetExcelToDataSet(FileFullPath, no_HDR, SheetName).Tables[SheetName];
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        /// <summary>
        /// 获取Excel文件中指定SheetName的内容到DataTable
        /// </summary>
        /// <param name="FileFullPath">Excel物理路径</param>
        /// <param name="SheetName">SheetName</param>
        /// <returns>DataTable</returns>
        public static DataTable GetExcelToDataTableBySheet(string FileFullPath, string SheetName)
        {
            try
            {
                return GetExcelToDataTableBySheet(FileFullPath, SheetName, false);
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        #endregion

        #region Excel2DataSet
        /// <summary>
        /// 获取Excel文件中所有Sheet的内容到DataSet，以Sheet名做DataTable名
        /// </summary>
        /// <param name="FileFullPath">Excel物理路径</param>
        /// <param name="no_HDR">第一行不是标题：true;第一行是标题：false;</param>
        /// <returns>DataSet</returns>
        public static DataSet GetExcelToDataSet(string FileFullPath, bool no_HDR)
        {
            try
            {
                string strConn = GetExcelConnectionString(FileFullPath, no_HDR);
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                DataSet ds = new DataSet();
                foreach (string colName in GetExcelSheetNames(FileFullPath))
                {
                    OleDbDataAdapter odda = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", colName), conn);                    //("select * from [Sheet1$]", conn);
                    odda.Fill(ds, colName);
                }
                conn.Close();
                return ds;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        /// <summary>
        /// 获取Excel文件中指定Sheet的内容到DataSet，以Sheet名做DataTable名
        /// </summary>
        /// <param name="FileFullPath">Excel物理路径</param>
        /// <param name="no_HDR">第一行不是标题：true;第一行是标题：false;</param>
        /// <param name="SheetName">第一行不是标题：true;第一行是标题：false;</param>
        /// <returns>DataSet</returns>
        public static DataSet GetExcelToDataSet(string FileFullPath, bool no_HDR, string SheetName)
        {
            try
            {
                string strConn = GetExcelConnectionString(FileFullPath, no_HDR);
                OleDbConnection conn = new OleDbConnection(strConn);
                conn.Open();
                DataSet ds = new DataSet();
                OleDbDataAdapter odda = new OleDbDataAdapter(string.Format("SELECT * FROM [{0}]", SheetName), conn);                    //("select * from [Sheet1$]", conn);
                odda.Fill(ds, SheetName);
                conn.Close();
                return ds;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message);
            }
        }
        #endregion

        #region 删除过时文件
        //删除过时文件
        public static bool DeleteOldFile(string servepath)
        {
            try
            {
                FileInfo F = new FileInfo(servepath);
                F.Delete();
                return true;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.Message + "删除" + servepath + "出错.");
            }
        }
        #endregion

        #region 在Excel文件中创建表,Excel物理路径如果文件不是一个已存在的文件，会自动创建文件
        /// <summary>
        /// 在一个Excel文件中创建Sheet
        /// </summary>
        /// <param name="servepath">Excel物理路径,如果文件不是一个已存在的文件，会自动创建文件</param>
        /// <param name="sheetName">Sheet Name</param>
        /// <param name="cols">表头列表</param>
        /// <returns>bool</returns>
        public static bool CreateSheet(string servepath, string sheetName, string[] cols)
        {
            try
            {
                if (sheetName.Trim() == "")
                {
                    throw new Exception("需要提供表名。");
                }
                //if (!File.Exists(servepath))
                //{
                //    throw new Exception(servepath+"不是一个有效的文件路径。");
                //}
                if (cols.Equals(null))
                {
                    throw new Exception("创建表需要提供字段列表。");
                }
                using (OleDbConnection conn = new OleDbConnection(GetExcelConnectionStringByWrite(servepath)))
                {
                    conn.Open();
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    if (sheetName.LastIndexOf('$') > 0)
                    {
                        sheetName = sheetName.Substring(sheetName.Length - 1);
                    }
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 3600;
                    StringBuilder sql = new StringBuilder();
                    sql.Append("CREATE TABLE [" + sheetName + "](");
                    foreach (string s in cols)
                    {
                        sql.Append("[" + s + "] text,");
                    }
                    sql = sql.Remove(sql.Length - 1, 1);
                    sql.Append(")");
                    cmd.CommandText = sql.ToString();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        #endregion

        #region DataTable2Sheet,把一个DataTable写入Excel中的表,Excel物理路径,如果文件不是一个已存在的文件，会自动创建文件
        /// <summary>
        /// 把一个DataTable写入到一个或多个Sheet中
        /// </summary>
        /// <param name="servepath">Excel物理路径,如果文件不是一个已存在的文件，会自动创建文件</param>
        /// <param name="dt">DataTable</param>
        /// <returns>bool</returns>
        public static bool DataTable2Sheet(string servepath, DataTable dt)
        {
            try
            {
                return DataTable2Sheet(servepath, dt, dt.TableName);
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        /// <summary>
        /// 把一个DataTable写入到一个或多个Sheet中
        /// </summary>
        /// <param name="servepath">Excel物理路径,如果文件不是一个已存在的文件，会自动创建文件</param>
        /// <param name="dt">DataTable</param>
        /// <param name="maxrow">一个Sheet的行数</param>
        /// <returns>bool</returns>
        public static bool DataTable2Sheet(string servepath, DataTable dt, int maxrow)
        {
            try
            {
                return DataTable2Sheet(servepath, dt, dt.TableName, maxrow);
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        /// <summary>
        /// 把一个DataTable写入到一个或多个Sheet中
        /// </summary>
        /// <param name="servepath">Excel物理路径,如果文件不是一个已存在的文件，会自动创建文件</param>
        /// <param name="dt">DataTable</param>
        /// <param name="sheetName">Sheet Name</param>
        /// <returns>bool</returns>
        public static bool DataTable2Sheet(string servepath, DataTable dt, string sheetName)
        {
            try
            {
                return DataTable2Sheet(servepath, dt, dt.TableName, 0);
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        /// <summary>
        /// 把一个DataTable写入到一个或多个Sheet中
        /// </summary>
        /// <param name="servepath">Excel物理路径,如果文件不是一个已存在的文件，会自动创建文件</param>
        /// <param name="dt">DataTable</param>
        /// <param name="sheetName">Sheet Name</param>
        /// <param name="maxrow">一个Sheet的行数</param>
        /// <returns>bool</returns>
        public static bool DataTable2Sheet(string servepath, DataTable dt, string sheetName, int maxrow)
        {
            try
            {
                if (sheetName.Trim() == "")
                {
                    throw new Exception("需要提供表名。");
                }
                StringBuilder strSQL = new StringBuilder();
                //看看目标表是否已存在
                List<string> tables = GetExcelSheetNames2List(servepath);
                if (tables.Contains(sheetName))
                {
                    //存在,直接写入
                    using (OleDbConnection conn = new OleDbConnection(GetExcelConnectionStringByWrite(servepath)))
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            StringBuilder strfield = new StringBuilder();
                            StringBuilder strvalue = new StringBuilder();
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                strfield.Append("[" + dt.Columns[j].ColumnName + "]");
                                strvalue.Append("'" + dt.Rows[i][j].ToString() + "'");
                                if (j != dt.Columns.Count - 1)
                                {
                                    strfield.Append(",");
                                    strvalue.Append(",");
                                }
                            }
                            if (maxrow == 0)//不需要限制一个表的行数
                            {
                                cmd.CommandText = strSQL.Append(" insert into [" + sheetName + "]( ")
                                .Append(strfield.ToString()).Append(") values (").Append(strvalue).Append(")").ToString();
                            }
                            else
                            {
                                //加1才可才防止i=0的情况只写入一行
                                string sheetNameT = sheetName + ((i + 1) / maxrow + (Math.IEEERemainder(i + 1, maxrow) == 0 ? 0 : 1)).ToString();
                                if (!tables.Contains(sheetNameT))
                                {
                                    tables = GetExcelSheetNames2List(servepath);
                                    string[] cols = new string[dt.Columns.Count];
                                    for (int ii = 0; ii < dt.Columns.Count; ii++)
                                    {
                                        cols[ii] = dt.Columns[ii].ColumnName;
                                    }
                                    if (!(CreateSheet(servepath, sheetNameT, cols)))
                                    {
                                        throw new Exception("在" + servepath + "上创建表" + sheetName + "失败.");
                                    }
                                    else
                                    {
                                        tables = GetExcelSheetNames2List(servepath);
                                    }
                                }
                                cmd.CommandText = strSQL.Append(" insert into [" + sheetNameT + "]( ")
                                .Append(strfield.ToString()).Append(") values (").Append(strvalue).Append(")").ToString();

                            }
                            cmd.ExecuteNonQuery();
                            strSQL.Remove(0, strSQL.Length);
                        }



                        conn.Close();
                    }
                }
                else
                {
                    //不存在,需要先创建
                    using (OleDbConnection conn = new OleDbConnection(GetExcelConnectionStringByWrite(servepath)))
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;
                        //创建表
                        string[] cols = new string[dt.Columns.Count];
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            cols[i] = dt.Columns[i].ColumnName;
                        }

                        //产生写数据的语句
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            StringBuilder strfield = new StringBuilder();
                            StringBuilder strvalue = new StringBuilder();
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                strfield.Append("[" + dt.Columns[j].ColumnName + "]");
                                strvalue.Append("'" + dt.Rows[i][j].ToString() + "'");
                                if (j != dt.Columns.Count - 1)
                                {
                                    strfield.Append(",");
                                    strvalue.Append(",");
                                }
                            }
                            if (maxrow == 0)//不需要限制一个表的行数
                            {
                                if (!tables.Contains(sheetName))
                                {
                                    if (!(CreateSheet(servepath, sheetName, cols)))
                                    {
                                        throw new Exception("在" + servepath + "上创建表" + sheetName + "失败.");
                                    }
                                    else
                                    {
                                        tables = GetExcelSheetNames2List(servepath);
                                    }
                                }
                                cmd.CommandText = strSQL.Append(" insert into [" + sheetName + "]( ")
                                .Append(strfield.ToString()).Append(") values (").Append(strvalue).Append(")").ToString();
                            }
                            else
                            {
                                //加1才可才防止i=0的情况只写入一行
                                string sheetNameT = sheetName + ((i + 1) / maxrow + (Math.IEEERemainder(i + 1, maxrow) == 0 ? 0 : 1)).ToString();

                                if (!tables.Contains(sheetNameT))
                                {
                                    for (int ii = 0; ii < dt.Columns.Count; ii++)
                                    {
                                        cols[ii] = dt.Columns[ii].ColumnName;
                                    }
                                    if (!(CreateSheet(servepath, sheetNameT, cols)))
                                    {
                                        throw new Exception("在" + servepath + "上创建表" + sheetName + "失败.");
                                    }
                                    else
                                    {
                                        tables = GetExcelSheetNames2List(servepath);
                                    }
                                }
                                cmd.CommandText = strSQL.Append(" insert into [" + sheetNameT + "]( ")
                                .Append(strfield.ToString()).Append(") values (").Append(strvalue).Append(")").ToString();

                                //
                            }
                            cmd.ExecuteNonQuery();
                            strSQL.Remove(0, strSQL.Length);
                        }
                        conn.Close();
                    }
                }
                return true;
            }
            catch (Exception ee)
            {
                throw ee;
            }
        }
        #endregion
    }
}