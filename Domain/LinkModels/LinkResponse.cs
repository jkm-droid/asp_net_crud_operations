using System.Collections.Generic;
using Domain.Entities;

namespace Domain.LinkModels
{
    public class LinkResponse
    {
        public bool HasLinks { get; set; }
        
        public LinkCollectionWrapper<Entity> LinkedEntities { get; set; }

        public LinkResponse()
        {
            LinkedEntities = new LinkCollectionWrapper<Entity>();
        }
    }
}