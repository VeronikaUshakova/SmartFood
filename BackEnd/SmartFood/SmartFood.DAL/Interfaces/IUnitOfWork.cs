using SmartFood.DAL.DataContext;
using SmartFood.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartFood.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        BoxInterface Boxes { get; }
        DeliveryInterface Deliveries { get; }
        FoodEstablishmentInterface FoodEstablishments { get; }
        HistoryBoxInterface HistoryBoxes { get; }
        ProductInterface Products { get; }
        ShipperInterface Shippers { get; }
    }
}
