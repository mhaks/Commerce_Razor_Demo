using CommerceRazorDemo.Models;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Net.Mail;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace CommerceRazorDemo.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CommerceRazorDemoContext(serviceProvider.GetRequiredService<DbContextOptions<CommerceRazorDemoContext>>()))
            {
                if (context == null)
                {
                    throw new ArgumentNullException(nameof(context));
                }

                #region states
                // US States
                if (!context.StateLocation.Any())
                {                    
                    var states = new StateLocation[]
                    {
                        new StateLocation{Name = "Alabama", Abbreviation = "AL", TaxRate = 0.04M },
                        new StateLocation{Name = "Alaska", Abbreviation = "AK", TaxRate = 0.05M },
                        new StateLocation{Name = "Arizona", Abbreviation = "AZ", TaxRate = 0.06M },
                        new StateLocation{Name = "Arkansas", Abbreviation = "AR", TaxRate = 0.07M },
                        new StateLocation{Name = "California", Abbreviation = "CA", TaxRate = 0.08M },
                        new StateLocation{Name = "Colorado", Abbreviation = "CO", TaxRate = 0.07M },
                        new StateLocation{Name = "Connecticut", Abbreviation = "CT", TaxRate = 0.06M },
                        new StateLocation{Name = "Delaware", Abbreviation = "DE", TaxRate = 0.05M },
                        new StateLocation{Name = "Florida", Abbreviation = "FL", TaxRate = 0.04M },
                        new StateLocation{Name = "Georgia", Abbreviation = "GA", TaxRate = 0.05M },
                        new StateLocation{Name = "Hawaii", Abbreviation = "HI", TaxRate = 0.06M },
                        new StateLocation{Name = "Idaho", Abbreviation = "ID", TaxRate = 0.07M },
                        new StateLocation{Name = "Illinois", Abbreviation = "IL", TaxRate = 0.08M },
                        new StateLocation{Name = "Indiana", Abbreviation = "IN", TaxRate = 0.07M },
                        new StateLocation{Name = "Iowa", Abbreviation = "IA", TaxRate = 0.06M },
                        new StateLocation{Name = "Kansas", Abbreviation = "KS", TaxRate = 0.05M },
                        new StateLocation{Name = "Kentucky", Abbreviation = "KY", TaxRate = 0.04M },
                        new StateLocation{Name = "Louisiana", Abbreviation = "LA", TaxRate = 0.05M },
                        new StateLocation{Name = "Maine", Abbreviation = "ME", TaxRate = 0.06M },
                        new StateLocation{Name = "Maryland", Abbreviation = "MD", TaxRate = 0.07M },
                        new StateLocation{Name = "Massachusetts", Abbreviation = "MA", TaxRate = 0.08M },
                        new StateLocation{Name = "Michigan", Abbreviation = "MI", TaxRate = 0.07M },
                        new StateLocation{Name = "Minnesota", Abbreviation = "MN", TaxRate = 0.06M },
                        new StateLocation{Name = "Mississippi", Abbreviation = "MS", TaxRate = 0.05M },
                        new StateLocation{Name = "Missouri", Abbreviation = "MO", TaxRate = 0.04M },
                        new StateLocation{Name = "Montana", Abbreviation = "MT", TaxRate = 0.05M },
                        new StateLocation{Name = "Nebraska", Abbreviation = "NE", TaxRate = 0.06M },
                        new StateLocation{Name = "Nevada", Abbreviation = "NV", TaxRate = 0.07M },
                        new StateLocation{Name = "New Hampshire", Abbreviation = "NH", TaxRate = 0.08M },
                        new StateLocation{Name = "New Jersey", Abbreviation = "NJ", TaxRate = 0.08M },
                        new StateLocation{Name = "New Mexico", Abbreviation = "NM", TaxRate = 0.07M },
                        new StateLocation{Name = "New York", Abbreviation = "NY", TaxRate = 0.06M },
                        new StateLocation{Name = "North Carolina", Abbreviation = "NC", TaxRate = 0.05M },
                        new StateLocation{Name = "North Dakota", Abbreviation = "ND", TaxRate = 0.04M },
                        new StateLocation{Name = "Ohio", Abbreviation = "OH", TaxRate = 0.08M },
                        new StateLocation{Name = "Oklahoma", Abbreviation = "OK", TaxRate = 0.04M },
                        new StateLocation{Name = "Oregon", Abbreviation = "OR", TaxRate = 0.05M },
                        new StateLocation{Name = "Pennsylvania", Abbreviation = "PA", TaxRate = 0.08M },
                        new StateLocation{Name = "Rhode Island", Abbreviation = "RI", TaxRate = 0.07M },
                        new StateLocation{Name = "South Carolina", Abbreviation = "SC", TaxRate = 0.04M },
                        new StateLocation{Name = "South Dakota", Abbreviation = "SD", TaxRate = 0.04M },
                        new StateLocation{Name = "Tennessee", Abbreviation = "TN", TaxRate = 0.04M },
                        new StateLocation{Name = "Texas", Abbreviation = "TX", TaxRate = 0.0M },
                        new StateLocation{Name = "Utah", Abbreviation = "UT", TaxRate = 0.05M },
                        new StateLocation{Name = "Vermont", Abbreviation = "VT", TaxRate = 0.05M },
                        new StateLocation{Name = "Virginia", Abbreviation = "VA", TaxRate = 0.06M },
                        new StateLocation{Name = "Washington", Abbreviation = "WA", TaxRate = 0.04M },
                        new StateLocation{Name = "West Virginia", Abbreviation = "WV", TaxRate = 0.03M },
                        new StateLocation{Name = "Wisconsin", Abbreviation = "WI", TaxRate = 0.06M },
                        new StateLocation{Name = "Wyoming", Abbreviation = "WY", TaxRate = 0.00M },
                        new StateLocation{Name = "District of Columbia", Abbreviation = "DC", TaxRate = 0.06M },
                        new StateLocation{Name = "Guam", Abbreviation = "GU", TaxRate = 0.06M },
                        new StateLocation{Name = "Marshall Islands", Abbreviation = "MH", TaxRate = 0.06M },
                        new StateLocation{Name = "Northern Mariana Island", Abbreviation = "MP", TaxRate = 0.06M },
                        new StateLocation{Name = "Puerto Rico", Abbreviation = "PR", TaxRate = 0.06M },
                        new StateLocation{Name = "Virgin Islands", Abbreviation = "VI", TaxRate = 0.06M }
                    };

                    foreach(var item in states)
                        context.StateLocation.Add(item);

                    context.SaveChanges();                                                         
                }
                #endregion

                #region product category
                if (!context.ProductCategory.Any())
                {
                    var categories = new ProductCategory[]
                    {
                        new ProductCategory { Title = "Sports"},
                        new ProductCategory { Title = "Household"},
                        new ProductCategory { Title = "Technology"},
                        new ProductCategory { Title = "Fashion" }
                    };

                    foreach(var item in categories)
                    {
                        context.ProductCategory.Add(item);
                    }
                    context.SaveChanges();
                }
                #endregion

                #region product
                if(!context.Product.Any())
                {
                    var products = new Product[]
                    {
                        new Product{ Title="Air Jordan Sneaker", Brand="Nike", Description="Original Air Jordan", Price=119.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Sports").Id, ModelNumber="S001"},
                        new Product{ Title="Predator", Brand="Adidas", Description="Soccer shoe", Price=79.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Sports").Id, ModelNumber="S002" },
                        new Product{ Title="Tiger Woods Shirt", Brand="Nike", Description="Tiger Woods final round shirt", Price=49.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Sports").Id, ModelNumber="S003" },
                        new Product{ Title="Workout Top", Brand="Under Armour", Description="Basic workout shirt", Price=39.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Sports").Id, ModelNumber="S004" },
                        new Product{ Title="Workout Short", Brand="Under Armour", Description="Basic workout short", Price=39.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Sports").Id, ModelNumber="S005" },
                        new Product{ Title="Attack Helmet", Brand="Giro", Description="Giro Attack aerodymic cycling helmet", Price=99.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Sports").Id, ModelNumber="S006" },
                        

                        new Product{ Title="Rivet Aiden Mid-Century Modern Tufted Velvet Loveseat Sofa", Brand="Rivet", Description="This sleek, mid-Century inspired loveseat is designed to impress. ", Price=739.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Household").Id, ModelNumber="H001" },
                        new Product{ Title="HERCULES Lesley Series Contemporary Black LeatherSoft Sofa with Encasing Frame", Brand="Flash Furniture", Description="You're great at what you do and your company has grown so make sure you convey that to clients when you choose your reception furniture.", Price=1257.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Household").Id, ModelNumber="H002"  },
                        new Product{ Title="Premium Microfiber Cleaning Cloth", Brand="OVWO", Description="80% Polyester/ 20% Polyamide", Price=9.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Household").Id, ModelNumber="H003"  },
                        new Product{ Title="Bathroom Cleaner Spray", Brand="Lysol", Description="KILLS 99.9% OF BATHROOM VIRUSES AND BACTERIA", Price=3.97M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Household").Id, ModelNumber="H004"  },
                        new Product{ Title="Linen White Bath Towels 4-Pack", Brand="Hammam", Description=" 4-Piece super soft and absorbent Turkish cotton bath towels.", Price=33.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Household").Id, ModelNumber="H005"  },
                        new Product{ Title="Sure-Crisp Air Fryer Countertop Toaster Oven", Brand="Hamilton Beach", Description="The Sure-Crisp air fry convection function circulates air around food as it cooks", Price=69.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Household").Id, ModelNumber="H005"  },


                        new Product{ Title="Inspiron 3511 Premium Laptop", Brand="Dell", Description="Intel Core i5-1035G1 Quad-Core Processor (4Cores, 6MB Intel Smart Cache, 8 Threads", Price=679.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Technology").Id, ModelNumber="T001"  },
                        new Product{ Title="XPS 17 9720 Laptop", Brand="Dell", Description="12th Generation Intel Core i7-12700H (24MB Cache, up to 4.7 GHz, 14 cores)", Price=2499.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Technology").Id, ModelNumber="T002" },
                        new Product{ Title="iPhone 13 Pro", Brand="Apple", Description="6.1-inch Super Retina XDR display with ProMotion for a faster, more responsive feel", Price=999.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Technology").Id, ModelNumber="T003" },
                        new Product{ Title="iPhone 13 Mini", Brand="Apple", Description="5.4-inch Super Retina XDR display", Price=729.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Technology").Id, ModelNumber="T004" },
                        new Product{ Title="65-Inch Class UQ9000", Brand="LG", Description="Everything you need to bring your favorite content to life with the power of 4K and the extras you crave.", Price=576.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Technology").Id, ModelNumber="T005" },
                        new Product{ Title="iPad Air", Brand="Apple", Description="10.9-inch, Wi-Fi + Cellular, 64GB", Price=749.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Technology").Id, ModelNumber="T006" }, 


                        new Product{ Title="Mens Tilden Cap Oxford Shoe", Brand="Clarks ", Description="A classic captoe derby crafted from rich, full grain leather.", Price=59.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Fashion").Id, ModelNumber="F001" },
                        new Product{ Title="Mens Cotrell Step Slip-On Loafer", Brand="Clarks", Description="Slip-on casual", Price=54.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Fashion").Id, ModelNumber="F002" },
                        new Product{ Title="Mens Dress Shirt Regular Fit Poplin Solid", Brand="Van Heusen", Description="Fabric features enhanced wrinkle resistance for easy care at home.", Price=24.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Fashion").Id, ModelNumber="F003" },
                        new Product{ Title="Mens Long Sleeve Dress Shirt", Brand="Alberto Danelli", Description="Our cotton blend dress shirt is the perfect fitting day to night closet must have. ", Price=29.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Fashion").Id, ModelNumber="F004" },
                        new Product{ Title="Mens Classic Fit Easy Khaki", Brand="Dockers", Description="64% Cotton, 34% Polyester, 2% Elastane", Price=39.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Fashion").Id, ModelNumber="F005"},
                        new Product{ Title="Mens American Chino Flat Front Straight Fit Pant", Brand="Izod", Description="100% Cotton", Price=31.99M, AvailableQty=10, ProductCategoryId=context.ProductCategory.Single(x => x.Title == "Fashion").Id, ModelNumber="F006" }
                    };

                    foreach(var item in products)
                        context.Product.Add(item);

                    context.SaveChanges();
                }
                #endregion

                #region customer
                if (!context.Customer.Any())
                {
                    var customers = new Customer[]
                    {
                        new Customer{UserName = "jerry", FirstName = "Jerry", LastName="Seinfeld", Address1="151 5th Avenue", Address2="#201", City="New York", StateLocationId=context.StateLocation.Single(x=>x.Abbreviation=="NY").Id, PostalCode="10001", PhoneNumber="212-555-1234", EmailAddress="jerry@seinfeld.com"},
                        new Customer{UserName = "elaine", FirstName = "Elaine", LastName="Benes", Address1="325 Columbus Avenue", Address2="#12", City="New York", StateLocationId=context.StateLocation.Single(x=>x.Abbreviation=="NY").Id, PostalCode="10001", PhoneNumber="212-555-2345", EmailAddress="elaine@seinfeld.com"},
                        new Customer{UserName = "kramer", FirstName = "Cosmo", LastName="Kramer", Address1="151 5th Avenue", Address2="#202", City="New York", StateLocationId=context.StateLocation.Single(x=>x.Abbreviation=="NY").Id, PostalCode="10001", PhoneNumber="212-555-3456", EmailAddress="kramer@seinfeld.com"},
                        new Customer{UserName = "george", FirstName = "George", LastName="Costanza", Address1="525 42nd St", Address2="#604", City="New York", StateLocationId=context.StateLocation.Single(x=>x.Abbreviation=="NY").Id, PostalCode="10001", PhoneNumber="212-555-4567", EmailAddress="george@seinfeld.com"},
                        new Customer{UserName = "newman", FirstName = "N", LastName="Newman", Address1="151 5th Avenue", Address2="#5", City="New York", StateLocationId=context.StateLocation.Single(x=>x.Abbreviation=="NY").Id, PostalCode="10001", PhoneNumber="212-555-5678", EmailAddress="newman@seinfeld.com"}
                    };

                    foreach(var item in customers)
                        context.Customer.Add(item);

                    context.SaveChanges();
                }

                #endregion

                #region order status
                if(!context.OrderStatus.Any())
                {
                    var orderStatus = new OrderStatus[]
                    {
                        new OrderStatus { Name = "Cart"},
                        new OrderStatus { Name = "Processing"},
                        new OrderStatus { Name = "Cancelled"},
                        new OrderStatus { Name = "Shipped"},
                        new OrderStatus { Name = "Delivered"},
                        new OrderStatus { Name = "Returned"},
                    };

                    foreach (var item in orderStatus)   
                        context.OrderStatus.Add(item);

                    context.SaveChanges();
                }
                #endregion

            }
        }
       

    }
}
