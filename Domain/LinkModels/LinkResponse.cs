using System.Collections.Generic;
using Domain.Entities;

namespace Domain.LinkModels
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }
        
        public LinkCollectionWrapper<Entity> LinkedEntities { get; set; }
        public List<Entity> LinkEntities { get; set; }
        public List<Entity> ShapedEntities { get; set; }

        public LinkResponse()
        {
            LinkedEntities = new LinkCollectionWrapper<Entity>();
            LinkEntities = new List<Entity>();
            ShapedEntities = new List<Entity>();
        }
    }
}