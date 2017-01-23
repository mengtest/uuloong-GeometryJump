using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iBoxDB.LocalServer;
using UnityEngine;
/*********************************************************************//**
*	namespace	:	Assets.Scripts.DB
*
*	describe	:	N/A
*	class name	:	DBOperation
*
*	Ver		|	change date			|		author			|		describe	|
*	--------|-----------------------|-----------------------|-------------------|
*	V0.01	|	2016/9/7 22:13:11				|		Mr.Li			|					|
*
*	Copytight (c) 2015 Goodall	Corporation. All rights reserved.
*
*	|-----------------------------------------------------------------------|
*	|	以下信息为公司机密，未经本公司书面同意禁止向第三方披露				|
*	|	版权所有：百益技术有限公司											|
*	|-----------------------------------------------------------------------|
*
**********************************************************************//**/

public class DataBase {
    public static string Path() {
        return Application.persistentDataPath;
    }

    public static int DBAdress {
        get {
            return 2;
        }
    }

    public static string Key {
        get {
            return "Id";
        }
    }
}
public class DBOperation {
    private DB server = null;
    private DB.AutoBox db = null;
    private static DBOperation _instance;

    public static DBOperation GetInstance() {
        if( null == _instance ) {
            _instance = new DBOperation();
            _instance.Init();
        }
        return _instance;
    }
    DBOperation() {

    }

    ~DBOperation() {
        server.Close();
        server.Dispose();
    }
    void Init() {
        DB.Root(DataBase.Path());
        Debug.Log(DataBase.Path());
        server = new DB(DataBase.DBAdress);
        db = server.Open();
    }

    /// <summary>
    /// 保存对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="obj">对象</param>
    /// <returns></returns>
    public  bool Save<T>(T obj) where T:class {
        EnsureTable<T>();
        // db.Update<T>(typeof(T).ToString(),key,obj);
        return db.Insert<T>(typeof(T).ToString(), obj);
    }

    /// <summary>
    /// 查询所有记录
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="order">排序字段</param>
    /// <returns></returns>
    public List<T> FindAll<T>(string order = "") where T : class,new() {
        EnsureTable<T>();
        List<T> ret = new List<T>();
        try {
            string sql = "from " + typeof(T).ToString();
            var t = db.Select<T>(sql);
            var _arry = t.ToArray();
            for (int i = 0; i < _arry.Length; i++) {
                ret.Add(_arry[i]);
            }
            return ret;
        } catch(Exception e) {
            throw e;
        }
    }

    /// <summary>
    /// 查询一个结果
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="sql">查询SQl语句 from typeof(T).ToString() </param>
    /// <returns></returns>
    public T FindOneBySql<T>(string Key ,object value) where T:class, new() {
        EnsureTable<T>();
        try {
            string sql = string.Format("from {0} where {1} ==?", typeof(T).ToString(), Key);
            var t =  db.Select<T>(sql,value);
            var _arr = t.ToArray();
            if (_arr.Length >= 1)
                return _arr[0];
            else
                return null;
        } catch(Exception e) {
            throw e;
        }

    }

    /// <summary>
    /// 根据查询语句返回对象数组
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sql"></param>
    /// <returns></returns>
    public T[] FindArryBySql<T>(string sql = "") where T : class, new() {
        EnsureTable<T>();
        try {
            var t = db.Select<T>(sql);
            return t.ToArray();
        } catch(Exception e) {
            throw e;
        }
    }

    /// <summary>
    /// 根据给定的键值删除记录
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    //public bool Delete<T>(object key) where T : class {
    //    EnsureTable<T>();
    //    try {
    //        return db.Delete(typeof(T).ToString(), key);
    //    } catch(Exception e) {
    //        throw e;
    //    }

    //}

    public bool Delete<T>(params object[] ks) where T : class {
        EnsureTable<T>();
        try {
            var box = db.Cube();
            var b = box.Bind(typeof(T).ToString(),ks);
            b.Delete();
            return box.Commit().Assert();
        } catch (Exception e) {
            throw e;
        }
    }

    /// <summary>
    ///  获取表中记录个数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public long GetCount<T>()where T : class {
        EnsureTable<T>();
        try {
            string sql = string.Format("from {0} where Id !=?", typeof(T).ToString());
            return db.SelectCount(sql,-1);
        } catch (Exception e) {
            throw e;
        }
    }

    /// <summary>
    /// 获取一个新的ID。id从0开始
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T NewID<T>() where T: class ,new() {
        EnsureTable<T>();
        try {

            string   _order = " order by " + DataBase.Key  + " limit 0 ";
            string sql = "from " + typeof(T).ToString() + _order;
            var t = db.Select<T>(sql);
            var _arry = t.ToArray();
            if( _arry == null || _arry.Count<T>() <= 0 ) {
                return null;
            } else {
                int index = _arry.Count<T>();
                return _arry[index-1];
            }
        } catch (Exception e) {
            throw e;
        }
    }
    /// <summary>
    /// 用于检测正在使用的表是否存在，如果不存在则直接创建一个
    /// </summary>
    /// <typeparam name="T">对象类型-表名</typeparam>
    void EnsureTable<T>() where T:class {

        var t =  db.GetDatabase().GetSchemata().Keys;
        string Dtable = typeof(T).ToString();
        bool isExit = false;
        if( null != t && t.Count != 0) {
            foreach (string table in t) {
                // Debug.Log(table);
                if (table == Dtable) {
                    isExit = true;
                    break;
                }
            }
        }


        if( !isExit ) {
            server.Close();
            server = new DB(DataBase.DBAdress);
            DB.Root(DataBase.Path());
            server = new DB(DataBase.DBAdress);
            server.GetConfig().EnsureTable<T>(typeof(T).ToString(),DataBase.Key);
            db = server.Open();
        }
    }
}

