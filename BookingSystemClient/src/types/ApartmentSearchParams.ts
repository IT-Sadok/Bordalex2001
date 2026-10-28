export default interface ApartmentSearchParams {
  destination: string;
  dates: {
    checkIn: Date;
    checkOut: Date;
  };
  guests: {
    adults: number;
    children: number;
    infants: number;
    pets: number;
  };
}
