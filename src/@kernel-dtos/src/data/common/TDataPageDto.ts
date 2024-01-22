

export interface TDataPageDto<T> {
    items: T[];
    maxCount?: number;
    pageSize?: number;
    pageIndex?: number;
    totalCount?: number;
}
