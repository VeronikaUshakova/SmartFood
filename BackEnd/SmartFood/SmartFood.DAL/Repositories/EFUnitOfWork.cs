using System;
using System.Collections.Generic;
using System.Text;
using SmartFood.DAL.Entities;
using SmartFood.DAL.Interfaces;
using SmartFood.DAL.DataContext;

namespace SmartFood.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private DatabaseContext context;
        private BoxRepository boxRepository;
        private DeliveryRepository deliveryRepository;
        private FoodEstablishmentRepository foodEstablishmentRepository;
        private HistoryBoxRepository historyBoxRepository;
        private ProductRepository productRepository;
        private ShipperRepository shipperRepository;
        private AdminRepository adminRepository;
        public EFUnitOfWork()
        {
            context = new DatabaseContext(DatabaseContext.ops.dbOptions);
        }

        public BoxInterface Boxes
        {
            get
            {
                if (boxRepository == null)
                {
                    boxRepository = new BoxRepository(context);
                }
                return boxRepository;
            }
        }

        public DeliveryInterface Deliveries
        {
            get
            {
                if (deliveryRepository == null)
                {
                    deliveryRepository = new DeliveryRepository(context);
                }
                return deliveryRepository;
            }
        }

        public FoodEstablishmentInterface FoodEstablishments
        {
            get
            {
                if (foodEstablishmentRepository == null)
                {
                    foodEstablishmentRepository = new FoodEstablishmentRepository(context);
                }
                return foodEstablishmentRepository;
            }
        }

        public HistoryBoxInterface HistoryBoxes
        {
            get
            {
                if (historyBoxRepository == null)
                {
                    historyBoxRepository = new HistoryBoxRepository(context);
                }
                return historyBoxRepository;
            }
        }
        public ProductInterface Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(context);
                }
                return productRepository;
            }
        }

        public ShipperInterface Shippers
        {
            get
            {
                if (shipperRepository == null)
                {
                    shipperRepository = new ShipperRepository(context);
                }
                return shipperRepository;
            }
        }

        public AdminRepository Admins
        {
            get
            {
                if (adminRepository == null)
                {
                    adminRepository = new AdminRepository(context);
                }
                return adminRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
