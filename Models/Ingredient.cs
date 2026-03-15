using SQLite;

namespace UseItUp.Models
{
    // 这是一个食材的实体类，对应数据库里的一张表
    public class Ingredient
    {
        [PrimaryKey, AutoIncrement] // 主键，自动递增生成唯一ID
        public int Id { get; set; }

        // 食材名称
        public string Name { get; set; }

        // 食材数量
        public int Quantity { get; set; }

        // 过期时间
        public DateTime ExpiryDate { get; set; }

        // 照片路径（为下周的拍照功能预留的关键字段）
        public string PhotoPath { get; set; }
    }
}