namespace JjOnlineStore.Web
{
    public static class ViewPaths
    {
        // Areas/Admin
        public const string CreateCategoryView = "~/Areas/Admin/Views/Categories/Create.cshtml";

        //Account Controller
        public const string LoginView = "~/Views/Account/Login.cshtml";
        public const string RegisterView = "~/Views/Account/Register.cshtml";

        //Products Controller
        public const string ProductsIndex = "~/Views/Products/Index.cshtml";
        public const string ProductsDetails = "~/Views/Products/Details.cshtml";

        //Manage Controller
        public const string ChangePasswordView = "~/Views/Manage/ChangePassword.cshtml";
    }
}