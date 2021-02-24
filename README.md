# Task-Script
ASP project for students at VTU.

## 24/02/2021
---
Стъпки за създаване на ASP проект.

1. Създава се празен solution (Blank solution).
2. Първо започваме с базата. И в него се създава solution папка на име Database. Solution папката може да си създадете от: 
    1. Десен бутон на Solution-а
    2. Add
    3. New solution folder
3. След това отваряте проекта във файловата система на компютъра и си създавате папка със същото име.
4. В новосъздадената папка се създава проект **(Class Library)**, който да унаследява TaskScript.Database.Models 
5. Създавате класове, които да съдържат properties за всички колонки на модела.
    - Когато се прави one-to-many. 
        - Там където е Id-то и е foreign key трябва да има object propety, който съвпада с типът на таблицата, която ще бъде "One" в релацията. Например, ако имаме много задачи (Task) и един проект (Project). В обекта Task трябва да имаме property ProjectId, което от типа на primary key-a на таблицата Projects, и property Project от тип Project, което да бъде virtual.
        - А при многото, тоест в Project обекта, трябва да има колекция от тип ICollection< Task > Tasks. Като колекцията трябва да се инстанцира в конструктора на обекта. 
        this.Tasks = new HashSet< Task >();
6. След моделите се създава, нов проект **(Class Library)** в папката Database, който да е с име TaskScript.Database.
7. В този проект трябва да се изтеглят NuGet packages.
    1. Десен бутон на проекта (TaskScript.Database)
    2. Manage NuGet Packages
    3. Таба Browse и търсим в търсачката и теглим
        1. Microsoft.EntityFrameworkCore - от него получаваме два важни класа: DbContext (за базата), DbSet (за таблиците)
        2. Microsoft.EntityFrameworkCore.SqlServer - нужно е, за да се направи връзката с SqlServer-a
        3. Microsoft.EntityFrameworkCore.Tools - чрез него ще можете да си правите миграции
        4. Microsoft.AspNetCore.Identity.EntityFrameworkCore - трябва за по-късно, когато ще се добавя Identity
8. Създава се клас, който трябва да се казва като името на проекта и след това DatabaseContext или DbContext. В случая TaskScriptDatabaseContext
9. В него се наследява IdentityDbContext, за да имаме и asp user and roles в базата ни
10. Правим си конструктор, който да приема DbContextOptions и да ги запазва в field.
```
private DbContextOptions options;

public TaskScriptDatabaseContext(DbContextOptions options)
    : base(options)
{
    this.options = options;
}
```
11. Създават се пропъртита за таблиците от тип DbSet< >, като в стрелките се слага името на модела, за който ще е таблицата и пропъртито се пише в множествено число.
12. Трябва да се override-нат два метода
```
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    base.OnConfiguring(optionsBuilder);

    if (!optionsBuilder.IsConfigured)
    {
        optionsBuilder.UseSqlServer("Data Source=.;Database=TaskScript;Integrated Security=True;"); // Примерен connection string
    }
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);
}
```
13. Трябва да се направи миграция. Тя може да се направи с Package Manager Console. Може да намерите прозореца в:
    1. View (В главното меню)
    2. Other windows
    3. Package Manager Console
14. След отварянето, вътре написвате Add-Migration Initial
15. Може да си промените базата с команда в конзолата. Update-Database
16. Правите нова solution folder и във файловата система също, която да се казва Application.
17. Вътре добавяте ASP проект, като трябва да изберете ASP.NET Core Web App (Model-View-Controller). И на Authentication да смените от No Authentication на Individual User Authentication.
18. След като се създаде изтривате папката Data.
19. И в startUp трябва да смените ApplicationDbContext на TaskScriptDatabaseContext.
20. Да се добави код в Configure методът. В if-а, който проверява env.IsDevelopment(). Най-отдолу на if-a.
```
using (var serviceScope = app.ApplicationServices.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<TaskScriptDatabaseContext>();

    bool isDatabaseAlreadyCreated = dbContext.Database.EnsureCreated() == false;

    if (isDatabaseAlreadyCreated)
    {
        dbContext.Database.Migrate();
    }
}
```

Това е достатъчно, за да работите с актуалната база.

За да си scaffold-нете контролер за някоя таблица.
1. Десен бутон на Controllers
2. Add
3. Controller
4. MVC Controller with views, using Entity Framework

Накрая попълвате полетата спрямо нещата, които искате.