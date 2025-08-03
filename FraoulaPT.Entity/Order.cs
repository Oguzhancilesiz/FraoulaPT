using FraoulaPT.Core.Abstracts;
using FraoulaPT.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FraoulaPT.Entity
{
    public class Order : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        [Required]
        public string OrderNumber { get; set; }
        
        public Guid UserId { get; set; }
        
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingFee { get; set; } = 0;
        
        public Guid? ShippingRateId { get; set; }
        
        [ForeignKey("ShippingRateId")]
        public ShippingRate? ShippingRate { get; set; }
        
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public DateTime? ShippedDate { get; set; }
        
        public DateTime? DeliveredDate { get; set; }
        
        // Kargo bilgileri
        [MaxLength(200)]
        public string ShippingAddress { get; set; }
        
        [MaxLength(50)]
        public string TrackingNumber { get; set; }
        
        [MaxLength(100)]
        public string CargoCompany { get; set; }
        
        // Ödeme bilgileri
        public PaymentMethod PaymentMethod { get; set; }
        
        public bool IsPaid { get; set; } = false;
        
        public DateTime? PaidDate { get; set; }
        
        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
        
        // İlişkiler
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem : IEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public Guid OrderId { get; set; }
        
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        
        public Guid ProductId { get; set; }
        
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        
        public int Quantity { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        
        // IEntity properties
        public Status Status { get; set; } = Status.Active;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; }
        public Guid? CreatedByUserId { get; set; }
        public Guid? UpdatedByUserId { get; set; }
        public int AutoID { get; set; }
    }

    public enum OrderStatus
    {
        Pending = 1,        // Beklemede
        Confirmed = 2,      // Onaylandı
        Processing = 3,     // Hazırlanıyor
        Shipped = 4,        // Kargoya verildi
        Delivered = 5,      // Teslim edildi
        Cancelled = 6       // İptal edildi
    }

    public enum PaymentMethod
    {
        CreditCard = 1,     // Kredi Kartı
        DebitCard = 2,      // Banka Kartı
        Transfer = 3,       // Havale
        CashOnDelivery = 4  // Kapıda Ödeme
    }
}