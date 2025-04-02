export type ErrorDetail = {
    code: string;
    description: string;
    type: number;
}

export type ErrorResponse = {
    type: string;
    title: string;
    detail: string;
    errors: ErrorDetail[];
}