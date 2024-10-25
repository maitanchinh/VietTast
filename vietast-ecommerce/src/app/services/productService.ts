import { Filter } from '../models/Filter';
import { ProductFilter } from '../models/Product';
import { fetchData, fetchDataWithFilter } from '../utils/api';

export const getProductsByFilter = async (filter: Filter<ProductFilter>) => {
    try {
        const response = await fetchDataWithFilter('product/filter', filter);
        return response;
    } catch (error) {
        throw error;
    }
};

export const getCategories = async () => {
    try {
        const response = await fetchData('category');
        return response;
    } catch (error) {
        throw error;
    }
}