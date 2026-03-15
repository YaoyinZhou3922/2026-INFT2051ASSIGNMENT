namespace INFT_2051__USEITUP_
{
    // AppShell 继承自 Shell，是整个应用导航结构的后台大脑
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            // 初始化组件用来加载并解析上面的 XAML 界面
            InitializeComponent();

            // ==========================================
            // 注册页面路由 (Routing)
            // ==========================================
            // “AddIngredientPage”不在应用首页的底部或侧边栏里，
            // 是通过点击按钮临时跳过去的，所以手动注册一个“路牌”。
            // 代码里呼叫 `GoToAsync("AddIngredientPage")` 时，系统可找。
            Routing.RegisterRoute("AddIngredientPage", typeof(UseItUp.Views.AddIngredientPage));
        }
    }
}