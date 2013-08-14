using Statera.Xamarin.Common;
using System;
namespace Common.Library.Interfaces
{
    interface ISQLiteConnection
    {
        void BeginTransaction();
        TimeSpan BusyTimeout { get; set; }
        void Close();
        void Commit();
        Statera.Xamarin.Common.SQLiteCommand CreateCommand(string cmdText, params object[] ps);
        int CreateIndex(string indexName, string tableName, string columnName, bool unique = false);
        int CreateIndex(string tableName, string columnName, bool unique = false);
        void CreateIndex<T>(System.Linq.Expressions.Expression<Func<T, object>> property, bool unique = false);
        int CreateTable(Type ty, Statera.Xamarin.Common.CreateFlags createFlags = CreateFlags.None);
        int CreateTable<T>(Statera.Xamarin.Common.CreateFlags createFlags = CreateFlags.None);
        string DatabasePath { get; }
        System.Collections.Generic.IEnumerable<object> DeferredQuery(Statera.Xamarin.Common.TableMapping map, string query, params object[] args);
        System.Collections.Generic.IEnumerable<T> DeferredQuery<T>(string query, params object[] args) where T : new();
        int Delete(object objectToDelete);
        int Delete<T>(object primaryKey);
        int DeleteAll<T>();
        void Dispose();
        int DropTable<T>();
        void EnableLoadExtension(int onoff);
        int Execute(string query, params object[] args);
        T ExecuteScalar<T>(string query, params object[] args);
        object Find(object pk, Statera.Xamarin.Common.TableMapping map);
        T Find<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : new();
        T Find<T>(object pk) where T : new();
        T Get<T>(System.Linq.Expressions.Expression<Func<T, bool>> predicate) where T : new();
        T Get<T>(object pk) where T : new();
        Statera.Xamarin.Common.TableMapping GetMapping(Type type, Statera.Xamarin.Common.CreateFlags createFlags = CreateFlags.None);
        Statera.Xamarin.Common.TableMapping GetMapping<T>();
        System.Collections.Generic.List<Statera.Xamarin.Common.SQLiteConnection.ColumnInfo> GetTableInfo(string tableName);
        IntPtr Handle { get; }
        int Insert(object obj);
        int Insert(object obj, string extra);
        int Insert(object obj, string extra, Type objType);
        int Insert(object obj, Type objType);
        int InsertAll(System.Collections.IEnumerable objects);
        int InsertAll(System.Collections.IEnumerable objects, string extra);
        int InsertAll(System.Collections.IEnumerable objects, Type objType);
        int InsertOrReplace(object obj);
        int InsertOrReplace(object obj, Type objType);
        bool IsInTransaction { get; }
        System.Collections.Generic.List<object> Query(Statera.Xamarin.Common.TableMapping map, string query, params object[] args);
        System.Collections.Generic.List<T> Query<T>(string query, params object[] args) where T : new();
        void Release(string savepoint);
        void Rollback();
        void RollbackTo(string savepoint);
        void RunInTransaction(Action action);
        string SaveTransactionPoint();
        bool StoreDateTimeAsTicks { get; }
        Statera.Xamarin.Common.TableQuery<T> Table<T>() where T : new();
        System.Collections.Generic.IEnumerable<Statera.Xamarin.Common.TableMapping> TableMappings { get; }
        bool TimeExecution { get; set; }
        bool Trace { get; set; }
        int Update(object obj);
        int Update(object obj, Type objType);
        int UpdateAll(System.Collections.IEnumerable objects);
    }
}
