//-----------------------------------------------------------------------
// <copyright file="IGenericRepository.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Data.SqlClient;

    /// <summary>
    /// Generic Repository for Data Access.
    /// </summary>
    /// <typeparam name="TEntity">Entity.</typeparam>
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get all values.
        /// </summary>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Enumerable of Entity Type.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        ///  Get all values for query.
        /// </summary>
        /// <returns>IQueryable of Entity Type.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Get a value by predicate.
        /// </summary>
        /// <param name="predicate">predicate expression to find value.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Value of entity.</returns>
        Task<TEntity> FindByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Find specific values.
        /// </summary>
        /// <param name="predicate">Predicate expression to find values.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Enumerable of Entity Type.</returns>
        Task<IEnumerable<TEntity>> FindAllByAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        ///  Find specific values for query.
        /// </summary>
        /// <param name="predicate">Predicate expression to find values.</param>
        /// <returns>IQueryable of Entity Type.</returns>
        IQueryable<TEntity> FindAllBy(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Find specific values.
        /// </summary>
        /// <param name="predicate">Predicate expression to find values.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Enumerable of Entity Type.</returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Insert a value.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>ValueTask.</returns>
        ValueTask<EntityEntry<TEntity>> InsertAsync(TEntity entity, CancellationToken cancellationToken);

        /// <summary>
        /// Insert a value.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>ValueTask.</returns>
        Task InsertAsync(IEnumerable<TEntity> entity, CancellationToken cancellationToken);

        /// <summary>
        /// Delete all values based on predicate.
        /// </summary>
        /// <param name="predicate">Predicate expression to find values.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Delete specific entity by entity.
        /// </summary>
        /// <param name="entityToDelete">Entity.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Task.</returns>
        Task DeleteAsync(TEntity entityToDelete, CancellationToken cancellationToken);

        /// <summary>
        /// Update specific entity by entity.
        /// </summary>
        /// <param name="entityToUpdate">Entity.</param>
        /// <param name="cancellationToken">Cancellation Token.</param>
        /// <returns>Task.</returns>
        Task UpdateAsync(TEntity entityToUpdate, CancellationToken cancellationToken);

        ///// <summary>
        ///// ExecuteSqlRawAsync function does not return any out put patameter.
        ///// </summary>
        ///// <param name="storedProcedureName">Stored Procedure Name.</param>
        ///// <param name="sqlParameter">parameterDefinitions.</param>
        ///// <param name="cancellationToken">cancellationToken.</param>
        ///// <returns>Enumerable of Entity Type.</returns>
        //Task UpdatewithIDAsync(TEntity entityToUpdate, TEntity currentEntity, CancellationToken cancellationToken);

        /// <summary>
        /// ExecuteSqlRawAsync function does not return any out put patameter.
        /// </summary>
        /// <param name="storedProcedureName">Stored Procedure Name.</param>
        /// <param name="sqlParameter">parameterDefinitions.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>Enumerable of Entity Type.</returns>
        Task<int> ExecuteSqlRawAsync(string storedProcedureName, SqlParameter[] sqlParameter, CancellationToken cancellationToken);

        /// <summary>
        /// ExecuteSqlRawAsync function return out put patameter.
        /// </summary>
        /// <param name="storedProcedureName">storedProcedureName.</param>
        /// <param name="sqlParameter">parameterDefinitions.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>Enumerable of Entity Type.</returns>
        Task<int> ExecuteSqlRawAsync(string storedProcedureName, ref SqlParameter[] sqlParameter, CancellationToken cancellationToken);

        /// <summary>
        /// From Sql Raw Async.
        /// </summary>
        /// <param name="storedProcedureName">storedProcedureName.</param>
        /// <param name="sqlParameter">parameterDefinitions.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>Returns number of rows affected.</returns>
        Task<IEnumerable<TEntity>> FromSqlRawAsync(string storedProcedureName, SqlParameter[] sqlParameter, CancellationToken cancellationToken);

        /// <summary>
        /// FromSqlRawAsync function return out put patameter.
        /// </summary>
        /// <param name="storedProcedureName">storedProcedureName.</param>
        /// <param name="sqlParameter">parameterDefinitions.</param>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>Returns number of rows affected.</returns>
        Task<List<TEntity>> FromSqlRawAsync(string storedProcedureName, ref SqlParameter[] sqlParameter, CancellationToken cancellationToken);

        /// <summary>
        /// Commit Async.
        /// </summary>
        /// <param name="cancellationToken">cancellationToken.</param>
        /// <returns>Returns number of rows affected.</returns>
        Task<int> CommitAsync(CancellationToken cancellationToken);
     }
}