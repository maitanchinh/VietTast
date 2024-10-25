import { Category } from '../models/Category';
import { Filter } from '../models/Filter';
import { Product, ProductFilter } from '../models/Product';
import { fetchData, fetchDataWithFilter } from '../utils/api';

export const getProductsByFilter = async (filter: Filter<ProductFilter>) => {
    try {
        const response = await fetchDataWithFilter<ProductFilter, Product>('product/filter', filter);
        return response;
    } catch (error) {
        throw error;
    }
};

export const getCategories = async () => {
    try {
        const response = await fetchData<Category>('category');
        return response;
    } catch (error) {
        throw error;
    }
}