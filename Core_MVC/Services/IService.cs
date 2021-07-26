using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_MVC.Services
{
	/// <summary>
	/// The interface that will contains methods fro Performing CRUD operations
	/// using Entity class. The TEntity is generatic type which will alws be a 'class'
	/// where TEntity: class, is the generic constraints definition
	/// </summary>
	/// <typeparam name="TEntity">The Entity Class</typeparam>
	/// <typeparam name="TPk">The input Parmary Key</typeparam>
	public interface IService<TEntity, in TPk> where TEntity: class
	{
		Task<IEnumerable<TEntity>> GetAsync();
		Task<TEntity> GetAsync(TPk id);
		Task<TEntity> CreateAsync(TEntity entity);
		Task<TEntity> UpdateAsync(TPk id, TEntity entity);
		Task<TEntity> DeleteAsync(TPk id);
	}
}
