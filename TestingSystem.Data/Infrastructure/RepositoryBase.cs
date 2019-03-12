namespace TestingSystem.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Defines the <see cref="RepositoryBase{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> where T : class
    {
        /// <summary>
        /// Defines the dataContext
        /// </summary>
        private TestingSystemEntities dataContext;

        /// <summary>
        /// Defines the dbSet
        /// </summary>
        private readonly IDbSet<T> dbSet;

        /// <summary>
        /// Gets the DbFactory
        /// </summary>
        protected IDbFactory DbFactory { get; private set; }

        /// <summary>
        /// Gets the DbContext
        /// </summary>
        protected TestingSystemEntities DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="dbFactory">The dbFactory<see cref="IDbFactory"/></param>
        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/></param>
        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/></param>
        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/></param>
        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="where">The where<see cref="Expression{Func{T, bool}}"/></param>
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="T"/></returns>
        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// The GetMany
        /// </summary>
        /// <param name="where">The where<see cref="Expression{Func{T, bool}}"/></param>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="where">The where<see cref="Expression{Func{T, bool}}"/></param>
        /// <returns>The <see cref="T"/></returns>
        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }
    }
}
