using System;
using UseItUp.Models;
using UseItUp.Services;

namespace UseItUp.Views
{
    public partial class AddIngredientPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public AddIngredientPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;

            // 将日期选择器的默认值设置为今天
            ExpiryDatePicker.Date = DateTime.Today;
        }

        // 保存逻辑：点击底部 Save 按钮时触发
        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // 1. 表单验证：名字不能为空
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                await DisplayAlert("提示", "请输入食材名称。", "好的");
                return;
            }

            // 2. 数据清洗：安全地转换用户输入的数量（如果为空或乱码，默认设为1）
            int quantity = 1;
            if (!string.IsNullOrWhiteSpace(QuantityEntry.Text))
            {
                int.TryParse(QuantityEntry.Text, out quantity);
            }

            // 3. 封装数据：生成一个新的食材对象，强制转换日期格式以匹配 SQLite 要求
            var newIngredient = new Ingredient
            {
                Name = NameEntry.Text.Trim(),
                Quantity = quantity,
                ExpiryDate = Convert.ToDateTime(ExpiryDatePicker.Date),
                PhotoPath = string.Empty
            };

            // 4. 存入数据库
            await _databaseService.SaveIngredientAsync(newIngredient);

            // 5. 路由返回：保存成功后，自动返回上一页（即主页）
            await Shell.Current.GoToAsync("..");
        }
    }
}