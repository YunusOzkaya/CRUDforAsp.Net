1. **Proje Başlangıcı:**
   - Asp.Net Core MVC ile bir proje oluşturduktan sonra `Microsoft.EntityFrameworkCore.SqlServer` ve `Microsoft.EntityFrameworkCore.Tools` dependency'lerini NuGet Package Manager üzerinden yükleyin.

2. **Veritabanı Bağlantısı Tanımlama:**
   - Proje klasörünün içine "Data" adında bir klasör oluşturun.
   - Bu klasör içinde, veritabanı bağlantısını tanımlayan ve DbSet'leri içeren bir sınıf oluşturun. İlk örnekte "Employee" tablosunu kullanacağımızı varsayalım. İşte bir örnek:

   using CRUD.Models.Domain;
   using Microsoft.EntityFrameworkCore;

   namespace CRUD.Data
   {
       public class DBContextClass : DbContext
       {
           public DBContextClass(DbContextOptions options) : base(options)
           {
           }

           public DbSet<Employee> Employees { get; set; }
       }
   }
Yukarıdaki kod, "CRUD" adlı projeyi ve "Models" klasörünün içindeki "Domain" klasöründeki "Employee" modelini kullanır.

"Employee" modeli şu şekildedir:

csharp
Copy code
namespace CRUD.Models.Domain
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Salary { get; set; }
        public string Department { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
Veritabanı Bağlantısı Ayarı:

"Program.cs" dosyasına gidin ve aşağıdaki değişiklikleri yapın:
csharp
Copy code
using CRUD.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Container'a hizmetleri ekleyin.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DBContextClass>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("CrudConnectionString")));
Eklediğiniz kod satırı, veritabanı bağlantısını "appsettings.json" dosyasına eklediğiniz bağlantı dizesini kullanarak yapılandırır. "CrudConnectionString" burada "appsettings.json" dosyasına eklediğimiz bağlantı dizesidir.

Ayrıca, bu adımları tamamladıktan sonra bir "Migration" oluşturmanız gerekecektir. Migration, bağlantı dizesinde belirlediğiniz veritabanına ve oluşturduğunuz modele göre tabloyu oluşturur.

Controller Oluşturma:

Controller'ları oluşturmak için "Controllers" klasörüne gidin ve sağ tıklayarak "Add Controller" seçeneğini seçin. Örneğin, Employee ekleme işlemi için "EmployeesController" adını verin.

Ardından, oluşturulan Controller sınıfının içine eklemek istediğiniz aksiyonları (örneğin, "Add" işlemi) ekleyebilirsiniz.

Örnek olarak:

public IActionResult Add()
{
    return View();
}
Yukarıdaki kod, "Add" işlemini gerçekleştiren bir aksiyonu temsil eder. Bu aksiyon, bir görünümü döndürür.
