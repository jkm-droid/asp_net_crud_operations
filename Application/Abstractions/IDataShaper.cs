using System.Collections.Generic;
using System.Dynamic;
using Domain.Entities;

namespace Application.Abstractions
{
    public interface IDataShaper<T>
    {
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string fieldString);
        ShapedEntity ShapeData(T entity, string fieldString);
    }
}