
using Microsoft.EntityFrameworkCore;

/**

    Существуют 3 таблицы:
    1. Order (заказ)
    - Id (int)
    - Number (nvarchar(max)) *редактируется *используется для фильтрации
    - Date (datetime2(7)) *редактируется *используется для фильтрации
    - ProviderId (int) *редактируется *используется для фильтрации
    2. OrderItem (элемент заказа)
    - Id (int)
    - OrderId (int)
    - Name (nvarchar(max)) *редактируется *используется для фильтрации
    - Quantity decimal(18, 3) *редактируется
    - Unit (nvarchar(max)) *редактируется *используется для фильтрации
    3. Provider (поставщик, заполнена изначально, нигде не редактируется)
    - Id (int)
    - Name (nvarchar(max)) *используется для фильтрации
    
    create table Providers
    (
        ID int primary key identity(1,1),
        Name nvarchar(max)     
    )

    create table Orders
    (
        ID int primary key identity(1,1),
        OrderID int 
    )

    create table OrderItems
    (
        ID int primary key identity(1,1),
        OrderID int,
        Name nvarchar(max),
        Quantity decimal(18, 3),
        Unit nvarchar(max),
        foreign key(OrderID) references Orders(ID) on delete no action on update no action
    )


 */

public class Provider
{
    public int ID { get; set; }
    public string Name { get; set; }

}

public class OrderItem
{
    public int ID { get; set; }
    public int ProviderID { get; set; }
    public virtual Provider Provider  { get; set; }
    public int OrderID { get; set; }
    public virtual Order Order  { get; set; }
    public string Name { get; set; }
    public string Desc { get; set; }
    public string PropertiesJson { get; set; }
    public string ImageUri { get; set; }
    public float Quantity { get; set; }
    public string Unit { get; set; }

    public string GetPosterPath()
    {
        return ImageUri;
    }

}


public class Order
{
    public int ID { get; set; }

    
    public string Number { get; set; }
    public System.DateTime Date { get; set; }


    public int ProviderID { get; set; }
    public virtual Provider Provider { get; set; }

}


public class ProductionDbContext: DbContext
{

    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<Provider> Providers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }

    public ProductionDbContext(): base(){}
    public ProductionDbContext(DbContextOptions<ProductionDbContext> options): base( options ){        
    }

    //
    // Сводка:
    //     Override this method to configure the database (and other options) to be used
    //     for this context. This method is called for each instance of the context that
    //     is created. The base implementation does nothing.
    //     In situations where an instance of Microsoft.EntityFrameworkCore.DbContextOptions
    //     may or may not have been passed to the constructor, you can use Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured
    //     to determine if the options have already been set, and skip some or all of the
    //     logic in Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder).
    //
    // Параметры:
    //   optionsBuilder:
    //     A builder used to create or modify options for this context. Databases (and other
    //     extensions) typically define extension methods on this object that allow you
    //     to configure the context.
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        //optionsBuilder.UseSqlServer(@"Server=kest;Database=ProductionDbContext;Trusted_Connection=True;MultipleActiveResultSets=True");        
        optionsBuilder.UseInMemoryDatabase("ProductionDbContext");
    }
    //
    // Сводка:
    //     Override this method to further configure the model that was discovered by convention
    //     from the entity types exposed in Microsoft.EntityFrameworkCore.DbSet`1 properties
    //     on your derived context. The resulting model may be cached and re-used for subsequent
    //     instances of your derived context.
    //
    // Параметры:
    //   modelBuilder:
    //     The builder being used to construct the model for this context. Databases (and
    //     other extensions) typically define extension methods on this object that allow
    //     you to configure aspects of the model that are specific to a given database.
    //
    // Примечания:
    //     If a model is explicitly set on the options for this context (via Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel))
    //     then this method will not be run.
    protected override void OnModelCreating(ModelBuilder modelBuilder){

    }
      
}