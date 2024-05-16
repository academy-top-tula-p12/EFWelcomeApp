using EFWelcomeApp;
using Microsoft.EntityFrameworkCore;

//using (HrContext context = new())
//{
//    context.Database.EnsureDeleted();
//    //context.Database.EnsureCreated();
//}

//using(HrContext context = new())
//{
//    //context.Database.Migrate();
//    await context.Database.MigrateAsync();
//}



// C - Create
using (HrContext context = new())
{
    Company yandex = new Company() { Title = "Yandex" };
    Company ozon = new Company() { Title = "Ozon" };

    Employee bob = new Employee() { Name = "Bobby", Age = 28, Company = ozon };
    Employee ann = new Employee() { Name = "Anna", Age = 32, Company = yandex };

    //context.Employees.AddRange(bob, ann);
    await context.Employees.AddRangeAsync(bob, ann);
    await context.SaveChangesAsync();
}

// U - Update
using (HrContext context = new())
{
    Employee? employee = context.Employees.FirstOrDefault();
    if (employee is not null)
    {
        employee.Name = "Bobby Fisher";
        employee.Age = 40;

        context.SaveChanges();
    }
}

// D - Delete
//using (HrContext context = new())
//{
//    Employee? employee = context.Employees.FirstOrDefault();
//    if (employee is not null)
//    {
//        context.Employees.Remove(employee);

//        context.SaveChanges();
//    }
//}


// R - Read
using (HrContext context = new())
{
    var employees = await context.Employees.ToListAsync();
    Console.WriteLine("Employees:");
    foreach (var e in employees)
        Console.WriteLine($"{e.Id}: {e.Name} ({e.Age})");
}
