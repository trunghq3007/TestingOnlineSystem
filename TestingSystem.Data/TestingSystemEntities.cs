namespace TestingSystem.Data
{
    using System.Data.Entity;
    using TestingSystem.Data.Infrastructure;
    using TestingSystem.Models;

    /// <summary>
    /// Defines the <see cref="TestingSystemEntities" />
    /// </summary>
    public class TestingSystemEntities : DbContext, IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestingSystemEntities"/> class.
        /// </summary>
        public TestingSystemEntities() : base("TestingSystem")
        {
        }

        /// <summary>
        /// Gets or sets the QuestionCategories
        /// </summary>
        public DbSet<QuestionCategory> QuestionCategories { get; set; }

        /// <summary>
        /// Gets or sets the Questions
        /// </summary>
        public DbSet<Question> Questions { get; set; }

        /// <summary>
        /// Gets or sets the Answers
        /// </summary>
        public DbSet<Answer> Answers { get; set; }

        /// <summary>
        /// Gets or sets the ExamPapers
        /// </summary>
        public DbSet<ExamPaper> ExamPapers { get; set; }

        /// <summary>
        /// Gets or sets the ExamPaperQuesions
        /// </summary>
        public DbSet<ExamPaperQuesion> ExamPaperQuesions { get; set; }

        /// <summary>
        /// Gets or sets the Users
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the UserGroups
        /// </summary>
        public DbSet<UserGroup> UserGroups { get; set; }

        /// <summary>
        /// Gets or sets the Groups
        /// </summary>
        public DbSet<Group> Groups { get; set; }

        /// <summary>
        /// Gets or sets the RoleActions
        /// </summary>
        public DbSet<RoleAction> RoleActions { get; set; }

        /// <summary>
        /// Gets or sets the Roles
        /// </summary>
        public DbSet<Roles> Roles { get; set; }

        /// <summary>
        /// Gets or sets the Actions
        /// </summary>
        public DbSet<Action> Actions { get; set; }

        /// <summary>
        /// Gets or sets the Exams
        /// </summary>
        public DbSet<Exam> Exams { get; set; }

        /// <summary>
        /// Gets or sets the Tests
        /// </summary>
        public DbSet<Test> Tests { get; set; }

        /// <summary>
        /// Gets or sets the Candidates
        /// </summary>
        public DbSet<Candidate> Candidates { get; set; }

        /// <summary>
        /// Gets or sets the TestResults
        /// </summary>
        public DbSet<TestResult> TestResults { get; set; }

        /// <summary>
        /// Gets or sets the ManagerTests
        /// </summary>
        public DbSet<ManagerTest> ManagerTests { get; set; }

        /// <summary>
        /// The Commit
        /// </summary>
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        /// <summary>
        /// The OnModelCreating
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder<see cref="DbModelBuilder"/></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configtion
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
            modelBuilder
                .Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();










            // Dual foreign key QuestionCategory
            modelBuilder.Entity<QuestionCategory>()
                .HasRequired(q => q.CreatedBys)
                .WithMany(t => t.QuestionCategoriesCreateUser)
                .HasForeignKey(q => q.CreatedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QuestionCategory>()
                .HasRequired(q => q.ModifiedBys)
                .WithMany(t => t.QuestionCategoriesModifiedUser)
                .HasForeignKey(q => q.ModifiedBy)
                .WillCascadeOnDelete(false);




            // Dual foreign key ExamPaper
            modelBuilder.Entity<ExamPaper>()
                .HasRequired(e => e.ModifiedBys)
                .WithMany(t => t.ExamPapersModifiedUser)
                .HasForeignKey(e => e.ModifiedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ExamPaper>()
                .HasRequired(e => e.CreatedBys)
                .WithMany(t => t.ExamPapersCreateUser)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);



            // Dual foreign key Question
            modelBuilder.Entity<Question>()
                .HasRequired(e => e.ModifiedUser)
                .WithMany(t => t.QuestionModifiedUser)
                .HasForeignKey(e => e.ModifiedBy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasRequired(e => e.CreaterUser)
                .WithMany(t => t.QuestionCreateUser)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);
        }
    }
}
