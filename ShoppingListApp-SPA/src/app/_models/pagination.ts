import { Category } from './category';

export interface Pagination {
    currentPage: number;
    itemsPerPage: number;
    totalItems: number;
    totalPages: number;
    category?: Category;
}

export class PaginationResult<T> {
    result: T;
    pagination: Pagination;
}
