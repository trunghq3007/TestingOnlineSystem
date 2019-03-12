namespace TestingSystem.Data.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Defines the <see cref="IRepository{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class
    {
        // Marks an entity as new
        /// <summary>
        /// The Add
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/></param>
        void Add(T entity);

        // Marks an entity as modified
        /// <summary>
        /// The Update
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/></param>
        void Update(T entity);

        // Marks an entity to be removed
        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="entity">The entity<see cref="T"/></param>
        void Delete(T entity);

        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="where">The where<see cref="Expression{Func{T, bool}}"/></param>
        void Delete(Expression<Func<T, bool>> where);

        // Get an entity by int id
        /// <summary>
        /// The GetById
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="T"/></returns>
        T GetById(int id);

        // Get an entity using delegate
        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="where">The where<see cref="Expression{Func{T, bool}}"/></param>
        /// <returns>The <see cref="T"/></returns>
        T Get(Expression<Func<T, bool>> where);

        // Gets all entities of type T
        /// <summary>
        /// The GetAll
        /// </summary>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> GetAll();

        // Gets entities using delegate
        /// <summary>
        /// The GetMany
        /// </summary>
        /// <param name="where">The where<see cref="Expression{Func{T, bool}}"/></param>
        /// <returns>The <see cref="IEnumerable{T}"/></returns>
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}
