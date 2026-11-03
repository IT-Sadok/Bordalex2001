import apiClient from "../api/apiClient";
import type Apartment from "../types/Apartment";
import type GetAllAvailableApartmentsQueryParams from "../types/GetAllAvailableApartmentsQueryParams";
import type PagedResult from "../types/PagedResult";

export const getAllAvailableApartmentsAsync = async (
  params: GetAllAvailableApartmentsQueryParams,
  signal?: AbortSignal
): Promise<PagedResult<Apartment>> => {
  const response = await apiClient.get<PagedResult<Apartment>>(
    "/api/apartments/available",
    {
      params,
      signal
    },
  );

  return response.data;
};
