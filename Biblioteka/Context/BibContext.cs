using Biblioteka.Areas.Identity.Data;
using Biblioteka.Extensions;
using Biblioteka.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Hosting;

namespace Biblioteka.Context
{
    public class BibContext : IdentityDbContext<BibUser>
    {
        public BibContext(DbContextOptions<BibContext> options) : base(options)
        {

        }
        public DbSet<Event> Event { get; set; }
        public DbSet<Author> Author {get; set;}      
        public DbSet<Book_Author> Book_Author { get; set; }

        public DbSet<Book> Book { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<BookType> BookType { get; set; }
        public DbSet<Book_Tag> Book_Tag { get; set; }
        public DbSet<Book_Opinions> Book_Opinions { get; set; }
        public DbSet<Tag> Tag { get; set; }
        
        public DbSet<Borrowing> Borrowing{ get; set; }
        public DbSet<Reader> Reader { get; set; }
        public DbSet<Reader_Borrowings> Reader_Borrowings { get; set; }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<EmployeeData> EmployeeData { get; set; }      
        public DbSet<Position> Position { get; set; }
        
        public DbSet<Room> Room { get; set; }
        public DbSet<RoomReservation> RoomReservation { get; set; }

        public DbSet<EmployeeConfirmingReturn> EmployeeConfirmingReturnsBook { get; set; }
        public DbSet<EmployeeConfirmingPayment> EmployeeConfirmingPaymentsBook { get; set; }    

        public DbSet<FAQ> FAQ { get; set; }

        public DbSet<Suggestion> Suggestion { get; set; }
        public DbSet<Reader_Events> Reader_Events { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Model.SetMaxIdentifierLength(30);

            // Klucze główne tabel
            modelBuilder.Entity<BookType>().HasKey(p => p.typeId);
            modelBuilder.Entity<Borrowing>().HasKey(p => p.borrowId);
            modelBuilder.Entity<Employee>().HasKey(p => p.employeeId);
            modelBuilder.Entity<EmployeeData>().HasKey(p => p.employeeId);
            modelBuilder.Entity<Room>().HasKey(p => p.roomNumber);
            modelBuilder.Entity<RoomReservation>().HasKey(p => p.reservationId);
            modelBuilder.Entity<Event>().HasKey(p => p.eventId);
            modelBuilder.Entity<FAQ>().HasKey(p => p.FAQId);
            modelBuilder.Entity<Suggestion>().HasKey(p => p.suggestionId);

            //modelBuilder.Entity<Author>().Property(p => p.authorId).UseIdentityColumn(seed:1, increment:1);

            // Relacja 1:1
            modelBuilder.Entity<Employee>()
                .HasOne(x => x.employeeData)
                .WithOne(x => x.employee);

            // Relacje N:N
            modelBuilder.Entity<Book_Opinions>().ToTable("Book_Opinions");
            modelBuilder.Entity<Book_Opinions>().HasKey(p => new { p.bookId, p.readerId });
            modelBuilder.Entity<Book_Opinions>()
                .HasOne(x => x.book)
                .WithMany(x => x.opinions)
                .HasForeignKey(x => x.bookId);
            modelBuilder.Entity<Book_Opinions>()
                .HasOne(x => x.reader)
                .WithMany(x => x.bookOpinions)
                .HasForeignKey(x => x.readerId);


            modelBuilder.Entity<Book_Author>().ToTable("Book_Author");
            modelBuilder.Entity<Book_Author>().HasKey(p => new { p.bookId, p.authorId });
            modelBuilder.Entity<Book_Author>()
                .HasOne(x => x.book)
                .WithMany(x => x.authors)
                .HasForeignKey(x => x.bookId);
            modelBuilder.Entity<Book_Author>()
                .HasOne(x => x.author)
                .WithMany(x => x.books)
                .HasForeignKey(x => x.authorId);
            /*modelBuilder.Entity<Book>()
            .HasMany(e => e.authors)
            .WithMany(e => e.books)
            .UsingEntity<Book_Author>();*/

            modelBuilder.Entity<Book_Tag>().ToTable("Book_Tag");
            modelBuilder.Entity<Book_Tag>().HasKey(p => new { p.bookId, p.tagId });
            modelBuilder.Entity<Book_Tag>()
                .HasOne(x => x.book)
                .WithMany(x => x.tags)
                .HasForeignKey(x => x.bookId);
            modelBuilder.Entity<Book_Tag>()
                .HasOne(x => x.tag)
                .WithMany(x => x.books)
                .HasForeignKey(x => x.tagId);

            modelBuilder.Entity<Reader_Borrowings>().ToTable("Reader_Borrowings");
            modelBuilder.Entity<Reader_Borrowings>().HasKey(p => new { p.readerId, p.borrowId });
            modelBuilder.Entity<Reader_Borrowings>()
                .HasOne(x => x.reader)
                .WithMany(x => x.borrowings)
                .HasForeignKey(x => x.readerId);
            modelBuilder.Entity<Reader_Borrowings>()
                .HasOne(x => x.borrow)
                .WithMany(x => x.readers)
                .HasForeignKey(x => x.borrowId);


            modelBuilder.Entity<Reader_Events>().ToTable("Reader_Events");
            modelBuilder.Entity<Reader_Events>().HasKey(p => new { p.readerId, p.eventId });
            modelBuilder.Entity<Reader_Events>()
                .HasOne(x => x.reader)
                .WithMany(x => x.events)
                .HasForeignKey(x => x.readerId);
            modelBuilder.Entity<Reader_Events>()
                .HasOne(x => x.eventt)
                .WithMany(x => x.readers)
                .HasForeignKey(x => x.eventId);


            modelBuilder.Entity<Borrowing>()
               .Property(b => b.LateFee)
               .HasColumnType("decimal(18, 2)");

            //Konfiguracja danych nowych użytkowników
            modelBuilder.ApplyConfiguration(new BibUserEntityConfiguration());


            // Dodawanie przykładowych danych
            modelBuilder.Seed();
        }

    }
}

internal class BibUserEntityConfiguration : IEntityTypeConfiguration<BibUser>
{
    public void Configure(EntityTypeBuilder<BibUser> builder)
    {
        builder.Property(u => u.name).IsRequired().HasMaxLength(20);
        builder.Property(u => u.surname).IsRequired().HasMaxLength(40);
        builder.Property(u => u.Email).HasMaxLength(40);
    }
}