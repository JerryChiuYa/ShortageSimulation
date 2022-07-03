# Welcome to Shortage Simulation

## Overview
This is a simulate Shortage report.<br>
Here, you can input your Sales Orders, view the Shortage components.<br>

### Input the Sales Orders
<img src="/ImgForIntro/Input.png" width="80%" height="80%"/>
<p></p>

### Result
<img src="/ImgForIntro/Result.png"/>
<p></p>
<hr>

### Built with
<ul>
	<li><a href='https://visualstudio.microsoft.com/zh-hant/vs/'>Visual Studio 2022</a></li>
	<li><a href='https://docs.microsoft.com/zh-tw/dotnet/core/whats-new/dotnet-core-3-1'>.NET Core 3.1</a></li>
	<li><a href='https://getbootstrap.com/docs/5.1/getting-started/introduction/'>Bootstrap 5.1.3 & Javascript</a></li>

</ul>

### NuGet Package
<ul>
	<li><a href='https://docs.microsoft.com/zh-tw/ef/core/'>Microsoft.EntityFrameworkCore, .Relational, .SqlServer (3 packages)</a></li>

</ul>

### Database & Tools
<ul>
    <li><a href='https://docs.microsoft.com/zh-tw/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16'>SQL Server Management Studio (SSMS)</a></li>
	<li><a href='https://marketplace.visualstudio.com/items?itemName=ErikEJ.EFCorePowerTools'>EF Core Power Tools-To do Entity Framework</a></li>
</ul>


## Get Started
### 1.Prepare the data
<ul>
<li>Use SSMS</li>
<p>Please copy my variable name and data type to let C# easily to use.</p>
<img src="/ImgForIntro/ssms.png" width="70%" height="70%"/>
<img src="/ImgForIntro/data.png" width="50%" height="50%"/>

<li>Use EF Power tools</li>
<img src="/ImgForIntro/ef1.png" width="50%" height="50%"/>

<li>In appsettings.json</li>

```json
{
"ConnectionStrings": { "Shortage": "Data Source=localhost;Initial Catalog=Shortage;Persist Security Info=False;User ID=JC;Password=38405200;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;" }
}
```

<li>In startup.cs</li>

```Csharp
 public void ConfigureServices(IServiceCollection services)
{
		//Add context Service
	services.AddDbContext<ShortageContext>(s=>s.UseSqlServer("name=ConnectionStrings:Shortage"));
}

```

</ul>

<hr>

### 2.Dependence Injection

<ul>
<li>Create an Interface and a class for following use</li>
</ul>

```CSharp
//Interface ICustomerService
     public interface ICustomerServices
    {
        public Task<List<ShortageViewModel>> GetAllFgNames();
        public Task<List<FinishedGoods>> GetAllFgGoods();
        public Task<List<Materials>> GetAllMaterialStocks();
    }

// class CustomerServices
  public class CustomerServices : ICustomerServices
    {
	..................................................
```

<ul>
<li>In startup.cs</li>
</ul>

```Csharp
 public void ConfigureServices(IServiceCollection services)
{
		//Dependence Injection
         services.AddScoped<ICustomerServices, CustomerServices>();
}

```

### 3.Use JQuery to create Add and Remove button

<ul>
<li>In View of Home</li>

```html5
<form method="post" asp-controller="Home" asp-action="Calculate">
<div class="FG" id="C1">
    <label>成品料號</label>
<input type="text" list="data" name="FGName" required/>
<label>數量</label>
<input type="text" name="OrderQty" required/>
<button type="button" class="btn btn-warning" onclick="Remove(this)" hidden="hidden">Remove</button>
<p></p>
<datalist id="data">
    @{
        foreach (var  item in Model)
        {
                        <option>@item.FGName</option>
        }
    }

</datalist>
</div>

<button type="button" class="btn btn-success" onclick="Copy(this)">Add</button>
<input type="submit" class="btn btn-primary" value="Calculate"/>
</form>

<script>
    var i=2;
    var newId = '';
    function Copy(item){
        //設ID
        newId="C"+i;
        //複製
        $('#C1').clone().attr('id', newId).insertAfter('#C1');
        $(('#' + newId) +'>button').removeAttr('hidden');

        //清空料號及數量
        $(('#' + newId) + '>input[name=FGName]').val("");
        $(('#' + newId) + '>input[name=OrderQty]').val("");
        i=i+1;
    }
 
        function Remove(item){
            $(item).closest(".FG").remove();
    }
</script>
```



