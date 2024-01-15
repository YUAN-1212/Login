using Microsoft.EntityFrameworkCore;


namespace Domain.Model
{
    /*
     需安裝以下 SQLite NuGet:
     1.SQLite
     2.Microsoft.EntityFrameworkCore
     3.Microsoft.EntityFrameworkCore.Sqlite
     */

    public class WebDbContext : DbContext
    {
        static string path = @"D:\\97.測試\\SampleLogin\\Login\\Domain\\App_Data\\";
        public string DbPath { get; }
        public WebDbContext()
        {
            //var path = @"D:\\97.測試\\SampleLogin\\Domain\\App_Data\\";
            DbPath = System.IO.Path.Join(path, "Member.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");


        #region TABLES
        /// <summary>
        /// 共用代碼
        /// </summary>
        public DbSet<CodeTable> CodeTables { get; set; }

        /// <summary>
        /// 會員資料
        /// </summary>
        public DbSet<MemberData> MemberDatas { get; set; }
        #endregion
    }
}
