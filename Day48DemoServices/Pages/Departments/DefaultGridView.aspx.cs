﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UserServices.Services;

namespace Day48DemoServices.Pages.Departments
{
    public partial class DefaultGridView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) return;

            var departmentService = new DepartmentService();

            GridView1.DataSource = departmentService.GetAll();
            GridView1.DataBind();
        }
    }
}