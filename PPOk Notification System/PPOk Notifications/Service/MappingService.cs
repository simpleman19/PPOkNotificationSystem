using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dapper;

namespace PPOk_Notifications.Service {


	/**
	 * Allows dapper to map attributes with different names to C# standard names.
	 */
	public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper {

		public ColumnAttributeTypeMapper()
			: base(new SqlMapper.ITypeMap[]
				{
					new CustomPropertyTypeMap(
					   typeof(T),
					   (type, columnName) =>
						   type.GetProperties().FirstOrDefault(prop =>
							   prop.GetCustomAttributes(false)
								   .OfType<ColumnAttribute>()
								   .Any(attr => attr.Name == columnName)
							   )
					   ),
					new DefaultTypeMap(typeof(T))
				}) {}
	}

	/**
	 * Establishes a attribute flag that can be defined above a variable 
	 * to specify its name in the database. WILL NOT map the name when
	 * performing an insert of that object.
	 */
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
	public class ColumnAttribute : Attribute {
		public string Name { get; set; }
	}

	/**
	 * Allows Dapper to fall back to the default mapping convention so
	 * not every variable requires a mapping attribute.
	 */
	public class FallbackTypeMapper : SqlMapper.ITypeMap {
		private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

		public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers) {
			_mappers = mappers;
		}


		public ConstructorInfo FindConstructor(string[] names, Type[] types) {
			foreach (var mapper in _mappers) {
				try {
					ConstructorInfo result = mapper.FindConstructor(names, types);
					if (result != null) {
						return result;
					}
				} catch (NotImplementedException) {}
			}
			return null;
		}

		public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName) {
			foreach (var mapper in _mappers) {
				try {
					var result = mapper.GetConstructorParameter(constructor, columnName);
					if (result != null) {
						return result;
					}
				} catch (NotImplementedException) {}
			}
			return null;
		}

		public SqlMapper.IMemberMap GetMember(string columnName) {
			foreach (var mapper in _mappers) {
				try {
					var result = mapper.GetMember(columnName);
					if (result != null) {
						return result;
					}
				} catch (NotImplementedException) {}
			}
			return null;
		}


		public ConstructorInfo FindExplicitConstructor() {
			return _mappers
				.Select(mapper => mapper.FindExplicitConstructor())
				.FirstOrDefault(result => result != null);
		}
	}
}