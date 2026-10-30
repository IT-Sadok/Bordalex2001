import type ApartmentSearchParams from "../types/ApartmentSearchParams";

export default function parseApartmentSearchParams(
  params: URLSearchParams,
): ApartmentSearchParams | null {
  const destination = params.get("destination")?.trim();
  const checkIn = params.get("checkIn");
  const checkOut = params.get("checkOut");

  const adults = Number(params.get("adults"));
  const children = Number(params.get("children") ?? 0);
  const infants = Number(params.get("infants") ?? 0);
  const pets = Number(params.get("pets") ?? 0);

  if (!destination || !checkIn || !checkOut) {
    return null;
  }

  const checkInDate = new Date(`${checkIn}T00:00:00`);
  const checkOutDate = new Date(`${checkOut}T00:00:00`);

  if (
    !Number.isFinite(checkInDate.getTime()) ||
    !Number.isFinite(checkOutDate.getTime()) ||
    checkOutDate <= checkInDate ||
    !Number.isInteger(adults) ||
    adults < 1 ||
    [children, infants, pets].some(
      (value) => !Number.isInteger(value) || value < 0,
    )
  ) {
    return null;
  }

  return {
    destination,
    dates: {
      checkIn: checkInDate,
      checkOut: checkOutDate,
    },
    guests: {
      adults,
      children,
      infants,
      pets,
    },
  };
}
