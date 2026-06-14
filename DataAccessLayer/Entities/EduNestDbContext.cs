using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Entities
{
    public class EduNestDbContext : DbContext
    {
        public EduNestDbContext(DbContextOptions<EduNestDbContext> options) : base(options) { }

        // ── Identity ──────────────────────────────────────────────────────────
        public DbSet<User> Users { get; set; }
        public DbSet<Tutor> Tutors { get; set; }
        public DbSet<Student> Students { get; set; }

        // ── Subject & Availability ────────────────────────────────────────────
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<TutorSubject> TutorSubjects { get; set; }
        public DbSet<Availability> Availabilities { get; set; }

        // ── Booking & Payment ─────────────────────────────────────────────────
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }

        // ── Lesson & Tracking ─────────────────────────────────────────────────
        public DbSet<Lesson> Lessons { get; set; }

        // ── Learning Content ──────────────────────────────────────────────────
        public DbSet<MaterialSection> MaterialSections { get; set; }
        public DbSet<Material> Materials { get; set; }

        // ── Chat ──────────────────────────────────────────────────────────────
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<ConversationUser> ConversationUsers { get; set; }
        public DbSet<Message> Messages { get; set; }

        // ── Wallet & Payout ───────────────────────────────────────────────────
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<WalletTransaction> WalletTransactions { get; set; }
        public DbSet<Payout> Payouts { get; set; }
        public DbSet<TutorBankAccount> TutorBankAccounts { get; set; }

        //metric
        public DbSet<AppMetric> AppMetrics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ── Composite PKs ─────────────────────────────────────────────────
            modelBuilder.Entity<TutorSubject>()
                .HasKey(ts => new { ts.SubjectId, ts.TutorId });

            modelBuilder.Entity<ConversationUser>()
                .HasKey(cu => new { cu.ConversationId, cu.UserId });

            // ── User → Tutor / Student (1:1) ─────────────────────────────────
            modelBuilder.Entity<Tutor>()
                .HasOne(t => t.User)
                .WithOne(u => u.Tutor)
                .HasForeignKey<Tutor>(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Tutor → Wallet (1:1) ──────────────────────────────────────────
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.Tutor)
                .WithOne(t => t.Wallet)
                .HasForeignKey<Wallet>(w => w.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Tutor → BankAccount (1:1) ─────────────────────────────────────
            modelBuilder.Entity<TutorBankAccount>()
                .HasOne(b => b.Tutor)
                .WithOne(t => t.BankAccount)
                .HasForeignKey<TutorBankAccount>(b => b.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── TutorSubject ──────────────────────────────────────────────────
            modelBuilder.Entity<TutorSubject>()
                .HasOne(ts => ts.Tutor)
                .WithMany(t => t.TutorSubjects)
                .HasForeignKey(ts => ts.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TutorSubject>()
                .HasOne(ts => ts.Subject)
                .WithMany(s => s.TutorSubjects)
                .HasForeignKey(ts => ts.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Availability ──────────────────────────────────────────────────
            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Tutor)
                .WithMany(t => t.Availabilities)
                .HasForeignKey(a => a.TutorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Availability>()
                .HasOne(a => a.Subject)
                .WithMany(s => s.Availabilities)
                .HasForeignKey(a => a.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Availability>()
    .Property(a => a.StartTime)
    .HasColumnType("time without time zone");

            modelBuilder.Entity<Availability>()
                .Property(a => a.EndTime)
                .HasColumnType("time without time zone");

            // ── Booking ───────────────────────────────────────────────────────
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Availability)
                .WithMany(a => a.Bookings)
                .HasForeignKey(b => b.AvailabilityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Student)
                .WithMany(s => s.Bookings)
                .HasForeignKey(b => b.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Payment ───────────────────────────────────────────────────────
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithMany(b => b.Payments)
                .HasForeignKey(p => p.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Lesson ────────────────────────────────────────────────────────
            modelBuilder.Entity<Lesson>()
                .HasOne(l => l.Booking)
                .WithMany(b => b.Lessons)
                .HasForeignKey(l => l.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Material → Availability ───────────────────────────────────────
            modelBuilder.Entity<Material>()
                .HasOne(m => m.Availability)
                .WithMany(a => a.Materials)
                .HasForeignKey(m => m.AvailabilityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MaterialSection>()
                .HasOne(s => s.Availability)
                .WithMany(a => a.MaterialSections)
                .HasForeignKey(s => s.AvailabilityId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Material>()
                .HasOne(m => m.Section)
                .WithMany(s => s.Materials)
                .HasForeignKey(m => m.MaterialSectionId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Conversation ──────────────────────────────────────────────────
            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── ConversationUser ──────────────────────────────────────────────
            modelBuilder.Entity<ConversationUser>()
                .HasOne(cu => cu.Conversation)
                .WithMany(c => c.ConversationUsers)
                .HasForeignKey(cu => cu.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConversationUser>()
                .HasOne(cu => cu.User)
                .WithMany(u => u.ConversationUsers)
                .HasForeignKey(cu => cu.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Message ───────────────────────────────────────────────────────
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── WalletTransaction ─────────────────────────────────────────────
            modelBuilder.Entity<WalletTransaction>()
                .HasOne(wt => wt.Wallet)
                .WithMany(w => w.WalletTransactions)
                .HasForeignKey(wt => wt.WalletId)
                .OnDelete(DeleteBehavior.Cascade);

            // ── Payout ────────────────────────────────────────────────────────
            modelBuilder.Entity<Payout>()
                .HasOne(p => p.Tutor)
                .WithMany(t => t.Payouts)
                .HasForeignKey(p => p.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Payout>()
                .HasOne(p => p.WalletTransaction)
                .WithOne(wt => wt.Payout)
                .HasForeignKey<Payout>(p => p.WalletTransactionId)
                .OnDelete(DeleteBehavior.SetNull);

            // ── Indexes ───────────────────────────────────────────────────────
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<Booking>()
                .HasIndex(b => new { b.AvailabilityId, b.StudentId, b.Status });

            modelBuilder.Entity<Availability>()
                .HasIndex(a => new { a.TutorId, a.DayOfWeek });

            modelBuilder.Entity<Lesson>()
                .HasIndex(l => new { l.BookingId, l.ScheduleTime });

            modelBuilder.Entity<MaterialSection>()
                .HasIndex(s => new { s.AvailabilityId, s.DisplayOrder });

            modelBuilder.Entity<Material>()
                .HasIndex(m => new { m.AvailabilityId, m.MaterialSectionId, m.CreatedAt });

            modelBuilder.Entity<AppMetric>()
    .HasIndex(x => new { x.Type, x.DeviceId });

            // ── PostgreSQL lowercase naming ───────────────────────────────────  ← ADD HERE
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Lowercase table names
                entity.SetTableName(entity.GetTableName()?.ToLower());

                // Lowercase column names
                foreach (var property in entity.GetProperties())
                    property.SetColumnName(property.GetColumnName().ToLower());

                // Lowercase key names
                foreach (var key in entity.GetKeys())
                    key.SetName(key.GetName()?.ToLower());

                // Lowercase foreign key names
                foreach (var fk in entity.GetForeignKeys())
                    fk.SetConstraintName(fk.GetConstraintName()?.ToLower());

                // Lowercase index names
                foreach (var index in entity.GetIndexes())
                    index.SetDatabaseName(index.GetDatabaseName()?.ToLower());
            }
        }
    }
}
