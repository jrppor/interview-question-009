export interface ApiResponse<T> {
  success: boolean;
  message?: string | null;
  errors?: string[] | null;
  data?: T | null;
}

export function unwrap<T>(response: ApiResponse<T>): T {
  if (!response.success || response.data == null) {
    throw new Error(response.message ?? 'Unexpected response from server');
  }
  return response.data;
}
