namespace CustomAuthorizationTask.Permissions
{
    public static class Permissions
    {
        public static class Products
        {
            public const string View = "Permissions.Products.View";
            public const string Create = "Permissions.Products.Create";
            public const string Edit = "Permissions.Products.Edit";
            public const string Delete = "Permissions.Products.Delete";
        }

        public static class Catagory
        {
            public const string View = "Permissions.Catagory.View";
            public const string Create = "Permissions.Catagory.Create";
            public const string Edit = "Permissions.Catagory.Edit";
            public const string Delete = "Permissions.Catagory.Delete";
        }

        public static class SubCatagory
        {
            public const string View = "Permissions.SubCatagory.View";
            public const string Create = "Permissions.SubCatagory.Create";
            public const string Edit = "Permissions.SubCatagory.Edit";
            public const string Delete = "Permissions.SubCatagory.Delete";
        }
    }

    public static class Policies
    {   
        //product policies
        public const string RequireProductsView = "RequireProductsView";
        public const string RequireProductsCreate = "RequireProductsCreate";
        public const string RequireProductsUpdate = "RequireProductsUpdate";
        public const string RequireProductsDelete = "RequireProductsDelete";

        //catagory policies
        public const string RequireCatagoryView = "RequireCatagoryView";
        public const string RequireCatagoryCreate = "RequireCatagoryCreate";
        public const string RequireCatagoryUpdate = "RequireCatagoryUpdate";
        public const string RequireCatagoryDelete = "RequireCatagoryDelete";

        //sub catagory policies
        public const string RequireSubCatagoryView = "RequireSubCatagoryView";
        public const string RequireSubCatagoryCreate = "RequireSubCatagoryCreate";
        public const string RequireSubCatagoryUpdate = "RequireSubCatagoryUpdate";
        public const string RequireSubCatagoryDelete = "RequireSubCatagoryDelete";
    }
}
