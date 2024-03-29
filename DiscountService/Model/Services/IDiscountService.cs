using AutoMapper;
using DiscountService.Infrastructure.Contexts;
using DiscountService.Model.Entities;

namespace DiscountService.Model.Services;

public interface IDiscountService
{
    DiscountDto GetDiscountByCode(string Code);
    bool UseDiscount(Guid Id);
    bool AddNewDiscount(string Code, int Amount);
}

public class RDiscountService : IDiscountService
{
    private readonly DiscountDataBaseContext context;
    private readonly IMapper mapper;

    public RDiscountService(DiscountDataBaseContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public bool AddNewDiscount(string Code, int Amount)
    {
        DiscountCode discountCode = new DiscountCode()
        {
            Amount = Amount,
            Code = Code,
            Used = false,
        };
        context.DiscountCodes.Add(discountCode);
        context.SaveChanges();
        return true;
    }

    public DiscountDto GetDiscountByCode(string Code)
    {
        var discountCode = context.DiscountCodes.SingleOrDefault(p => p.Code.Equals(Code));

        if (discountCode == null)
            throw new Exception("Discount Not Found....");
        var result = mapper.Map<DiscountDto>(discountCode);
        return result;
    }

    public bool UseDiscount(Guid Id)
    {
        var discountCode = context.DiscountCodes.Find(Id);
        if (discountCode == null)
            throw new Exception("Discount Not Found....");
        discountCode.Used = true;
        context.SaveChanges();
        return true;
    }
}

public class DiscountDto
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string Code { get; set; }
    public bool Used { get; set; }
}
