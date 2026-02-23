namespace product_service.ports.shared.enums
{
    public enum ApiMessage
    {
        ProductSuccess = 1,
        OperationSuccess = 2,
        BadRequest = 3,
        ValidationError = 4,
        InternalServerError = 5,
        CategorySuccess = 6,
        BadRequestProduct = 7,
        BadRequestProductUpdate = 8,
        BadRequestDeleteProduct = 9,
        BadRequestCategory = 10,
        BadRequestDeleteCategory = 11,
        DeleteCategorySuccess = 12,
        UpdateCategorySuccess = 13,
        BadRequestStock = 14,
    }
}
