﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xatzipe.BatchEnumerable
{
    /// <summary>
    /// Abstract Class that implements the base functionality to return items in batches
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class AbstractBatchEnumerable<TModel, TResult> : AbstractBaseBatchEnumerable<TResult>, IBatchEnumerable<TModel, TResult>
    {


        /// <summary>
        /// 
        /// </summary>
        protected IQueryable<TModel> Items;

        /// <summary>
        /// 
        /// </summary>
		protected Expression<Func<TModel, bool>> Filter;

        /// <summary>
        /// 
        /// </summary>
		protected Expression<Func<TModel, TResult>> Response;

        /// <summary>
        /// 
        /// </summary>
		protected Func<IQueryable<TModel>, IOrderedQueryable<TModel>> Order;

        /// <summary>
        /// 
        /// </summary>
        protected int BatchSizeLocal;

        /// <summary>
        /// AbstractBatchEnumerable  constructor
        /// </summary>
        /// <param name="response"></param>
        /// <param name="order"></param>
        /// <param name="filter"></param>
        /// <param name="batchSize"></param>
        public AbstractBatchEnumerable (
            Expression<Func<TModel, TResult>> response,
            Func<IQueryable<TModel>, IOrderedQueryable<TModel>> order = null,
            Expression<Func<TModel, bool>> filter = null,
            int batchSize = 10
        )
        {
            Response = response;
            Order = order;
            Filter = filter;
            BatchSizeLocal = batchSize;
        }
    }
}