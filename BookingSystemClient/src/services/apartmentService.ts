import apiClient from "../api/apiClient";
import type Apartment from "../types/Apartment";
import type PagedResult from "../types/PagedResult";

export const getAllAvailableApartmentsAsync = async (
  page: number,
  size: number,
  sort: string = "asc",
  sortBy: string = "date",
): Promise<PagedResult<Apartment>> => {
  const response = await apiClient.get<PagedResult<Apartment>>(
    "/api/apartments/available",
    {
      params: {
        page,
        size,
        sort,
        sortBy,
      },
    },
  );

  return response.data;
};
