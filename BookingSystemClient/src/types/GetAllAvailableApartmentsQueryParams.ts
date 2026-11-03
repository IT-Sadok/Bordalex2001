export default interface GetAllAvailableApartmentsQueryParams {
  page: number;
  size: number;
  sort?: "asc" | "desc";
  sortBy?: "date" | "price";
  destination?: string;
  checkIn?: string;
  checkOut?: string;
}
