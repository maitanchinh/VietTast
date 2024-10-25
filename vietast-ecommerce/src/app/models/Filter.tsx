export interface Filter<T> {
    searchTerm?: string;
    sortBy?: keyof T;
    sortOrder?: 'asc' | 'desc';
    page?: number;
    pageSize?: number;
    criteria?: Partial<T>;
}