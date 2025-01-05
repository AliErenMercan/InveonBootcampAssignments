using CourseInside.Data;
using CourseInside.Models;

namespace CourseInside.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                var admin = new User
                {
                    Email = "admin@example.com",
                    PasswordHash = "admin123",
                    Name = "Admin User",
                    Role = "Admin"
                };
                context.Users.Add(admin);

                var user = new User
                {
                    Email = "user@example.com",
                    PasswordHash = "user123",
                    Name = "Normal User",
                    Role = "User"
                };
                context.Users.Add(user);
                context.SaveChanges();
            }

            if (!context.Courses.Any())
            {
                var courses = new List<Course>
                {
                    new Course { Title = "React ile Web Geliştirme", Description = "React'i sıfırdan ileri seviyeye öğrenin", Price = 149.99m, Category = "Teknoloji" },
                    new Course { Title = "Profesyonel Fotoğrafçılık", Description = "Işık ve kompozisyon teknikleri", Price = 79.99m, Category = "Sanat" },
                    new Course { Title = "C# ile Backend Geliştirme", Description = "C# ve .NET Core ile backend mimarilerini öğrenin", Price = 129.99m, Category = "Teknoloji" },
                    new Course { Title = "Java ile Yazılım Geliştirme", Description = "Java temelleri ve Spring Boot", Price = 139.99m, Category = "Teknoloji" },
                    new Course { Title = "İleri Excel Teknikleri", Description = "Pivot tablolar, makrolar ve iş zekası", Price = 59.99m, Category = "Ofis Uygulamaları" },
                    new Course { Title = "Temel Muhasebe Eğitimi", Description = "Gelir, gider, bilanço ve vergi uygulamaları", Price = 89.99m, Category = "Finans" },
                    new Course { Title = "Mobil Uygulama Geliştirme (Flutter)", Description = "Flutter ile cross-platform uygulamalar yapın", Price = 149.99m, Category = "Teknoloji" },
                    new Course { Title = "Kişisel Gelişim ve Etkili İletişim", Description = "Diksiyon, beden dili, ikna kabiliyeti", Price = 49.99m, Category = "Kişisel Gelişim" },
                    new Course { Title = "Yaratıcı Yazarlık Atölyesi", Description = "Etkili metin yazarlığı ve hikaye kurgusu", Price = 69.99m, Category = "Edebiyat" },
                    new Course { Title = "Sosyal Medya Pazarlama", Description = "Sosyal medya stratejileri ve içerik yönetimi", Price = 79.99m, Category = "Pazarlama" },
                    new Course { Title = "Veri Analitiği ve Python", Description = "Numpy, Pandas, veri görselleştirme", Price = 159.99m, Category = "Teknoloji" },
                    new Course { Title = "Temel Web Tasarımı (HTML & CSS)", Description = "HTML5, CSS3, responsive tasarım", Price = 39.99m, Category = "Teknoloji" },
                    new Course { Title = "Dijital Pazarlama 101", Description = "SEO, SEM, e-mail marketing temelleri", Price = 99.99m, Category = "Pazarlama" },
                    new Course { Title = "3D Modelleme ve Blender", Description = "Blender arayüz, modelleme ve render teknikleri", Price = 119.99m, Category = "Tasarım" },
                    new Course { Title = "İleri Unity Oyun Geliştirme", Description = "Unity 2D/3D projeler ve optimizasyon", Price = 199.99m, Category = "Oyun Geliştirme" },
                    new Course { Title = "Mikroservis Mimarisi", Description = "Docker, Kubernetes ve mikroservis prensipleri", Price = 179.99m, Category = "Teknoloji" },
                    new Course { Title = "Blockchain Temelleri", Description = "Kriptografi, akıllı sözleşmeler, Ethereum", Price = 189.99m, Category = "Teknoloji" },
                    new Course { Title = "İleri JavaScript", Description = "ES6+, async/await, design patterns", Price = 129.99m, Category = "Teknoloji" },
                    new Course { Title = "Temel Resim Teknikleri", Description = "Karakalem, suluboya, yağlıboya başlangıç", Price = 59.99m, Category = "Sanat" },
                    new Course { Title = "Kısa Film Yapım Atölyesi", Description = "Senaryo, çekim, kurgulama ve prodüksiyon", Price = 99.99m, Category = "Sanat" },
                };
                context.Courses.AddRange(courses);
                context.SaveChanges();
            }
        }
    }
}
