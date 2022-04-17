using Android.Content;
using Android.Database.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BDSuggestion.ControlPersonalizado
{
    public class DBHelper : SQLiteOpenHelper
    {

        private static readonly string DB_NAME = "dbsugest.db";
        private static readonly int VERSION = 1;
        private Context Context { get; set; }

        public DBHelper(Context context) : base(context, DB_NAME, null, VERSION)
        {
            this.Context = context;
        }


        /*
         * Cria uma copia do banco de dados na pasta da aplicação específica, um auxílio a configuração:  android:allowBackup="false" 
         * para impedir que o banco de dados seja mantido ao excluír o aplicativo do celular.
         */

        public async void CriaSQLiteDB(string path, int ID)
        {
       
            Stream streamSQLite;
            FileStream streamWriter;

            try
            {
                
                if (!File.Exists(path))
                {
                    streamSQLite = Context.Resources.OpenRawResource(ID);
                    streamWriter = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);

                    if (streamSQLite != null && streamWriter != null)
                    {
                        CopiaSQLiteDB(streamSQLite, streamWriter);
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", string.Format("{0}\n{1}", ex.Message, ex.StackTrace), "OK");
            }

        }
        private bool CopiaSQLiteDB(Stream streamSQLite, FileStream streamWriter)
        {
            bool isSuccess = false;
            int lenght = 256;
            Byte[] buffer = new Byte[lenght];
            try
            {
                int bytesRead = streamSQLite.Read(buffer, 0, lenght);
                while (bytesRead > 0)
                {
                    streamWriter.Write(buffer, 0, bytesRead);
                    bytesRead = streamSQLite.Read(buffer, 0, lenght);
                }
                isSuccess = true;
            }
            catch { }
            finally
            {
                streamSQLite.Close();
                streamWriter.Close();
            }
            return isSuccess;
        }
        public override void OnCreate(SQLiteDatabase db)
        { }
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        { }
    }
}
