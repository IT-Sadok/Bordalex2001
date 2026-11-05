export default interface ApartmentSearchParams {
  destination: string;
  dates: {
    checkIn: Date | null;
    checkOut: Date | null;
  };
  guests: {
    adults: number;
    children: number;
    infants: number;
    pets: number;
  };
}
