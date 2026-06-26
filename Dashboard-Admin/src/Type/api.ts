export interface PaginationMeta {
    page: number;
    pageSize: number;
    totalCount: number;
    totalPages: number;
    hasNextPage: boolean;
    hasPreviousPage: boolean;
    timestamp: string;
  }
  
  export interface ApiResponse<T> {
    success: boolean;
    message: string;
    data: T;
    meta?: PaginationMeta;
    errors?: string[] | null;
    traceId?: string;
  }

  export interface ApiErrorResponse {
    success: boolean
    message: string
    errors: string[]
    statusCode: number
    traceId: string
  }