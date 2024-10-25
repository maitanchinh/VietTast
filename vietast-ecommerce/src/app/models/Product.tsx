export interface Product {
    productId: string;
    imageUrl: string;
    name: string;
    price: number;
    rate: number;
    quantity: number;
}

export interface ProductFilter{
    categoryId?: string;
    name?: string;
    price?: number;
    rate?: number;
    quantity?: number;
}
