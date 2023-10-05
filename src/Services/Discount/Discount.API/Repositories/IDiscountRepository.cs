using Discount.API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Generic;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
    }
}

/**
CREATE TABLE Coupon(
	ID SERIAL PRIMARY KEY NOT NULL,
    ProductName VARCHAR(24) NOT NULL,
    Description TEXT,
    Amount INT
);

INSERT INTO Coupon (ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);

INSERT INTO Coupon (ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);
**/
