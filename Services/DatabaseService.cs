using SQLite;
using UseItUp.Models;

namespace UseItUp.Services
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _database;

        // 懒加载初始化：只有在真正需要时才创建数据库文件
        private async Task Init()
        {
            if (_database != null)
                return;

            // 在设备的本地存储中创建一个名为 UseItUp.db3 的 SQLite 数据库
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "UseItUp.db3");
            _database = new SQLiteAsyncConnection(dbPath);

            // 根据 Ingredient 模型创建表
            await _database.CreateTableAsync<Ingredient>();
        }

        // 读取功能：获取数据库中的所有食材记录
        public async Task<List<Ingredient>> GetIngredientsAsync()
        {
            await Init();
            return await _database.Table<Ingredient>().ToListAsync();
        }

        // 保存功能：如果是新数据就插入，如果是已有数据就更新
        public async Task<int> SaveIngredientAsync(Ingredient item)
        {
            await Init();
            if (item.Id != 0)
                return await _database.UpdateAsync(item);
            else
                return await _database.InsertAsync(item);
        }

        // 删除功能：从数据库中永久移除一条记录
        public async Task<int> DeleteIngredientAsync(Ingredient item)
        {
            await Init();
            return await _database.DeleteAsync(item);
        }
    }
}